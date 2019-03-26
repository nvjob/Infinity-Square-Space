// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityStandardAssets.ImageEffects;



public class Interface : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public bool loadingOn = true;
    public bool hideLockMouse = true;
    public bool clearPlayerPrefs = false;
    public bool giveMineralsForTesting = false;
    public Text liveIndicatorText;
    public Transform speedIndicatorTransform;
    public GameObject aim;
    public GameObject friendlyPriority, enemyPriority;
    public Text friendlyCounterText, enemyCounterText, friendlySurvivedText, enemySurvivedText;
    public GameObject inventoryScreen, informationScreen, playerIsDeadScreen, friendsDestroyedScreen, enemiesDestroyedScreen, loadingScreen, loadingRing, damageScreen;
    [Header("Statistics")]
    public Text lifetimeStatisticsText;
    public Text distanceStatisticsText, destroyedStatisticsText, lifetimeStatisticsDeadText, distanceStatisticsDeadText, destroyedStatisticsDeadText;
    [Header("Button Other")]
    public Button returnToGameButton;
    public Button mainMenuButton, informationYesButton, informationNoButton;
    [Header("Button Minerals")]
    public Toggle powerButton;
    public Toggle speedButton, lightningButton, laserButton, pulseButton, maskerButton, antigravityButton, tamingButton, eraserButton, collapsarButton, destroyerButton;
    [Header("Counter Minerals")]
    public Text lifeCounterText;
    public Text powerCounterText, speedCounterText, lightningCounterText, laserCounterText, pulseCounterText;
    public Text maskerCounterText, antigravityCounterText, tamingCounterText, eraserCounterText, collapsarCounterText, destroyerCounterText;
    public AudioMixer masterMixer;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(1), delay1 = new WaitForSeconds(8), delay2 = new WaitForSeconds(60);
    Color enabledColor, disabledColor, notAvailableColor;
    Camera mainCamera;
    Image powerImage, speedImage, lightningImage, laserImage, pulseImage, maskerImage, antigravityImage, tamingImage, eraserImage, collapsarImage, destroyerImage;
    float speedIndicator;
    int lifeCounter, powerCounter, speedCounter, lightningCounter, laserCounter, pulseCounter, maskerCounter, antigravityCounter, tamingCounter, eraserCounter, collapsarCounter, destroyerCounter;
    int amountEnemyAIs, fewEnemiesLeft, amountFriendlyAIs, fewFriendsLeft;
    bool powerTrigger, lightningTrigger, laserTrigger, pulseTrigger, maskerTrigger, antigravityTrigger, tamingTrigger, eraserTrigger, collapsarTrigger, destroyerTrigger;
    bool pause, triggerEnemy0, triggerEnemy1, triggerEnemy2, triggerFriendly0, triggerFriendly1, triggerFriendly2;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        Time.timeScale = 1;
        Class_Interface.HideLockMouse(hideLockMouse);
        Class_Interface.mouseSensitivityValue = PlayerPrefs.GetFloat("mouseSensitivityValue");
        Class_Interface.globalPausa = Class_Interface.loading = false;
        Class_Interface.powerEnabled = Class_Interface.defaultEnabled = Class_Interface.lightningEnabled = Class_Interface.laserEnabled = Class_Interface.pulseEnabled = false;
        Class_Interface.speedEnabled = Class_Interface.collapsarEnabled = Class_Interface.destroyerEnabled = Class_Interface.eraserEnabled = Class_Interface.antigravityEnabled = false;
        Class_AI.amountEnemyAIs = Class_AI.amountFriendlyAIs = 0;

        inventoryScreen.SetActive(false);
        informationScreen.SetActive(false);
        playerIsDeadScreen.SetActive(false);
        friendsDestroyedScreen.SetActive(false);
        enemiesDestroyedScreen.SetActive(false);

        //--------------

        QualitySettings.lodBias = Class_Interface.LODValue = PlayerPrefs.GetFloat("LODValue");
        Class_Interface.distanceLod = 1000 * Class_Interface.LODValue;

        float[] distancesCul = new float[32];
        distancesCul[11] = 750 * Class_Interface.LODValue;
        distancesCul[13] = distancesCul[15] = 1500 * Class_Interface.LODValue;
        distancesCul[18] = 1000 * Class_Interface.LODValue;
        distancesCul[19] = 2200 * Class_Interface.LODValue;
        distancesCul[20] = 1100 * Class_Interface.LODValue;
        distancesCul[21] = 650 * Class_Interface.LODValue;
        distancesCul[31] = 550 * Class_Interface.LODValue;
        mainCamera = Camera.main;
        mainCamera.layerCullDistances = distancesCul;
        mainCamera.layerCullSpherical = true;       

        //--------------

        if (PlayerPrefs.GetInt("GraphicsQualityLow") == 0)
        {
            mainCamera.renderingPath = RenderingPath.DeferredShading;
            QualitySettings.pixelLightCount = 3;
            mainCamera.GetComponent<GlobalFog>().enabled = true;
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowDistance = 350 * Class_Interface.LODValue;
            Class_Interface.dynamicLighting = Class_Interface.shadows = true;
        }
        else
        {
            mainCamera.renderingPath = RenderingPath.Forward;
            QualitySettings.pixelLightCount = 1;
            mainCamera.GetComponent<GlobalFog>().enabled = false;
            QualitySettings.shadows = ShadowQuality.Disable;
            Class_Interface.dynamicLighting = Class_Interface.shadows = false;
        }

        masterMixer.SetFloat("EffectsVolume", Mathf.Log(PlayerPrefs.GetFloat("effectsVolumeValue")) * Mathf.Abs(Mathf.Log(PlayerPrefs.GetFloat("effectsVolumeValue"))) * 15);
        masterMixer.SetFloat("OSTVolume", Mathf.Log(PlayerPrefs.GetFloat("musicVolumeValue")) * 22);

        //--------------

        enabledColor = new Color(0.65f, 0.65f, 0.65f, 1);
        disabledColor = new Color(1, 1, 1, 1);
        notAvailableColor = new Color(1, 1, 1, 0.2f);

        powerImage = powerButton.GetComponent<Image>();
        speedImage = speedButton.GetComponent<Image>();
        lightningImage = lightningButton.GetComponent<Image>();
        laserImage = laserButton.GetComponent<Image>();
        pulseImage = pulseButton.GetComponent<Image>();
        maskerImage = maskerButton.GetComponent<Image>();
        antigravityImage = antigravityButton.GetComponent<Image>();
        tamingImage = tamingButton.GetComponent<Image>();
        eraserImage = eraserButton.GetComponent<Image>();
        collapsarImage = collapsarButton.GetComponent<Image>();
        destroyerImage = destroyerButton.GetComponent<Image>();

        returnToGameButton.onClick.AddListener(ReturnToGame);
        mainMenuButton.onClick.AddListener(MainMenu);
        informationYesButton.onClick.AddListener(ExitToMenu);
        informationNoButton.onClick.AddListener(ReturnToGame);

        speedIndicatorTransform.localScale = new Vector3(0.025f, 1, 1);

        //--------------

        MineralsPlayerPrefs(false);
        StartCoroutine(Autosave());
        StartCoroutine(Statistics());
        StartCoroutine(EnemyFriendlyPriority());
        StartCoroutine(Class_Interface.SubtractionMineralsOverTime());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        if (loadingOn == true && Class_AI.aiSpawnOn == true && Class_StarSystem.generationOn == true)
        {
            loadingScreen.SetActive(true);
            Class_Interface.loading = true;
            System.GC.Collect();
        }
        else
        {
            loadingScreen.SetActive(false);
            Class_Interface.loading = false;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void EntranceToInventory()
    {
        //--------------

        aim.SetActive(false);
        inventoryScreen.SetActive(true);
        pause = Class_Interface.globalPausa = true;
        Time.timeScale = 0;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void ReturnToGame()
    {
        //--------------

        inventoryScreen.SetActive(false);
        informationScreen.SetActive(false);
        aim.SetActive(true);
        pause = Class_Interface.globalPausa = false;
        Time.timeScale = 1;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void MainMenu()
    {
        //--------------

        inventoryScreen.SetActive(false);
        informationScreen.SetActive(true);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void ExitToMenu()
    {
        //--------------

        if (Class_Interface.lifeCounter > 9) Class_Interface.lifeCounter -= Mathf.RoundToInt(Class_Interface.lifeCounter / Random.Range(13, 25));
        if (Class_Interface.powerCounter > 9) Class_Interface.powerCounter -= Mathf.RoundToInt(Class_Interface.powerCounter / Random.Range(13, 25));
        if (Class_Interface.speedCounter > 9) Class_Interface.speedCounter -= Mathf.RoundToInt(Class_Interface.speedCounter / Random.Range(13, 25));
        if (Class_Interface.lightningCounter > 9) Class_Interface.lightningCounter -= Mathf.RoundToInt(Class_Interface.lightningCounter / Random.Range(13, 25));
        if (Class_Interface.laserCounter > 9) Class_Interface.laserCounter -= Mathf.RoundToInt(Class_Interface.laserCounter / Random.Range(13, 25));
        if (Class_Interface.pulseCounter > 9) Class_Interface.pulseCounter -= Mathf.RoundToInt(Class_Interface.pulseCounter / Random.Range(13, 25));
        if (Class_Interface.maskerCounter > 9) Class_Interface.maskerCounter -= Mathf.RoundToInt(Class_Interface.maskerCounter / Random.Range(13, 25));
        if (Class_Interface.antigravityCounter > 9) Class_Interface.antigravityCounter -= Mathf.RoundToInt(Class_Interface.antigravityCounter / Random.Range(13, 25));
        if (Class_Interface.tamingCounter > 9) Class_Interface.tamingCounter -= Mathf.RoundToInt(Class_Interface.tamingCounter / Random.Range(13, 25));
        if (Class_Interface.eraserCounter > 9) Class_Interface.eraserCounter -= Mathf.RoundToInt(Class_Interface.eraserCounter / Random.Range(13, 25));
        if (Class_Interface.collapsarCounter > 9) Class_Interface.collapsarCounter -= Mathf.RoundToInt(Class_Interface.collapsarCounter / Random.Range(13, 25));
        if (Class_Interface.destroyerCounter > 9) Class_Interface.destroyerCounter -= Mathf.RoundToInt(Class_Interface.destroyerCounter / Random.Range(13, 25));

        MineralsPlayerPrefs(true);
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void LateUpdate()
    {
        //--------------

        if (Class_Controller.playerAlive == true)
        {
            if (Class_Interface.loading == false)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (inventoryScreen.activeSelf == false) EntranceToInventory();
                    else ReturnToGame();
                }
                if (loadingRing.activeSelf == true) loadingRing.SetActive(false);
            }
            else if (loadingRing.activeSelf == false) loadingRing.SetActive(true);

            if (Class_Controller.currentSpeed >= 1)
            {
                if (Class_Interface.speedEnabled == true)
                {
                    if (speedIndicator != 1.3f) speedIndicator = 1.3f;
                }
                else
                {
                    if (speedIndicator > 1.08f) speedIndicator -= 0.007f;
                    else if (speedIndicator <= 1.08f) speedIndicator = 1.08f;
                }
                speedIndicatorTransform.localScale = new Vector3(Vector3.one.x * Class_Controller.currentSpeed * 0.016f, Vector3.one.y, Vector3.one.z);
                if (speedIndicatorTransform.localScale.x > speedIndicator) speedIndicatorTransform.localScale = new Vector3(speedIndicator, Vector3.one.y, Vector3.one.z);
            }

            Inventory();
        }
        else if (playerIsDeadScreen.activeSelf == false)
        {
            PlayerPrefs.SetInt("Mineral 0", 0);
            playerIsDeadScreen.SetActive(true);
        }

        //--------------

        if (pause == false) Class_Interface.HideLockMouse(hideLockMouse);
        else Class_Interface.HideLockMouse(false);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Inventory()
    {
        //--------------

        if (lifeCounter != Class_Interface.lifeCounter)
        {
            if (lifeCounter > Class_Interface.lifeCounter && damageScreen.activeSelf == false) damageScreen.SetActive(true);
            lifeCounter = Class_Interface.lifeCounter;
            lifeCounterText.text = liveIndicatorText.text = lifeCounter.ToString();
        }

        //--------------

        if (Class_Interface.powerCounter > 0)
        {
            if (powerButton.isOn == true && powerTrigger == false)
            {
                Class_Interface.powerEnabled = powerTrigger = true;
                powerImage.color = enabledColor;
            }
            else if (powerButton.isOn == false && powerTrigger == true)
            {
                Class_Interface.powerEnabled = powerTrigger = false;
                powerImage.color = disabledColor;
            }
        }
        else if (powerImage.color != notAvailableColor)
        {
            Class_Interface.powerEnabled = powerButton.isOn = false;
            powerTrigger = true;
            powerImage.color = notAvailableColor;
            Class_Interface.powerCounter = 0;
        }

        if (powerCounter != Class_Interface.powerCounter)
        {
            powerCounter = Class_Interface.powerCounter;
            powerCounterText.text = powerCounter.ToString();
        }

        //--------------

        if (Class_Interface.speedCounter <= 0)
        {
            if (speedImage.color != notAvailableColor) speedImage.color = notAvailableColor;
            if (Class_Interface.speedCounter != 0) Class_Interface.speedCounter = 0;
        }
        else if (speedImage.color != disabledColor) powerImage.color = disabledColor;

        if (speedCounter != Class_Interface.speedCounter)
        {
            speedCounter = Class_Interface.speedCounter;
            speedCounterText.text = speedCounter.ToString();
        }

        //--------------

        if (Class_Interface.lightningCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)) lightningButton.isOn = true;

            if (lightningButton.isOn == true && lightningTrigger == false)
            {                
                laserButton.isOn = pulseButton.isOn = maskerButton.isOn = tamingButton.isOn = eraserButton.isOn = collapsarButton.isOn = destroyerButton.isOn = false;
                Class_Interface.lightningEnabled = lightningTrigger = laserTrigger = pulseTrigger = maskerTrigger = tamingTrigger = eraserTrigger = collapsarTrigger = destroyerTrigger = true;
                lightningImage.color = enabledColor;
            }
            else if (lightningButton.isOn == false && lightningTrigger == true)
            {
                Class_Interface.lightningEnabled = lightningTrigger = false;
                lightningImage.color = disabledColor;
            }
        }
        else if (lightningImage.color != notAvailableColor)
        {
            Class_Interface.lightningEnabled = lightningButton.isOn = false;
            lightningTrigger = true;
            lightningImage.color = notAvailableColor;
            Class_Interface.lightningCounter = 0;
        }

        if (lightningCounter != Class_Interface.lightningCounter)
        {
            lightningCounter = Class_Interface.lightningCounter;
            lightningCounterText.text = lightningCounter.ToString();
        }

        //--------------

        if (Class_Interface.laserCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3)) laserButton.isOn = true;

            if (laserButton.isOn == true && laserTrigger == false)
            {                
                lightningButton.isOn = pulseButton.isOn = maskerButton.isOn = tamingButton.isOn = eraserButton.isOn = collapsarButton.isOn = destroyerButton.isOn = false;
                Class_Interface.laserEnabled = lightningTrigger = laserTrigger = pulseTrigger = maskerTrigger = tamingTrigger = eraserTrigger = collapsarTrigger = destroyerTrigger = true;
                laserImage.color = enabledColor;
            }
            else if (laserButton.isOn == false && laserTrigger == true)
            {
                Class_Interface.laserEnabled = laserTrigger = false;
                laserImage.color = disabledColor;
            }
        }
        else if (laserImage.color != notAvailableColor)
        {
            Class_Interface.laserEnabled = laserButton.isOn = false;
            laserTrigger = true;
            laserImage.color = notAvailableColor;
            Class_Interface.laserCounter = 0;
        }

        if (laserCounter != Class_Interface.laserCounter)
        {
            laserCounter = Class_Interface.laserCounter;
            laserCounterText.text = laserCounter.ToString();
        }

        //--------------

        if (Class_Interface.pulseCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4)) pulseButton.isOn = true;

            if (pulseButton.isOn == true && pulseTrigger == false)
            {
                lightningButton.isOn = laserButton.isOn = eraserButton.isOn = collapsarButton.isOn = destroyerButton.isOn = false;
                Class_Interface.pulseEnabled = lightningTrigger = laserTrigger = pulseTrigger = eraserTrigger = collapsarTrigger = destroyerTrigger = true;
                pulseImage.color = enabledColor;
            }
            else if (pulseButton.isOn == false && pulseTrigger == true)
            {
                Class_Interface.pulseEnabled = pulseTrigger = false;
                pulseImage.color = disabledColor;
            }
        }
        else if (pulseImage.color != notAvailableColor)
        {
            Class_Interface.pulseEnabled = pulseButton.isOn = false;
            pulseTrigger = true;
            pulseImage.color = notAvailableColor;
            Class_Interface.pulseCounter = 0;
        }

        if (pulseCounter != Class_Interface.pulseCounter)
        {
            pulseCounter = Class_Interface.pulseCounter;
            pulseCounterText.text = pulseCounter.ToString();
        }

        //--------------

        if (Class_Interface.maskerCounter > 0)
        {
            if (maskerButton.isOn == true && maskerTrigger == false)
            {              
                lightningButton.isOn = laserButton.isOn = tamingButton.isOn = eraserButton.isOn = destroyerButton.isOn = false;
                Class_Interface.maskerEnabled = lightningTrigger = laserTrigger = maskerTrigger = tamingTrigger = eraserTrigger = destroyerTrigger = true;
                maskerImage.color = enabledColor;
            }
            else if (maskerButton.isOn == false && maskerTrigger == true)
            { 
                Class_Interface.maskerEnabled = maskerTrigger = false;
                maskerImage.color = disabledColor;
            }
        }
        else if (maskerImage.color != notAvailableColor)
        {
            Class_Interface.maskerEnabled = maskerButton.isOn = false;
            maskerTrigger = true;
            maskerImage.color = notAvailableColor;
            Class_Interface.maskerCounter = 0;
        }

        if (maskerCounter != Class_Interface.maskerCounter)
        {
            maskerCounter = Class_Interface.maskerCounter;
            maskerCounterText.text = maskerCounter.ToString();
        }

        //--------------

        if (Class_Interface.antigravityCounter > 0)
        {
            if (antigravityButton.isOn == true && antigravityTrigger == false)
            {
                Class_Interface.antigravityEnabled = antigravityTrigger = true;
                antigravityImage.color = enabledColor;
            }
            if (antigravityButton.isOn == false && antigravityTrigger == true)
            {
                Class_Interface.antigravityEnabled = antigravityTrigger = false;
                antigravityImage.color = disabledColor;
            }
        }
        else if (antigravityImage.color != notAvailableColor)
        {
            Class_Interface.antigravityEnabled = antigravityButton.isOn = false;
            antigravityTrigger = true;
            antigravityImage.color = notAvailableColor;
            Class_Interface.antigravityCounter = 0;
        }

        if (antigravityCounter != Class_Interface.antigravityCounter)
        {
            antigravityCounter = Class_Interface.antigravityCounter;
            antigravityCounterText.text = antigravityCounter.ToString();
        }

        //--------------

        if (Class_Interface.tamingCounter > 0)
        {
            if (tamingButton.isOn == true && tamingTrigger == false)
            {              
                lightningButton.isOn = laserButton.isOn = maskerButton.isOn = eraserButton.isOn = destroyerButton.isOn = false;
                Class_Interface.tamingEnabled = lightningTrigger = laserTrigger = maskerTrigger = tamingTrigger = eraserTrigger = destroyerTrigger = true;
                tamingImage.color = enabledColor;
            }
            else if (tamingButton.isOn == false && tamingTrigger == true)
            {
                Class_Interface.tamingEnabled = tamingTrigger = false;
                tamingImage.color = disabledColor;
            }
        }
        else if (tamingImage.color != notAvailableColor)
        {
            Class_Interface.tamingEnabled = tamingButton.isOn = false;
            tamingTrigger = true;
            tamingImage.color = notAvailableColor;
            Class_Interface.tamingCounter = 0;
        }

        if (tamingCounter != Class_Interface.tamingCounter)
        {
            tamingCounter = Class_Interface.tamingCounter;
            tamingCounterText.text = tamingCounter.ToString();
        }

        //--------------

        if (Class_Interface.eraserCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5)) eraserButton.isOn = true;

            if (eraserButton.isOn == true && eraserTrigger == false)
            {                
                lightningButton.isOn = laserButton.isOn = pulseButton.isOn = maskerButton.isOn = tamingButton.isOn = collapsarButton.isOn = destroyerButton.isOn = false;
                Class_Interface.eraserEnabled = lightningTrigger = laserTrigger = pulseTrigger = maskerTrigger = tamingTrigger = eraserTrigger = collapsarTrigger = destroyerTrigger = true;
                eraserImage.color = enabledColor;
            }
            else if (eraserButton.isOn == false && eraserTrigger == true)
            {
                Class_Interface.eraserEnabled = eraserTrigger = false;
                eraserImage.color = disabledColor;
            }
        }
        else if (eraserImage.color != notAvailableColor)
        {
            Class_Interface.eraserEnabled = eraserButton.isOn = false;
            eraserTrigger = true;
            eraserImage.color = notAvailableColor;
            Class_Interface.eraserCounter = 0;
        }

        if (eraserCounter != Class_Interface.eraserCounter)
        {
            eraserCounter = Class_Interface.eraserCounter;
            eraserCounterText.text = eraserCounter.ToString();
        }

        //--------------

        if (Class_Interface.collapsarCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha6)) collapsarButton.isOn = true;

            if (collapsarButton.isOn == true && collapsarTrigger == false)
            {                
                lightningButton.isOn = laserButton.isOn = pulseButton.isOn = eraserButton.isOn = destroyerButton.isOn = false;
                Class_Interface.collapsarEnabled = lightningTrigger = laserTrigger = pulseTrigger = eraserTrigger = collapsarTrigger = destroyerTrigger = true;
                collapsarImage.color = enabledColor;
            }
            else if(collapsarButton.isOn == false && collapsarTrigger == true)
            {
                Class_Interface.collapsarEnabled = collapsarTrigger = false;
                collapsarImage.color = disabledColor;
            }
        }
        else if (collapsarImage.color != notAvailableColor)
        {
            Class_Interface.collapsarEnabled = collapsarButton.isOn = false;
            collapsarTrigger = true;
            collapsarImage.color = notAvailableColor;
            Class_Interface.collapsarCounter = 0;
        }

        if (collapsarCounter != Class_Interface.collapsarCounter)
        {
            collapsarCounter = Class_Interface.collapsarCounter;
            collapsarCounterText.text = collapsarCounter.ToString();
        }

        //--------------

        if (Class_Interface.destroyerCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha7)) destroyerButton.isOn = true;

            if (destroyerButton.isOn == true && destroyerTrigger == false)
            {                
                lightningButton.isOn = laserButton.isOn = pulseButton.isOn = maskerButton.isOn = tamingButton.isOn = eraserButton.isOn = collapsarButton.isOn = false;
                Class_Interface.destroyerEnabled = lightningTrigger = laserTrigger = pulseTrigger = maskerTrigger = tamingTrigger = eraserTrigger = collapsarTrigger = destroyerTrigger = true;
                destroyerImage.color = enabledColor;
            }
            else if(destroyerButton.isOn == false && destroyerTrigger == true)
            {
                Class_Interface.destroyerEnabled = destroyerTrigger = false;
                destroyerImage.color = disabledColor;
            }
        }
        else if (destroyerImage.color != notAvailableColor)
        {
            Class_Interface.destroyerEnabled = destroyerButton.isOn = false;
            destroyerTrigger = true;
            destroyerImage.color = notAvailableColor;
            Class_Interface.destroyerCounter = 0;
        }

        if (destroyerCounter != Class_Interface.destroyerCounter)
        {
            destroyerCounter = Class_Interface.destroyerCounter;
            destroyerCounterText.text = destroyerCounter.ToString();
        }

        //--------------

        if (Input.GetKeyDown(KeyCode.Alpha1)) lightningButton.isOn = laserButton.isOn = pulseButton.isOn = eraserButton.isOn = collapsarButton.isOn = destroyerButton.isOn = false;
        Class_Interface.defaultEnabled = lightningButton.isOn == false && laserButton.isOn == false && pulseButton.isOn == false && eraserButton.isOn == false && collapsarButton.isOn == false && destroyerButton.isOn == false;

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Statistics()
    {
        //--------------

        float lifetime = PlayerPrefs.GetFloat("LifeTime");
        float distance = PlayerPrefs.GetFloat("LifeDistance");

        //--------------

        while (Class_Controller.playerAlive == true)
        {
            yield return delay0;

            destroyedStatisticsText.text = "Destroyed " + PlayerPrefs.GetInt("DestroyedEnemies").ToString();

            lifetime += 0.016f;
            PlayerPrefs.SetFloat("LifeTime", Mathf.Round(lifetime));
            lifetimeStatisticsText.text = "Lifetime " + Mathf.Round(lifetime).ToString();

            distance += Class_Controller.currentSpeed * 0.003f;
            PlayerPrefs.SetFloat("LifeDistance", Mathf.Round(distance));
            distanceStatisticsText.text = "Distance " + Mathf.Round(distance).ToString(); 
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator EnemyFriendlyPriority()
    {
        //--------------

        while (Class_Controller.playerAlive == true)
        {
            yield return delay1;

            if (triggerEnemy0 == false)
            {
                if (amountEnemyAIs > 0 && amountEnemyAIs == Class_AI.amountEnemyAIs)
                {
                    triggerEnemy0 = true;
                    fewEnemiesLeft = Mathf.FloorToInt(amountEnemyAIs / 4);
                }
            }
            else
            {
                if (triggerEnemy1 == false && amountEnemyAIs <= fewEnemiesLeft) triggerEnemy1 = true; // There are few Enemies left
                if (triggerEnemy2 == false && amountEnemyAIs <= 0)
                {
                    triggerEnemy2 = true;
                    PlayerPrefs.SetInt(Class_StarSystem.seed.ToString(), 1);
                    MineralsPlayerPrefs(true);
                    friendlySurvivedText.text = "Inhabitants Survived : " + amountFriendlyAIs.ToString();
                    enemiesDestroyedScreen.SetActive(true);
                    yield return delay1;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
                }
            }

            //---

            if (triggerFriendly0 == false)
            {
                if (amountFriendlyAIs > 0 && amountFriendlyAIs == Class_AI.amountFriendlyAIs)
                {
                    triggerFriendly0 = true;
                    fewFriendsLeft = Mathf.FloorToInt(amountFriendlyAIs / 4);
                }
            }
            else
            {
                if (triggerFriendly1 == false && amountFriendlyAIs <= fewFriendsLeft) triggerFriendly1 = true; // There are few Friends left
                if (triggerFriendly2 == false && amountFriendlyAIs <= 0)
                {
                    triggerFriendly2 = true;
                    PlayerPrefs.SetInt(Class_StarSystem.seed.ToString(), 2);
                    MineralsPlayerPrefs(true);
                    enemySurvivedText.text = "Enemies Survived : " + amountEnemyAIs.ToString();
                    friendsDestroyedScreen.SetActive(true);   
                    yield return delay1;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
                }
            }

            //---

            if (triggerEnemy0 == true && triggerFriendly0 == true)
            {
                if (loadingOn == true && Class_AI.aiSpawnOn == true && Class_StarSystem.generationOn == true) Class_Interface.loading = false;

                if (amountEnemyAIs >= amountFriendlyAIs)
                {
                    friendlyPriority.SetActive(false);
                    enemyPriority.SetActive(true);
                }
                else
                {
                    friendlyPriority.SetActive(true);
                    enemyPriority.SetActive(false);
                }

                if (friendlyCounterText.text != amountFriendlyAIs.ToString()) friendlyCounterText.text = amountFriendlyAIs.ToString();
                if (enemyCounterText.text != amountEnemyAIs.ToString()) enemyCounterText.text = amountEnemyAIs.ToString();
            }

            //---

            amountEnemyAIs = Class_AI.amountEnemyAIs;
            amountFriendlyAIs = Class_AI.amountFriendlyAIs;
        }

        //--------------

        lifetimeStatisticsDeadText.text = "Lived : " + Mathf.Round(PlayerPrefs.GetFloat("LifeTime")).ToString();
        distanceStatisticsDeadText.text = "Overcame the space : " + Mathf.Round(PlayerPrefs.GetFloat("LifeDistance")).ToString();
        destroyedStatisticsDeadText.text = "Destroyed Enemies : " + PlayerPrefs.GetInt("DestroyedEnemies").ToString();

        //--------------

        yield return delay1;
        PlayerPrefs.SetFloat("LifeTime", 0);
        PlayerPrefs.SetFloat("LifeDistance", 0);
        PlayerPrefs.SetInt("DestroyedEnemies", 0);
        yield return null;
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnApplicationPause()
    {
        //--------------

        MineralsPlayerPrefs(true);
        System.GC.Collect();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnApplicationQuit()
    {
        //--------------

        MineralsPlayerPrefs(true);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Autosave()
    {
        //--------------


        while (Class_Controller.playerAlive == true)
        {
            yield return delay2;
            MineralsPlayerPrefs(true);
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void MineralsPlayerPrefs(bool save)
    {
        //--------------

        if (save == true)
        {
            PlayerPrefs.SetInt("Mineral 0", Class_Interface.lifeCounter);
            PlayerPrefs.SetInt("Mineral 1", Class_Interface.powerCounter);
            PlayerPrefs.SetInt("Mineral 2", Class_Interface.speedCounter);
            PlayerPrefs.SetInt("Mineral 3", Class_Interface.lightningCounter);
            PlayerPrefs.SetInt("Mineral 4", Class_Interface.laserCounter);
            PlayerPrefs.SetInt("Mineral 5", Class_Interface.pulseCounter);
            PlayerPrefs.SetInt("Mineral 6", Class_Interface.maskerCounter);
            PlayerPrefs.SetInt("Mineral 7", Class_Interface.antigravityCounter);
            PlayerPrefs.SetInt("Mineral 8", Class_Interface.tamingCounter);
            PlayerPrefs.SetInt("Mineral 9", Class_Interface.eraserCounter);
            PlayerPrefs.SetInt("Mineral 10", Class_Interface.collapsarCounter);
            PlayerPrefs.SetInt("Mineral 11", Class_Interface.destroyerCounter);
        }
        else
        {
            if (clearPlayerPrefs == true) PlayerPrefs.DeleteAll();
            else if (giveMineralsForTesting == true)
            {
                PlayerPrefs.SetInt("Mineral 0", 100);
                PlayerPrefs.SetInt("Mineral 1", 100);
                PlayerPrefs.SetInt("Mineral 2", 100);
                PlayerPrefs.SetInt("Mineral 3", 100);
                PlayerPrefs.SetInt("Mineral 4", 100);
                PlayerPrefs.SetInt("Mineral 5", 100);
                PlayerPrefs.SetInt("Mineral 6", 100);
                PlayerPrefs.SetInt("Mineral 7", 100);
                PlayerPrefs.SetInt("Mineral 8", 100);
                PlayerPrefs.SetInt("Mineral 9", 100);
                PlayerPrefs.SetInt("Mineral 10", 100);
                PlayerPrefs.SetInt("Mineral 11", 100);
            }

            Class_Interface.lifeCounter = PlayerPrefs.GetInt("Mineral 0");
            lifeCounterText.text = liveIndicatorText.text = Class_Interface.lifeCounter.ToString();
            Class_Interface.powerCounter = PlayerPrefs.GetInt("Mineral 1");
            powerCounterText.text = Class_Interface.powerCounter.ToString();
            Class_Interface.speedCounter = PlayerPrefs.GetInt("Mineral 2");
            speedCounterText.text = Class_Interface.speedCounter.ToString();
            Class_Interface.lightningCounter = PlayerPrefs.GetInt("Mineral 3");
            lightningCounterText.text = Class_Interface.lightningCounter.ToString();
            Class_Interface.laserCounter = PlayerPrefs.GetInt("Mineral 4");
            laserCounterText.text = Class_Interface.laserCounter.ToString();
            Class_Interface.pulseCounter = PlayerPrefs.GetInt("Mineral 5");
            pulseCounterText.text = Class_Interface.pulseCounter.ToString();
            Class_Interface.maskerCounter = PlayerPrefs.GetInt("Mineral 6");
            maskerCounterText.text = Class_Interface.maskerCounter.ToString();
            Class_Interface.antigravityCounter = PlayerPrefs.GetInt("Mineral 7");
            antigravityCounterText.text = Class_Interface.antigravityCounter.ToString();
            Class_Interface.tamingCounter = PlayerPrefs.GetInt("Mineral 8");
            tamingCounterText.text = Class_Interface.tamingCounter.ToString();
            Class_Interface.eraserCounter = PlayerPrefs.GetInt("Mineral 9");
            eraserCounterText.text = Class_Interface.eraserCounter.ToString();
            Class_Interface.collapsarCounter = PlayerPrefs.GetInt("Mineral 10");
            collapsarCounterText.text = Class_Interface.collapsarCounter.ToString();
            Class_Interface.destroyerCounter = PlayerPrefs.GetInt("Mineral 11");
            destroyerCounterText.text = Class_Interface.destroyerCounter.ToString();
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
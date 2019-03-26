// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class InterfaceMenu : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public bool clearPlayerPrefs = false;
    public bool killPlayer = false;
    public bool hideLockMouse = true;
    public GameObject escMenu, firstReported, anotherGuardReported, initialScreen, gui;
    public Button graphicsQualityButton, okButton, cancelButton, exitButton;
    public Slider LODSlider, mouseSensitivitySlider,  effectsVolumeSlider, musicVolumeSlider;
    public Text squareGuardsText;
    public AudioMixer masterMixer;

    //--------------

    Text graphicsQualityText;
    bool graphicsQualityChanged, graphicsQualityOff;
    float LODSliderValue, mouseSensitivityValue, effectsVolumeSliderValue, musicVolumeSliderValue;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        Class_Interface.HideLockMouse(hideLockMouse);

        if (killPlayer == true) PlayerPrefs.SetInt("Mineral 0", 0);
        if (clearPlayerPrefs == true) PlayerPrefs.DeleteAll();

        graphicsQualityButton.onClick.AddListener(_DynamicLightingButton);
        okButton.onClick.AddListener(_OkButton);
        cancelButton.onClick.AddListener(_CancelButton);
        exitButton.onClick.AddListener(_ExitButton);

        graphicsQualityText = graphicsQualityButton.GetComponent<Text>();
        
        if (Class_Interface.initialScreen == false)
        {
            Class_Interface.initialScreen = true;
            initialScreen.SetActive(true);
        }

        Class_StarSystem.systemSelected = false;

        CheckSettings();

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (initialScreen.activeSelf == false)
        {
            if (PlayerPrefs.GetInt("FirstStart") == 0)
            {
                firstReported.SetActive(true);
                PlayerPrefs.SetInt("FirstStart", 1);                                       
                PlayerPrefs.SetInt("GraphicsQualityLow", 0);
                PlayerPrefs.SetFloat("LODValue", 1.0f);
                PlayerPrefs.SetFloat("mouseSensitivityValue", 1.0f);                
                PlayerPrefs.SetFloat("effectsVolumeValue", 1.0f);
                PlayerPrefs.SetFloat("musicVolumeValue", 0.5f);
                GiveMinerals();
            }

            if (PlayerPrefs.GetInt("FirstStart") != 0 && PlayerPrefs.GetInt("Mineral 0") == 0 && anotherGuardReported.activeSelf == false)
            {
                anotherGuardReported.SetActive(true);
                PlayerPrefs.SetInt("SquareGuardsDied", PlayerPrefs.GetInt("SquareGuardsDied") + 1);
                squareGuardsText.text = "Square guards died : " + PlayerPrefs.GetInt("SquareGuardsDied");
                GiveMinerals();
            }

            if (escMenu.activeSelf == false && firstReported.activeSelf == false && anotherGuardReported.activeSelf == false)
            {
                if (gui.activeSelf == false) gui.SetActive(true);
                if (Class_Interface.menuControlLock == true) Class_Interface.menuControlLock = false;
                Class_Interface.HideLockMouse(hideLockMouse);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    escMenu.SetActive(true);
                    CheckSettings();
                }
            }
            else if (escMenu.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Q)) _CancelButton();
                masterMixer.SetFloat("EffectsVolume", Mathf.Log(effectsVolumeSlider.value) * Mathf.Abs(Mathf.Log(effectsVolumeSlider.value)) * 15);
                masterMixer.SetFloat("OSTVolume", Mathf.Log(musicVolumeSlider.value) * 22);
            } 
        }        

        if (initialScreen.activeSelf == true || escMenu.activeSelf == true || firstReported.activeSelf == true || anotherGuardReported.activeSelf == true)
        {
            if (gui.activeSelf == true) gui.SetActive(false);
            if (Class_Interface.menuControlLock == false) Class_Interface.menuControlLock = true;
            if (escMenu.activeSelf == true || firstReported.activeSelf == true || anotherGuardReported.activeSelf == true) Class_Interface.HideLockMouse(false);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void CheckSettings()
    {
        //--------------

        if (PlayerPrefs.GetInt("GraphicsQualityLow") == 0)
        {
            graphicsQualityOff = false;
            graphicsQualityText.text = "Graphics Quality : High";
        }
        else
        {
            graphicsQualityOff = true;
            graphicsQualityText.text = "Graphics Quality : Low";
        }

        //--------------

        LODSliderValue = LODSlider.value = PlayerPrefs.GetFloat("LODValue");

        mouseSensitivityValue = mouseSensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivityValue");

        effectsVolumeSliderValue = effectsVolumeSlider.value = PlayerPrefs.GetFloat("effectsVolumeValue");
        musicVolumeSliderValue = musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolumeValue");

        masterMixer.SetFloat("EffectsVolume", Mathf.Log(effectsVolumeSliderValue) * Mathf.Abs(Mathf.Log(effectsVolumeSlider.value)) * 15);
        masterMixer.SetFloat("OSTVolume", Mathf.Log(musicVolumeSliderValue) * 22);

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void _DynamicLightingButton()
    {
        //--------------

        graphicsQualityChanged = true;

        //--------------

        if (graphicsQualityOff == false)
        {
            graphicsQualityOff = true;
            graphicsQualityText.text = "Graphics Quality : Low";
        }
        else
        {
            graphicsQualityOff = false;
            graphicsQualityText.text = "Graphics Quality : High";
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void _OkButton()
    {
        //--------------

        escMenu.SetActive(false);        

        if (graphicsQualityChanged == true)
        {
            graphicsQualityChanged = false;
            if (graphicsQualityOff == true) PlayerPrefs.SetInt("GraphicsQualityLow", 1);
            else PlayerPrefs.SetInt("GraphicsQualityLow", 0);
        }

        if (LODSliderValue != LODSlider.value) PlayerPrefs.SetFloat("LODValue", LODSlider.value);
        if (mouseSensitivityValue != mouseSensitivitySlider.value) PlayerPrefs.SetFloat("mouseSensitivityValue", mouseSensitivitySlider.value);
        if (effectsVolumeSliderValue != effectsVolumeSlider.value) PlayerPrefs.SetFloat("effectsVolumeValue", effectsVolumeSlider.value);
        if (musicVolumeSliderValue != musicVolumeSlider.value) PlayerPrefs.SetFloat("musicVolumeValue", musicVolumeSlider.value);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void _CancelButton()
    {
        //--------------

        escMenu.SetActive(false);
        graphicsQualityChanged = false;
        masterMixer.SetFloat("EffectsVolume", Mathf.Log(effectsVolumeSliderValue) * Mathf.Abs(Mathf.Log(effectsVolumeSliderValue)) * 15);
        masterMixer.SetFloat("OSTVolume", Mathf.Log(musicVolumeSliderValue) * 22);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void _ExitButton()
    {
        //--------------

        Application.Quit();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void GiveMinerals()
    {
        //--------------        

        PlayerPrefs.SetInt("Mineral 0", Random.Range(5, 8));
        PlayerPrefs.SetInt("Mineral 1", Random.Range(10, 15));
        PlayerPrefs.SetInt("Mineral 2", Random.Range(10, 15));
        PlayerPrefs.SetInt("Mineral 3", Random.Range(10, 18));
        PlayerPrefs.SetInt("Mineral 4", Random.Range(10, 20));
        PlayerPrefs.SetInt("Mineral 5", Random.Range(1, 5));
        PlayerPrefs.SetInt("Mineral 6", Random.Range(2, 8));
        PlayerPrefs.SetInt("Mineral 7", Random.Range(10, 18));
        PlayerPrefs.SetInt("Mineral 8", Random.Range(2, 7));
        PlayerPrefs.SetInt("Mineral 9", Random.Range(1, 4));
        PlayerPrefs.SetInt("Mineral 10", Random.Range(1, 4));
        PlayerPrefs.SetInt("Mineral 11", Random.Range(1, 5));

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

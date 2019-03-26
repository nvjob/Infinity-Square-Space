// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class BlackHole : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public bool on = true;
    public ParticleSystem crownA;
    public ParticleSystem crownB;
    public Transform satellitesOrbit;
    public ParticleSystem quasarA;
    public ParticleSystem quasarB;
    public Material material;
    public GameObject asteroid;
    public AudioSource soundFx;
    public GameObject QuasarAColider, QuasarBColider;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(2);
    Transform thisTransform;
    ParticleSystem.MainModule particleMain;
    float rotateSeedG, distanceDetectedPlayer;
    bool triggerDetectedPlayer;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        soundFx.enabled = false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        if (on == true)
        {
            rotateSeedG = Class_StarSystem.SeedAbsCos() * 0.02f;

            StartCoroutine(GenerationBlackHole());
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void FixedUpdate()
    {
        //--------------

        if (on == true) thisTransform.Rotate(Vector3.up * rotateSeedG);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator GenerationBlackHole()
    {
        //--------------

        material.SetFloat("_HoleSize", 0.7f + (Mathf.Cos(Class_StarSystem.seed * 12.5f) * 0.11f));

        Color colorA = new Color(RenderSettings.fogColor.r, RenderSettings.fogColor.g, RenderSettings.fogColor.b, 0.9f);
        Color colorB = new Color(0.62f, 0.43f, 0.4f, 0.9f);

        particleMain = crownA.main;
        particleMain.startColor = new ParticleSystem.MinMaxGradient(colorA, colorB);
        colorA = new Color(colorA.r * 1.4f, colorA.g * 1.4f, colorA.b * 1.4f, 0.9f);
        particleMain = crownB.main;
        particleMain.startColor = new ParticleSystem.MinMaxGradient(colorA, new Color(colorB.r * 1.4f, colorB.g * 1.4f, colorB.b * 1.4f, 0.9f));
        colorA = new Color(colorA.r * 1.3f, colorA.g * 1.3f, colorA.b * 1.3f, 0.9f);
        colorB = new Color(0.9f, 0.4f, 0.4f, 0.9f);
        particleMain = quasarA.main;
        particleMain.startColor = new ParticleSystem.MinMaxGradient(colorA, colorB);
        var particleShape = quasarA.shape;
        particleShape.radius = Class_StarSystem.SeedAbsCos() * 0.5f;
        particleShape.angle = 1 + (Mathf.Abs(Mathf.Cos(Class_StarSystem.seed * Class_StarSystem.SeedAbsCos())) * 3);
        particleMain = quasarB.main;
        particleMain.startColor = new ParticleSystem.MinMaxGradient(colorA, colorB);
        particleShape = quasarB.shape;
        particleShape.radius = Class_StarSystem.SeedAbsCos() * 0.5f;
        particleShape.angle = 1 + (Mathf.Abs(Mathf.Cos(Class_StarSystem.seed * Class_StarSystem.SeedAbsCos())) * 3);

        //--------------

        thisTransform.localScale = Vector3.one * (Class_StarSystem.SeedAbsCos() + 0.3f) * 0.9f;
        soundFx.maxDistance = thisTransform.localScale.x * 800;
        distanceDetectedPlayer = Mathf.Pow(soundFx.maxDistance + 50, 2);
        float scaleblackHole = (Class_StarSystem.SeedAbsCos() + 0.2f) * 50;
        thisTransform.rotation = new Quaternion(Class_StarSystem.SeedAbsCos() * 60, Class_StarSystem.SeedAbsCos() * 60, Class_StarSystem.SeedAbsCos() * -60, 0);

        //--------------

        satellitesOrbit.localRotation = Quaternion.Euler(Class_AdditionalTools.PositionSeed(thisTransform, 1.1f) * 15, Class_AdditionalTools.PositionSeed(thisTransform, 2.2f) * 15, Class_AdditionalTools.PositionSeed(thisTransform, 3.3f) * 15);

        float systemSize = 2 * scaleblackHole / thisTransform.localScale.x;
        int systemDensity = 7;

        //--------------

        for (int x = -systemDensity; x < systemDensity; x++)
        {
            for (int z = -systemDensity; z < systemDensity; z++)
            {
                yield return null;

                float distance = Vector3.Distance(new Vector3(x, 0, z), Vector3.zero);

                if (distance < systemDensity && distance > systemDensity * 0.6f)
                {
                    float xt = (systemSize * 0.5f - 0.5f) + (x * systemSize);
                    float zt = (systemSize * 0.5f - 0.5f) + (z * systemSize);
                    float cosQtObj = Mathf.Cos((11.1f + xt + z) * (10.3f + x + zt) * Class_StarSystem.seed * 0.033f);
                    float lperlinf = Mathf.PerlinNoise(cosQtObj, cosQtObj);

                    if (lperlinf > 0.1f && lperlinf < 0.75f)
                    {
                        GameObject obj = Instantiate(asteroid);
                        Transform tr = obj.transform;
                        obj.GetComponent<Asteroid>().minAmountDetails = 2;
                        obj.GetComponent<Asteroid>().maxAmountDetails = 2;
                        tr.rotation = Random.rotation;
                        tr.localScale = Vector3.one * (1 + (lperlinf * scaleblackHole * 0.8f));
                        tr.parent = satellitesOrbit;
                        float yt = Mathf.Cos(lperlinf * (lperlinf + xt + zt)) * scaleblackHole;
                        tr.localPosition = new Vector3(xt + (Mathf.Cos(lperlinf * zt) * scaleblackHole), yt, zt + (Mathf.Cos(lperlinf * xt) * scaleblackHole));
                        obj.SetActive(true);
                    }
                }
            }
        }

        //--------------

        while (Class_Controller.playerAlive == true)
        {
            triggerDetectedPlayer = Class_Controller.SqrMagnitudeToPlayer(thisTransform) < distanceDetectedPlayer;
            if (soundFx.enabled != triggerDetectedPlayer) soundFx.enabled = triggerDetectedPlayer;

            QuasarAColider.SetActive(true);
            QuasarBColider.SetActive(true);

            yield return delay0;

            QuasarAColider.SetActive(false);
            QuasarBColider.SetActive(false);

            yield return delay0;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

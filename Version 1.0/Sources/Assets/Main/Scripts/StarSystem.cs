// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class StarSystem : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public int testSeed = 99;
    public bool testSeedOn = true;
    public bool generation = false;
    public bool sunOn = true;
    public bool aiSpawnOn = true;
    public GameObject sun;
    public GameObject blackHole;
    public Material skyMaterial;
    public GameObject planetarySystem;
    public GameObject asteroidsGroup;
    public Transform starBackground;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.5f), delay1 = new WaitForSeconds(0.1f);
    Transform thisTransform;
    float systemSize;
    int systemDensity;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------
        
        if (testSeedOn == true) Class_StarSystem.seed = testSeed;
        else Class_StarSystem.seed = PlayerPrefs.GetInt("SeedGen");
        thisTransform = transform;
        Class_AI.aiSpawnOn = aiSpawnOn;
        Class_StarSystem.generationOn = generation;

        Class_PlanetarySystem.listPlanets.Clear();
        Class_PlanetarySystem.listPlanets = new List<GameObject>();

        float genColA = Class_StarSystem.SeedAbsCos() * 0.93f;
        skyMaterial.SetColor("_ColorA", Color.HSVToRGB(genColA, 0.24f, 0.29f));
        skyMaterial.SetColor("_ColorB", Color.HSVToRGB(Class_StarSystem.SeedAbsCos(), 0.27f, 0.3f));
        RenderSettings.fogColor = Color.HSVToRGB((Class_StarSystem.SeedAbsCos() + genColA) * 0.5f, 0.35f, 0.47f);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------        

        if (generation == true) StartCoroutine(GenerationStarSystem());

        //--------------

        if (sunOn == true)
        {
            if (Class_StarSystem.seed % 7 == 0)
            {
                sun.SetActive(false);
                blackHole.SetActive(true);
            }
            else
            {
                blackHole.SetActive(false);
                sun.SetActive(true);
            }
        }
        else
        {
            blackHole.SetActive(false);
            sun.SetActive(false);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator GenerationStarSystem()
    {
        //--------------

        systemSize = 500 + ((Class_StarSystem.SeedAbsCos() + 0.2f) * 225) + Mathf.Abs(Mathf.Cos(Class_StarSystem.SeedAbsCos() * 371.45f) * 275);
        systemDensity = 3;

        //--------------

        yield return delay0;       
        
        for (int x = -systemDensity; x < systemDensity; x++)
        {
            for (int z = -systemDensity; z < systemDensity; z++)
            {
                yield return delay1;

                Vector3 position = new Vector3(x, 0, z);
                float distance = Vector3.Distance(position, Vector3.zero);

                if (distance < systemDensity && distance > systemDensity * 0.15f)
                {
                    float xt = (systemSize * 0.5f - 0.5f) + (x * systemSize);
                    float zt = (systemSize * 0.5f - 0.5f) + (z * systemSize);                    
                    float cosQtObj = Mathf.Cos((11.1f + xt + z) * (10.3f + x + zt) * Class_StarSystem.seed * 0.033f);
                    float lperlinf = Mathf.PerlinNoise(cosQtObj, cosQtObj);

                    if (lperlinf > 0.25f && lperlinf < 0.4f)
                    {
                        GameObject obj = Instantiate(planetarySystem);
                        Transform tr = obj.transform;
                        float yt = (Mathf.Pow(lperlinf * 10, 7) * 0.1f) - 700;
                        tr.rotation = thisTransform.rotation;
                        tr.parent = thisTransform;
                        tr.localPosition = new Vector3(xt, yt, zt);
                    }
                    else if (lperlinf > 0.4f && lperlinf < 0.6f)
                    {
                        GameObject obj = Instantiate(asteroidsGroup);
                        Transform tr = obj.transform;
                        float yt = (Mathf.Pow(lperlinf * 10, 5.9f) * 0.1f) - 1150;
                        tr.rotation = thisTransform.rotation;
                        tr.parent = thisTransform;
                        tr.localPosition = new Vector3(xt, yt, zt);
                    }
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (starBackground.position != Class_Controller.playerPosition) starBackground.position = Class_Controller.playerPosition;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
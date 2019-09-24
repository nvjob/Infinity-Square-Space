// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class PlanetarySystem : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public GameObject planet;
    public GameObject moon;
    public GameObject asteroid;
    public GameObject spawnAI;
    public Transform satellitesOrbit;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(1.5f);
    Transform thisTransform;
    float randomRotationUp, sqrtDistanceLod;    
    bool rotationLod, planetDestroyed;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        Class_AdditionalTools.RandomName(gameObject, "Planetary System");
        sqrtDistanceLod = Mathf.Pow(Class_Interface.distanceLod, 2);

        randomRotationUp = Class_AdditionalTools.PositionSeed(thisTransform, 0.73f) * 0.05f;
        satellitesOrbit.transform.localRotation = Quaternion.Euler(Class_AdditionalTools.PositionSeed(thisTransform, 1.1f) * 15, Class_AdditionalTools.PositionSeed(thisTransform, 2.2f) * 15, Class_AdditionalTools.PositionSeed(thisTransform, 3.3f) * 15);

        //--------------

        StartCoroutine(Class_PlanetarySystem.Generation(thisTransform, satellitesOrbit, planet, moon, asteroid, spawnAI));
        StartCoroutine(Optimization());
        StartCoroutine(FakeFixedUpdate());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Optimization()
    {
        //--------------

        while (planetDestroyed == false)
        {
            yield return delay0;

            planetDestroyed = planet.GetComponent<Planet>().planetDestroyed;
            if (Class_Controller.SqrMagnitudeToPlayer(thisTransform) < sqrtDistanceLod) rotationLod = false;
            else rotationLod = true;
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator FakeFixedUpdate()
    {
        //--------------        

        while (planetDestroyed == false)
        {
            if (rotationLod == false)
            {
                yield return new WaitForFixedUpdate();
                satellitesOrbit.Rotate(Vector3.up * randomRotationUp);
            }
            else yield return delay0;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
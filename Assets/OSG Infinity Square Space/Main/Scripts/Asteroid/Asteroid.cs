// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class Asteroid : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    public GameObject asteroidCubes;
    public GameObject lod;
    public int minAmountDetails = 3;
    public int maxAmountDetails = 4;

    //--------------

    Transform thisTransform;



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

        Class_AdditionalTools.RandomName(gameObject, "Asteroid");
        Class_AdditionalTools.RandomName(asteroidCubes, "Asteroid Cubes");        

        thisTransform.localRotation = Quaternion.Euler(Class_AdditionalTools.PositionSeed(thisTransform, 1.1f) * 90, Class_AdditionalTools.PositionSeed(thisTransform, 2.2f) * 90, Class_AdditionalTools.PositionSeed(thisTransform, 3.3f) * 90);
        int amountAsteroids = minAmountDetails + Mathf.RoundToInt(Mathf.Abs(Class_AdditionalTools.PositionSeed(transform, 8.98f)) * maxAmountDetails);

        StartCoroutine(Class_Asteroid.GenAsteroid(thisTransform, asteroidCubes.transform, Mathf.Pow(Class_Interface.distanceLod, 2), asteroidCubes, lod, amountAsteroids));

        //--------------
    }    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
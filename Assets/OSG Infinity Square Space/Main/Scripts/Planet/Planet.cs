// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class Planet : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            


    public GameObject landscape;
    public GameObject atmosphere;
    public GameObject forest;
    public GameObject gravity;
    public GameObject collidersToOptimizeDestruction;

    //--------------

    [HideInInspector]
    public bool planetDestroyed;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(1.5f);
    GameObject planetCube;
    Transform thisTransform, planetCubeTransform, atmosphereTransform;
    float sqrtDistanceLod, randomRotationUp, randomRotationLeft;
    bool rotationLod, brokenStage0, brokenStage1, brokenStage2;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        atmosphereTransform = atmosphere.transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start ()
    {
        //--------------

        Class_AdditionalTools.RandomName(gameObject, "Planet");
        sqrtDistanceLod = Mathf.Pow(Class_Interface.distanceLod, 2);
        Class_PlanetarySystem.listPlanets.Add(gameObject);

        randomRotationUp = Class_AdditionalTools.PositionSeed(thisTransform, 0.39f) * 0.025f;
        randomRotationLeft = Class_AdditionalTools.PositionSeed(thisTransform, 0.719f) * 0.025f;

        float uvOffsetX = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.115f));
        landscape.GetComponent<Landscape>().uvOffset = new Vector2(uvOffsetX, 0.5f);

        //--------------

        planetCube = ChunkCubesPool.GiveCube();
        planetCubeTransform = planetCube.transform;
        planetCube.GetComponent<ChunkCube>().uvOffset = new Vector2(uvOffsetX, 0.5f);
        planetCubeTransform.parent = thisTransform;
        planetCubeTransform.localScale = Vector3.one * 10;
        planetCubeTransform.SetPositionAndRotation(thisTransform.position, thisTransform.rotation);
        planetCube.SetActive(true);

        //--------------

        if (Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.15f)) < 0.3f)
        {
            atmosphere.SetActive(false);
            forest.SetActive(false);
        }
        else
        {
            atmosphereTransform.localRotation = Quaternion.Euler(Mathf.Cos(uvOffsetX * 1.1f) * 10, Mathf.Cos(uvOffsetX * 2.2f) * 10, Mathf.Cos(uvOffsetX * 3.3f) * 10);
            atmosphereTransform.localScale = Vector3.one * (18 + Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 9.39f) * 4));
        }

        //--------------

        StartCoroutine(Optimization());
        StartCoroutine(FakeFixedUpdate());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Optimization()
    {
        //--------------

        while (brokenStage0 == false)
        {
            yield return delay0;            

            int planetChildCount = planetCubeTransform.childCount;            

            if (brokenStage1 == false && planetChildCount > 0) brokenStage1 = true;

            if (brokenStage1 == true)
            {
                if (planetChildCount < 6 && brokenStage2 == false)
                {
                    brokenStage2 = true;
                    rotationLod = false;
                    Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(1), planetCubeTransform.localScale.x * Random.Range(0.75f, 1.25f));
                    while (gravity.transform.childCount != 0)
                    {
                        yield return null;
                        foreach (Transform child in gravity.transform) child.transform.parent = null;
                    }
                    Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(1), planetCubeTransform.localScale.x * Random.Range(0.75f, 1.25f));
                    yield return null;
                    gravity.SetActive(false);
                    atmosphere.SetActive(false);
                    forest.SetActive(false);
                }

                if (planetChildCount == 0) brokenStage0 = true;
            }            

            if (brokenStage2 == false)
            {
                if (Class_Controller.SqrMagnitudeToPlayer(thisTransform) < sqrtDistanceLod) rotationLod = false;
                else rotationLod = true;
            }            
        }

        //--------------

        planetDestroyed = true;
        Class_PlanetarySystem.listPlanets.Remove(gameObject);
        planetCube.SetActive(false);
        collidersToOptimizeDestruction.SetActive(false);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    IEnumerator FakeFixedUpdate()
    {
        //--------------        

        while (brokenStage2 == false)
        {
            if (rotationLod == false)
            {
                yield return new WaitForFixedUpdate();
                thisTransform.Rotate(Vector3.up * randomRotationUp);
                thisTransform.Rotate(Vector3.left * randomRotationLeft);
            }
            else yield return delay0;
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
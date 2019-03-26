// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class Moon : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    public GameObject landscape;
    public GameObject satellitesOrbit;
    public GameObject atmosphere;
    public GameObject gravity;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(1.5f);
    GameObject moonCube;
    Transform thisTransform, moonCubeTransform, satellitesOrbitTransform;
    float sqrtDistanceLod, randomRotationUp, randomRotationLeft, scaleMoon;
    bool rotationLod, brokenStage0, brokenStage1, brokenStage2, noSatellites;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;        
        satellitesOrbitTransform = satellitesOrbit.transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start ()
    {
        //--------------

        Class_AdditionalTools.RandomName(gameObject, "Moon");
        sqrtDistanceLod = Mathf.Pow(Class_Interface.distanceLod, 2);

        scaleMoon = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.112f));
        thisTransform.localScale = Vector3.one * (1 + (scaleMoon * 2));

        randomRotationUp = Class_AdditionalTools.PositionSeed(thisTransform, 0.179f) * 0.03f;
        randomRotationLeft = Class_AdditionalTools.PositionSeed(thisTransform, 0.813f) * 0.03f;

        float uvOffsetX = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.123f));
        landscape.GetComponent<Landscape>().uvOffset = new Vector2(uvOffsetX, 0.5f);

        //--------------

        moonCube = ChunkCubesPool.GiveCube();
        moonCubeTransform = moonCube.transform;
        moonCube.GetComponent<ChunkCube>().uvOffset = new Vector2(uvOffsetX, 0.5f);
        moonCubeTransform.parent = thisTransform;
        moonCubeTransform.localScale = Vector3.one * 10;
        moonCubeTransform.SetPositionAndRotation(thisTransform.position, thisTransform.rotation);
        moonCube.SetActive(true);

        //--------------

        if (Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.151f)) < 0.6f) atmosphere.SetActive(false);
        else atmosphere.transform.localRotation = Quaternion.Euler(Mathf.Cos(uvOffsetX * 1.1f) * 16, Mathf.Cos(uvOffsetX * 2.2f) * 16, Mathf.Cos(uvOffsetX * 3.3f) * 16);

        if (Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 125.15f)) < 0.5f)
        {
            satellitesOrbit.SetActive(false);
            noSatellites = true;
        }
        else StartCoroutine(Class_Moon.GenSatellites(thisTransform, satellitesOrbitTransform, scaleMoon, uvOffsetX));

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

            int moonChildCount = moonCubeTransform.childCount;

            if (brokenStage1 == false && moonChildCount > 0) brokenStage1 = true;

            if (brokenStage1 == true)
            {
                if (moonChildCount < 6 && brokenStage2 == false)
                {
                    brokenStage2 = true;
                    rotationLod = false;
                    Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(1), moonCubeTransform.localScale.x * Random.Range(0.75f, 1.25f));
                    while (gravity.transform.childCount != 0)
                    {
                        yield return null;
                        foreach (Transform child in gravity.transform) child.transform.parent = null;
                    }
                    Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(1), moonCubeTransform.localScale.x * Random.Range(0.75f, 1.25f));
                    yield return null;
                    gravity.SetActive(false);
                    atmosphere.SetActive(false);
                }

                if (moonChildCount == 0) brokenStage0 = true;
            }

            if (brokenStage2 == false)
            {
                if (Class_Controller.SqrMagnitudeToPlayer(thisTransform) < sqrtDistanceLod) rotationLod = false;
                else rotationLod = true;
            }
        }

        //--------------

        moonCube.SetActive(false);

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

                moonCubeTransform.Rotate(Vector3.up * randomRotationUp);
                moonCubeTransform.Rotate(Vector3.left * randomRotationLeft);
                if (noSatellites == false) satellitesOrbitTransform.Rotate(Vector3.up * randomRotationUp * -2);
            }
            else yield return delay0;
        }

        //--------------
    }
          


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
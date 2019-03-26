// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class ChunkCube : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    [HideInInspector]
    public Vector2 uvOffset;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.1f);
    Transform thisTransform;
    BoxCollider thisBoxCollider;
    Renderer thisRenderer;
    bool brokenStage0, outPool, visible;
    int numberDebris, numberCubeOutOfPool;
    float thisScaleX, cubeOutOfPoolScale, currentDamage, amountDamage, faultSize, faultSizeOptimize;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void Awake()
    {
        //--------------

        thisTransform = transform;
        thisBoxCollider = GetComponent<BoxCollider>();
        thisRenderer = GetComponent<Renderer>();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnEnable()
    {
        //--------------

        if (outPool == false)
        {
            outPool = true;
            Class_AdditionalTools.RandomName(gameObject, "Chunk Cube");
            Class_AdditionalTools.UvMesh(GetComponent<MeshFilter>().mesh, uvOffset.x, uvOffset.y);
            amountDamage = currentDamage = 0;
            brokenStage0 = false;
            thisScaleX = thisTransform.lossyScale.x;

            if (thisScaleX <= 4) gameObject.layer = 21;
            else if (thisScaleX > 4 && thisScaleX <= 8) gameObject.layer = 20;
            else if (thisScaleX > 8) gameObject.layer = 19;

            faultSize = Random.Range(2.0f, 3.0f) + thisScaleX * 0.75f;
            faultSizeOptimize = 1;
            numberDebris = Random.Range(2, 5);
            numberCubeOutOfPool = Random.Range(2, 4);
            cubeOutOfPoolScale = 1.0f / numberCubeOutOfPool;
            thisRenderer.enabled = thisBoxCollider.enabled = true;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnCollisionEnter(Collision collision)
    {
        //--------------        

        if (Class_ChunkCube.DamageType(collision.collider, 1, brokenStage0, outPool) == true)
        {
            currentDamage = Class_ChunkCube.CurrentDamage(collision.collider, false, false, false);
            StartCoroutine(SplitСube());
        }

        if (Class_ChunkCube.DamageType(collision.collider, 2, brokenStage0, outPool) == true)
        {
            currentDamage = Class_ChunkCube.CurrentDamage(collision.collider, false, true, false);
            StartCoroutine(SplitСube());
        }

        //-------------- 
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerEnter(Collider collider)
    {
        //--------------

        if (Class_ChunkCube.DamageType(collider, 3, brokenStage0, outPool) == true)
        {
            currentDamage = Class_ChunkCube.CurrentDamage(collider, true, true, false);
            StartCoroutine(SplitСube());
        }

        if (Class_ChunkCube.DamageType(collider, 4, brokenStage0, outPool) == true)
        {
            currentDamage = Class_ChunkCube.CurrentDamage(collider, true, true, true);
            StartCoroutine(SplitСube());
        }

        if (Class_ChunkCube.DamageType(collider, 5, brokenStage0, outPool) == true) faultSizeOptimize = 1.025f;
        if (Class_ChunkCube.DamageType(collider, 6, brokenStage0, outPool) == true) faultSizeOptimize = 1.05f;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator SplitСube()
    {
        //--------------

        amountDamage += currentDamage;

        if (amountDamage >= 10.0f + (faultSize * 0.5f) && brokenStage0 == false)
        {
            brokenStage0 = true;
            StartCoroutine(Class_ChunkCube.CheckChildCount(thisTransform, gameObject));
            if (thisScaleX > faultSize * faultSizeOptimize)
            {
                Class_ChunkCube.CubeOutOfPool(thisTransform, numberCubeOutOfPool, cubeOutOfPoolScale, uvOffset);
                thisRenderer.enabled = false;
                thisBoxCollider.enabled = false;
            }
            else
            {
                outPool = false;

                if (visible == true)
                {
                    Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(0), Random.Range(0.75f, 1.25f));
                    Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(1), Random.Range(0.75f, 1.25f));
                    for (int i = 0; i < numberDebris; i++) Class_ChunkCube.DebrisOutOfPool(thisTransform, thisRenderer, DebrisesPool.GiveDebris(Random.Range(0, 2)), numberDebris, Random.Range(0.4f, 0.9f), uvOffset);
                }

                Class_Mineral.RandomMinerals(thisTransform);
                yield return delay0;
                ChunkCubesPool.TakeCube(gameObject);
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnDisable() { outPool = false; }

    void OnBecameInvisible() { visible = false; }

    void OnBecameVisible() { visible = true; }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_ChunkCube
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(3);
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator CheckChildCount(Transform thisTransform, GameObject gameObject)
    {
        //--------------

        int counter0 = 0;

        while (true)
        {
            yield return delay0;

            if (thisTransform.childCount == 0)
            {
                ChunkCubesPool.TakeCube(gameObject);
            }
            else if (thisTransform.childCount > 0 && thisTransform.childCount <= 2)
            {
                if (counter0++ >= 75)
                {
                    counter0 = 0;
                    if (Class_Controller.SqrMagnitudeToPlayer(thisTransform) > 250000) ChunkCubesPool.TakeCube(gameObject);
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void CubeOutOfPool(Transform thisTransform, int numberCubeOutOfPool, float cubeOutOfPoolScale, Vector2 uvOffset)
    {
        //--------------

        for (int z = 0; z < numberCubeOutOfPool; z++)
        {
            for (int y = 0; y < numberCubeOutOfPool; y++)
            {
                for (int x = 0; x < numberCubeOutOfPool; x++)
                {
                    GameObject cubeOutOfPool = ChunkCubesPool.GiveCube();

                    if (cubeOutOfPool != null)
                    {
                        Transform cubeOutOfPoolTransform = cubeOutOfPool.transform;
                        cubeOutOfPoolTransform.rotation = thisTransform.rotation;
                        cubeOutOfPoolTransform.parent = thisTransform;
                        cubeOutOfPoolTransform.localScale = Vector3.one * cubeOutOfPoolScale;
                        float xt = (cubeOutOfPoolScale * 0.5f - 0.5f) + (x * cubeOutOfPoolScale);
                        float yt = (cubeOutOfPoolScale * 0.5f - 0.5f) + (y * cubeOutOfPoolScale);
                        float zt = (cubeOutOfPoolScale * 0.5f - 0.5f) + (z * cubeOutOfPoolScale);
                        cubeOutOfPoolTransform.localPosition = new Vector3(xt, yt, zt);
                        cubeOutOfPool.GetComponent<ChunkCube>().uvOffset = uvOffset;
                        cubeOutOfPool.SetActive(true);
                    }
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static bool DamageType(Collider collider, int type, bool brokenStage0, bool outPool)
    {
        //--------------

        GameObject obj = collider.gameObject;

        if (outPool == true)
        {
            if (brokenStage0 == false)
            {
                if (type == 1) return obj.layer == 12 || obj.layer == 25;
                else if (type == 2) return obj.layer == 13 || obj.layer == 15;
                else if (type == 3) return obj.layer == 9;
                else if (type == 4) return obj.layer == 12 || obj.layer == 13 || obj.layer == 15 || obj.layer == 25;
                else if (type == 5) return obj.name == "Colliders to Optimize Destruction 0";
                else if (type == 6) return obj.name == "Colliders to Optimize Destruction 1";
                else return false;
            }
            else return false;
        }
        else return false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static float CurrentDamage(Collider collider, bool triggerEnter, bool aiBullet, bool eraser)
    {
        //--------------

        float currentDamage = 0;

        if (triggerEnter == false)
        {
            if (aiBullet == false)
            {
                if (Class_Interface.powerEnabled == false)
                {
                    if (collider.CompareTag("Bullet Default")) currentDamage = 4;
                    if (collider.CompareTag("Bullet Light")) currentDamage = 3;
                    if (collider.CompareTag("Bullet Laser")) currentDamage = 4;
                }
                else
                {
                    if (collider.CompareTag("Bullet Default")) currentDamage = 5;
                    if (collider.CompareTag("Bullet Light")) currentDamage = 4;
                    if (collider.CompareTag("Bullet Laser")) currentDamage = 5;
                }

                if (collider.CompareTag("Bullet Destroyer")) currentDamage = 30;
            }
            else
            {
                if (collider.CompareTag("Bullet Default") || collider.CompareTag("Bullet Light") || collider.CompareTag("Bullet Laser")) currentDamage = 2;
            }
        }
        else
        {
            if (eraser == false)
            {
                if (collider.name == "Pulse Level 0") currentDamage = 0.1f;
                if (collider.name == "Pulse Level 1") currentDamage = 0.5f;
                if (collider.name == "Pulse Level 2") currentDamage = 1.5f;
                if (collider.name == "Pulse Level 3") currentDamage = 20;
                if (collider.name == "High Speed") currentDamage = 20;
            }
            else
            {
                if (collider.CompareTag("Bullet Eraser") || collider.CompareTag("Bullet Collapsar") || collider.CompareTag("SunKilling")) currentDamage = 30;
            }
        }

        return currentDamage;

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void ParticlesOutOfPool(Transform thisTransform, GameObject obj, float scale)
    {
        //--------------

        if (obj != null)
        {
            Transform tr = obj.transform;
            tr.SetPositionAndRotation(thisTransform.position, Random.rotation);
            tr.localScale = thisTransform.lossyScale * scale;
            obj.SetActive(true);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void DebrisOutOfPool(Transform thisTransform, Renderer thisRenderer, GameObject obj, int numberDebris, float scale, Vector2 uvOffset)
    {
        //--------------

        if (obj != null)
        {
            Transform tr = obj.transform;
            tr.SetPositionAndRotation(thisTransform.position, Random.rotation);
            tr.localScale = (thisRenderer.bounds.size / numberDebris) * scale;
            Class_AdditionalTools.UvMesh(obj.GetComponent<MeshFilter>().mesh, uvOffset.x, uvOffset.y);
            obj.SetActive(true);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
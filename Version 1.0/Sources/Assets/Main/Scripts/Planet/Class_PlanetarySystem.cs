// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Class_PlanetarySystem
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static List<GameObject> listPlanets = new List<GameObject>();

    static WaitForSeconds delay0 = new WaitForSeconds(0.1f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator Generation(Transform thisTransform, Transform satellitesOrbit, GameObject planet, GameObject moon, GameObject asteroid, GameObject spawnAI)
    {
        //--------------

        float scaleSystem = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 5.18f));
        int typeAI = 0;
        int discretenessOrbitRadius = 3;
        float conditionalSizeOrbit = 80 + (scaleSystem * 40);

        Transform ptr = Transform.Instantiate(planet).transform;
        ptr.SetPositionAndRotation(thisTransform.position, thisTransform.rotation);
        ptr.localScale = Vector3.one * (4.5f + (scaleSystem * 3));
        ptr.parent = thisTransform;

        if (Class_AI.aiSpawnOn == true)
        {
            if (listPlanets.Count > 0)
            {
                if (Class_AdditionalTools.PositionSeed(thisTransform, 0.456f) < 0.85f) typeAI = 1;
                else typeAI = 2;
            }
            else typeAI = 2;
        }

        yield return delay0;

        Vector3 curPos = thisTransform.position;
        Quaternion curRot = thisTransform.rotation;

        Vector3 center = Vector3.zero;

        for (int x = -discretenessOrbitRadius; x < discretenessOrbitRadius; x++)
        {
            for (int z = -discretenessOrbitRadius; z < discretenessOrbitRadius; z++)
            {
                yield return delay0;

                Vector3 position = new Vector3(x, 0, z);
                float distance = Vector3.Distance(position, center);

                if (distance < discretenessOrbitRadius && distance > discretenessOrbitRadius * 0.5f)
                {
                    float xt = (conditionalSizeOrbit * 0.5f - 0.5f) + (x * conditionalSizeOrbit);
                    float zt = (conditionalSizeOrbit * 0.5f - 0.5f) + (z * conditionalSizeOrbit);
                    float cosQtObj = Mathf.Cos((11.1f + xt * 3.3f + z) * (10.3f + x + zt * 3.3f) * Class_StarSystem.seed * 0.033f);
                    float lperlinf = Mathf.PerlinNoise(cosQtObj, cosQtObj);

                    if (lperlinf > 0.25f && lperlinf < 0.4f)
                    {
                        Transform mtr = Transform.Instantiate(moon).transform;
                        mtr.parent = satellitesOrbit;
                        float yt = (Mathf.Pow(lperlinf * 10, 5) * 0.5f) - 200;
                        mtr.localPosition = new Vector3(xt, yt, zt);
                    }
                    else if (lperlinf > 0.4f && lperlinf < 0.6f)
                    {
                        Transform atr = Transform.Instantiate(asteroid).transform;
                        atr.localScale = Vector3.one * Mathf.Pow(lperlinf * 10, 1.5f) * 2.5f;
                        atr.parent = satellitesOrbit;
                        float yt = (Mathf.Pow(lperlinf * 10, 4) * 0.5f) - 200;
                        atr.localPosition = new Vector3(xt, yt, zt);
                    }
                    if (lperlinf > 0.4f && Class_AI.aiSpawnOn)
                    {
                        GameObject obj = GameObject.Instantiate(spawnAI, curPos, curRot);
                        if (typeAI == 1) obj.GetComponent<SpawnAI>().enemyAiOn = true;
                        if (typeAI == 2) obj.GetComponent<SpawnAI>().friendlyAiOn = true;
                        obj.transform.position = curPos + new Vector3(xt * 0.6f, 0, zt * 0.6f);
                    }
                }
            }
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
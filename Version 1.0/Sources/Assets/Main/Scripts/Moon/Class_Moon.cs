// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Moon
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(0.05f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenSatellites(Transform thisTransform, Transform satellitesOrbitTransform, float scaleMoon, float uvOffsetX)
    {
        //--------------

        satellitesOrbitTransform.localRotation = Quaternion.Euler(Class_AdditionalTools.PositionSeed(thisTransform, 1.1f) * 15, Class_AdditionalTools.PositionSeed(thisTransform, 2.2f) * 15, Class_AdditionalTools.PositionSeed(thisTransform, 3.3f) * 15);

        float systemSize = 9 - (1 + (scaleMoon * 2.25f));
        int systemDensity = 5;

        Vector3 center = Vector3.zero;

        for (int x = -systemDensity; x < systemDensity; x++)
        {
            for (int z = -systemDensity; z < systemDensity; z++)
            {
                yield return delay0;

                Vector3 position = new Vector3(x, 0, z);
                float distance = Vector3.Distance(position, center);

                if (distance < systemDensity && distance > systemDensity * 0.6f)
                {
                    float xt = (systemSize * 0.5f - 0.5f) + (x * systemSize);
                    float zt = (systemSize * 0.5f - 0.5f) + (z * systemSize);
                    float cosQtObj = Mathf.Cos((11.1f + xt + z) * (10.3f + x + zt) * Class_StarSystem.seed * 0.033f);
                    float lperlinf = Mathf.PerlinNoise(cosQtObj, cosQtObj);

                    if (lperlinf > 0.1f && lperlinf < 0.75f)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject obj = ChunkCubesPool.GiveCube();
                            obj.GetComponent<ChunkCube>().uvOffset = new Vector2(uvOffsetX, 0.5f);
                            Transform tr = obj.transform;
                            tr.rotation = Random.rotation;
                            tr.localScale = Vector3.one * (1 + (lperlinf * scaleMoon * 8));
                            tr.parent = satellitesOrbitTransform;
                            float scaleMoonM = scaleMoon * 10;
                            float yt = Mathf.Cos(lperlinf * (lperlinf + xt + zt)) * scaleMoonM;
                            tr.localPosition = new Vector3(xt + (Mathf.Cos(lperlinf * zt) * scaleMoonM), yt, zt + (Mathf.Cos(lperlinf * xt) * scaleMoonM));
                            obj.SetActive(true);
                            yield return null;
                        }
                    }
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
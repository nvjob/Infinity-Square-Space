// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Asteroid
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(1.5f), delay1 = new WaitForSeconds(0.1f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenAsteroid(Transform thisTransform, Transform asteroidCubesTransform, float distanceLod, GameObject asteroidCubes, GameObject lod, int amountAsteroids)
    {
        //--------------

        for (int i = 0; i < amountAsteroids; i++)
        {
            yield return null;

            GameObject cubeOutOfPool = ChunkCubesPool.GiveCube();

            if (cubeOutOfPool != null)
            {
                cubeOutOfPool.layer = 30;
                Transform cubeOutOfPoolTransform = cubeOutOfPool.transform;
                cubeOutOfPoolTransform.parent = asteroidCubesTransform;
                float uvOffsetX = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, amountAsteroids + i + 1.27f));
                cubeOutOfPool.GetComponent<ChunkCube>().uvOffset = new Vector2(uvOffsetX, 0.025f);
                float cubeOutOfPoolScaleXZ = 0.15f + Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, uvOffsetX + i + 4.13f) * 0.7f);
                float cubeOutOfPoolY = 0.15f + Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, cubeOutOfPoolScaleXZ + uvOffsetX + i + 1.9f) * 0.7f);
                cubeOutOfPoolTransform.localScale = new Vector3(cubeOutOfPoolScaleXZ, cubeOutOfPoolY, cubeOutOfPoolScaleXZ);
                float xt = Class_AdditionalTools.PositionSeed(thisTransform, i + cubeOutOfPoolScaleXZ * 1.241f) * (0.6f - (0.6f * cubeOutOfPoolScaleXZ));
                float yt = Class_AdditionalTools.PositionSeed(thisTransform, i + cubeOutOfPoolY * 8.83f) * (0.6f - (0.6f * cubeOutOfPoolY));
                float zt = Class_AdditionalTools.PositionSeed(thisTransform, i + cubeOutOfPoolScaleXZ * 3.31f) * (0.6f - (0.6f * cubeOutOfPoolScaleXZ));
                cubeOutOfPoolTransform.localPosition = new Vector3(xt, yt, zt);
                float xr = Class_AdditionalTools.PositionSeed(thisTransform, xt + cubeOutOfPoolScaleXZ * 7.13f) * 20;
                float yr = Class_AdditionalTools.PositionSeed(thisTransform, yt + cubeOutOfPoolY * 6.93f) * 20;
                float zr = Class_AdditionalTools.PositionSeed(thisTransform, zt + cubeOutOfPoolScaleXZ * 1.13f) * 20;
                cubeOutOfPoolTransform.localRotation = Quaternion.Euler(xr, yr, zr);
                cubeOutOfPool.SetActive(true);
            }
        }

        //--------------

        Class_AdditionalTools.UvMesh(lod.GetComponent<MeshFilter>().mesh, 0.4f, 0.025f);

        while (true)
        {
            yield return delay0;

            if (asteroidCubes.transform.childCount > 0)
            {
                if (Class_Controller.SqrMagnitudeToPlayer(thisTransform) < distanceLod)
                {
                    asteroidCubes.SetActive(true);
                    lod.SetActive(false);
                }
                else
                {
                    asteroidCubes.SetActive(false);
                    lod.SetActive(true);
                }
            }
            else thisTransform.gameObject.SetActive(false);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenAsteroidsGroup(Transform thisTransform, GameObject asteroid, int asteroidGroupRadius, float asteroidSize)
    {
        //--------------

        yield return delay1;

        float seed = Mathf.Pow(Mathf.Cos(Class_StarSystem.seed) * 10, 2);
        thisTransform.rotation = new Quaternion(seed * 10, seed * 10, seed * -10, 0);

        Vector3 center = Vector3.zero;

        for (int x = -asteroidGroupRadius; x < asteroidGroupRadius; x++)
        {
            for (int z = -asteroidGroupRadius; z < asteroidGroupRadius; z++)
            {
                yield return delay1;

                Vector3 position = new Vector3(x, 0, z);
                float distance = Mathf.Sqrt((position - center).sqrMagnitude);

                if (distance < asteroidGroupRadius && distance > asteroidGroupRadius * 0.15f)
                {
                    float xt = (asteroidSize * 0.5f - 0.5f) + (x * asteroidSize);
                    float zt = (asteroidSize * 0.5f - 0.5f) + (z * asteroidSize);
                    float mathfCos = Mathf.Cos((11.1f + xt + z + thisTransform.position.x + thisTransform.position.z) * (10.3f + x + zt + thisTransform.position.y) * Class_StarSystem.seed * 0.033f);
                    float perlinNoise = Mathf.PerlinNoise(mathfCos, mathfCos);
                    if (perlinNoise > 0.25f && perlinNoise < 0.4f)
                    {
                        GameObject obj = GameObject.Instantiate(asteroid);
                        Transform tr = obj.transform;
                        float yt = (Mathf.Pow(perlinNoise * 10, 5.3f) * 0.1f) - 60;
                        tr.parent = thisTransform;
                        tr.localPosition = new Vector3(xt, yt, zt);
                        tr.localScale = Vector3.one * Mathf.Pow(perlinNoise * 10, 4) * 0.12f;
                    }
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
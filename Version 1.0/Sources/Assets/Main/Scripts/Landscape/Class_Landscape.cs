// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Landscape
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(0.1f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenChunkLandscape(Transform thisTransform)
    {
        //--------------

        Vector2 uvOffset = new Vector2(Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.41f)), 0.5f);
        int numberCubeOutOfPool = Mathf.FloorToInt(Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 7.18f)) * 3) + 2;

        float cycleOffset = 1;

        //--------------

        for (int i = 0; i < numberCubeOutOfPool; i++)
        {
            yield return delay0;

            GameObject cubeOutOfPool = ChunkCubesPool.GiveCube();
            Transform cubeTransform = cubeOutOfPool.transform;
            cubeTransform.parent = thisTransform;

            cycleOffset += i * 1.33f;

            float cubeScale = Mathf.Abs(Mathf.Cos(numberCubeOutOfPool * cycleOffset)) * 0.7f + 0.05f;
            float xs = 1 - (Mathf.Cos(cubeScale * cycleOffset) * 0.2f);
            float ys = 1 - (Mathf.Cos(cubeScale * cycleOffset * 1.35f) * 0.2f);
            float zs = 1 - (Mathf.Cos(cubeScale * cycleOffset * 0.76f) * 0.2f);
            cubeTransform.localScale = new Vector3(cubeScale * xs, cubeScale * ys, cubeScale * zs);

            float localVar0 = Mathf.Cos(cycleOffset * cubeScale) * 0.5f;
            float xt = localVar0 - (localVar0 * cubeScale);
            float localVar1 = Mathf.Cos((1.78f + cycleOffset * 1.14f) * cubeScale) * 0.5f;
            float yt = localVar1 - (localVar1 * cubeScale);
            float localVar2 = Mathf.Cos((5.78f + cycleOffset * 1.64f) * cubeScale) * 0.5f;
            float zt = localVar2 - (localVar2 * cubeScale);

            cubeTransform.localPosition = new Vector3(xt, yt, zt);
            float rt = Mathf.Cos(cubeScale * numberCubeOutOfPool * cycleOffset) * 15;
            cubeTransform.localRotation = Quaternion.Euler(rt, -rt, rt);
            cubeOutOfPool.GetComponent<ChunkCube>().uvOffset = uvOffset;
            cubeOutOfPool.SetActive(true);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenLandscape(Transform thisTransform, GameObject chunkLandscape, Vector2 uvOffset)
    {
        //--------------

        float seedGeneration = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.22f));
        int numberCubeOutOfPool = Mathf.FloorToInt(Mathf.Abs(Mathf.Cos(seedGeneration * Class_StarSystem.seed)) * 11) + 9;

        int cubeOutOfPoolSeedPosition = 1;
        float cycleOffset = 1;
        GameObject cubeOutOfPool;

        //--------------

        for (int i = 0; i < numberCubeOutOfPool; i++)
        {
            yield return delay0;

            if (seedGeneration > 0.5f)
            {
                cubeOutOfPool = ChunkCubesPool.GiveCube();
                cubeOutOfPool.GetComponent<ChunkCube>().uvOffset = uvOffset;
            }
            else cubeOutOfPool = GameObject.Instantiate(chunkLandscape);

            Transform cubeTransform = cubeOutOfPool.transform;
            cubeTransform.parent = thisTransform;

            cycleOffset += i * 1.33f;

            float cubeScale = Mathf.Abs(Mathf.Cos(numberCubeOutOfPool * cycleOffset * seedGeneration)) * 0.25f + 0.05f;
            float xs = 1 - (Mathf.Cos((cubeScale + seedGeneration) * cycleOffset) * 0.1f);
            float ys = 1 - (Mathf.Cos((cubeScale + seedGeneration) * cycleOffset * 1.35f) * 0.1f);
            float zs = 1 - (Mathf.Cos((cubeScale + seedGeneration) * cycleOffset * 0.76f) * 0.1f);
            cubeTransform.localScale = new Vector3(cubeScale * xs, cubeScale * ys, cubeScale * zs);

            cubeOutOfPoolSeedPosition = Mathf.FloorToInt(Mathf.Abs(Mathf.Cos((1.3f + seedGeneration) * cycleOffset + cubeOutOfPoolSeedPosition + cubeScale)) * 6) + 1;

            float localVar0 = Mathf.Cos((cubeOutOfPoolSeedPosition + cycleOffset) * (cubeScale + seedGeneration)) * 0.46f;
            float localVar1 = localVar0 - (localVar0 * cubeScale);
            float localVar2 = Mathf.Cos((cubeOutOfPoolSeedPosition * 1.78f + cycleOffset * 1.14f) * (cubeScale + seedGeneration)) * 0.46f;
            float localVar3 = localVar2 - (localVar2 * cubeScale);

            if (cubeOutOfPoolSeedPosition == 1) cubeTransform.localPosition = new Vector3(localVar1, 0.5f, localVar3);
            if (cubeOutOfPoolSeedPosition == 2) cubeTransform.localPosition = new Vector3(localVar1, -0.5f, localVar3);
            if (cubeOutOfPoolSeedPosition == 3) cubeTransform.localPosition = new Vector3(0.5f, localVar1, localVar3);
            if (cubeOutOfPoolSeedPosition == 4) cubeTransform.localPosition = new Vector3(-0.5f, localVar1, localVar3);
            if (cubeOutOfPoolSeedPosition == 5) cubeTransform.localPosition = new Vector3(localVar3, localVar1, 0.5f);
            if (cubeOutOfPoolSeedPosition == 6) cubeTransform.localPosition = new Vector3(localVar3, localVar1, -0.5f);

            float rt = Mathf.Cos((cubeScale + seedGeneration) * numberCubeOutOfPool * cycleOffset) * 20;
            cubeTransform.localRotation = Quaternion.Euler(rt, -rt, rt);

            if (cubeOutOfPool.activeSelf == false) cubeOutOfPool.SetActive(true);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
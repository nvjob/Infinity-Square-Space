// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Forest
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(1);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator Generation(Transform thisTransform, Material treesMaterial)
    {
        //--------------

        yield return delay0;

        float seedGen = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.9f));

        Material localTreeMaterial = new Material(treesMaterial);
        localTreeMaterial.SetTextureOffset("_MainTex", new Vector2(seedGen, 0));

        Vector3 treeScale = Vector3.one / thisTransform.lossyScale.x;
        float scaleProportion = 1 / thisTransform.lossyScale.x;

        int numberTreeOutOfPool = Mathf.FloorToInt(seedGen * 30) + 50;
        int treePackType = Mathf.FloorToInt(seedGen * 8) + 1;

        int treeOutOfPoolSeedPosition = 1;
        float cycleOffset = 1;
        GameObject treeObject = null;

        //--------------

        for (int i = 0; i < numberTreeOutOfPool; i++)
        {
            yield return null;

            cycleOffset += i * 1.37f;

            float localVar0 = Mathf.Cos((numberTreeOutOfPool + cycleOffset) * (seedGen * 4.7f));
            int treeType = Mathf.RoundToInt(Mathf.PerlinNoise(localVar0, localVar0) * 5.7f);

            if (treePackType == 1)
            {
                if (treeType <= 2) treeObject = TreesPool.GiveTree(6);
                else treeObject = TreesPool.GiveTree(5);
            }
            else if (treePackType == 2)
            {
                if (treeType == 1) treeObject = TreesPool.GiveTree(0);
                else if (treeType == 2) treeObject = TreesPool.GiveTree(6);
                else if (treeType == 3) treeObject = TreesPool.GiveTree(4);
            }
            else if (treePackType == 3)
            {
                if (treeType == 1) treeObject = TreesPool.GiveTree(1);
                else if (treeType == 2) treeObject = TreesPool.GiveTree(5);
                else if (treeType == 3) treeObject = TreesPool.GiveTree(7);
            }
            else if (treePackType == 4)
            {
                if (treeType == 1) treeObject = TreesPool.GiveTree(0);
                else if (treeType == 2) treeObject = TreesPool.GiveTree(5);
                else if (treeType == 3) treeObject = TreesPool.GiveTree(7);
            }
            else if (treePackType == 5)
            {
                if (treeType <= 2) treeObject = TreesPool.GiveTree(3);
                else treeObject = TreesPool.GiveTree(2);
            }
            else if (treePackType == 6)
            {
                if (treeType == 1) treeObject = TreesPool.GiveTree(0);
                else if (treeType == 2) treeObject = TreesPool.GiveTree(6);
                else if (treeType == 3) treeObject = TreesPool.GiveTree(5);
            }
            else if (treePackType == 7)
            {
                if (treeType == 1) treeObject = TreesPool.GiveTree(7);
                else if (treeType == 2) treeObject = TreesPool.GiveTree(6);
                else if (treeType == 3) treeObject = TreesPool.GiveTree(4);
            }
            else if (treePackType == 8)
            {
                if (treeType == 1) treeObject = TreesPool.GiveTree(1);
                else if (treeType == 2) treeObject = TreesPool.GiveTree(3);
                else if (treeType == 3) treeObject = TreesPool.GiveTree(2);
            }

            if (treeObject != null)
            {
                Transform treeTransform = treeObject.transform;
                treeTransform.parent = thisTransform;
                float treeOutOfPoolScale = 0.5f + (Mathf.Abs(Mathf.Cos(seedGen * cycleOffset)) * 1.25f);
                treeTransform.localScale = treeScale * treeOutOfPoolScale;
                treeOutOfPoolSeedPosition = Mathf.FloorToInt(Mathf.Abs(Mathf.Cos(((13.61f * treeOutOfPoolScale) + seedGen) * cycleOffset + treeOutOfPoolSeedPosition)) * 6) + 1;

                float localVar1 = Mathf.Cos((treeOutOfPoolSeedPosition + cycleOffset + Class_StarSystem.seed) * (treeOutOfPoolScale + seedGen)) * 0.425f;
                float localVar2 = localVar1 - (localVar1 * scaleProportion);
                float localVar3 = Mathf.Cos((treeOutOfPoolSeedPosition + cycleOffset + Class_StarSystem.seed) * (treeOutOfPoolScale + seedGen + 0.561f)) * 0.425f;
                float localVar4 = localVar3 - (localVar3 * scaleProportion);

                float treeOutOfPoolRotation = Mathf.Cos(cycleOffset * seedGen) * 125;

                if (treeOutOfPoolSeedPosition == 1)
                {
                    treeTransform.localPosition = new Vector3(localVar2, 0.5f, localVar4);
                    treeTransform.eulerAngles = new Vector3(0, treeOutOfPoolRotation, 0);
                }

                if (treeOutOfPoolSeedPosition == 2)
                {
                    treeTransform.localPosition = new Vector3(localVar2, -0.5f, localVar4);
                    treeTransform.eulerAngles = new Vector3(0, treeOutOfPoolRotation, 180);
                }

                if (treeOutOfPoolSeedPosition == 3)
                {
                    treeTransform.localPosition = new Vector3(0.5f, localVar2, localVar4);
                    treeTransform.eulerAngles = new Vector3(treeOutOfPoolRotation, 0, -90);
                }

                if (treeOutOfPoolSeedPosition == 4)
                {
                    treeTransform.localPosition = new Vector3(-0.5f, localVar2, localVar4);
                    treeTransform.eulerAngles = new Vector3(treeOutOfPoolRotation, 0, 90);
                }

                if (treeOutOfPoolSeedPosition == 5)
                {
                    treeTransform.localPosition = new Vector3(localVar4, localVar2, 0.5f);
                    treeTransform.eulerAngles = new Vector3(90, treeOutOfPoolRotation, treeOutOfPoolRotation);
                }

                if (treeOutOfPoolSeedPosition == 6)
                {
                    treeTransform.localPosition = new Vector3(localVar4, localVar2, -0.5f);
                    treeTransform.eulerAngles = new Vector3(-90, treeOutOfPoolRotation, -treeOutOfPoolRotation);
                }

                foreach (Transform child in treeTransform) child.GetComponent<Renderer>().material = localTreeMaterial;

                treeObject.SetActive(true);
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void ParticlesDestroyed(Transform thisTransform)
    {
        //--------------

        GameObject obj = ExplosionParticlesPool.GiveExplosionParticle(3);

        if (obj != null)
        {
            Transform tr = obj.transform;
            tr.SetPositionAndRotation(thisTransform.position + (thisTransform.up * 3), Random.rotation);
            tr.localScale = Vector3.one + (thisTransform.lossyScale * Random.Range(2.7f, 3.3f));
            obj.SetActive(true);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
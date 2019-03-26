// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_EnemyAI
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(0.5f), delay1 = new WaitForSeconds(1.5f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator Customization(GameObject gameObject, MeshFilter bodyLod0, MeshFilter eyeLod0, MeshFilter bodyLod1, MeshFilter eyeLod1, GameObject particlesDead)
    {
        //--------------

        EnemyAI enemyAI = gameObject.GetComponent<EnemyAI>();
        bool aggression = false, taming = false, trigger0 = false, trigger1 = false, trigger2 = false;

        //--------------

        while (enemyAI.dead == false)
        {
            yield return delay0;

            if (Class_Interface.tamingEnabled == true)
            {
                taming = true;
                aggression = false;
            }
            else
            {
                taming = false;
                if (enemyAI.aiAgr == true) aggression = true;
                else aggression = false;
            }

            //---

            if (taming == false && aggression == false)
            {
                if (trigger0 == false)
                {
                    trigger0 = true;
                    trigger1 = false;
                    trigger2 = false;
                    Class_AdditionalTools.UvMesh(eyeLod0.mesh, 0.15f, 0.25f);
                    Class_AdditionalTools.UvMesh(eyeLod1.mesh, 0.15f, 0.25f);
                }
            }
            else if (taming == true && aggression == false)
            {
                if (trigger1 == false)
                {
                    trigger0 = false;
                    trigger1 = true;
                    trigger2 = false;
                    Class_AdditionalTools.UvMesh(eyeLod0.mesh, 0.5f, 0.25f);
                    Class_AdditionalTools.UvMesh(eyeLod1.mesh, 0.5f, 0.25f);
                }
            }
            else if (taming == false && aggression == true)
            {
                if (trigger2 == false)
                {
                    trigger0 = false;
                    trigger1 = false;
                    trigger2 = true;
                    Class_AdditionalTools.UvMesh(eyeLod0.mesh, 0.85f, 0.25f);
                    Class_AdditionalTools.UvMesh(eyeLod1.mesh, 0.85f, 0.25f);
                }
            }
        }

        //--------------

        if (enemyAI.dead == true)
        {
            Class_AdditionalTools.UvMesh(bodyLod0.mesh, 0.15f, 0.04f);
            Class_AdditionalTools.UvMesh(bodyLod1.mesh, 0.15f, 0.04f);
            Class_AdditionalTools.UvMesh(eyeLod0.mesh, 0.85f, 0.04f);
            Class_AdditionalTools.UvMesh(eyeLod1.mesh, 0.85f, 0.04f);
            particlesDead.SetActive(true);
            yield return delay1;
            particlesDead.SetActive(false);
        }

        //--------------
    }
         


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
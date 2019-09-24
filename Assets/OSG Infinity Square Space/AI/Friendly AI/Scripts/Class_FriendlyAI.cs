// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_FriendlyAI
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(0.5f), delay1 = new WaitForSeconds(1.5f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator Customization(GameObject gameObject, MeshFilter bodyLod0, MeshFilter eyeLod0, MeshFilter bodyLod1, MeshFilter eyeLod1, GameObject particlesDead, float eyeLod0UvX)
    {
        //--------------

        FriendlyAI friendlyAI = gameObject.GetComponent<FriendlyAI>();
        bool trigger0 = false, trigger1 = false;

        //--------------

        while (friendlyAI.dead == false)
        {
            yield return delay0;

            if (friendlyAI.aiAgr == true)
            {
                if (trigger0 == false)
                {
                    trigger0 = true;
                    trigger1 = false;
                    Class_AdditionalTools.UvMesh(eyeLod0.mesh, 0.85f, 0.25f);
                    Class_AdditionalTools.UvMesh(eyeLod1.mesh, 0.85f, 0.25f);
                }
            }
            else
            {
                if (trigger1 == false)
                {
                    trigger0 = false;
                    trigger1 = true;
                    Class_AdditionalTools.UvMesh(eyeLod0.mesh, eyeLod0UvX, 0.4f);
                    Class_AdditionalTools.UvMesh(eyeLod1.mesh, eyeLod0UvX, 0.4f);
                }
            }
        }

        //--------------

        if (friendlyAI.dead == true)
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
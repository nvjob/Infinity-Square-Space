// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Mineral
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(1);
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator FakeUpdate(Transform thisTransform, Rigidbody thisRigidbody, GameObject gameObject, LineRenderer thisLineRenderer)
    {
        //--------------

        int counter0 = 0;
        thisRigidbody.isKinematic = false;
        thisRigidbody.detectCollisions = true;
        thisLineRenderer.enabled = false;

        //--------------

        while (counter0++ < 60)
        {
            yield return delay0;

            if (thisLineRenderer.enabled == true) thisLineRenderer.enabled = false;

            float sqrMagnitudeToPlayer = Class_Controller.SqrMagnitudeToPlayer(thisTransform);
            if (sqrMagnitudeToPlayer < 10000)
            {
                if (counter0 != 0) counter0 = 0;
                if (thisRigidbody.isKinematic == true) thisRigidbody.isKinematic = false;
                if (thisRigidbody.detectCollisions == false) thisRigidbody.detectCollisions = true;
                if (Class_Controller.mineralsCollector == true && sqrMagnitudeToPlayer < 1600)
                {
                    thisRigidbody.AddForce((Class_Controller.playerPosition - thisTransform.position).normalized * 800, ForceMode.Acceleration);
                    if (thisLineRenderer.enabled == false) thisLineRenderer.enabled = true;
                    thisLineRenderer.SetPosition(0, thisTransform.position);
                    thisLineRenderer.SetPosition(1, Class_Controller.playerPosition);
                }
            }
            else if (sqrMagnitudeToPlayer >= 10000 && sqrMagnitudeToPlayer > 250000)
            {
                if (thisRigidbody.isKinematic == false) thisRigidbody.isKinematic = true;
                if (thisRigidbody.detectCollisions == true) thisRigidbody.detectCollisions = false;
            }
            else if (sqrMagnitudeToPlayer >= 250000) MineralsPool.TakeMineral(gameObject);
        }

        //--------------

        MineralsPool.TakeMineral(gameObject);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void RandomMinerals(Transform thisTransform)
    {
        //--------------

        SelectMineral(thisTransform, 0.04f, 0); // life
        SelectMineral(thisTransform, 0.1f, 1); // power
        SelectMineral(thisTransform, 0.04f, 2); // speed
        SelectMineral(thisTransform, 0.045f, 3); // lighting
        SelectMineral(thisTransform, 0.15f, 4); // laser
        SelectMineral(thisTransform, 0.01f, 5); // pulse
        SelectMineral(thisTransform, 0.005f, 6); // masker
        SelectMineral(thisTransform, 0.08f, 7); // antigravity
        SelectMineral(thisTransform, 0.005f, 8); // taming
        SelectMineral(thisTransform, 0.0025f, 9); // eraser
        SelectMineral(thisTransform, 0.0025f, 10); // collapser
        SelectMineral(thisTransform, 0.025f, 11); // destroyer

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private static void SelectMineral(Transform thisTransform, float chance, int mineralType)
    {
        //--------------

        Random.InitState(System.DateTime.Now.Millisecond);

        float random = Random.value;

        if (random > 0 && random <= chance)
        {
            GameObject mineral = MineralsPool.GiveMineral(mineralType);

            if (mineral != null)
            {
                mineral.transform.position = thisTransform.position + Random.insideUnitSphere;
                mineral.SetActive(true);
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
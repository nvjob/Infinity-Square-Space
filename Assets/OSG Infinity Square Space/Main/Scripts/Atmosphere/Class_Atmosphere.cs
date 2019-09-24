// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Atmosphere
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(0.1f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenAtmosphere(Transform thisTransform, GameObject clouds, int amountClouds, Vector2 uvOffset)
    {
        //--------------        

        for (int i = 0; i < amountClouds; i++)
        {
            yield return null;

            GameObject cloudsObject = GameObject.Instantiate(clouds);
            cloudsObject.GetComponent<Cloud>().uvOffset = uvOffset;
            Transform cloudsTransform = cloudsObject.transform;
            cloudsTransform.parent = thisTransform;
            float cloudScale = (10 + Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, (amountClouds + i) * 1.73f) * 11)) * 0.01f;
            cloudsTransform.localScale = Vector3.one * cloudScale;
            int sideSelection = Mathf.RoundToInt(1 + Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, (amountClouds + i) * cloudScale * 1.73f)) * 5);
            float lVar0 = Class_AdditionalTools.PositionSeed(thisTransform, sideSelection + i + cloudScale * 3.141f) * (48 - (50 * cloudScale));
            float lVar1 = Class_AdditionalTools.PositionSeed(thisTransform, sideSelection + i + cloudScale * 7.23f) * (48 - (50 * cloudScale));
            if (sideSelection == 1) cloudsTransform.localPosition = new Vector3(lVar0 * 0.01f, 0.44f, lVar1 * 0.01f);
            if (sideSelection == 2) cloudsTransform.localPosition = new Vector3(lVar0 * 0.01f, -0.44f, lVar1 * 0.01f);
            if (sideSelection == 3) cloudsTransform.localPosition = new Vector3(0.44f, lVar0 * 0.01f, lVar1 * 0.01f);
            if (sideSelection == 4) cloudsTransform.localPosition = new Vector3(-0.44f, lVar0 * 0.01f, lVar1 * 0.01f);
            if (sideSelection == 5) cloudsTransform.localPosition = new Vector3(lVar1 * 0.01f, lVar0 * 0.01f, 0.44f);
            if (sideSelection == 6) cloudsTransform.localPosition = new Vector3(lVar1 * 0.01f, lVar0 * 0.01f, -0.44f);
            float xr = Class_AdditionalTools.PositionSeed(thisTransform, lVar0 + cloudScale * 7.13f) * 5;
            float yr = Class_AdditionalTools.PositionSeed(thisTransform, lVar0 + lVar1 + cloudScale * 6.93f) * 5;
            float zr = Class_AdditionalTools.PositionSeed(thisTransform, lVar1 + cloudScale * 1.13f) * 5;
            cloudsTransform.localRotation = Quaternion.Euler(xr, yr, zr);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator GenCloud(Transform thisTransform, GameObject cloud, Vector3 uvOffset, int amountClouds)
    {
        //--------------

        for (int i = 0; i < amountClouds; i++)
        {
            yield return delay0;

            GameObject cloudObject = GameObject.Instantiate(cloud);
            cloudObject.SetActive(false);
            Class_AdditionalTools.UvMesh(cloudObject.GetComponent<MeshFilter>().mesh, uvOffset.x, uvOffset.y);
            Transform cloudTransform = cloudObject.transform;
            cloudTransform.parent = thisTransform;
            float cloudScale = 0.1f + Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, (amountClouds + i) * 0.41f) / 4);
            cloudTransform.localScale = Vector3.one * cloudScale;
            float xt = Class_AdditionalTools.PositionSeed(thisTransform, i + cloudScale * 3.141f) * (0.35f - (0.35f * cloudScale));
            float yt = Class_AdditionalTools.PositionSeed(thisTransform, i + cloudScale * 7.23f) * (0.35f - (0.35f * cloudScale));
            float zt = Class_AdditionalTools.PositionSeed(thisTransform, i + cloudScale * 6.31f) * (0.35f - (0.35f * cloudScale));
            cloudTransform.localPosition = new Vector3(xt, yt, zt);
            float xr = Class_AdditionalTools.PositionSeed(thisTransform, xt + cloudScale * 7.13f) * 5;
            float yr = Class_AdditionalTools.PositionSeed(thisTransform, yt + cloudScale * 6.93f) * 5;
            float zr = Class_AdditionalTools.PositionSeed(thisTransform, zt + cloudScale * 1.13f) * 5;
            cloudTransform.localRotation = Quaternion.Euler(xr, yr, zr);
            cloudObject.SetActive(true);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
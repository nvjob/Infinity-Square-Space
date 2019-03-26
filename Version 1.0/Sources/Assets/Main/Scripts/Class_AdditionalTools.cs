// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using System.Collections.Generic;



public static class Class_AdditionalTools
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    public static float PositionSeed(Transform thisTransform, float factor)
    {
        //--------------

        Vector3 position = thisTransform.position;
        float positionSeed = Mathf.Cos((position.x + position.y + position.z + Class_StarSystem.seed) * factor);
        return positionSeed;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void RandomName(GameObject obj, string name)
    {
        //--------------

        obj.name = name + " " + Random.Range(1, 99999999).ToString();

        //--------------
    }
       


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void UvMesh(Mesh mesh, float uvOffsetX, float uvOffsetY)
    {
        //--------------

        List<Vector2> uvGen = new List<Vector2>();
        for (int u = 0; u < mesh.vertexCount; u++) uvGen.Add(new Vector2(uvOffsetX, uvOffsetY));
        mesh.SetUVs(0, uvGen);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

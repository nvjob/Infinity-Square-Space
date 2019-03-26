// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class Atmosphere : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public GameObject clouds;
    public bool cloudsOn = false;
       
    //--------------
    
    Transform thisTransform;    
    Vector2 uvOffset;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        Class_AdditionalTools.RandomName(gameObject, "Atmosphere");
        
        uvOffset = new Vector2(Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.141f)), 0.5f);
        Class_AdditionalTools.UvMesh(GetComponent<MeshFilter>().mesh, uvOffset.x, uvOffset.y);
        int amountClouds = 10 + Mathf.RoundToInt(Mathf.Abs(Class_AdditionalTools.PositionSeed(transform, 3.141f)) * 25);

        if (cloudsOn == true) StartCoroutine(Class_Atmosphere.GenAtmosphere(thisTransform, clouds, amountClouds, uvOffset));

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
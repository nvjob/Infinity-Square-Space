// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class EnemyAICustomization : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public MeshFilter bodyLod0;
    public MeshFilter eyeLod0;
    public MeshFilter bodyLod1;
    public MeshFilter eyeLod1;
    public GameObject particlesDead;

    //--------------

    Transform thisTransform;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        

    void Awake()
    {
        //--------------

        thisTransform = transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start ()
    {
        //--------------

        if (Class_Interface.shadows == true)
        {
            bodyLod0.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            bodyLod0.GetComponent<Renderer>().receiveShadows = true;
        }
        else
        {
            bodyLod0.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            bodyLod0.GetComponent<Renderer>().receiveShadows = false;
        }

        //--------------

        float uvB = Mathf.Abs(Class_AdditionalTools.PositionSeed(thisTransform, 0.171f * Class_AI.amountEnemyAIs));
        Class_AdditionalTools.UvMesh(bodyLod0.mesh, uvB, 0.7f);
        Class_AdditionalTools.UvMesh(bodyLod1.mesh, uvB, 0.7f);
        Class_AdditionalTools.UvMesh(eyeLod0.mesh, 0.15f, 0.25f);
        Class_AdditionalTools.UvMesh(eyeLod1.mesh, 0.15f, 0.25f);

        StartCoroutine(Class_EnemyAI.Customization(gameObject, bodyLod0, eyeLod0, bodyLod1, eyeLod1, particlesDead));

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

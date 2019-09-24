// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class Sun : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public bool on = true;
    public GameObject sunBody;
    public ParticleSystem sunCrown;
    public ParticleSystem sunPlasma;

    //--------------

    Transform thisTransform;
    float rotateSeedG;



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

        if (on == true)
        {
            rotateSeedG = Class_StarSystem.SeedAbsCos() * 0.02f;

            Class_AdditionalTools.UvMesh(sunBody.GetComponent<MeshFilter>().mesh, Class_StarSystem.SeedAbsCos(), 0.5f);
            thisTransform.localScale = Vector3.one * (Class_StarSystem.SeedAbsCos() + 0.2f);
            Color sunParticleColor = Color.HSVToRGB(Class_StarSystem.SeedAbsCos(), 1, 0.1f);
            ParticleSystem.MainModule sunCrownMain = sunCrown.main;
            sunCrownMain.startColor = new ParticleSystem.MinMaxGradient(new Color(sunParticleColor.r, sunParticleColor.g, sunParticleColor.b, 0.95f));
            ParticleSystem.MainModule sunPlasmaMain = sunPlasma.main;
            sunPlasmaMain.startColor = new ParticleSystem.MinMaxGradient(new Color(sunParticleColor.r * 9, sunParticleColor.g * 9, sunParticleColor.b * 9, 0.7f));
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void FixedUpdate()
    {
        //--------------

        if (on == true) thisTransform.Rotate(Vector3.up * rotateSeedG);

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

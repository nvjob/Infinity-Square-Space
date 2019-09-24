// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class Illumination : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public Light light0;
    public Light light1;
    public Light light3;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        float[] shadowCullDistances = new float[32];
        shadowCullDistances[28] = 200 * Class_Interface.LODValue;
        shadowCullDistances[31] = 150 * Class_Interface.LODValue;

        light0.layerShadowCullDistances = light1.layerShadowCullDistances = shadowCullDistances;

        //--------------

        light0.color = light1.color = Color.HSVToRGB(Class_StarSystem.SeedAbsCos(), 0.3f, 1.0f);
        RenderSettings.ambientLight = Color.HSVToRGB(Class_StarSystem.SeedAbsCos() * 0.93f, 0.1f, 1.0f);

        //--------------

        light3.color = Color.HSVToRGB(Class_StarSystem.SeedAbsCos(), 0.2f, 0.9f);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
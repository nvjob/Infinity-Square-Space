// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class WeaponCollapsar : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public GameObject light0, light1, light2, light3;       



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        if (light0 == true)
        {
            if (light0.activeSelf != Class_Interface.dynamicLighting) light0.SetActive(Class_Interface.dynamicLighting);
        }

        //--------------

        if (light1 == true)
        {
            if (light1.activeSelf != Class_Interface.dynamicLighting) light1.SetActive(Class_Interface.dynamicLighting);
        }

        //--------------

        if (light2 == true)
        {
            if (light2.activeSelf != Class_Interface.dynamicLighting) light2.SetActive(Class_Interface.dynamicLighting);
        }

        //--------------

        if (light3 == true)
        {
            if (light3.activeSelf != Class_Interface.dynamicLighting) light3.SetActive(Class_Interface.dynamicLighting);
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

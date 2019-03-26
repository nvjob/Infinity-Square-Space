// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_Interface
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static float LODValue, distanceLod, mouseSensitivityValue;
    public static int lifeCounter, powerCounter, speedCounter, lightningCounter, laserCounter, pulseCounter, maskerCounter, antigravityCounter, tamingCounter, eraserCounter, collapsarCounter, destroyerCounter;
    public static bool powerEnabled, speedEnabled, lightningEnabled, laserEnabled, pulseEnabled, maskerEnabled, antigravityEnabled, tamingEnabled, eraserEnabled, collapsarEnabled, destroyerEnabled, defaultEnabled;
    public static bool globalPausa, loading, initialScreen, dynamicLighting, shadows, menuControlLock;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(2.5f);



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static IEnumerator SubtractionMineralsOverTime()
    {
        //--------------

        while (Class_Controller.playerAlive == true)
        {
            yield return delay0;
            if (speedEnabled == true && speedCounter > 0 && Class_Controller.currentSpeed > 60) speedCounter -= 1;
            if (maskerEnabled == true && maskerCounter > 0) maskerCounter -= 1;
            if (antigravityEnabled == true && antigravityCounter > 0) antigravityCounter -= 1;
            if (tamingEnabled == true && tamingCounter > 0) tamingCounter -= 1;
            yield return delay0;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void HideLockMouse(bool On)
    {
        //--------------

        if (On == true)
        {
            if (Cursor.visible == true) Cursor.visible = false;
            if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if (Cursor.visible == false) Cursor.visible = true;
            if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

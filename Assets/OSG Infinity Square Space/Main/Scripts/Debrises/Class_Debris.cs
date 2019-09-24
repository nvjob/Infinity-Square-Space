// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public static class Class_Debris
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static bool DamageType(Collider collider, bool onTriggerStay, bool brokenStage0)
    {
        //--------------

        if (brokenStage0 == false)
        {
            GameObject obj = collider.gameObject;

            if (onTriggerStay == true) return obj.layer == 9;
            else return obj.layer == 12 || obj.layer == 13 || obj.layer == 15;
        }
        else return false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static float CurrentDamage(Collider collider, bool triggerStay, bool triggerEnter)
    {
        //--------------

        float currentDamage = 0;

        if (triggerStay == false && triggerEnter == false)
        {
            if (collider.CompareTag("Bullet Default")) currentDamage = 0.45f;
            if (collider.CompareTag("Bullet Light")) currentDamage = 0.45f;
            if (collider.CompareTag("Bullet Laser")) currentDamage = 0.55f;
            if (collider.CompareTag("Bullet Destroyer")) currentDamage = 0.9f;
        }
        else if (triggerStay == true && triggerEnter == false)
        {
            if (collider.name == "Pulse Level 0") currentDamage = 0.1f;
            if (collider.name == "Pulse Level 1") currentDamage = 0.1f;
            if (collider.name == "Pulse Level 2") currentDamage = 0.3f;
            if (collider.name == "Pulse Level 3") currentDamage = 0.35f;
            if (collider.name == "High Speed") currentDamage = 1;
        }
        else if (triggerStay == false && triggerEnter == true)
        {
            if (collider.CompareTag("Bullet Eraser") || collider.CompareTag("Bullet Collapsar")) currentDamage = 0.8f;
        }

        return currentDamage;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
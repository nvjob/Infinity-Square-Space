// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class TreeCollider : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(1.5f);
    Transform thisTransform;
    float amountDamage, currentDamage;
    bool visible;



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

        Class_AdditionalTools.RandomName(gameObject, "Tree");

        StartCoroutine(Optimization());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Optimization()
    {
        //--------------        

        int counter0 = 0;

        while (counter0 < 10)
        {
            yield return delay0;
            counter0++;
            if (Physics.CheckSphere(thisTransform.position + (thisTransform.up * 3), 1.5f, 1 << 19 | 1 << 20 | 1 << 21) == true) TreesPool.TakeTree(gameObject);
        }

        //--------------

        while (true)
        {
            yield return delay0;
            if (Physics.CheckSphere(thisTransform.position, 1, 1 << 19 | 1 << 20 | 1 << 21) == false)
            {
                amountDamage = 10;
                Particles();
            }
        }

        //--------------
    }    



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnCollisionEnter(Collision collision)
    {
        //--------------

        if (Class_Debris.DamageType(collision.collider, false, false) == true)
        {
            currentDamage = Class_Debris.CurrentDamage(collision.collider, false, false);
            Particles();
        }

        //-------------- 
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerStay(Collider collider)
    {
        //--------------

        if (Class_Debris.DamageType(collider, true, false) == true)
        {
            currentDamage = Class_Debris.CurrentDamage(collider, true, false);
            Particles();
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerEnter(Collider collider)
    {
        //--------------

        if (Class_Debris.DamageType(collider, false, false) == true)
        {
            currentDamage = Class_Debris.CurrentDamage(collider, false, true);
            Particles();
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Particles()
    {
        //--------------

        amountDamage += currentDamage;

        if (amountDamage >= 1.8f)
        {
            if (visible == true) Class_Forest.ParticlesDestroyed(thisTransform);
            Class_Mineral.RandomMinerals(thisTransform);
            TreesPool.TakeTree(gameObject);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnBecameInvisible() { visible = false; }
    void OnBecameVisible() { visible = true; }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

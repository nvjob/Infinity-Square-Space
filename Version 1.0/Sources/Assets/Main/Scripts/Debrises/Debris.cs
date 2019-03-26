// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class Debris : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static WaitForSeconds delay0 = new WaitForSeconds(2);
    Transform thisTransform;
    Rigidbody thisRigidbody;
    bool brokenStage0, visible;
    float amountDamage, currentDamage, distanceLod;
    int counter0;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        thisRigidbody = GetComponent<Rigidbody>();
        distanceLod = Mathf.Pow(Class_Interface.distanceLod * 0.5f, 2);

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void OnEnable()
    {
        //--------------

        Class_AdditionalTools.RandomName(gameObject, "Debris");        

        thisRigidbody.AddExplosionForce(Random.Range(100, 210), thisTransform.position + Random.onUnitSphere, 1);
        brokenStage0 = false;
        counter0 = 0;

        //--------------

        StartCoroutine(Optimization());

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnCollisionEnter(Collision collision)
    {
        //--------------

        if (Class_Debris.DamageType(collision.collider, false, brokenStage0) == true)
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

        if (Class_Debris.DamageType(collider, true, brokenStage0) == true)
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

        if (Class_Debris.DamageType(collider, false, brokenStage0) == true)
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

        if (amountDamage >= 2 && brokenStage0 == false)
        {
            brokenStage0 = true;

            if (visible == true)
            {
                Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(2), Random.Range(0.75f, 1.25f));
                Class_ChunkCube.ParticlesOutOfPool(thisTransform, ExplosionParticlesPool.GiveExplosionParticle(3), Random.Range(0.75f, 1.25f));
            }

            Class_Mineral.RandomMinerals(thisTransform);
            DebrisesPool.TakeDebris(gameObject);
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Optimization()
    {
        //--------------

        while (true)
        {
            yield return delay0;
 
            if (visible == true && counter0 != 0) counter0 = 0;
            if (counter0++ >= 7) DebrisesPool.TakeDebris(gameObject);
            if (Class_Controller.SqrMagnitudeToPlayer(thisTransform) > distanceLod) DebrisesPool.TakeDebris(gameObject);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnBecameInvisible() { visible = false; }
    void OnBecameVisible() { visible = true; }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
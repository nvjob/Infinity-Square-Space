// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class BulletEraser : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public float bulletLifetime = 4;
    public GameObject lightFx;

    //--------------

    WaitForSeconds delay0;
    Rigidbody thisRigidbody;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisRigidbody = GetComponent<Rigidbody>();
        delay0 = new WaitForSeconds(bulletLifetime);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnEnable()
    {
        //--------------

        if (lightFx == true)
        {
            if (lightFx.activeSelf != Class_Interface.dynamicLighting) lightFx.SetActive(Class_Interface.dynamicLighting);
        }

        //--------------

        thisRigidbody.isKinematic = false;
        thisRigidbody.detectCollisions = true;
        thisRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        StartCoroutine(BulletLifetime());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator BulletLifetime()
    {
        //--------------

        yield return delay0;

        thisRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        thisRigidbody.detectCollisions = false;
        thisRigidbody.isKinematic = true;

        WeaponsPool.TakeWeapons(gameObject);

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

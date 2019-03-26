// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public bool bullet;
    public bool bulletFx;
    public bool bulletFxDestroyOverDistance;
    public bool destroyOverTime;
    public float bulletLifetime;
    public float sparksTime = 1;
    public float bulletRadiusExplosion = 5;
    public float bulletPowerExplosion = 10;
    public GameObject sparks;
    public GameObject lightFx;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.1f);
    Transform thisTransform;
    Rigidbody thisRigidbody;
    GameObject collisionObject;
    Collider bulletCollider;
    Vector3 startPos;
    float playerDistanceWeapon;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;

        //--------------

        if (bullet == true) bulletFx = false;        

        if (bulletFx == true)
        {
            bullet = false;
            thisRigidbody = GetComponent<Rigidbody>();
        }

        //--------------

        if (sparks == true) bulletCollider = GetComponent<Collider>();

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

        if (bulletFx == true) thisRigidbody.isKinematic = false;

        if (bulletFxDestroyOverDistance == true)
        {
            startPos = thisTransform.position;
            playerDistanceWeapon = Mathf.Pow(Class_Controller.playerDistanceWeapon, 2);
            StartCoroutine(Coroutine_0());
        }

        if (sparks == true)
        {
            sparks.SetActive(false);
            bulletCollider.enabled = true;
        }

        if (destroyOverTime == true) Invoke("DestrBool", bulletLifetime);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnDisable()
    {
        //--------------

        if (bulletFxDestroyOverDistance == true) StopCoroutine(Coroutine_0());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    IEnumerator Coroutine_0()
    {
        //--------------

        while (true)
        {
            yield return delay0;

            if (playerDistanceWeapon < (startPos - thisTransform.position).sqrMagnitude) DestrBool();
        }

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void OnCollisionEnter(Collision collision)
    {
        //--------------

        if (bullet == true)
        {
            collisionObject = collision.gameObject;

            if (collisionObject.layer == 8 || collisionObject.layer == 10 || collisionObject.layer == 14 || collisionObject.layer == 28 || collisionObject.layer == 29 || collisionObject.layer == 30 || collisionObject.layer == 31)
            {
                if (collisionObject.layer == 10 || collisionObject.layer == 14 || collisionObject.layer == 28)
                {
                    Rigidbody rbc = collisionObject.GetComponent<Rigidbody>();
                    if (rbc != null) rbc.AddExplosionForce(bulletPowerExplosion, thisTransform.position, bulletRadiusExplosion, 3.0F);
                }

                if (sparks == true)
                {
                    sparks.transform.rotation = Quaternion.LookRotation(-thisTransform.forward, thisTransform.localPosition);
                    sparks.SetActive(true);
                    bulletCollider.enabled = false;
                    Invoke("DestrBool", sparksTime);
                }
                else DestrBool();
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       


    void DestrBool()
    {
        //--------------

        if (bulletFx == true)
        {
            thisRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            thisRigidbody.isKinematic = true;
        }

        if (sparks == true) sparks.SetActive(false);

        WeaponsPool.TakeWeapons(gameObject);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

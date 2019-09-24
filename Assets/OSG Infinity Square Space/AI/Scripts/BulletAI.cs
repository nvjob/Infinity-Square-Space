// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class BulletAI : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    public float bulletLifetime = 4;
    public float explosionRadius = 3;
    public float explosionPower = 20;

    //--------------
    
    Transform thisTransform;
    Rigidbody thisRigidbody;
    
    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        thisRigidbody = GetComponent<Rigidbody>();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnEnable()
    {
        //--------------
                
        thisRigidbody.isKinematic = false;
        thisRigidbody.detectCollisions = true;
        thisRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Class_BulletAI.delay0 = new WaitForSeconds(bulletLifetime);
        StartCoroutine(Class_BulletAI.BulletLifetime(thisRigidbody, gameObject));

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnCollisionEnter(Collision collision)
    {
        //--------------

        Class_BulletAI.Explosion(thisTransform, collision.gameObject, gameObject, thisRigidbody, explosionPower, explosionRadius);

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
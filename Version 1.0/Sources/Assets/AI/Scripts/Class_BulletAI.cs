// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public static class Class_BulletAI
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static WaitForSeconds delay0;



    public static IEnumerator BulletLifetime(Rigidbody thisRigidbody, GameObject gameObject)
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



    public static void Explosion(Transform thisTransform, GameObject collisionObject, GameObject gameObject, Rigidbody thisRigidbody, float explosionPower, float explosionRadius)
    {
        //--------------

        if (collisionObject.layer == 8 || collisionObject.layer == 10 || collisionObject.layer == 14 || collisionObject.layer == 28 || collisionObject.layer == 29 || collisionObject.layer == 30 || collisionObject.layer == 31)
        {
            if (collisionObject.layer == 10 || collisionObject.layer == 14 || collisionObject.layer == 28)
            {
                Rigidbody collisionRigidbody = collisionObject.GetComponent<Rigidbody>();
                if (collisionRigidbody != null) collisionRigidbody.AddExplosionForce(explosionPower, thisTransform.position, explosionRadius, 3.0F);
            }

            thisRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            thisRigidbody.detectCollisions = false;
            thisRigidbody.isKinematic = true;

            WeaponsPool.TakeWeapons(gameObject);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
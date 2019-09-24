// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class WeaponPulse : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public GameObject lightFx;

    //--------------

    Rigidbody colliderRigidbody;
    Transform thisTransform;
    GameObject colliderObject;
    int power;



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

        if (lightFx == true)
        {
            if (lightFx.activeSelf != Class_Interface.dynamicLighting) lightFx.SetActive(Class_Interface.dynamicLighting);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerStay(Collider collider)
    {
        //--------------

        colliderObject = collider.gameObject;

        if (colliderObject.layer == 10 || colliderObject.layer == 28)
        {
            colliderRigidbody = colliderObject.GetComponent<Rigidbody>();
            if (colliderRigidbody != null)
            {
                colliderRigidbody.AddTorque(colliderObject.transform.position + Random.insideUnitSphere * 1000);
                if (colliderObject.layer == 28) power = 200;
                else if (colliderObject.layer == 10) power = 20;
                colliderRigidbody.AddExplosionForce(power, thisTransform.position, 25);
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using System.Collections;



public class Billboard : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public float billboardRotationSpeed = 0.1f;

    //--------------

    Transform thisTransform, cameraTransform;
    bool trigger0;
    Quaternion cameraQuaternion;
    WaitForSeconds delay0;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        cameraTransform = Camera.main.transform;
        thisTransform = transform;
        delay0 = new WaitForSeconds(billboardRotationSpeed);

        //--------------
    }    



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnBecameVisible()
    {
        //--------------

        if (trigger0 == false)
        {
            trigger0 = true;
            StartCoroutine("FakeUpdate");
        }
        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnBecameInvisible()
    {
        //--------------

        if (trigger0 == true)
        {
            trigger0 = false;
            StopCoroutine("FakeUpdate");
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator FakeUpdate()
    {
        //--------------

        while (true)
        {
            if (cameraQuaternion != cameraTransform.rotation)
            {
                thisTransform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
                cameraQuaternion = cameraTransform.rotation;
            }

            yield return delay0;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

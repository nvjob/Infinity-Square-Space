// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuController : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            


    public Transform cameraRig;

    //--------------

    Rigidbody thisRigidbody;
    Transform thisTransform;    
    Vector3 planetPosition;
    bool systemSelection, downLimiter, topLimiter;
    float directionForwardBack, directionLeftRight, verticalVelocity, horizontalVelocity;
    float mouseX, mouseXSmooth, mouseXVelocity, mouseY, mouseYSmooth, mouseYVelocity;
    int counter0, counter1;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        thisRigidbody = GetComponent<Rigidbody>();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void FixedUpdate()
    {
        //--------------

        if (Class_Interface.menuControlLock == false)
        {
            thisRigidbody.velocity = ((thisTransform.forward * directionForwardBack) + (thisTransform.right * directionLeftRight)) * (Mathf.Pow(thisTransform.position.y, 1.5f) * 0.015f) * 2;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (Class_Interface.menuControlLock == false)
        {
            Vector2 axisNormalized = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).normalized;
            directionForwardBack = Mathf.SmoothDamp(directionForwardBack, axisNormalized.x, ref verticalVelocity, 0.15f);
            directionLeftRight = Mathf.SmoothDamp(directionLeftRight, axisNormalized.y, ref horizontalVelocity, 0.15f);

            mouseX = Input.GetAxis("Mouse X") * 0.1f * PlayerPrefs.GetFloat("mouseSensitivityValue");
            mouseX = Mathf.Clamp(mouseX, -18.0f, 18.0f);
            mouseXSmooth = Mathf.SmoothDamp(mouseXSmooth, mouseX, ref mouseXVelocity, 0.15f);
            thisTransform.Rotate(0, mouseXSmooth, 0);

            cameraRig.rotation = thisTransform.rotation;

            if (downLimiter == false && topLimiter == false)
            {
                mouseY = Input.GetAxis("Mouse Y") * 0.13f * PlayerPrefs.GetFloat("mouseSensitivityValue");
                mouseY = Mathf.Clamp(mouseY, -50.0f, 50.0f);
                mouseYSmooth = Mathf.SmoothDamp(mouseYSmooth, mouseY, ref mouseYVelocity, 0.1f);
                thisTransform.Translate(-Vector3.up * mouseYSmooth);
                if (thisTransform.position.y < 45) downLimiter = true;
                if (thisTransform.position.y > 380) topLimiter = true;
            }

            if (downLimiter == true)
            {
                thisTransform.Translate(Vector3.up * 0.25f);
                if (counter0++ > 25)
                {
                    counter0 = 0;
                    downLimiter = false;
                }
            }

            if (topLimiter == true)
            {
                thisTransform.Translate(-Vector3.up * 1.5f);
                if (counter1++ > 25)
                {
                    counter1 = 0;
                    topLimiter = false;
                }
            }

            cameraRig.position = thisTransform.position;

            if (Input.GetMouseButtonDown(0))
            {
                if (systemSelection == true)
                {
                    PlayerPrefs.SetInt("SeedGen", Class_MenuController.MenuSeed(planetPosition));
                    SceneManager.LoadScene("main");
                }
            }
        }

        //--------------

        Class_Controller.playerPosition = thisTransform.position;

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void OnTriggerEnter(Collider other)
    {
        //--------------

        if (other.gameObject.layer == 29 && Class_Interface.menuControlLock == false)
        {
            planetPosition = other.gameObject.transform.position;
            if (PlayerPrefs.GetInt(Class_MenuController.MenuSeed(planetPosition).ToString(), 0) == 0) systemSelection = true;
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void OnTriggerExit(Collider other)
    {
        //--------------

        if (other.gameObject.layer == 29 && Class_Interface.menuControlLock == false)
        {
            systemSelection = false;
        }

        //--------------
    }

    
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}





public static class Class_MenuController
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static int MenuSeed(Vector3 position)
    {
        //--------------

        return Mathf.FloorToInt(Mathf.Abs(Mathf.Cos((position.x * 2) + (position.y * 3) + (position.y * 4) + 99)) * 100000); ;

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class Controller : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public float gravityForce = 1.1f;
    public float fieldOfView = 90.0f;
    public Camera mainCamera;
    public Transform mainCameraRig;
    public Transform pointOfShotStart;
    public Transform pointOfShotEnd;
    public Transform aim;
    public LineRenderer laserLineRenderer;
    public GameObject pulseObject;
    public GameObject collapsarObject;
    public LineRenderer destroyerLineRenderer;
    public GameObject highSpeed;
    public GameObject weaponsDefaultSparks, weaponsLightSparks, weaponsLaserSparks, weaponsPulseSparks, weaponsDestroyerSparks;
    public GameObject fxSpeed0, fxSpeed1, fxSpeed2, fxSpeed3, fireObject;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.3f);

    Rigidbody thisRigidbody;
    Transform thisTransform, collapsarTransform, gravityTransform;
    Vector3 hitPointVector3, forceVector3, lastPosition;
    GameObject laserColliderObject;
    RaycastHit weaponsRaycastHit, gravityRaycastHit;
    float xSmooth, ySmooth, zSmooth, xVelocity, yVelocity, zVelocity, axisY, axisX, axisZ;
    float gravityForceA, gravityForceB, gravityRotation;
    float collapsarCounterAdd, speedChanger;
    int powerCounterAdd, lightningCounterAdd, laserCounterAdd, destroyerCounterAdd;
    bool systemBoundary, gravityTrigger, blackHoleTrigger, laserTrigger0, laserTrigger1, destroyerTrigger0, destroyerTrigger1, collapsarTrigger, sunTrigger;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        collapsarTransform = collapsarObject.transform;
        Class_Controller.playerPosition = lastPosition = thisTransform.position;

        thisRigidbody = GetComponent<Rigidbody>();
        thisRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        thisRigidbody.useGravity = false;

        laserLineRenderer.enabled = destroyerLineRenderer.enabled = false;
        pulseObject.SetActive(false);
        weaponsPulseSparks.SetActive(false);
        gravityForceA = gravityForce * 0.01f;
        gravityForceB = gravityRotation = speedChanger = 1;
        Class_Controller.speedMax = 70.0f;
        Class_Controller.currentSpeed = 0;
        Class_Controller.playerAlive = true;
        Class_Controller.mineralsCollector = false;
        Class_Controller.defaultMouseButtonDown = Class_Controller.lightningMouseButtonDown = Class_Controller.laserMouseButtonDown = Class_Controller.pulseMouseButtonDown = Class_Controller.eraserMouseButtonDown = Class_Controller.collapsarMouseButtonDown = Class_Controller.destroyerMouseButtonDown = false;

        StartCoroutine(SlowUpdate());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator SlowUpdate()
    {
        //--------------        

        while (Class_Controller.playerAlive == true)
        {
            yield return delay0;

            float distanceFromCenter = (thisTransform.position - Vector3.zero).sqrMagnitude;
            if (systemBoundary == false && distanceFromCenter > 20270000) systemBoundary = true;
            else if (distanceFromCenter < 20250000) systemBoundary = false;

            if (Class_Controller.currentSpeed > 50) highSpeed.SetActive(true);
            else highSpeed.SetActive(false);

            if (Class_Controller.currentSpeed >= 18 && gravityTrigger == false) fxSpeed0.SetActive(true);
            else fxSpeed0.SetActive(false);

            if (Class_Controller.currentSpeed >= 34 && gravityTrigger == false) fxSpeed1.SetActive(true);
            else fxSpeed1.SetActive(false);

            if (Class_Controller.currentSpeed >= 50 && gravityTrigger == false) fxSpeed2.SetActive(true);
            else fxSpeed2.SetActive(false);

            if (Class_Controller.currentSpeed >= 65 && gravityTrigger == false) fxSpeed3.SetActive(true);
            else fxSpeed3.SetActive(false);

            if (thisTransform.parent == null && gravityTrigger == true) gravityTrigger = false;   
        }

        //--------------

        highSpeed.SetActive(false);
        fxSpeed0.SetActive(false);
        fxSpeed1.SetActive(false);
        fxSpeed2.SetActive(false);
        fxSpeed3.SetActive(false);
        weaponsDefaultSparks.SetActive(false);
        weaponsLightSparks.SetActive(false);
        weaponsLaserSparks.SetActive(false);
        weaponsPulseSparks.SetActive(false);
        weaponsDestroyerSparks.SetActive(false);
        laserLineRenderer.enabled = destroyerLineRenderer.enabled = false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void FixedUpdate()
    {
        //--------------

        if (Class_Controller.playerAlive == true && Class_Interface.globalPausa == false && Class_Interface.loading == false)
        {
            if (lastPosition != thisTransform.position)
            {
                CurrentSpeed();
                Gravity();
                lastPosition = thisTransform.position;
            }

            if (Class_Controller.currentSpeed <= Class_Controller.speedMax)
            {
                if (systemBoundary == false)
                {
                    if (Input.GetAxis("Vertical") > 0.1f) thisRigidbody.AddForce(thisTransform.forward * 30.0f * speedChanger, ForceMode.Acceleration);
                    else if (Input.GetAxis("Vertical") < -0.1f) thisRigidbody.AddForce(-thisTransform.forward * 30.0f * speedChanger, ForceMode.Acceleration);
                }
                else
                {
                    thisRigidbody.AddForce(thisTransform.forward * 40.0f, ForceMode.Acceleration);
                    thisTransform.LookAt(Vector3.zero);
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void CurrentSpeed()
    {
        //--------------

        Class_Controller.currentSpeed = (thisTransform.position - lastPosition).magnitude * 50.0f;

        if (Class_Controller.currentSpeed >= 0.3f)
        {
            if (Class_Interface.speedEnabled == true)
            {
                if (speedChanger < 1.4f) speedChanger += 0.05f;
                else if(speedChanger > 1.4f) speedChanger = 1.4f;
                if (Class_Controller.speedMax != 85.0f) Class_Controller.speedMax = 85.0f;
            }
            else
            {
                if (speedChanger > 1) speedChanger -= 0.05f;
                else if (speedChanger < 1) speedChanger = 1;
                if (Class_Controller.speedMax > 70) Class_Controller.speedMax -= 0.4f;
                else if (Class_Controller.speedMax < 70) Class_Controller.speedMax = 70;
            }
        }

        if (Class_Controller.currentSpeed > 2 && Class_Controller.currentSpeed < Class_Controller.speedMax) thisRigidbody.drag = 0.5f - (Class_Controller.currentSpeed * 0.004f);
        else if (Class_Controller.currentSpeed < 2 || Class_Controller.currentSpeed >= Class_Controller.speedMax) thisRigidbody.drag = 0.5f;

        if (Class_Controller.currentSpeed > 0.5f)
        {
            float fieldOfViewSpeedMax = fieldOfView + Class_Controller.speedMax * 0.2971f;
            mainCamera.fieldOfView = fieldOfView + Class_Controller.currentSpeed * 0.3f;
            if (mainCamera.fieldOfView > fieldOfViewSpeedMax) mainCamera.fieldOfView = fieldOfViewSpeedMax;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Gravity()
    {
        //--------------

        if (gravityTrigger == true && gravityTransform != null)
        {
            if (Class_Interface.antigravityEnabled == false && blackHoleTrigger == false)
            {
                if (Physics.Raycast(thisTransform.position, -thisTransform.up, out gravityRaycastHit, 2, 1 << 29 | 1 << 19 | 1 << 20 | 1 << 21))
                {
                    thisTransform.rotation = Quaternion.LookRotation(thisTransform.forward, gravityRaycastHit.normal);
                    thisRigidbody.angularDrag = 2;
                    gravityForceB = 0.1f;
                    gravityRotation = 0;
                }
                else
                {
                    thisRigidbody.angularDrag = 0.1f;
                    gravityForceB = gravityRotation = 1;
                }

                Vector3 directionTogravity = gravityTransform.position - thisTransform.position;
                thisRigidbody.AddForce(Mathf.Sqrt(directionTogravity.sqrMagnitude) * gravityForceA * gravityForceB * directionTogravity, ForceMode.Acceleration);

                Quaternion targetRotation = Quaternion.FromToRotation(thisTransform.up, -directionTogravity * gravityRotation) * thisTransform.rotation;
                thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRotation, 0.05f);
            }

            if (blackHoleTrigger == true) thisRigidbody.AddForce(gravityForceA * 8 * (gravityTransform.position - thisTransform.position), ForceMode.Acceleration);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Update()
    {
        //--------------

        if (Class_Controller.playerAlive == true && Class_Interface.globalPausa == false && Class_Interface.loading == false && systemBoundary == false)
        {
            mainCameraRig.rotation = Quaternion.Slerp(mainCameraRig.rotation, thisTransform.rotation, Time.deltaTime * 4.0f);
            mainCameraRig.position = thisTransform.position;

            axisX = Input.GetAxis("Mouse X") * 0.075f * Class_Interface.mouseSensitivityValue;
            axisY = Input.GetAxis("Mouse Y") * -0.075f * Class_Interface.mouseSensitivityValue;
            axisZ = Input.GetAxis("Horizontal") * Time.deltaTime * 75.0f;

            axisX = Mathf.Clamp(axisX, -18.0f, 18.0f);
            axisY = Mathf.Clamp(axisY, -18.0f, 18.0f);
            axisZ = Mathf.Clamp(axisZ, -30.0f, 30.0f);

            xSmooth = Mathf.SmoothDamp(xSmooth, axisX, ref xVelocity, 0.1f);
            ySmooth = Mathf.SmoothDamp(ySmooth, axisY, ref yVelocity, 0.1f);
            zSmooth = Mathf.SmoothDamp(zSmooth, axisZ, ref zVelocity, 0.2f);

            thisTransform.Rotate(ySmooth, xSmooth, zSmooth);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerEnter(Collider collider)
    {
        //--------------

        if (Class_Controller.playerAlive == true && collider.gameObject.layer == 27)
        {
            if (collider.CompareTag("BlackHole")) blackHoleTrigger = true;
            thisTransform.parent = gravityTransform = collider.transform;
            if (gravityTrigger == false) Invoke("GravityTrigger", 0.2f);
        }

        //--------------
    }


    void GravityTrigger() { gravityTrigger = true; }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerExit(Collider collider)
    {
        //--------------

        if (Class_Controller.playerAlive == true)
        {
            if (collider.gameObject.layer == 27)
            {
                if (thisTransform.parent != null) thisTransform.parent = null;
                gravityTrigger = blackHoleTrigger = false;
            }

            if (collider.gameObject.layer == 25 && fireObject.activeSelf == true) fireObject.SetActive(false);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void OnTriggerStay(Collider collider)
    {
        //--------------

        if (Class_Controller.playerAlive == true && collider.gameObject.layer == 25)
        {
            if (sunTrigger == false)
            {
                sunTrigger = true;
                if (fireObject.activeSelf == false) fireObject.SetActive(true);
                Invoke("SunDamage", 0.5f);
            }
            if (collider.CompareTag("SunKilling"))
            {
                Class_Interface.lifeCounter = 1;
                SubtractionLife();                
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void SunDamage()
    {
        //-----------

        if (sunTrigger == true)
        {
            sunTrigger = false;
            SubtractionLife();
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnCollisionEnter(Collision collision)
    {
        //--------------

        if (Class_Controller.playerAlive == true)
        {
            GameObject collisionObject = collision.gameObject;

            if (collisionObject.layer == 11 && collisionObject != null)
            {
                MineralsPool.TakeMineral(collisionObject);
                if (collisionObject.CompareTag("Mineral 0")) Class_Interface.lifeCounter++;
                else if (collisionObject.CompareTag("Mineral 1")) Class_Interface.powerCounter++;
                else if (collisionObject.CompareTag("Mineral 2")) Class_Interface.speedCounter++;
                else if (collisionObject.CompareTag("Mineral 3")) Class_Interface.lightningCounter++;
                else if (collisionObject.CompareTag("Mineral 4")) Class_Interface.laserCounter++;
                else if (collisionObject.CompareTag("Mineral 5")) Class_Interface.pulseCounter++;
                else if (collisionObject.CompareTag("Mineral 6")) Class_Interface.maskerCounter++;
                else if (collisionObject.CompareTag("Mineral 7")) Class_Interface.antigravityCounter++;
                else if (collisionObject.CompareTag("Mineral 8")) Class_Interface.tamingCounter++;
                else if (collisionObject.CompareTag("Mineral 9")) Class_Interface.eraserCounter++;
                else if (collisionObject.CompareTag("Mineral 10")) Class_Interface.collapsarCounter++;
                else if (collisionObject.CompareTag("Mineral 11")) Class_Interface.destroyerCounter++;
            }

            if (collisionObject.layer == 13 && collisionObject.CompareTag("Bullet Default")) SubtractionLife();
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void SubtractionLife()
    {
        //--------------

        if (Class_Interface.lifeCounter >= 1) Class_Interface.lifeCounter--;
        else
        {
            Class_Interface.lifeCounter = 0;
            thisRigidbody.isKinematic = true;
            Class_Controller.playerAlive = false;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (Class_Controller.playerAlive == true && Class_Interface.globalPausa == false)
        {
            if (Class_Controller.playerPosition != thisTransform.position) Class_Controller.playerPosition = thisTransform.position;

            if (Class_Interface.loading == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Class_Interface.powerEnabled == true) Power_Weapon();
                    if (Class_Interface.defaultEnabled == true) Default_Weapon();
                    if (Class_Interface.lightningEnabled == true) Lightning_Weapon();
                    if (Class_Interface.laserEnabled == true) StartCoroutine(Laser_Weapon());
                    if (Class_Interface.pulseEnabled == true) StartCoroutine(Pulse_Weapon());
                    if (Class_Interface.eraserEnabled == true) Eraser_Weapon();
                    if (Class_Interface.collapsarEnabled == true) Collapsar_Weapon();
                    if (Class_Interface.destroyerEnabled == true) StartCoroutine(Destroyer_Weapon());
                }
                else
                {
                    if (Class_Controller.defaultMouseButtonDown == true) Class_Controller.defaultMouseButtonDown = false;
                    if (Class_Controller.lightningMouseButtonDown == true) Class_Controller.lightningMouseButtonDown = false;
                    if (Class_Controller.laserMouseButtonDown == true) Class_Controller.laserMouseButtonDown = false;
                    if (Class_Controller.pulseMouseButtonDown == true) Class_Controller.pulseMouseButtonDown = false;
                    if (Class_Controller.eraserMouseButtonDown == true) Class_Controller.eraserMouseButtonDown = false;
                    if (Class_Controller.collapsarMouseButtonDown == true) Class_Controller.collapsarMouseButtonDown = false;
                    if (Class_Controller.destroyerMouseButtonDown == true) Class_Controller.destroyerMouseButtonDown = false;
                }

                Laser_Weapon_Add();
                Collapsar_Weapon_Add();
                Destroyer_Weapon_Add();


                if (Class_Interface.speedCounter > 0)
                {
                    if (Class_Interface.speedEnabled != Input.GetKey(KeyCode.LeftShift)) Class_Interface.speedEnabled = Input.GetKey(KeyCode.LeftShift);
                }
                else
                {
                    if (Class_Interface.speedEnabled == true) Class_Interface.speedEnabled = false;
                }

                if (Class_Controller.mineralsCollector != Input.GetKey(KeyCode.E)) Class_Controller.mineralsCollector = Input.GetKey(KeyCode.E);
            }
        }

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
        

    void Power_Weapon()
    {
        //-----------

        if (powerCounterAdd++ >= 5)
        {
            powerCounterAdd = 0;
            Class_Interface.powerCounter -= 1;
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Default_Weapon()
    {
        //-----------

        Class_Controller.defaultMouseButtonDown = true;

        if (weaponsDefaultSparks.activeSelf == false)
        {
            weaponsDefaultSparks.SetActive(true);
            Invoke("Default_Weapon_Add", 0.5f);
        }

        //-----------

        GameObject bulletFx = WeaponsPool.GiveWeapons(3);        

        if (Physics.CapsuleCast(aim.position, aim.position, 2.5f, aim.forward, out weaponsRaycastHit, 350, 1 << 10 | 1 << 28 | 1 << 29 | 1 << 19 | 1 << 20 | 1 << 21 | 1 << 31))
        {
            hitPointVector3 = weaponsRaycastHit.point;
            Class_Controller.playerDistanceWeapon = weaponsRaycastHit.distance;

            GameObject bullet = WeaponsPool.GiveWeapons(0);
            bullet.transform.SetPositionAndRotation(hitPointVector3, Quaternion.LookRotation(weaponsRaycastHit.normal));
            bullet.SetActive(true);
            
            bulletFx.transform.position = pointOfShotStart.position;
            bulletFx.transform.LookAt(hitPointVector3);
            bulletFx.SetActive(true);
            bulletFx.GetComponent<Rigidbody>().AddForce(bulletFx.transform.forward * (500 + (Class_Controller.currentSpeed * 9) + weaponsRaycastHit.distance));
        }
        else
        {
            Class_Controller.playerDistanceWeapon = 100;
            bulletFx.transform.SetPositionAndRotation(pointOfShotStart.position, pointOfShotStart.rotation);
            bulletFx.SetActive(true);
            bulletFx.GetComponent<Rigidbody>().AddForce(aim.forward * (500 + Class_Controller.currentSpeed * 9));
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Default_Weapon_Add()
    {
        //-----------

        if (weaponsDefaultSparks.activeSelf == true) weaponsDefaultSparks.SetActive(false);

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Lightning_Weapon()
    {
        //-----------

        Class_Controller.lightningMouseButtonDown = true;

        if (weaponsLightSparks.activeSelf == false)
        {
            weaponsLightSparks.SetActive(true);
            Invoke("Lightning_Weapon_Add", 0.65f);
        }

        //-----------

        GameObject bulletFx = WeaponsPool.GiveWeapons(5);

        if (Physics.CapsuleCast(aim.position, aim.position, 5, aim.forward, out weaponsRaycastHit, 200, 1 << 10 | 1 << 28 | 1 << 29 | 1 << 19 | 1 << 20 | 1 << 21 | 1 << 31))
        {
            hitPointVector3 = weaponsRaycastHit.point;
            Class_Controller.playerDistanceWeapon = weaponsRaycastHit.distance;

            GameObject bullet = WeaponsPool.GiveWeapons(1);
            bullet.transform.SetPositionAndRotation(hitPointVector3, Quaternion.LookRotation(weaponsRaycastHit.normal));
            bullet.SetActive(true);
            
            bulletFx.transform.position = pointOfShotStart.position;
            bulletFx.transform.LookAt(hitPointVector3);
            bulletFx.SetActive(true);
            bulletFx.GetComponent<Rigidbody>().AddForce(bulletFx.transform.forward * (500 + Class_Controller.currentSpeed * 9));
        }
        else
        {
            Class_Controller.playerDistanceWeapon = 100;
            bulletFx.transform.position = pointOfShotStart.position;
            bulletFx.SetActive(true);
            bulletFx.GetComponent<Rigidbody>().AddForce(aim.forward * (500 + Class_Controller.currentSpeed * 9));
        }

        //-----------

        if (lightningCounterAdd++ >= 2)
        {
            lightningCounterAdd = 0;
            Class_Interface.lightningCounter -= 1;
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Lightning_Weapon_Add()
    {
        //-----------

        if (weaponsLightSparks.activeSelf == true) weaponsLightSparks.SetActive(false);

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Laser_Weapon()
    {
        //-----------

        if (laserTrigger0 == false) Class_Controller.laserMouseButtonDown = true;

        weaponsLaserSparks.SetActive(true);

        if (Physics.CapsuleCast(aim.position, aim.position, 2.5f, aim.forward, out weaponsRaycastHit, 500, 1 << 10 | 1 << 28 | 1 << 29 | 1 << 19 | 1 << 20 | 1 << 21 | 1 << 31))
        {
            laserColliderObject = weaponsRaycastHit.collider.gameObject;
            Class_Controller.playerDistanceWeapon = weaponsRaycastHit.distance;
            laserTrigger1 = true;
        }
        else
        {
            Class_Controller.playerDistanceWeapon = 100;
            laserTrigger1 = false;
        }

        laserTrigger0 = true;
        int stopCounter = 0;

        //-----------

        while (laserTrigger0 == true)
        {
            yield return delay0;

            GameObject bulletFx = WeaponsPool.GiveWeapons(4);
            bulletFx.transform.position = pointOfShotStart.position;
            bulletFx.SetActive(true);

            if (laserTrigger1 == true)
            {
                GameObject bullet = WeaponsPool.GiveWeapons(2);
                bullet.transform.SetPositionAndRotation(hitPointVector3, Quaternion.LookRotation(weaponsRaycastHit.normal));
                bullet.SetActive(true);
                bulletFx.transform.LookAt(hitPointVector3);                
                bulletFx.GetComponent<Rigidbody>().AddForce(bulletFx.transform.forward * (1000 + Class_Controller.currentSpeed * 9));
            }
            else bulletFx.GetComponent<Rigidbody>().AddForce(aim.forward * (1000 + Class_Controller.currentSpeed * 9));

            if (stopCounter++ > 3)
            {
                stopCounter = 0;
                laserLineRenderer.enabled = laserTrigger0 = false;
                weaponsLaserSparks.SetActive(false);
            }
        }

        //-----------

        if (laserCounterAdd++ >= 10)
        {
            laserCounterAdd = 0;
            Class_Interface.laserCounter -= 1;
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Laser_Weapon_Add()
    {
        //-----------

        if (Class_Interface.laserEnabled == true && laserTrigger0 == true)
        {
            laserLineRenderer.enabled = true;
            laserLineRenderer.SetPosition(0, pointOfShotStart.position);

            if (laserTrigger1 == true)
            {
                if (laserColliderObject != null) hitPointVector3 = laserColliderObject.transform.position;
                else hitPointVector3 = weaponsRaycastHit.point;
                laserLineRenderer.SetPosition(1, hitPointVector3);
            }
            else laserLineRenderer.SetPosition(1, pointOfShotEnd.position);
        }

        //-----------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Pulse_Weapon()
    {
        //-----------

        if (weaponsPulseSparks.activeSelf == false && pulseObject.activeSelf == false)
        {
            Class_Controller.pulseMouseButtonDown = true;

            yield return null;
            if (Class_Interface.powerEnabled == false) pulseObject.transform.localScale = Vector3.one * 25;
            else pulseObject.transform.localScale = Vector3.one * 33;
            pulseObject.SetActive(true);
            weaponsPulseSparks.SetActive(true);
            yield return delay0;
            pulseObject.SetActive(false);
            yield return delay0;
            weaponsPulseSparks.SetActive(false);

            Class_Interface.pulseCounter -= 1;
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Eraser_Weapon()
    {
        //-----------

        Class_Controller.eraserMouseButtonDown = true;

        Class_Controller.playerDistanceWeapon = 300;
        GameObject bullet = WeaponsPool.GiveWeapons(11);
        if (Class_Interface.powerEnabled == false) bullet.transform.localScale = Vector3.one * 10;
        else bullet.transform.localScale = Vector3.one * 12;
        bullet.transform.SetPositionAndRotation(aim.position, aim.rotation);
        bullet.SetActive(true);
        if (Class_Interface.powerEnabled == false) bullet.GetComponent<Rigidbody>().AddForce(aim.forward * 400);
        else bullet.GetComponent<Rigidbody>().AddForce(aim.forward * 500);

        //-----------

        Class_Interface.eraserCounter -= 1;

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Collapsar_Weapon()
    {
        //-----------

        if (collapsarTrigger == false)
        {
            Class_Controller.collapsarMouseButtonDown = true;

            if (Class_Interface.powerEnabled == true) collapsarTransform.localScale = Vector3.one;
            else collapsarTransform.localScale = Vector3.one * 0.7f;

            collapsarObject.SetActive(true);
            collapsarTrigger = true;
            collapsarCounterAdd = 0;

            Class_Interface.collapsarCounter -= 1;
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Collapsar_Weapon_Add()
    {
        //-----------

        if (collapsarTrigger == true)
        {
            collapsarCounterAdd += Time.deltaTime * 3.6f;

            if (collapsarCounterAdd > 10)
            {
                collapsarObject.SetActive(false);
                collapsarTrigger = false;
                collapsarCounterAdd = 0;
            }
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    IEnumerator Destroyer_Weapon()
    {
        //-----------

        if (destroyerTrigger0 == false) Class_Controller.destroyerMouseButtonDown = true;

        weaponsDestroyerSparks.SetActive(true);    

        if (Physics.CapsuleCast(aim.position, aim.position, 3, aim.forward, out weaponsRaycastHit, 600, 1 << 10 | 1 << 28 | 1 << 29 | 1 << 19 | 1 << 20 | 1 << 21 | 1 << 31))
        {
            laserColliderObject = weaponsRaycastHit.collider.gameObject;
            Class_Controller.playerDistanceWeapon = weaponsRaycastHit.distance;
            destroyerTrigger1 = true;            
        }
        else
        {
            Class_Controller.playerDistanceWeapon = 100;            
            destroyerTrigger1 = false;
        }

        destroyerTrigger0 = true;
        int stopCounter = 0;

        //-----------

        while (destroyerTrigger0 == true)
        {
            yield return delay0;

            GameObject bulletFx = WeaponsPool.GiveWeapons(8);
            bulletFx.transform.position = pointOfShotStart.position;
            bulletFx.SetActive(true);

            if (destroyerTrigger1 == true)
            {
                GameObject bullet;
                if (Class_Interface.powerEnabled == false) bullet = WeaponsPool.GiveWeapons(6);
                else bullet = WeaponsPool.GiveWeapons(7);
                bullet.transform.SetPositionAndRotation(hitPointVector3, Quaternion.LookRotation(weaponsRaycastHit.normal));
                bullet.SetActive(true);
                bulletFx.transform.LookAt(hitPointVector3);                
                bulletFx.GetComponent<Rigidbody>().AddForce(bulletFx.transform.forward * (1000 + Class_Controller.currentSpeed * 9));
            }
            else bulletFx.GetComponent<Rigidbody>().AddForce(aim.forward * (1000 + Class_Controller.currentSpeed * 9));

            if (stopCounter++ > 3)
            {
                destroyerLineRenderer.enabled = destroyerTrigger0 = false;
                weaponsDestroyerSparks.SetActive(false);
                stopCounter = 0;                
            }
        }

        //-----------

        if (destroyerCounterAdd++ >= 2)
        {
            destroyerCounterAdd = 0;
            Class_Interface.destroyerCounter -= 1;
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Destroyer_Weapon_Add()
    {
        //-----------

        if (Class_Interface.destroyerEnabled == true && destroyerTrigger0 == true)
        {
            if (destroyerLineRenderer.enabled == false) destroyerLineRenderer.enabled = true;
            destroyerLineRenderer.SetPosition(0, pointOfShotStart.position);

            if (destroyerTrigger1 == true)
            {
                if (laserColliderObject != null) hitPointVector3 = laserColliderObject.transform.position;
                else hitPointVector3 = weaponsRaycastHit.point;
                destroyerLineRenderer.SetPosition(1, hitPointVector3);
            }
            else destroyerLineRenderer.SetPosition(1, pointOfShotEnd.position);
        }

        //-----------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
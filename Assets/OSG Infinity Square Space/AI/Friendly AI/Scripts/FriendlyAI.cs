// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FriendlyAI : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public float atackDistance0 = 120, atackDistance1 = 85, atackDistance2 = 50, atackDistance3 = 15, atackDistance4 = 8;
    public float distanceLod0 = 250, distanceLod1 = 500, distanceLod2 = 1000;
    public float sizeMultiplier = 1.2f;
    public float lifeMultiplier = 1.5f;
    public float speedMultiplier = 1.5f;
    public float rotationalSpeed = 0.6f;
    public float gravityForce = 1.5f;
    public int bulletSpeed = 300;
    public int deadHideTime = 60;
    public int helpVariation = 10;
    public Vector2 timeChangeHome = new Vector2(20, 50);
    public GameObject weaponSparks;
    public bool helpPlayerOn = true;
    public bool randomRotationOn = true;
    public bool randomSpeedOn = true;
    public bool gravityOn = true;
    public bool seedScaleOn = true;
    public bool obstacleDetourOn = true;
    public AudioClip[] soundsFx;

    //--------------

    [HideInInspector]
    public bool dead, aiAgr;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.04f), delay1 = new WaitForSeconds(0.08f), delay2 = new WaitForSeconds(0.12f), delay2a = new WaitForSeconds(0.3f), delay3 = new WaitForSeconds(0.8f), delay4 = new WaitForSeconds(1.2f);
    AudioSource thisAudioSource;
    Rigidbody thisRigidbody;
    Transform thisTransform, gravityTransform, homePlanetTransform;
    GameObject collisionObject, colliderObject, enemyObject;
    Quaternion thisQuaternion;
    Vector3 planetPosition, enemyPosition;
    List<GameObject> homePlanetList;
    Collider[] enemies;    
    float atackDistancePow0, randomAtackDistance;
    float speed, lodSpeed, randomRotation, variableRotation, randomSpeed, amountLife, currentDamage, amountDamage, speedTaming, speedBehavior, localScaleX, sign, distanceHomePlanet, homePlanetScaleX, distanceToPlayer;
    int counter3, counter4, counter5, counter6, counter7, counter8, counter9, counter10, counter11, counter12, counter13, counter14, counter15, counter16, counter17, counter18, counter19, counter22, counter23;
    int homePlanetListCount, timeChangeHomeX, timeChangeHomeY, enemiesLength;
    bool attack, contusion, gravityTrigger, gravityValid, lodUpdate0, lodUpdate1, inVisible;           
    bool dodge, runAway, runAwayAdditionally, obstacleDetour, atHomePlanet, planetSelected, planetSelectedLod, playerIsVisible, enemiesIsVisible;
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;
        thisRigidbody = GetComponent<Rigidbody>();
        thisAudioSource = GetComponent<AudioSource>();
        thisQuaternion = thisTransform.rotation;
        atackDistancePow0 = Mathf.Pow(atackDistance0, 2);
        atackDistance1 = Mathf.Pow(atackDistance1, 2);
        atackDistance2 = Mathf.Pow(atackDistance2, 2);
        atackDistance3 = Mathf.Pow(atackDistance3, 2);
        atackDistance4 = Mathf.Pow(atackDistance4, 2);
        distanceLod0 = Mathf.Pow(distanceLod0, 2);
        distanceLod1 = Mathf.Pow(distanceLod1, 2);
        distanceLod2 = Mathf.Pow(distanceLod2, 2);
        timeChangeHomeX = Mathf.RoundToInt(timeChangeHome.x);
        timeChangeHomeY = Mathf.RoundToInt(timeChangeHome.y);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start ()
    {
        //--------------

        Class_AI.amountFriendlyAIs += 1;
        Class_AdditionalTools.RandomName(gameObject, "Friendly AI");

        distanceToPlayer = 99999;
        speedTaming = speedBehavior = lodSpeed = sign = variableRotation = 1;
        counter18 = 20;
        
        if (seedScaleOn == true) thisTransform.localScale = Vector3.one * (1 + (Mathf.Pow(Class_AdditionalTools.PositionSeed(thisTransform, 0.247f), 2) * sizeMultiplier));
        localScaleX = thisTransform.localScale.x;

        amountLife = 10 * (Mathf.Pow(localScaleX, 1.5f) * lifeMultiplier);

        StartCoroutine(Core());
        StartCoroutine(CustomUpdate());

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnBecameInvisible() { if (dead == true) inVisible = true; }
    void OnBecameVisible() { if (dead == true) inVisible = false; }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator Core()
    {
        //--------------

        yield return delay4;

        //--------------

        while (dead == false)
        {
            SeesEnemiesOrPlayer();
            SetCurrentState();
            RandomAction();
            AvoidObstacles();

            if (distanceToPlayer < distanceLod0)
            {
                if (lodUpdate0 == true || lodUpdate1 == true) lodUpdate0 = lodUpdate1 = false;
                if (lodSpeed != 1) lodSpeed = 1;
                if (playerIsVisible == false && enemiesIsVisible == false) yield return delay2;
                else yield return delay1;
            }
            else if (distanceToPlayer >= distanceLod0 && distanceToPlayer < distanceLod1)
            {
                if (lodUpdate0 == false) lodUpdate0 = true;
                if (lodUpdate1 == true) lodUpdate1 = false;
                if (lodSpeed != 3) lodSpeed = 3;
                if (playerIsVisible == false && enemiesIsVisible == false) yield return delay3;
                else yield return delay2;
            }
            else if (distanceToPlayer >= distanceLod1 && distanceToPlayer < distanceLod2)
            {
                if (lodUpdate1 == false) lodUpdate1 = true;
                if (lodSpeed != 10) lodSpeed = 10;
                if (playerIsVisible == false && enemiesIsVisible == false) yield return delay4;
                else yield return delay3;
            }
            else if (distanceToPlayer >= distanceLod2)
            {
                if (lodUpdate1 == false) lodUpdate1 = true;
                if (playerIsVisible == false && enemiesIsVisible == false && planetSelectedLod == false)
                {
                    if (lodSpeed != 0) lodSpeed = 0;
                    yield return delay2a;
                }
                else
                {
                    if (lodSpeed != 10) lodSpeed = 10;
                    yield return delay3;
                }
            }
        }

        //--------------

        weaponSparks.SetActive(false);

        while (dead == true)
        {
            yield return delay3;
            if (counter7 < deadHideTime)
            {
                counter7++;
                if (counter7 == 2) gameObject.layer = 28;
            }
            else if (inVisible == true) TrashForAI.InTrash(gameObject);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void SeesEnemiesOrPlayer()
    {
        //--------------

        if (counter22++ >= 5)
        {
            counter22 = 0;
            distanceToPlayer = Class_Controller.SqrMagnitudeToPlayer(thisTransform);
            enemiesIsVisible = Class_AI.EnemiesIsVisible(thisTransform, atackDistance0, 10);
            if (counter12++ > 6 && helpPlayerOn == true && playerIsVisible == false && enemiesIsVisible == false && distanceToPlayer < atackDistance2)
            {
                counter12 = 0;
                playerIsVisible = Random.Range(1, helpVariation) == 1;
                if (playerIsVisible == true) randomAtackDistance = Random.Range(0.85f, 1.15f);
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void SetCurrentState()
    {
        //--------------

        if (runAway == false)
        {
            if (enemiesIsVisible == false)
            {
                if (playerIsVisible == false) HomePlanet();
                else HelpPlayer();
            }
            else AttackOnEnemy();
        }
        else
        {
            HomePlanet();
            if (counter9++ > 150)
            {
                counter9 = 0;
                runAwayAdditionally = false;
            }
        }

        //--------------

        if (contusion == true)
        {
            if (counter6-- < 0) contusion = false;
            thisQuaternion = Quaternion.LookRotation(Random.insideUnitSphere);
        }

        //--------------

        if (weaponSparks != null && weaponSparks.activeSelf == true && lodUpdate0 == false && lodUpdate1 == false)
        {
            if (counter4++ > 4)
            {
                counter4 = 0;
                weaponSparks.SetActive(false);
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void RandomAction()
    {
        //--------------

        if (counter3-- <= 0)
        {
            counter3 = Random.Range(20, 60);
            randomAtackDistance = Random.Range(0.8f, 1.25f);
            if (randomRotationOn == true) randomRotation = Random.Range(0.6f, 1.6f);
            if (randomSpeedOn == true) randomSpeed = Random.Range(0.6f, 1.6f);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void AvoidObstacles()
    {
        //--------------  

        if (obstacleDetourOn == true && lodUpdate1 == false)
        {
            if (counter10++ >= 3)
            {
                counter10 = 0;

                if (obstacleDetour == false) obstacleDetour = Class_AI.ObstacleDetour(thisTransform, localScaleX);
                else if (counter11++ >= 2)
                {
                    counter11 = 0;
                    obstacleDetour = false;
                    sign = (Random.Range(0, 2) - 0.5f) * 2;
                }
            }
        }
        else if (obstacleDetour == true) obstacleDetour = false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void AttackOnEnemy()
    {
        //--------------

        if (variableRotation != 10) variableRotation = 10;

        enemies = Class_AI.EnemiesCollidersNearby(thisTransform, atackDistance0, 10);
        enemiesLength = enemies.Length;

        if (enemiesLength > 0)
        {
            if (counter16-- <= 0)
            {
                counter16 = 120;
                enemyObject = enemies[Random.Range(0, enemiesLength)].gameObject;
            }
            else if (enemyObject != null)
            {
                enemyPosition = enemyObject.transform.position;

                Shooting((enemyPosition - thisTransform.position).sqrMagnitude);

                counter17++;
                if (counter17 < counter18)
                {
                    if (speedBehavior != 1) speedBehavior = 1;
                    if (dodge == true) dodge = false;
                    thisQuaternion = Quaternion.LookRotation(enemyPosition - thisTransform.position);
                }
                else if (counter17 == counter18)
                {
                    speedBehavior = 1.5f;
                    dodge = true;
                    thisQuaternion = Class_AI.RotationSinus(thisTransform, 500);
                }
                else if (counter17 >= counter18 + 11)
                {
                    speedBehavior = 1;
                    dodge = false;
                    counter17 = 0;
                    counter18 = Random.Range(10, 60);
                }
            }
        }
        else speed = 5;

        if (counter23++ > 5)
        {
            counter23 = 0;
            gravityValid = true;
        }
        else gravityValid = false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void HelpPlayer()
    {
        //--------------

        if (variableRotation != 2) variableRotation = 2;        

        if (playerIsVisible == true)
        {
            if (distanceToPlayer < atackDistance2 && distanceToPlayer > atackDistance3 && speed != 5) speed = 5;
            else if (distanceToPlayer < atackDistance3 && distanceToPlayer > 33 && speed != 1) speed = 1;
            else if (distanceToPlayer < 33)
            {
                thisAudioSource.pitch = Random.Range(0.7f, 1.0f);
                thisAudioSource.panStereo = Random.Range(-0.1f, 0.1f);
                thisAudioSource.PlayOneShot(soundsFx[Random.Range(2, 5)], Random.Range(0.4f, 0.7f));
                playerIsVisible = false;
                if (speed != -3) speed = -4;
                Class_AI.RandomMinerals(thisTransform, true);
            }
        }

        thisQuaternion = Quaternion.LookRotation(Class_Controller.playerPosition - thisTransform.position);

        if (counter23++ > 5)
        {
            counter23 = 0;
            gravityValid = true;
        }
        else gravityValid = false;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Shooting(float distanceC)
    {
        //--------------        

        if (distanceC < atackDistancePow0 && distanceC > atackDistance1)
        {
            speed = 7 * speedBehavior;
            if (aiAgr == true || attack == true) aiAgr = attack = false;
        }
        else if (distanceC < atackDistance1 && distanceC > atackDistance2)
        {
            speed = 20 * speedBehavior;
            if (aiAgr == true || attack == true) aiAgr = attack = false;
        }
        else if (distanceC < atackDistance2 && distanceC > atackDistance3)
        {
            speed = 5 * speedBehavior;
            if (aiAgr == true) aiAgr = false;
            if (counter5++ > counter19)
            {
                counter5 = 0;
                counter19 = Random.Range(15, 25);
                attack = true;
            }
            else if (attack == true) attack = false;
        }
        else if (distanceC < atackDistance3 && distanceC > atackDistance4 * randomAtackDistance)
        {
            speed = 3 * speedBehavior;
            aiAgr = true;
            if (counter5++ > counter19)
            {
                counter5 = 0;
                counter19 = Random.Range(6, 16);
                attack = true;
            }
            else if (attack == true) attack = false;
        }
        else if (distanceC < atackDistance4 * randomAtackDistance)
        {
            if (dodge == false) speed = -12 * speedBehavior;
            else speed = 6 * speedBehavior;

            aiAgr = true;
            if (counter5++ > counter19)
            {
                counter5 = 0;
                counter19 = Random.Range(3, 7);
                attack = true;
            }
            else if (attack == true) attack = false;
        }
        
        //---

        if (attack == true && contusion == false && dodge == false)
        {
            if (Mathf.Sqrt(Class_Controller.SqrMagnitudeToPlayer(thisTransform)) < thisAudioSource.maxDistance)
            {
                thisAudioSource.pitch = Random.Range(0.7f, 1.15f);
                thisAudioSource.panStereo = Random.Range(-0.1f, 0.1f);
                thisAudioSource.PlayOneShot(soundsFx[Random.Range(0, 2)], Random.Range(0.1f, 0.25f));
            }
            if (weaponSparks != null && weaponSparks.activeSelf == false && lodUpdate0 == false && lodUpdate1 == false) weaponSparks.SetActive(true);
            GameObject bullet = WeaponsPool.GiveWeapons(10);
            bullet.transform.parent = null;
            bullet.transform.position = thisTransform.position + (thisTransform.forward * 1.5f);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(thisTransform.forward * bulletSpeed);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void HomePlanet()
    {
        //--------------

        attack = aiAgr = dodge = false;
        if (variableRotation != 1) variableRotation = 1;

        //--------------

        if (atHomePlanet == false)
        {
            homePlanetList = Class_PlanetarySystem.listPlanets;
            homePlanetListCount = homePlanetList.Count;

            for (int i = 0; i < homePlanetListCount; i++)
            {
                homePlanetTransform = homePlanetList[i].transform;
                if ((homePlanetTransform.position - thisTransform.position).sqrMagnitude < 90000)
                {
                    planetPosition = homePlanetTransform.position;
                    homePlanetScaleX = Mathf.Pow(homePlanetTransform.localScale.x * 15, 2);
                }
            }

            counter15 = Random.Range(timeChangeHomeX, timeChangeHomeY);
            atHomePlanet = planetSelected = true;
        }
        else
        {
            if (counter14++ > 4)
            {
                counter14 = 0;
                homePlanetList = Class_PlanetarySystem.listPlanets;
                if (homePlanetListCount > homePlanetList.Count) planetSelected = false;
                homePlanetListCount = homePlanetList.Count;
            }
        }

        //--------------

        if (planetSelected == false)
        {
            counter15 = Random.Range(timeChangeHomeX, timeChangeHomeY);
            homePlanetTransform = homePlanetList[Random.Range(0, homePlanetListCount)].transform;
            planetPosition = homePlanetTransform.position;
            homePlanetScaleX = Mathf.Pow(homePlanetTransform.localScale.x * 15, 2);
            planetSelected = planetSelectedLod = true;
        }
        else
        {
            if (counter13++ > 2)
            {
                counter13 = 0;
                distanceHomePlanet = (planetPosition - thisTransform.position).sqrMagnitude;

                if (distanceHomePlanet > homePlanetScaleX + 10000) speed = 14;
                else
                {
                    speed = 7;
                    if (counter15-- <= 0) planetSelected = false;
                }

                if (distanceHomePlanet > homePlanetScaleX)
                {
                    if (Random.Range(1, 8) == 1) thisQuaternion = Class_AI.RotationSinus(thisTransform, 1000);
                    else thisQuaternion = Quaternion.LookRotation(planetPosition - thisTransform.position);
                    gravityValid = false;
                }
                else
                {
                    if (Random.Range(1, 3) == 1) thisQuaternion = Class_AI.RotationSinus(thisTransform, 1000);

                    if (counter23++ > 5)
                    {
                        counter23 = 0;
                        gravityValid = true;
                    }
                    else gravityValid = false;

                    planetSelectedLod = false;
                    if (runAway == true)
                    {
                        if (counter8++ > 5)
                        {
                            counter8 = 0;
                            runAway = false;
                            runAwayAdditionally = true;
                            if (enemiesIsVisible == false) amountDamage = 0;
                        }
                    }
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnCollisionEnter(Collision collision)
    {
        //--------------

        if (dead == false)
        {
            collisionObject = collision.collider.gameObject;

            if (collisionObject.layer == 13)
            {
                if (collisionObject.CompareTag("Bullet Default"))
                {
                    currentDamage = 4;
                    counter6 = 3;
                    contusion = true;
                }
                Damage();
            }

            if (collisionObject.layer == 12)
            {
                if (collisionObject.CompareTag("Bullet Destroyer"))
                {
                    currentDamage = 0.2f;
                    counter6 = 20;
                    contusion = true;
                }
                Damage();
            }
        }

        if (collisionObject.gameObject.layer == 25) TrashForAI.InTrash(gameObject);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerEnter(Collider other)
    {
        //--------------

        if (dead == false)
        {
            colliderObject = other.gameObject;

            if (colliderObject.layer == 9)
            {
                if (colliderObject.name == "Pulse Level 0" || colliderObject.name == "Pulse Level 1" || colliderObject.name == "Pulse Level 2" ||
                    colliderObject.name == "Pulse Level 3")
                {
                    amountDamage = 0;
                    counter6 = 20;
                    contusion = true;
                }
            }

            if (colliderObject.layer == 12 && colliderObject.CompareTag("Bullet Collapsar"))
            {
                counter6 = 25;
                contusion = true;
            }

            if (colliderObject.gameObject.layer == 25)
            {
                counter6 = 10;
                contusion = true;
                currentDamage = 1000;
                Damage();
            }

            if (colliderObject.layer == 27)
            {
                gravityTransform = colliderObject.transform;
                thisTransform.parent = gravityTransform;
                gravityTrigger = true;
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerExit(Collider other)
    {
        //--------------

        if (dead == false && other.gameObject.layer == 27)
        {
            thisTransform.parent = null;
            gravityTrigger = false;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Damage()
    {
        //--------------

        if (dead == false)
        {
            amountDamage += currentDamage;
            if (amountDamage > amountLife * 0.75f && runAwayAdditionally == false) runAway = true;
            if (amountDamage > amountLife)
            {
                if (Mathf.Sqrt(Class_Controller.SqrMagnitudeToPlayer(thisTransform)) < thisAudioSource.maxDistance)
                {
                    thisAudioSource.pitch = Random.Range(1.15f, 1.4f);
                    thisAudioSource.panStereo = Random.Range(-0.1f, 0.1f);
                    thisAudioSource.PlayOneShot(soundsFx[Random.Range(2, 5)], Random.Range(0.9f, 1.0f));
                }
                dead = true;
                speed = 0;
                aiAgr = attack = contusion = false;
                Class_AI.amountFriendlyAIs -= 1;
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    IEnumerator CustomUpdate()
    {
        //--------------

        while (dead == false)
        {
            if (lodUpdate1 == false)
            {
                if (lodUpdate0 == false) yield return delay0;
                else yield return delay2;

                if (lodSpeed > 0)
                {
                    if (contusion == false)
                    {
                        thisRigidbody.AddForce(thisTransform.forward * speed * speedMultiplier * speedTaming * randomSpeed * lodSpeed);
                        if (obstacleDetour == false) thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, thisQuaternion, rotationalSpeed * variableRotation * randomRotation * lodSpeed);
                        else thisTransform.Rotate(Vector3.up * sign * 20 * rotationalSpeed * lodSpeed);
                    }
                    else thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, thisQuaternion, rotationalSpeed * variableRotation * randomRotation * lodSpeed);
                    Gravity();
                }
            }
            else
            {
                yield return delay3;

                if (lodSpeed > 0)
                {
                    thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, thisQuaternion, rotationalSpeed * variableRotation * lodSpeed);
                    thisRigidbody.AddForce(thisTransform.forward * speed * speedMultiplier * lodSpeed);
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Gravity()
    {
        //--------------

        if (gravityOn == true && gravityTrigger == true && gravityValid == true && gravityTransform != null)
        {
            Vector3 directionTogravity = gravityTransform.position - thisTransform.position;
            thisRigidbody.AddForce(directionTogravity.sqrMagnitude * 0.0001f * gravityForce * directionTogravity, ForceMode.Force);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

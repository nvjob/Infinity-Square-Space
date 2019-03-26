// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class SpawnAI : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public bool enemyAiOn;
    public GameObject enemyAi;
    public int spawnEnemyScale = 5;
    public int amountEnemyAi = 2;

    public bool friendlyAiOn;
    public GameObject friendlyAi;
    public int spawnFriendlyScale = 5;
    public int amountFriendlyAi = 2;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.05f), delay1 = new WaitForSeconds(0.5f);
    Transform thisTransform;
    bool friendlyAiEnd, enemyAiEnd;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void Awake()
    {
        //--------------

        thisTransform = transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        StartCoroutine("EnemyAi");
        StartCoroutine("FriendlyAi");
        StartCoroutine("AITRAshDestroy");

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator EnemyAi()
    {
        //--------------

        yield return delay1;

        if (enemyAiOn && enemyAi != null)
        {
            for (int z = 0; z < amountEnemyAi; z++)
            {
                for (int y = 0; y < amountEnemyAi; y++)
                {
                    for (int x = 0; x < amountEnemyAi; x++)
                    {
                        yield return delay0;
                        GameObject objAi = Instantiate(enemyAi, thisTransform.position, thisTransform.rotation);
                        float xt = (spawnEnemyScale * 0.5f - 0.5f) + (x * spawnEnemyScale);
                        float yt = (spawnEnemyScale * 0.5f - 0.5f) + (y * spawnEnemyScale);
                        float zt = (spawnEnemyScale * 0.5f - 0.5f) + (z * spawnEnemyScale);
                        objAi.transform.position = thisTransform.position + new Vector3(xt, yt, zt);
                    }
                }
            }
        }

        enemyAiEnd = true;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator FriendlyAi()
    {
        //--------------

        yield return delay1;

        if (friendlyAiOn && friendlyAi != null)
        {
            for (int z = 0; z < amountFriendlyAi; z++)
            {
                for (int y = 0; y < amountFriendlyAi; y++)
                {
                    for (int x = 0; x < amountFriendlyAi; x++)
                    {
                        yield return delay0;
                        GameObject objAi = Instantiate(friendlyAi, thisTransform.position, thisTransform.rotation);
                        float xt = (spawnFriendlyScale * 0.5f - 0.5f) + (x * spawnFriendlyScale);
                        float yt = (spawnFriendlyScale * 0.5f - 0.5f) + (y * spawnFriendlyScale);
                        float zt = (spawnFriendlyScale * 0.5f - 0.5f) + (z * spawnFriendlyScale);
                        objAi.transform.position = thisTransform.position + new Vector3(xt, yt, zt);
                    }
                }
            }
        }

        friendlyAiEnd = true;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator AITRAshDestroy()
    {
        //--------------

        while (true)
        {
            yield return delay1;
            if (enemyAiEnd == true && friendlyAiEnd == true) TrashForAI.InTrash(gameObject);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

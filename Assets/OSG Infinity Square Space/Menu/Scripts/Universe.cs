// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using System.Collections;



public class Universe : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
        

    public GameObject chunkUniverse;
    public int chunkUniverseScale = 50;
    public int numberOfChunkUniverse = 20;
    public Transform starBackground;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(0.15f);
    Transform thisTransform;
    Vector3 offsetPosition, chunkUniversePosition;
    Hashtable chunkHashtable = new Hashtable();
    float chunkUpdateTime;
    GameObject chunkObject;
    int xMove, zMove, playerX, playerZ;


    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void Start () 
	{
        //--------------

        thisTransform = transform;
        offsetPosition = Vector3.one * 99999;
        chunkUpdateTime = Time.realtimeSinceStartup;

        StartCoroutine("UniverseGenerator");

        //--------------
    }
       


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator UniverseGenerator()
    {
        //--------------

        while (true)
        {
            yield return delay0;

            xMove = Mathf.Abs((int)(Class_Controller.playerPosition.x - offsetPosition.x));
            zMove = Mathf.Abs((int)(Class_Controller.playerPosition.z - offsetPosition.z));

            if (xMove >= chunkUniverseScale || zMove >= chunkUniverseScale)
            {
                chunkUpdateTime = Time.realtimeSinceStartup;

                playerX = (int)(Mathf.Floor(Class_Controller.playerPosition.x / chunkUniverseScale) * chunkUniverseScale);
                playerZ = (int)(Mathf.Floor(Class_Controller.playerPosition.z / chunkUniverseScale) * chunkUniverseScale);

                for (int x = -numberOfChunkUniverse; x < numberOfChunkUniverse; x++)
                {
                    yield return null;

                    for (int z = -numberOfChunkUniverse; z < numberOfChunkUniverse; z++)
                    {
                        chunkUniversePosition = new Vector3(x * chunkUniverseScale + playerX, 0, z * chunkUniverseScale + playerZ);
                        string chunkName = "Chunk Universe " + ((int)chunkUniversePosition.x).ToString() + " " + ((int)chunkUniversePosition.z).ToString();
                        if (!chunkHashtable.ContainsKey(chunkName))
                        {
                            chunkObject = Instantiate(chunkUniverse, chunkUniversePosition, Quaternion.identity);
                            chunkObject.name = chunkName;
                            chunkHashtable.Add(chunkName, new ChunkClass(chunkObject, chunkUpdateTime));
                            chunkObject.transform.parent = thisTransform;
                            chunkObject.SetActive(true);
                        }
                        else (chunkHashtable[chunkName] as ChunkClass).chunkClassTime = chunkUpdateTime;
                    }
                }

                Hashtable newChunk = new Hashtable();
                foreach (ChunkClass chunks in chunkHashtable.Values)
                {
                    if (chunks.chunkClassTime != chunkUpdateTime) Destroy(chunks.chunkClassObj);
                    else newChunk.Add(chunks.chunkClassObj.name, chunks);
                }

                chunkHashtable = newChunk;
                offsetPosition = Class_Controller.playerPosition;
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (starBackground.position != Class_Controller.playerPosition) starBackground.position = Class_Controller.playerPosition;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}





class ChunkClass
{
    //--------------

    public GameObject chunkClassObj;
    public float chunkClassTime;

    public ChunkClass(GameObject obj, float time)
    {
        chunkClassObj = obj;
        chunkClassTime = time;
    }

    //--------------
}
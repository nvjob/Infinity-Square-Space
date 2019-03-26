// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class ChunkCubesPool : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    public int numberChunkCubes = 10000;
    public GameObject chunkCube;

    //--------------

    static Transform stThisTransform;
    static GameObject[] stChunkCubes;
    static int stNumberChunkCubes;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        stThisTransform = transform;
        stNumberChunkCubes = numberChunkCubes;

        stChunkCubes = new GameObject[numberChunkCubes];

        for (int i = 0; i < numberChunkCubes; i++)
        {
            stChunkCubes[i] = Instantiate(chunkCube);
            stChunkCubes[i].SetActive(false);
            stChunkCubes[i].transform.parent = stThisTransform;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static public GameObject GiveCube()
    {
        //--------------

        for (int i = 0; i < stNumberChunkCubes; i++) if (!stChunkCubes[i].activeSelf) return stChunkCubes[i];
        return null;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    static public void TakeCube(GameObject obj)
    {
        //--------------

        obj.SetActive(false);
        obj.transform.parent = stThisTransform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

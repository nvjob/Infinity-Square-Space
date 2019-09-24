// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using System.Collections.Generic;



public class ExplosionParticlesPool : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public List<ExplosionParticles> explosionParticlesList = new List<ExplosionParticles>();

    //--------------

    static Transform stThisTransform;
    static int[] stNumberExplosionParticles;
    static GameObject[][] stExplosionParticles;
    

    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        
    private void Awake()
    {
        //--------------

        stThisTransform = transform;
        AddObjectsToPool();

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    private void AddObjectsToPool()
    {
        //--------------

        stNumberExplosionParticles = new int[explosionParticlesList.Count];
        stExplosionParticles = new GameObject[explosionParticlesList.Count][];

        //--------------

        for (int num = 0; num < explosionParticlesList.Count; num++)
        {
            stNumberExplosionParticles[num] = explosionParticlesList[num].numberExplosionParticles;
            stExplosionParticles[num] = new GameObject[stNumberExplosionParticles[num]];
            InstanInPool(explosionParticlesList[num].explosionParticle, stExplosionParticles[num]);
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static private void InstanInPool(GameObject obj, GameObject[] objs)
    {
        //--------------

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Instantiate(obj);
            objs[i].SetActive(false);
            objs[i].transform.parent = stThisTransform;
        }
        
        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static public GameObject GiveExplosionParticle(int num)
    {
        //--------------
        
        for (int i = 0; i < stNumberExplosionParticles[num]; i++) if (!stExplosionParticles[num][i].activeSelf) return stExplosionParticles[num][i];
        return null;
        
        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    static public void TakeExplosionParticle(GameObject obj)
    {
        //--------------

        obj.SetActive(false);
        if (obj.transform.parent != stThisTransform) obj.transform.parent = stThisTransform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}









[System.Serializable]

public class ExplosionParticles
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public GameObject explosionParticle;
    public int numberExplosionParticles = 100;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public static class Class_AI
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static int amountEnemyAIs, amountFriendlyAIs;
    public static bool aiSpawnOn;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static bool EnemiesIsVisible(Transform thisTransform, float atackDistance0, int layer)
    {
        //--------------

        return Physics.CheckSphere(thisTransform.position, atackDistance0, 1 << layer);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static Collider[] EnemiesCollidersNearby(Transform thisTransform, float atackDistance0, int layer)
    {
        //--------------

        return Physics.OverlapSphere(thisTransform.position, atackDistance0, 1 << layer);

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static bool ObstacleDetour(Transform thisTransform, float localScaleX)
    {
        //--------------

        return Physics.CheckCapsule(thisTransform.position, thisTransform.position + (thisTransform.forward * localScaleX), 0.15f, 1 << 29 | 1 << 19 | 1 << 20 | 1 << 21 | 1 << 31);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    public static Quaternion RotationSinus(Transform thisTransform, float scale)
    {
        //--------------

        Vector3 position = thisTransform.position;
        return Quaternion.LookRotation(new Vector3(Mathf.Sin(position.x), Mathf.Sin(position.y), Mathf.Sin(position.z)) * scale);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public static void RandomMinerals(Transform thisTransform, bool addForce)
    {
        //--------------

        GameObject minerals = null;
        int randomMinerals = Random.Range(1, 13);
        if (randomMinerals == 1) minerals = MineralsPool.GiveMineral(0);
        else if (randomMinerals == 2) minerals = MineralsPool.GiveMineral(1);
        else if (randomMinerals == 3) minerals = MineralsPool.GiveMineral(2);
        else if (randomMinerals == 4) minerals = MineralsPool.GiveMineral(3);
        else if (randomMinerals == 5) minerals = MineralsPool.GiveMineral(4);
        else if (randomMinerals == 6) minerals = MineralsPool.GiveMineral(5);
        else if (randomMinerals == 7) minerals = MineralsPool.GiveMineral(6);
        else if (randomMinerals == 8) minerals = MineralsPool.GiveMineral(7);
        else if (randomMinerals == 9) minerals = MineralsPool.GiveMineral(8);
        else if (randomMinerals == 10) minerals = MineralsPool.GiveMineral(9);
        else if (randomMinerals == 11) minerals = MineralsPool.GiveMineral(10);
        else if (randomMinerals == 12) minerals = MineralsPool.GiveMineral(11);
        if (minerals != null)
        {
            minerals.transform.position = thisTransform.position;
            minerals.SetActive(true);
            if (addForce == true) minerals.GetComponent<Rigidbody>().AddForce(thisTransform.forward * 1);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
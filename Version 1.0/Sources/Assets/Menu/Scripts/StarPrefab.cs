// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class StarPrefab : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        
    public GameObject sun;
    public GameObject blackHole;
    public GameObject stateFriendly, stateEnemy;
    public GameObject selected;
    public Transform currentStateTransform;

    //--------------

    Transform thisTransform;
    int getIntSeedGeneration;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisTransform = transform;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnEnable()
    {
        //--------------

        Vector3 thisPosition = thisTransform.position;
        currentStateTransform.rotation = Random.rotation;

        //--------------

        getIntSeedGeneration = PlayerPrefs.GetInt(Class_MenuController.MenuSeed(thisPosition).ToString(), 0);

        if (getIntSeedGeneration == 0)
        {
            stateFriendly.transform.localScale = Vector3.one * 0.15f;
            stateEnemy.transform.localScale = Vector3.one * 0.15f;
            stateFriendly.SetActive(true);
            stateEnemy.SetActive(true);
        }
        else if (getIntSeedGeneration == 1)
        {
            stateFriendly.transform.localScale = Vector3.one * 0.2f;
            stateFriendly.SetActive(true);            
            stateEnemy.SetActive(false);
        }
        else if (getIntSeedGeneration == 2)
        {
            stateEnemy.transform.localScale = Vector3.one * 0.2f;
            stateFriendly.SetActive(false);
            stateEnemy.SetActive(true);            
        }

        selected.SetActive(false);

        //--------------

        float seedAbsCos = Mathf.Abs(Mathf.Cos(Class_MenuController.MenuSeed(thisPosition)));

        if (Class_MenuController.MenuSeed(thisPosition) % 7 == 0)
        {
            sun.SetActive(false);
            blackHole.SetActive(true);
            blackHole.transform.localScale = Vector3.one * (seedAbsCos + 0.35f) * 2.5f;
        }
        else
        {
            blackHole.SetActive(false);
            sun.SetActive(true);
            Class_AdditionalTools.UvMesh(sun.GetComponent<MeshFilter>().mesh, seedAbsCos, 0.5f);
            sun.transform.localScale = Vector3.one * (seedAbsCos + 0.35f);
        }

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerEnter(Collider other)
    {
        //--------------

        if (other.gameObject.layer == 23 && Class_StarSystem.systemSelected == false && getIntSeedGeneration == 0)
        {
            selected.SetActive(true);
            Class_StarSystem.systemSelected = true;
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnTriggerExit(Collider other)
    {
        //--------------

        if (other.gameObject.layer == 23 && getIntSeedGeneration == 0)
        {
            selected.SetActive(false);
            Class_StarSystem.systemSelected = false;
        }

        //--------------
    }
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

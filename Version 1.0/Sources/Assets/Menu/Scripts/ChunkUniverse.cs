// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ChunkUniverse : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       


    public int heightScaleUniverse = 150;
    public float detailScaleUniverse = 150;

    List<GameObject> stars = new List<GameObject>();
    GameObject starFromPool;
    Transform thisTransform;
    Vector3[] vertices;
    Vector3 starPosition;
    bool positionTaken;
    float posX, posZ, cosQtObj, localsclTel;
    


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    void Awake()
    {
        //--------------

        thisTransform = transform;
        vertices = GetComponent<MeshFilter>().mesh.vertices;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------        

        localsclTel = thisTransform.localScale.x;        

        StartCoroutine(LocationOfStars());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator LocationOfStars()
    {
        //--------------

        for (int v = 0; v < vertices.Length; v++)
        {
            yield return null;
                        
            posX = ((vertices[v].x * localsclTel) + (thisTransform.position.x + 9999)) / detailScaleUniverse;
            posZ = ((vertices[v].z * localsclTel) + (thisTransform.position.z + 9999)) / detailScaleUniverse;

            vertices[v].y = Mathf.PerlinNoise(posX, posZ) * heightScaleUniverse;

            cosQtObj = Mathf.Cos(vertices[v].y + thisTransform.position.x + thisTransform.position.z + v + 9.5f) * 10.5f;
            float lperlinf = Mathf.PerlinNoise(cosQtObj, cosQtObj);

            if (v % 5 == 0 && lperlinf > 0.5f && lperlinf < 0.52f)
            {
                starPosition = new Vector3(vertices[v].x * localsclTel + thisTransform.position.x, vertices[v].y, vertices[v].z * localsclTel + thisTransform.position.z);
                starFromPool = StarPool.GiveStar();

                if (starFromPool != null)
                {
                    if (stars.Count == 0)
                    {
                        starFromPool.transform.position = starPosition;
                        starFromPool.SetActive(true);
                        stars.Add(starFromPool);
                    }
                    else
                    {
                        positionTaken = false;
                        for (int i = 0; i < stars.Count; i++) if (stars[i].transform.position == starPosition) positionTaken = true;
                        if (positionTaken == false)
                        {
                            starFromPool.transform.position = starPosition;
                            starFromPool.SetActive(true);
                            stars.Add(starFromPool);
                        }
                    }
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnDestroy()
    {
        //--------------

        for (int i = 0; i < stars.Count; i++) if (stars[i] != null) StarPool.TakeStar(stars[i]);
        stars.Clear();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

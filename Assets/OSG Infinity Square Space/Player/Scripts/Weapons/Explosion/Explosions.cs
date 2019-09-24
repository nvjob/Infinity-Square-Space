// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class Explosions : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    


    public bool randomTime = false;
    public float minTime = 1.5f;
    public float maxTime = 2.5f;
    public AudioClip[] soundsFx;
    public Vector2 audioPitch = new Vector2(0.75f, 1.25f);
    public Vector2 audioVolume = new Vector2(0.75f, 1.0f);

    //--------------

    static int countCurent;
    static WaitForSeconds delay0 = new WaitForSeconds(0.1f);
    Transform thisTransform;
    AudioSource thisAudioSource;
    float time, distance;
    bool trigger0;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void Awake()
    {
        //--------------

        thisTransform = transform;
        if (soundsFx.Length > 0) thisAudioSource = GetComponent<AudioSource>();
        trigger0 = soundsFx.Length > 0;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnEnable()
    {
        //--------------

        StartCoroutine(OnEnableCoroutine());

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator OnEnableCoroutine()
    {
        //--------------

        if (trigger0 == true) distance = Mathf.Sqrt(Class_Controller.SqrMagnitudeToPlayer(thisTransform));

        //--------------

        if (trigger0 == true && distance < thisAudioSource.maxDistance)
        {
            countCurent++;
            yield return null;
            if (countCurent < 10)
            {
                thisAudioSource.clip = soundsFx[Random.Range(0, soundsFx.Length)];
                thisAudioSource.pitch = Random.Range(audioPitch.x, audioPitch.y);
                thisAudioSource.volume = Random.Range(audioVolume.x, audioVolume.y);
                thisAudioSource.Play();
                while (thisAudioSource.isPlaying == true) yield return delay0;
            }
            countCurent--;
        }
        else
        {
            if (randomTime == true) time = Random.Range(minTime, maxTime);
            else time = minTime;
            yield return new WaitForSeconds(time);
        }

        //--------------        

        ExplosionParticlesPool.TakeExplosionParticle(gameObject);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

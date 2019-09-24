// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class PlayerSoundSpeed : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        

    public AudioClip[] soundsFx;

    //--------------

    AudioSource thisAudioSource;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        thisAudioSource = GetComponent<AudioSource>();

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (Class_Controller.playerAlive == true && Class_Interface.globalPausa == false && Class_Controller.currentSpeed > 5)
        {
            if (thisAudioSource.isPlaying == false)
            {
                thisAudioSource.clip = soundsFx[0];
                thisAudioSource.Play();
            }
            thisAudioSource.pitch = 0.5f + (Class_Controller.currentSpeed * 0.0117f);
            thisAudioSource.volume = Class_Controller.currentSpeed * 0.011f;
        }
        else
        {
            if (thisAudioSource.isPlaying == true) thisAudioSource.Stop();
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

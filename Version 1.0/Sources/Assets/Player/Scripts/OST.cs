// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using System.Collections;
using UnityEngine;



public class OST : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public AudioSource audioSource0;
    public AudioSource audioSource1;
    public AudioClip[] sounds;

    //--------------

    static WaitForSeconds delay0 = new WaitForSeconds(5);
    bool triggerAudioDead, triggerDetectedEnemy;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Start()
    {
        //--------------

        audioSource0.loop = true;
        audioSource1.loop = true;

        StartCoroutine(DetectedEnemy());
            
        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (Class_Controller.playerAlive == true && Class_Interface.globalPausa == false)
        {
            if (Class_Interface.loading == false)
            {
                if (audioSource0.pitch != 0.98f) audioSource0.pitch = 0.98f;
                if (audioSource1.pitch != 0.98f) audioSource1.pitch = 0.98f;
            }
            else
            {
                if (audioSource0.pitch != 0.94f) audioSource0.pitch = 0.94f;
                if (audioSource0.volume != 0.6f) audioSource0.volume = 0.6f;
            }

            //---

            if (triggerDetectedEnemy == false)
            {
                if (audioSource0.isPlaying == false)
                {
                    audioSource0.clip = sounds[Random.Range(1, 6)];
                    audioSource0.Play();
                }

                if (audioSource0.volume < 0.8f) audioSource0.volume += Time.deltaTime * 0.14f;
                if (audioSource1.volume != 0) audioSource1.volume -= Time.deltaTime * 0.14f;
                if (audioSource1.volume <= 0.05f && audioSource1.isPlaying == true) audioSource1.Stop();
            }
            else
            {
                if (audioSource1.isPlaying == false)
                {
                    audioSource1.clip = sounds[6];
                    audioSource1.Play();
                }

                if (audioSource1.volume < 0.8f) audioSource1.volume += Time.deltaTime * 0.14f;
                if (audioSource0.volume != 0) audioSource0.volume -= Time.deltaTime * 0.14f;
                if (audioSource0.volume <= 0.05f && audioSource0.isPlaying == true) audioSource0.Stop();
            }

            //---
        }
        else
        {
            if (Class_Controller.playerAlive == false)
            {
                if (triggerAudioDead == false)
                {
                    audioSource0.Stop();
                    audioSource1.Stop();
                    audioSource0.volume = 1;
                    audioSource0.clip = sounds[0];
                    audioSource0.loop = false;
                    audioSource0.Play();
                    triggerAudioDead = true;
                }
            }
            else
            {
                if (triggerDetectedEnemy == false)
                {
                    if (audioSource0.pitch != 0.8f) audioSource0.pitch = 0.8f;
                    if (audioSource0.volume != 0.4f) audioSource0.volume = 0.4f;
                }
                else
                {
                    if (audioSource1.pitch != 0.8f) audioSource1.pitch = 0.8f;
                    if (audioSource1.volume != 0.4f) audioSource1.volume = 0.4f;
                }
            }
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    IEnumerator DetectedEnemy()
    {
        //--------------

        while (Class_Controller.playerAlive == true)
        {
            yield return delay0;
            triggerDetectedEnemy = Physics.CheckSphere(Class_Controller.playerPosition, Class_Controller.distanceDetectedEnemy - 10, 1 << 10);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
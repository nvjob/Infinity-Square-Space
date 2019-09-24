// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;



public class PlayerSoundWeapons : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public Vector2 defaultVolumeScale = new Vector2(0.7f, 1.0f);
    public Vector2 lightningVolumeScale = new Vector2(0.5f, 1.0f);
    public Vector2 laserVolumeScale = new Vector2(0.75f, 1.0f);
    public Vector2 pulseVolumeScale = new Vector2(0.5f, 1.0f);
    public Vector2 eraserVolumeScale = new Vector2(0.5f, 1.0f);
    public Vector2 collapsarVolumeScale = new Vector2(0.5f, 1.0f);
    public Vector2 destroyerVolumeScale = new Vector2(0.5f, 1.0f);
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

        if (Class_Controller.defaultMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.8f, 0.9f);
            else thisAudioSource.pitch = Random.Range(0.95f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(0, 2)], Random.Range(defaultVolumeScale.x, defaultVolumeScale.y));
        }

        //--------------

        if (Class_Controller.lightningMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.85f, 0.95f);
            else thisAudioSource.pitch = Random.Range(0.95f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(2, 4)], Random.Range(lightningVolumeScale.x, lightningVolumeScale.y));
        }

        //--------------

        if (Class_Controller.laserMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.85f, 0.95f);
            else thisAudioSource.pitch = Random.Range(0.98f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(4, 6)], Random.Range(laserVolumeScale.x, laserVolumeScale.y));
        }

        //--------------

        if (Class_Controller.pulseMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.85f, 0.95f);
            else thisAudioSource.pitch = Random.Range(0.98f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(6, 8)], Random.Range(pulseVolumeScale.x, pulseVolumeScale.y));
        }

        //--------------

        if (Class_Controller.eraserMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.85f, 0.95f);
            else thisAudioSource.pitch = Random.Range(0.98f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(8, 10)], Random.Range(eraserVolumeScale.x, eraserVolumeScale.y));
        }

        //--------------

        if (Class_Controller.collapsarMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.85f, 0.95f);
            else thisAudioSource.pitch = Random.Range(0.98f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(10, 12)], Random.Range(collapsarVolumeScale.x, collapsarVolumeScale.y));
        }

        //--------------

        if (Class_Controller.destroyerMouseButtonDown == true)
        {
            if (Class_Interface.powerEnabled == true) thisAudioSource.pitch = Random.Range(0.85f, 0.95f);
            else thisAudioSource.pitch = Random.Range(0.98f, 1.05f);
            thisAudioSource.panStereo = Random.Range(-0.3f, 0.3f);
            thisAudioSource.PlayOneShot(soundsFx[Random.Range(12, 14)], Random.Range(destroyerVolumeScale.x, destroyerVolumeScale.y));
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

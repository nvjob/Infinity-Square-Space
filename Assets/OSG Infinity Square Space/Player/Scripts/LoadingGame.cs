// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using UnityEngine.UI;



public class LoadingGame : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public Image backgroundImage;
    public Text messageText;
    public Text messageKeysText;

    //--------------

    float alpha0, alpha1, alpha2;
    bool trigger0, trigger1, trigger2;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        alpha0 = backgroundImage.color.a;
        alpha1 = messageText.color.a;
        alpha2 = messageKeysText.color.a;

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (Class_Interface.loading == false)
        {
            if (trigger0 == false && (alpha0 -= Time.deltaTime * 0.43f) <= 0.001f) trigger0 = true;
            if (trigger1 == false && (alpha1 -= Time.deltaTime * 0.5f) <= 0.001f) trigger1 = true;
            if (trigger2 == false && (alpha2 -= Time.deltaTime * 0.5f) <= 0.001f) trigger2 = true;
            if (trigger0 == true && trigger1 == true && trigger2 == true) gameObject.SetActive(false);

            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, alpha0);
            messageText.color = new Color(1, 1, 1, alpha1);
            messageKeysText.color = new Color(1, 1, 1, alpha2);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
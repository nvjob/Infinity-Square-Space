// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using UnityEngine.UI;



public class InitialScreen : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public Image backgroundImage;
    public Text logoText, versionText;

    //--------------

    float alpha0, alpha1;
    bool trigger0;
    int counter0;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        logoText.text = "Infinity Square/Space";
        versionText.text = "Master Version 1.0 by #NVJOB";
        logoText.color = versionText.color = new Color(1, 1, 1, 0);
        alpha1 = backgroundImage.color.a;

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (counter0 < 2)
        {
            if (trigger0 == false && (alpha0 += Time.deltaTime * 0.15f) >= 0.4f) trigger0 = true;
            if (trigger0 == true && (alpha0 -= Time.deltaTime * 0.225f) <= 0.001f)
            {
                trigger0 = false;
                counter0++;
                logoText.text = "The prototype of the game is open source";
                versionText.text = "GitHub.com/NVJOB/Infinity-Square-Space";
            }
            logoText.color = new Color(1, 1, 1, alpha0);
            versionText.color = new Color(1, 1, 1, alpha0);
        }
        else
        {
            if ((alpha1 -= Time.deltaTime * 0.65f) <= 0.001f) gameObject.SetActive(false);
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, alpha1);
        }

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

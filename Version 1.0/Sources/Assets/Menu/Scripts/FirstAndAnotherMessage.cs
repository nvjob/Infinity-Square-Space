// Infinity Square/Space. The prototype of the game is open source. V1.0
// https://github.com/nvjob/Infinity-Square-Space
// #NVJOB Nicholas Veselov
// https://nvjob.pro
// MIT license (see License_NVJOB.txt)



using UnityEngine;
using UnityEngine.UI;



public class FirstAndAnotherMessage : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public Image backgroundImage;
    public Text messageText;
    public Text messageKeysText;
    public Button okButton;

    //--------------

    Text okButtonText;
    float alpha0, alpha1;
    bool trigger0, trigger1, okTrigger;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //--------------

        okButton.onClick.AddListener(_OkButton);
        okButtonText = okButton.GetComponent<Text>();
        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);
        if (messageText != null) messageText.color = new Color(1, 1, 1, 0);
        if (messageKeysText != null) messageKeysText.color = new Color(1, 1, 1, 0);
        okButtonText.color = new Color(1, 1, 1, 0);

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //--------------

        if (okTrigger == false)
        {
            if (trigger0 == false && (alpha0 += Time.deltaTime * 0.5f) >= 0.8f) trigger0 = true;
            if (trigger1 == false && (alpha1 += Time.deltaTime * 0.25f) >= 0.4f) trigger1 = true;
        }
        else
        {
            if (trigger0 == true && (alpha0 -= Time.deltaTime * 0.9f) <= 0.001f) trigger0 = false;
            if (trigger1 == true && (alpha1 -= Time.deltaTime * 0.45f) <= 0.001f) trigger1 = false;
            if (trigger0 == false && trigger1 == false) gameObject.SetActive(false);
        }

        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, alpha0);
        if (messageText != null) messageText.color = okButtonText.color = new Color(1, 1, 1, alpha1);
        if (messageKeysText != null) messageKeysText.color = okButtonText.color = new Color(1, 1, 1, alpha1);

        //--------------
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void _OkButton()
    {
        //--------------

        okTrigger = true;

        //--------------
    }

    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

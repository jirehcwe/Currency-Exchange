/*
*  Copyright Jireh Chew / fractalD
*
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalisedText : MonoBehaviour
{

    public string textKey;
    Text localisedText;

    void Start()
    {
      localisedText = this.GetComponent<Text>();
    }
    void Update()
    {
      localisedText.text = GameManager.instance.Localiser[(int)GameManager.instance.languageSetting].GetTranslation(textKey);
    }
}

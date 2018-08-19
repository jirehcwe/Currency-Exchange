using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {
    public Settings settings;
    public Toggle mixedToggle;
    public Slider difficultySlider;
    public GameObject popUp;
    public Text difficultyLevel;
    // Use this for initialization
    void Start () {
        settings.mixedToggle = mixedToggle.isOn;
    }
	
	// Update is called once per frame
	void Update () {
        settings.mixedToggle = mixedToggle.isOn;
        /*
        settings.difficultySlider = difficultySlider.value;
        if (difficultySlider.value < 0.3333)
        {
            difficultyLevel.text = "Easy";
            settings.MaxAddRielsNum = 3;
            settings.MaxAddUsdNum = 3;
            settings.maxRielsValue = 12000;
            settings.bombSpeed = 0.05f;
        }
        else if (difficultySlider.value < 0.6666)
        {
            difficultyLevel.text = "Intermediate";
            settings.MaxAddRielsNum = 5;
            settings.MaxAddUsdNum = 5;
            settings.maxRielsValue = 40000;
            settings.bombSpeed = 0.1f;
        }
        else
        {
            difficultyLevel.text = "Hard";
            settings.MaxAddRielsNum = 7;
            settings.MaxAddUsdNum = 7;
            settings.maxRielsValue = 90000;
            settings.bombSpeed = 0.15f;
        }
        */
    }

    public void startPopUp()
    {
        popUp.SetActive(true);
    }

    public void closePopUp()
    {
        popUp.SetActive(false);
    }

    public void startMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}

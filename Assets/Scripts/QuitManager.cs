using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitManager : MonoBehaviour {
    public GameObject popUp;
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void startPopUp()
    {
        popUp.SetActive(true);
    }

    public void closePopUp()
    {
        popUp.SetActive(false);
    }
    public void startMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

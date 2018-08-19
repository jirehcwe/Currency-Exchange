using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour {

    public Image comboBar;
    public UnityEvent exceededAmount;

    [SerializeField]
    private float comboDecay = 0.1f;

	void Start () {
		
	}

	void Update () {
        comboBar.fillAmount -= comboDecay * Time.deltaTime;
        if (Input.GetKey(KeyCode.Return))
            comboBar.fillAmount = 1;
    }

    void updatePoints()
    {

    }

    void refreshCombo()
    {
        

    }
}

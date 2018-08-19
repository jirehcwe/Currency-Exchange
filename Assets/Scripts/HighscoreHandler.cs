using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour {
	RectTransform rectTrans;
    public GameObject inputHandler;



    Vector3 rectVelocity = Vector3.zero;
	float smoothTime = 0.5f;
    bool isGameOver;

	// Use this for initialization
	void Start () {
		rectTrans = this.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        isGameOver = GameManager.instance.isGameOver;

        if (isGameOver == true)
        {
            PanelSlideIn();
            GameManager.instance.GetComponent<InputHandler>().enabled = false;
        }
        else
        {
            PanelSlideOut();
            GameManager.instance.GetComponent<InputHandler>().enabled = true;
        }
    }

    public void PanelSlideIn()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, Vector3.zero);
        rectTrans.position = Vector3.SmoothDamp(rectTrans.position, screenPoint, ref rectVelocity, smoothTime);
    }

    public void PanelSlideOut()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(0, Screen.height*2, 0));
        rectTrans.position = Vector3.SmoothDamp(rectTrans.position, screenPoint, ref rectVelocity, smoothTime);
    }
}

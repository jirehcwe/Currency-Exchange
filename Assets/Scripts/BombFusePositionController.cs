using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombFusePositionController : MonoBehaviour {

    public Transform fuseEndPoint;
    private Image fuseImage;
    float yPos;
    float xPos;
    float xDistance;
    float yDistance;
    public float period;
    public float amplitude;
    public float xOffset;
    public float yOffset;
    public float rotationPeriod;
    public float rotationAmplitude;
    public float rotationOffset;

    public Vector3 fusePosOffset;

    // Use this for initialization
    void Start () {
        fuseImage = this.transform.parent.GetComponent<Image>();
        xDistance = this.transform.localPosition.x - fuseEndPoint.localPosition.x;
        yDistance = fuseEndPoint.localPosition.y + yOffset;
    }
	
	// Update is called once per frame
	void Update () {
        xPos = xDistance * fuseImage.fillAmount + fuseEndPoint.transform.localPosition.x;
        yPos = amplitude*Mathf.Sin(period * xDistance * fuseImage.fillAmount + xOffset) - yOffset;
        float zRotation = rotationAmplitude * Mathf.Sin(rotationPeriod * period * xDistance * fuseImage.fillAmount + xOffset + rotationOffset);
        this.transform.localPosition = new Vector3(xPos, yPos, 0);
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, zRotation);
        // Debug.Log(transform.localPosition);
	}
}

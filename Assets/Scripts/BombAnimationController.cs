using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombAnimationController : MonoBehaviour {

    public GameObject fuseImg;
    public float fuseFill;
    public float bombGrowthThreshold = 0.2f;
    public float bombUrgencyThreshold = 0.2f;
    public float animationSpeedMultiplier = 3f;
    public float bombGrowthMultiplier = 3f;
    public float bombGrowthSpeed;
    Vector3 originalScale;
    Vector3 referenceVel;
    Vector3 referenceVel2;
    // Use this for initialization
    void Start () {
        originalScale = this.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        fuseFill = fuseImg.GetComponent<Image>().fillAmount;
        if (fuseFill < bombGrowthThreshold && fuseFill > bombUrgencyThreshold)
        {
            this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, new Vector3(0.5f* bombGrowthMultiplier, 0.5f * bombGrowthMultiplier, 1), ref referenceVel, bombGrowthSpeed);
        }
        else if (fuseFill < bombUrgencyThreshold)
        {
            StartCoroutine(SetAnimationSpeed(animationSpeedMultiplier));
        }
        else if (fuseFill > bombUrgencyThreshold)
        {
            StartCoroutine(SetAnimationSpeed(1f));
            this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, originalScale, ref referenceVel, bombGrowthSpeed / 10);
        }
        
	}

    IEnumerator SetAnimationSpeed(float speed)
    {
        this.GetComponent<Animator>().speed = speed;
        yield return null;
    }
}

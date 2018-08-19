using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

    Transform target;
    Vector3 initialPos;
    public float remainingShakeTime;
    bool isShaking;
    float intensity;

	void Start () {
        target = this.transform;
        initialPos = target.localPosition;
        intensity = 1;
    }
	

	void Update () {
        if (remainingShakeTime>0 && isShaking == false && GameManager.instance.hasExploded == false)
        {
            StartCoroutine(DoShake());
        }
	}

    public void Shake(float duration, float intensity)
    {

        if (duration > 0)
        {
            initialPos = target.localPosition;
            remainingShakeTime += duration;
        }

        this.intensity = intensity;
    }

    public void Shake(GameObject objToShake, float duration, float intensity)
    {
        target = objToShake.transform;
        initialPos = target.localPosition;

        if (duration > 0)
        {
            
            remainingShakeTime += duration;
        }

        this.intensity = intensity;
    }

    IEnumerator DoShake()
    {
        GameManager.instance.hasExploded = true;
        isShaking = true;

        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + remainingShakeTime)
        {
            Vector3 randomPoint = new Vector3(initialPos.x + Random.Range(-1f, 1f)*intensity, initialPos.y + Random.Range(-1f, 1f)*intensity, target.localPosition.z);
            target.localPosition = randomPoint;
            // Debug.Log("stil shaking");
            yield return null;
        }

        isShaking = false;
        // Debug.Log("done shaking");
       
        intensity = 1;
        remainingShakeTime = 0;
        target.localPosition = initialPos;
    }
}

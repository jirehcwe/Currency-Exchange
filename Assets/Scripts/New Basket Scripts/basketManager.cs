using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketManager : MonoBehaviour {
    public GameObject groceryBasket, displayFruitGroup;
    public GameObject[] fruitGroups, prefabFruits;
    private int randNum, fruitNum, removeFruitGroupIndex=11, addFruitGroupIndex;
    private float targetRotateAngle = 0, randNumF;
    private bool reset;
    private GameObject fruit, fruitGroup;
    private int[] prices;

    // Use this for initialization
    void Start()
    {
    }

	// Update is called once per frame
	void Update () {
        if (reset == false && groceryBasket.transform.localEulerAngles.z < targetRotateAngle)
        {
            groceryBasket.transform.Rotate(new Vector3(0, 0, 1.2f));
        }
        else if (reset == true)
        {
            if (groceryBasket.transform.localEulerAngles.z > 180 && groceryBasket.transform.localEulerAngles.z > targetRotateAngle) groceryBasket.transform.Rotate(new Vector3(0, 0, 1.2f));
            if (groceryBasket.transform.localEulerAngles.z < 180) reset = false;
        }

        displayFruitGroup.transform.localPosition = new Vector3(-43, 123, 0);
        displayFruitGroup.transform.Translate(0, 3 * Mathf.Sin(1.5f * Time.time), 0);
    }



    void createNewFruitGroup(float price)
    {
        int[] limits = { 0, 0 };
        if (price < 5000) {
            fruitNum = 1;
            limits[0] = 0;
            limits[1] = 2;
        }
        else if (price < 9000)
        {
            fruitNum = 1;
            limits[0] = 3;
            limits[1] = 5;
        }
        else if (price < 16000)
        {
            fruitNum = 1;
            limits[0] = 6;
            limits[1] = 7;
        }
        else if (price < 25000)
        {
            fruitNum = 2;
            limits[0] = 0;
            limits[1] = 3;
        }
        else if (price < 30000)
        {
            fruitNum = 2;
            limits[0] = 0;
            limits[1] = 5;
        }
        else
        {
            fruitNum = 2;
            limits[0] = 0;
            limits[1] = 7;
        }
        for (int i = 0; i < fruitNum; i++)
        {
            randNum = Random.Range(limits[0], limits[1]);
            fruit = Instantiate(prefabFruits[randNum], fruitGroups[addFruitGroupIndex].transform);
            fruit.transform.localPosition = new Vector3(60f + i * 25, 5.4f, 0);
        }
    }

    void removeOldFruitGroup()
    {
        float sumwidth = 0;
        for (int i = 0; i < displayFruitGroup.transform.childCount; i++) Destroy(displayFruitGroup.transform.GetChild(i).gameObject);
        fruitGroup = Instantiate(fruitGroups[removeFruitGroupIndex], displayFruitGroup.transform, false);
        for (int j = 0; j < fruitGroup.transform.childCount; j++)
        {
            randNumF = Random.Range(1.1f, 1.4f);
            sumwidth = sumwidth + (int)(25*randNumF);
        }
        for (int j = 0; j < fruitGroup.transform.childCount; j++)
        {
            fruitGroup.transform.GetChild(j).gameObject.transform.localPosition = new Vector3(-sumwidth/2 + j * 30, 0, 0);
        }
        fruitGroup.transform.localEulerAngles = new Vector3(0, 0, 0);
        for (int i = 0; i < fruitGroups[removeFruitGroupIndex].transform.childCount; i++) Destroy(fruitGroups[removeFruitGroupIndex].transform.GetChild(i).gameObject);
    }

    public void startGame()
    {
        addFruitGroupIndex = 0;
        createNewFruitGroup(0);
        addFruitGroupIndex = 1;
        createNewFruitGroup(0);
        addFruitGroupIndex = 2;
        createNewFruitGroup(0);
    }

    public void nextBasket(float cost)
    {
        targetRotateAngle += 30;
        removeFruitGroupIndex += 1;
        if (removeFruitGroupIndex == 12) removeFruitGroupIndex -= 12;
        if (removeFruitGroupIndex == 11) {
            targetRotateAngle -= 360;
            reset = true;
        }
        addFruitGroupIndex = removeFruitGroupIndex + 3;
        if (addFruitGroupIndex >11) addFruitGroupIndex -= 12;
        createNewFruitGroup(cost);
        removeOldFruitGroup();
    }
}
//Ting Yit's part

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProblemManager : MonoBehaviour {

    List<GameObject> groceryItems = new List<GameObject>();
    public GameObject groceryBasket;
    public GameObject[] prefabs;
    public float[] itemCost;
    float totalCost = 0;
    float randNum;
    int itemCount = 0;
    public int maxItems = 6;
    public int xDisplace = -120;
    public int xSpacing = 35;
    public Text totalCostText;
    public float requiredAmount;
    public ProblemAndAnswer newProblem;
    public Wallet wallet;
        
    public float GetTotalCost()
    {
        return totalCost;
    }
    float createNewBasket()
    {
        for (int i=0; i< groceryItems.Count; i++){
            Destroy(groceryItems[i]);
        }
        groceryItems.Clear();
        itemCount = 0;
        totalCost = 0;
        for(int i = 0; i < 5; i++)
        {
            if (itemCount >= maxItems) break;
            randNum = Random.Range(0, 2);
            for (int j = 0; j < randNum; j++)
            {
                if (itemCount >= maxItems) break;
                groceryItems.Add(Instantiate(prefabs[i]) as GameObject);
                groceryItems[groceryItems.Count - 1].transform.SetParent(groceryBasket.transform, false);
                groceryItems[groceryItems.Count - 1].transform.localPosition = new Vector3(xDisplace, 0, 0);
                groceryItems[groceryItems.Count - 1].transform.localPosition += new Vector3(xSpacing*itemCount, 0, 0);
                groceryItems[groceryItems.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                itemCount++;
                totalCost += itemCost[i];
            }


        }

        return totalCost;

    }

    public void GenerateNewProblem(){
        float problemValue = 100 * Random.Range(5, (int)GameManager.instance.currentProblemLimitInRiels/100);      
        newProblem = new ProblemAndAnswer(problemValue);
        requiredAmount = newProblem.GetUSDSum();
//        wallet.createNoteInWallet(newProblem);
    }


}



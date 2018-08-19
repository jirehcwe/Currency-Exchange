//Ting Yit's part

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProblemManager : MonoBehaviour {

    List<GameObject> groceryItems = new List<GameObject>();
    //public GameObject groceryBasket;  // OLD CODE-> CONSIDER DELETING
    public GameObject[] prefabs;
    public Settings settings;
    public float[] itemCost;
    float totalCost = 0;
    float randNum;
    int itemCount = 0;
    public int maxItems = 6;
    public int xDisplace = -120;
    public int xSpacing = 35;
    public Text totalCostText;
    public float requiredAmount;
    public static ProblemAndAnswer newProblem;
    public Wallet wallet;
    public basketManager basketMgr;
        
    public float GetTotalCost()
    {
        return totalCost;
    }

    //OLD CODE -> CONSIDER DELETING
   /* float createNewBasket()
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

    }*/

    public void GenerateNewProblem(){
        float problemValue = 100 * Random.Range((int)GameManager.instance.currentProblemMinInRiels/100, (int)GameManager.instance.currentProblemLimitInRiels/100);
        newProblem = new ProblemAndAnswer(problemValue);
        requiredAmount = newProblem.GetUSDSum();
        basketMgr.nextBasket(newProblem.GetRielSum());
        // Debug.Log("generated new problem");
        List<Note> display = newProblem.GetRielAnswer();
        // for (int i = 0; i < display.Count; i++)
        // {
        //     Debug.Log("Notes passed: " + display[i].name);
        // }
        //  ///wallet.createNoteInWalletRiels(newProblem);
          if (settings.mixedToggle)
              wallet.createNoteInWalletMixed(newProblem);
          else
            wallet.createNoteInWalletRiels(newProblem);
    }


}



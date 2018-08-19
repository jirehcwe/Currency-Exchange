/*
*	Copyright Jireh Chew / fractalD
*
*
*/


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{

    #region GameManager stuff

    public static GameManager instance;
    public ProblemManager probGen;

    [Space(10)]
    [Header("UI")]

    public Image bombFuse;
    public Text currentBalanceText;
    public Text requiredUSDText;
    public Text requiredKHRText;
    public Text scoreText;
    public GameObject bombExplosionAnimation;
    public GameObject bombObj;
    public GameObject highscorePanel;
    public GameObject restartButton;
    public Image cashierImg;
    public Sprite[] cashierImgArray;
    public Image cashierMachineImg;
    public Sprite[] cashierMachineArray;
    public float restartButtonDelay = 1.5f;
    public GameObject cashRain;
    public GameObject shaker;
    public bool hasExploded = false;
    public basketManager bsktMgr;

    [Space(10)]
    [Header("Localisation Settings")]
    public List<Localisation> Localiser;

    public Language languageSetting = Language.English;

    public enum Language{
        English = 0,
        Khmer = 1,

        //must be same order as languages in Localiser
    }

    [Space(10)]
    [Header("Game Settings")]

    public GameObject playerWallet;
    public GameObject playerTable;
    public Settings settings;
    public float baseComboDecay;
    public float currentBalance = 0;
    public float requiredAmt;
    public bool isGameOver = false;


    private float threshold = 0.0001f;
    private float comboDecay;
    float previousBalance;

    [Space(10)]
    [SerializeField]
    public int score = 0;



    [Space(10)]
    [Header("Currency Settings")]

    public float smallestNoteDenomination = 100;
    public float rielsToUSDExchangeRate = 4000;
    public float maxSumLimitInRiels = 30000;
    public float currentProblemLimitInRiels;


    #endregion

    void Start()
    {
       //TODO: languageSetting = settings.x
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        baseComboDecay = settings.bombSpeed;
        comboDecay = baseComboDecay;
        currentProblemLimitInRiels = settings.maxRielsValue;
        bsktMgr.startGame();
        ResetProblem();
        UpdateUIVisuals();

    }

    void Update()
    {

        if (isGameOver == false)
        {
            MainGameplayLoop();
        }
        UpdateUIVisuals();
    }

    private void MainGameplayLoop()
    {
        currentBalance = DropZone.SumOfZoneInUSD(playerTable.GetComponent<DropZone>());
        //if (currentBalance != requiredAmt)
        //{

        //}

        if (currentBalance != previousBalance)
        {
            UpdatePoints();
            // Debug.Log("Current balance : " + currentBalance);
            // Debug.Log("required balance : " + probGen.requiredAmount);
        }
        previousBalance = currentBalance;
        requiredAmt = probGen.requiredAmount;
    }



    public void UpdatePoints()
    {
        // Debug.Log("Update Points called");

        if (((currentBalance - probGen.requiredAmount) * (currentBalance - probGen.requiredAmount)) < threshold)
        {
            ResetBomb();
            ResetProblem();
            score += 100;
        }

    }

    public void UpdateUIVisuals()
    {
        if (isGameOver == false)
        {
            cashRain.SetActive(false);
            currentBalanceText.text = "KHR" + (currentBalance * rielsToUSDExchangeRate).ToString("F0");
            requiredUSDText.text = ProblemManager.newProblem.GetUSDSum().ToString();
            requiredKHRText.text = ProblemManager.newProblem.GetRielSum().ToString("F0");
            //TODO: display either khmer or USD not both
            //TODO: rework difficulty system: scale with points/streak
            scoreText.text = Localiser[(int)languageSetting].Translate("Score") + ": " + score;
            TickBomb(); 
            if (currentBalance <= probGen.requiredAmount)
            {
                if (cashierImgArray[0] != null)
                    cashierImg.sprite = cashierImgArray[0];
                if (cashierMachineArray[0] != null)
                    cashierMachineImg.sprite = cashierMachineArray[0];
            }
            else if (currentBalance > probGen.requiredAmount)
            {
                //play error sound 
                //highlight previous note?
                //make auntie angry
                if (cashierImgArray[2] != null)
                    cashierImg.sprite = cashierImgArray[2];
            }

        }
        else if (isGameOver == true)
        {
            bombExplosionAnimation.SetActive(true);
            if (hasExploded == false)
            {
                shaker.GetComponent<Shaker>().Shake(2, 2);
            }   

            if (cashierImgArray[1] != null)
                cashierImg.sprite = cashierImgArray[1];
            if (cashierMachineArray[1] != null)
                cashierMachineImg.sprite = cashierMachineArray[1];

            cashRain.SetActive(true);
            //bomb graphic needs to change to exploded shell

            //display bomb auntie, shell of bomb and strewn notes (falling from the sky? cashier broken?)
        }
    }


    public void ResetBomb()
    {
        bombFuse.fillAmount = 1;
    }

    public void TickBomb()
    {
        if (isGameOver == false)
        {
            if (bombFuse.fillAmount > 0)
            {
                bombFuse.fillAmount -= comboDecay * Time.deltaTime;
            }
            else if (bombFuse.fillAmount <= 0)
            {
                isGameOver = true;
                bombFuse.fillAmount = 1;
                comboDecay = baseComboDecay;
            }
        }

    }
    public void ResetProblem()
    {
        //TODO: enter button to confirm
        DropZone.ClearDropzone(playerTable);
        DropZone.ClearDropzone(playerWallet);
        currentBalance = 0;
        probGen.GenerateNewProblem(); //duplicate?
        probGen.requiredAmount = ProblemManager.newProblem.GetUSDSum();
        bombExplosionAnimation.SetActive(false);
        isGameOver = false;
    }


}

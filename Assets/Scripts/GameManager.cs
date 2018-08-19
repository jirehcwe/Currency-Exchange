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


    public Text currentBalanceText;
    public Text requiredUSDText;
    public Text requiredKHRText;
    public Text USDText;
    public Text KHRText;
    public Text scoreText;
    public GameObject bombExplosionAnimation;
    public GameObject bombObj;
    public GameObject bombImg;
    public Image bombFuseImg;
    public GameObject highscorePanel;
    public GameObject restartButton;
    public Image cashierImg;
    public Sprite[] cashierImgArray;
    public Image cashierMachineImg;
    public Sprite[] cashierMachineArray;
    public GameObject angerImg;
    public float restartButtonDelay = 1.5f;
    public GameObject cashRain;
    public Color[] scoreColors;
    private Color scoreColor;
    public bool hasExploded = false;

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
    public basketManager basketMgr;
    public Settings settings;
    public float baseComboDecay;
    public float currentBalance = 0;
    public float requiredAmt;
    public bool isGameOver = false;
    public float timerRefill = 0.18f;



    private float threshold = 0.0001f;
    private float comboDecay;
    float previousBalance;

    [Space(10)]
    [SerializeField]
    public int score = 0;
    public int scoreStep = 100;


    [Space(10)]
    [Header("Currency Settings")]

    public float smallestNoteDenomination = 100;
    public float rielsToUSDExchangeRate = 4000;
    public float currentProblemLimitInRiels;
    public float currentProblemMinInRiels;

    [Space(10)]
    [Header("Audio Settings")]
    public AudioPlayer audioPlayer;

    #endregion

    void Start()
    {
        angerImg.SetActive(false);
        audioPlayer = GameObject.FindWithTag("Audio").GetComponent<AudioPlayer>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyObject(this);
        }
        ResetSettingsToBase();
        scoreColor = scoreColors[0];
        basketMgr.startGame();
        ResetProblem();
        ResetProblem();
        UpdateUIVisuals();
    }

    void Update()
    {
        //audioPlayer.bombTick.volume = bombFuseImg.fillAmount;
        baseComboDecay = settings.bombSpeed;
        comboDecay = baseComboDecay;
        currentProblemLimitInRiels = settings.maxRielsValue;
        currentProblemMinInRiels = settings.minRielsValue;
        if (isGameOver == false)
        {
            MainGameplayLoop();
        }
        UpdateUIVisuals();
    }

    private void MainGameplayLoop()
    {
        if (bombFuseImg.fillAmount < 0.2)
        {
            audioPlayer.StartBombTick();
        }
        else
        {
            audioPlayer.StopBombTick();
        }
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
            score += scoreStep;
            audioPlayer.CorrectResult();
        }

        difficultyManager(score);

    }
    private bool toggleMoneyShown;
    private int toggleCounter = 0;
    private int toggleThreshold = 5;

    public void difficultyManager(int score)
    {
        
        if (score < 500)
        {
            settings.MaxAddUsdNum = 1;
            settings.MinAddUsdNum = 0;
            settings.MaxAddRielsNum = 2;
            settings.MinAddRielsNum = 1;
            scoreColor = scoreColors[0];
            Debug.Log("score less than 500");
            showAllValues();
        }
        else if (score < 1000 && score >= 500)
        {
            settings.MaxAddUsdNum = 1;
            settings.MinAddUsdNum = 0;
            settings.MaxAddRielsNum = 3;
            settings.MinAddRielsNum = 1;
            settings.maxRielsValue = 9000;
            settings.minRielsValue = 5000;
            scoreColor = scoreColors[1];
            Debug.Log(scoreColor);
            scoreStep = 150;
            showAllValues();
        }
        else if (score <3000 && score >= 1000)
        {
            settings.MaxAddUsdNum = 1;
            settings.MinAddUsdNum = 1;
            settings.MaxAddRielsNum = 4;
            settings.MinAddRielsNum = 1;
            settings.maxRielsValue = 16000;
            settings.minRielsValue = 9000;
            scoreColor = scoreColors[2];
            scoreStep = 200;
            showAllValues();
        }
        else if(score < 6000 && score >= 3000)
        {
            settings.bombSpeed = 0.03f;
            settings.MaxAddUsdNum = 2;
            settings.MinAddUsdNum = 2;
            settings.MaxAddRielsNum = 4;
            settings.MinAddRielsNum = 1;
            settings.maxRielsValue = 25000;
            settings.minRielsValue = 14000;
            scoreColor = scoreColors[3];
            scoreStep = 250;
            toggleThreshold = 3;
            ChangeValueShown(toggleMoneyShown);

        }
        else if (score < 9000 && score >= 6000)
        {
            settings.bombSpeed = 0.06f;
            settings.maxRielsValue = 30000;
            settings.minRielsValue = 20000;
            scoreStep = 300;
            settings.MaxAddUsdNum = 2;
            settings.MinAddUsdNum = 1;
            settings.MaxAddRielsNum = 4;
            settings.MinAddRielsNum = 1;
            scoreColor = scoreColors[4];
            toggleThreshold = 2;
            ChangeValueShown(toggleMoneyShown);

        }
        else if (score <15000 && score >= 9000)
        {
            settings.bombSpeed = 0.09f;
            settings.maxRielsValue = 50000;
            settings.minRielsValue = 30000;
            settings.MaxAddUsdNum = 3;
            settings.MinAddUsdNum = 1;
            settings.MaxAddRielsNum = 4;
            settings.MinAddRielsNum = 1;
            scoreColor = scoreColors[5];
            toggleThreshold = 1;
            ChangeValueShown(toggleMoneyShown);
        } else
        {
            settings.bombSpeed = 0.15f;
            settings.maxRielsValue = 80000;
            settings.minRielsValue = 50000;
            settings.MaxAddUsdNum = 4;
            settings.MinAddUsdNum = 2;
            settings.MaxAddRielsNum = 4;
            settings.MinAddRielsNum = 2;
            scoreColor = scoreColors[6];
            toggleThreshold = 1;
            ChangeValueShown(toggleMoneyShown);
        }
        
        

    }


    public void UpdateUIVisuals()
    {
        if (isGameOver == false)
        {
            angerImg.SetActive(false);
            bombImg.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            bombFuseImg.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            cashRain.SetActive(false);
            currentBalanceText.text = "KHR$" + (currentBalance * rielsToUSDExchangeRate).ToString("F0");
            requiredUSDText.text = ProblemManager.newProblem.GetUSDSum().ToString();
            requiredKHRText.text = ProblemManager.newProblem.GetRielSum().ToString("F0");
            scoreText.text = "Score: " + score;
            scoreText.color = scoreColor;
            TickBomb();
            if (currentBalance <= probGen.requiredAmount)
            {
                if (cashierImgArray[0] != null)
                    cashierImg.sprite = cashierImgArray[0];
                if (cashierMachineArray[0] != null)
                    cashierMachineImg.sprite = cashierMachineArray[0];

                if (audioPlayer.auntieIsPlaying == true)
                {
                    if (audioPlayer.angryAuntie.isPlaying == false)
                    {
                        audioPlayer.auntieIsPlaying = false;
                    }
                }
            }
            else if (currentBalance > probGen.requiredAmount)
            {
                //play error sound 
                //highlight previous note?
                //make auntie angry
                angerImg.SetActive(true);
                if (cashierImgArray[2] != null)
                    cashierImg.sprite = cashierImgArray[2];
                audioPlayer.AngryAuntie();
            }

        }
        else if (isGameOver == true)
        {
            audioPlayer.StopBombTick();
            audioPlayer.BombExplosion();
            bombImg.GetComponent<Image>().color = new Color (1, 1, 1, 0);
            bombFuseImg.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            bombExplosionAnimation.SetActive(true);
            
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
        Debug.Log("adding");
        bombFuseImg.fillAmount += timerRefill* baseComboDecay;
        Debug.Log("added" + timerRefill * baseComboDecay);
    }

    public void TickBomb()
    {
        if (isGameOver == false)
        {
            if (bombFuseImg.fillAmount > 0)
            {
                bombFuseImg.fillAmount -= comboDecay * Time.deltaTime;
            }
            else if (bombFuseImg.fillAmount <= 0)
            {
                isGameOver = true;
                audioPlayer.StopBombTick();
                bombFuseImg.fillAmount = 1;
                comboDecay = baseComboDecay;

            }
        }

    }
    public void ResetProblem()
    {
        DropZone.ClearDropzone(playerTable);
        DropZone.ClearDropzone(playerWallet);
        currentBalance = 0;
        toggleCounter++;
        
        if (toggleCounter == toggleThreshold)
        {
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                toggleMoneyShown = true;
            }
            else toggleMoneyShown = false;
            Debug.Log("Toggle money shown!! " + toggleMoneyShown);
            toggleCounter = 0;
        }
        probGen.GenerateNewProblem(); //duplicate?
        probGen.requiredAmount = ProblemManager.newProblem.GetUSDSum();
        bombExplosionAnimation.SetActive(false);
        isGameOver = false;
        

    }

    public void ResetGame()
    {
        ResetSettingsToBase();
        ResetProblem();
    }

    public void Cheat()
    {
        if (Input.GetKey(KeyCode.Equals))
        {
            score += 3000;

        }

        if (Input.GetKey(KeyCode.G))
        {
            bombFuseImg.fillAmount -= 0.2f;
        }
    }
    public void ChangeValueShown(bool toggleMoneyShown)
    {
        if (toggleMoneyShown)
        {
            requiredUSDText.gameObject.SetActive(false);
            USDText.gameObject.SetActive(false);
            KHRText.gameObject.SetActive(true);
            requiredKHRText.gameObject.SetActive(true);
        }
        else
        {
            requiredUSDText.gameObject.SetActive(true);
            USDText.gameObject.SetActive(true);
            KHRText.gameObject.SetActive(false);
            requiredKHRText.gameObject.SetActive(false);
        }

    }
    public void showAllValues()
    {
        Debug.Log("showing all values");
        requiredUSDText.gameObject.SetActive(true);
        USDText.gameObject.SetActive(true);
        KHRText.gameObject.SetActive(true);
        requiredKHRText.gameObject.SetActive(true);
    }

    public void ResetSettingsToBase()
    {
        settings.bombSpeed = 0.01f;
        settings.maxRielsValue = 5000;
        settings.minRielsValue = 500;
        settings.MaxAddRielsNum = 1;
        settings.MinAddRielsNum = 1;
        settings.MaxAddUsdNum = 0;
        settings.MinAddUsdNum = 0;
        showAllValues();
        baseComboDecay = settings.bombSpeed;
        comboDecay = baseComboDecay;
        currentProblemLimitInRiels = settings.maxRielsValue;
        currentProblemMinInRiels = settings.minRielsValue;
    }

    public void ResetIsExploding()
    {
        audioPlayer.isExploding = false;
    }

    public void StartBombTick()
    {
        audioPlayer.StartBombTick();
    }


    public void StopBombTick()
    {
        audioPlayer.StopBombTick();
    }
}

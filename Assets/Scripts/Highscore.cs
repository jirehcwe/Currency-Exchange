using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {
    public Text scoreText;
    public Text rankingText;
    public GameObject popup;
    public List<Text> nameList = new List<Text>();
    public List<Text> scoreList = new List<Text>();
    public InputField nameText;
    private int score;
    private List<Player> highscoreList = new List<Player>();
    int playerPrefCount = 0;
    public bool hasSetScore = false;

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
        scoreText.text = score.ToString();
        playerPrefCount = PlayerPrefs.GetInt("PlayerPrefCount");
        //Debug.Log("PlayerPrefCount is at : " + playerPrefCount);
        loadList();
        displayHighScores();
        //Debug.Log("Playerpref first item is: " + PlayerPrefs.GetInt("HighScore" + 1));
        //Debug.Log("List count: " + highscoreList.Count);

	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.isGameOver)
        {
            
            if (hasSetScore == true)
            {
                //Debug.Log("set score liao");
                popup.SetActive(false);
                
            }
            else
            {
                //Debug.Log("haven set score");
                popup.SetActive(true);
               // GameManager.instance.GetComponent<InputHandler>().enabled = false;
                
            }

            float highScore = GameManager.instance.score;
            int i;
           
            //Debug.Log("Setting high score...");
            scoreText.text = highScore.ToString();
            if (highscoreList.Count == 0)
            {
                i = 0;
            }
            else
            {
                for (i = 0; i < highscoreList.Count; i++)
                {

                    if (highScore > highscoreList[i].getScore())
                    {
                        //Debug.Log("i value is: " + i);
                        break;
                    }

                }
            }
            Debug.Log("i value is: " + i);
            if (i < 10)
            {
                //Debug.Log("WENT INTO TOP 10 " + i);

                rankingText.text = "Great job! you are in the top 10!";
            }
            if (i > 9 && i < 50)
            {
                Debug.Log("WENT INTO TOP 50 " + i);
                rankingText.text = "Wow! You are getting there! Ranked: " + i;
            }
            if (i > 49 && i < 200)
            {
                rankingText.text = "Almost there! Keep trying! Ranked: " + i;
            }
            if (i > 200)
            {
                rankingText.text = "Oh no, a little practice goes a long way!";
            }
        }


	}

    void printList()
    {
        Debug.Log("Printing list.........");
        for (int i = 0; i < highscoreList.Count; i++)
        {
            Debug.Log(highscoreList[i].getScore());
        }
    }


    public void showSubmissionPanel()
    {
        GameManager.instance.score = 0;
        hasSetScore = false;
        GameManager.instance.ResetSettingsToBase();
        
    }

    public void setHighScore()
    {
        float highScore = GameManager.instance.score;
        int i;
        hasSetScore = true;
        // setScore = false;
        Debug.Log("Setting high score...");
        if (highscoreList.Count == 0)
        {
            i = 0;
        }
        else
        {
            for (i = 0; i < highscoreList.Count; i++)
            {

                if (highScore > highscoreList[i].getScore())
                {
                    //Debug.Log("i value is: " + i);
                    break;
                }

            }
        }
        if (highscoreList.Count > 200 && highscoreList[199].getScore() > highScore)
            Debug.Log("Score beyond 200th place");
        else
        {
            PlayerPrefs.SetInt("PlayerPrefCount", ++playerPrefCount);
            Player newPlayer = new Player();
            newPlayer.setName(nameText.text);
            newPlayer.setScore((int)highScore);
            highscoreList.Insert(i, newPlayer);
            Debug.Log("new player inserted");
            for (int j = 0; j < playerPrefCount; j++)
            {
                PlayerPrefs.SetInt("HighScore" + j, highscoreList[j].getScore());
                PlayerPrefs.SetString("Name" + j, highscoreList[j].getName());
            }
            if (highscoreList.Count > 200)
            {
                highscoreList.Remove(highscoreList[200]);
            }
        }

        displayHighScores();
        

    }

    void loadList()
    {
        
        for (int i = 0; i < playerPrefCount; i++)
        {
            Player player = new Player();
            player.setScore(PlayerPrefs.GetInt("HighScore" + i));
            player.setName(PlayerPrefs.GetString("Name" + i));
            highscoreList.Add(player);
        }
    }
    void displayHighScores()
    {
        int displayCount = 10;
        if (playerPrefCount < 10)
        {
            displayCount = playerPrefCount;
        }
        for (int i = 0; i < playerPrefCount; i++)
        {
            nameList[i].text = highscoreList[i].getName();
            scoreList[i].text = highscoreList[i].getScore().ToString();
        }
    }
}

public class Player {
    private string name;
    private int score;
    public void setName(string name)
    {
        this.name = name;
    }
    public void setScore(int score)
    {
        this.score = score;
    }

    public string getName()
    {
        return name;
    }
    public int getScore()
    {
        return score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioPlayer : MonoBehaviour {

    public AudioSource mainMenuBGM;
    public AudioSource playingBGM;
    public AudioSource sendMoney;
    public AudioSource removeMoneyFromTable;
    public AudioSource correctResult;
    public AudioSource angryAuntie;
    public AudioSource bombExplosion;
    public AudioSource bombTick;

    bool isPlayingBGM = false;
    bool mainMenuBGMPlaying = false;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            mainMenuBGM.Play();
            mainMenuBGMPlaying = true;
        }
        isExploding = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "Main" && isPlayingBGM == false)
        {
            mainMenuBGM.Stop();
            playingBGM.Play();
            isPlayingBGM = true;
            mainMenuBGMPlaying = false;
        }

        if (SceneManager.GetActiveScene().name == "Main Menu" && mainMenuBGMPlaying == false)
        {
            isPlayingBGM = false;
            playingBGM.Stop();
            mainMenuBGM.Play();
            mainMenuBGMPlaying = true;
        }
	}

    public void SendMoney()
    {
        sendMoney.PlayOneShot(sendMoney.clip);
    }

    public void RemoveMoney()
    {
        removeMoneyFromTable.PlayOneShot(removeMoneyFromTable.clip);
    }

    public bool auntieIsPlaying = false;

    public void AngryAuntie()
    {

        
        if (auntieIsPlaying == false)
        {
            auntieIsPlaying = true;
            angryAuntie.Play();
        }
    }

    public void CorrectResult()
    {
        correctResult.Play();
    }


    [HideInInspector]
    public bool isExploding = false;

    public void BombExplosion()
    {
        if (isExploding ==  false)
        {
            isExploding = true;
            bombExplosion.Play();
            
        }       
        
    }

    public void StopBombTick()
    {
        bombTick.Stop();
        isTicking = false;
    }

    public bool isTicking = false;

    public void StartBombTick()
    {
        if (isTicking == false)
        {
            isTicking = true;
            bombTick.Play();
        }
    }


}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float BGMLastTime;
    private float hypeMusicLastTime;
    private Vector3 cameraPosition;
    private bool isMatchOver;
    private bool isPlayerOneVictory;
    private Camera matchSetcamera;
    private Camera mainCamera;
    private Text uiText;
    private Button[] pauseButtons;
    private Button[] matchSetButtons;
    private bool isPaused;
    private GameObject pauseMenuObject;
    private GameObject MatchSetMenuObject;
    private GameObject nav;
    [SerializeField]
    private float matchTimer;
    [SerializeField]
    private Text matchTimerText;
    [SerializeField]
    private float rounds;
    [SerializeField]
    private float match;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Text ringOutText;
    private AudioManager[] playersTheme;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource menuSFX;
    [SerializeField]
    private AudioClip navChime;
    [SerializeField]
    private AudioClip navConfirm;
    [SerializeField]
    Image ringOut;
    [SerializeField]
    private GameObject playerBounds;
    [SerializeField]
    private AudioClip stageTheme;
    [SerializeField]
    private AudioClip playerOneTheme;
    [SerializeField]
    private AudioClip playerTwoTheme;
    private AudioClip[] playersHypeTheme;

    public float Match { get { return match; } set { match = value; } }
    public float Rounds { get { return rounds; } set { rounds = value; } }

    private void Awake()
    {
        stageTheme = GetComponent<AudioSource>().clip;
        ringOut.enabled = false;
        audioSource = GetComponent<AudioSource>();
        uiText = GameObject.Find("UIText").GetComponent<Text>();
        matchTimerText = GetComponentInChildren<Text>();
        pauseMenuObject = GameObject.FindGameObjectWithTag("ShowOnPause");
        MatchSetMenuObject = GameObject.FindGameObjectWithTag("MatchMenu");
        nav = GameObject.FindGameObjectWithTag("Nav");
        pauseButtons = new Button[2];
        matchSetButtons = new Button[2];
        foreach (Button button in pauseMenuObject.GetComponentsInChildren<Button>())
        {
            if (button.name.ToLower() == string.Format("resume"))
                pauseButtons[0] = button;
            else
                pauseButtons[1] = button;
        }
        foreach (Button button in MatchSetMenuObject.GetComponentsInChildren<Button>())
        {
            if (button.name.ToLower() == string.Format("rematch"))
                matchSetButtons[0] = button;
            else
                matchSetButtons[1] = button;
        }
        pauseMenuObject.SetActive(false);
        MatchSetMenuObject.SetActive(false);
        nav.transform.position = (pauseButtons[0].transform.position - new Vector3(110, 0, 0));
        
    }
    private void Start()
    {
        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
            {
                players[0] = player.GetComponent<Player>();
                playerOneTheme = player.GetComponent<AudioManager>().hypeMusic;
                //playersTheme[0] = player.GetComponent<AudioManager>();
            }
            else
            {
                players[1] = player.GetComponent<Player>();
                playerTwoTheme = player.GetComponent<AudioManager>().hypeMusic;
                // playersTheme[1] = player.GetComponent<AudioManager>();
            }
               
            rounds = 0;
            uiText.text = "";
        }
        
        foreach (var camera in GameObject.FindGameObjectsWithTag("camera"))
        {
            matchSetcamera = camera.GetComponent<Camera>();
        }
        foreach (var camera in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            mainCamera = camera.GetComponent<Camera>();
            cameraPosition = mainCamera.transform.position;
        }
        matchSetcamera.enabled = false;
    }

    private void Update()
    {
        RoundTimer();
        PauseMenu();
        RingOutVictory();
        MatchSet();
        StageTheme();
        PlayHypeMusic();
    }

    private void StageTheme()
    {
        if (!players[0].IsHyped && !players[1].IsHyped  && !isPaused && !isMatchOver)
        {
            if (BGMLastTime > 0.0f)
            { 
                if (audioSource.clip != stageTheme)
                {
                    audioSource.clip = stageTheme;
                    audioSource.time = BGMLastTime;
                    audioSource.Play();
                }
                else
                {
                    if(!audioSource.isPlaying)
                        audioSource.Play();
                        
                }
            }
            else if (!audioSource.isPlaying)
                audioSource.Play();
        }
        if (audioSource.clip == stageTheme && isMatchOver || 
            audioSource.clip == stageTheme && players[0].IsHyped ||
            audioSource.clip == stageTheme && players[1].IsHyped ||
            audioSource.clip == stageTheme && isPaused)
               BGMLastTime = audioSource.time;
    }
    private void PlayHypeMusic()
    {
        if(isPaused && audioSource.clip == playerOneTheme || isPaused && audioSource.clip == playerTwoTheme)
        {
            Debug.Log(hypeMusicLastTime);
            hypeMusicLastTime = audioSource.time;
        }
        if (players[0].IsHyped && !isPaused)
        {
            audioSource.clip = playerOneTheme;
            if (hypeMusicLastTime > 0.0f)
            {
                audioSource.time = hypeMusicLastTime;
                if (!audioSource.isPlaying)
                    audioSource.Play();
                hypeMusicLastTime = 0.0f;
            }
            else if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (players[1].IsHyped)
        {
            audioSource.clip = playerTwoTheme;
            audioSource.Play();
        }
    }
    private void RoundTimer()
    {
        if (!isMatchOver)
            matchTimer -= Time.deltaTime;
        if (matchTimer <= 0)
        {
            //matchTimer = 0;
            UpdateTimer();
            DetermineMomentumWinner();
        }
        else if(matchTimer > 0)
            UpdateTimer();
        
    }
    private void UpdateTimer()
    {
       
            int seconds = (int)(matchTimer % 60);
            matchTimerText.text = seconds.ToString();
        
        
    }
    private void DetermineMomentumWinner()
    {
       
        if (!isMatchOver)
        {
            var slider = gameObject.GetComponentInChildren<Slider>();

            if (slider.value > 50.0f)
            {
                //uiText.text = "Player 1 wins!";
                isPlayerOneVictory = true;
                isMatchOver = true;

            
                // playersTheme[0].StopHypeMusic();

            }
            else if(slider.value < 50.0f)
            {
                //uiText.text = "Player 2 wins!";
                isPlayerOneVictory = false;
                isMatchOver = true;
                // playersTheme[1].StopHypeMusic();
            }
            else
            {
                uiText.text = "DRAW!";
                isMatchOver = true;
            }
        }


    }
    
    private void PauseMenu()
    {
        if (PauseButton() && !isMatchOver)
        {
           if(!isPaused)
            {
               
                Time.timeScale = 0.0f;
                isPaused = true;
                audioSource.Pause();
                pauseMenuObject.SetActive(true);
                StartCoroutine("PauseNavigation");
            }
           
            
        }
    }
    private void PauseControls()
    {
        
        Vector3 resumeButton;
        Vector3 quitButton;
        if (isMatchOver)
        {

            Debug.Log(matchSetButtons[0].name);
            resumeButton = (matchSetButtons[0].transform.position - new Vector3(130, 10.0f, 0));
            quitButton = (matchSetButtons[1].transform.position - new Vector3(150, 1, 0));
            
            nav.transform.parent = MatchSetMenuObject.transform;
        }
        else
        {
            resumeButton = (pauseButtons[0].transform.position - new Vector3(110, 0, 0));
             quitButton = (pauseButtons[1].transform.position - new Vector3(110, 0, 0));
        }

        if (Navigation() == 1)
        {
            menuSFX.clip = navChime;
            if (nav.transform.position != resumeButton)
            {
                nav.transform.position = resumeButton;

                menuSFX.Play();
            }
            
        }
        else if (Navigation() == -1)
        {
            menuSFX.clip = navChime;
            if (nav.transform.position != quitButton)
            {
                nav.transform.position = quitButton;
                menuSFX.Play();
            }

        }
        else if (ConfirmButton())
        {

            menuSFX.clip = navConfirm;
            if (isPaused)
                menuSFX.Play();
            

            if (nav.transform.position == quitButton)
            {
                SceneManager.LoadScene("Main Menu");
                Time.timeScale = 1.0f;

            }
            else if (nav.transform.position == resumeButton)
            {
                if(isMatchOver)
                    SceneManager.LoadScene("RingMap");
                Time.timeScale = 1.0f;
                isPaused = false;
                pauseMenuObject.SetActive(false);
                
            }
        }
    }
    private bool PauseButton()
    {
        return Input.GetButtonDown("Submit");
    }
    private bool ConfirmButton()
    {
        bool buttonPressed = new bool();
        if(Input.GetButtonDown("Attack1"))
            buttonPressed = Input.GetButtonDown("Attack1"); 
        if (Input.GetButtonDown("Attack2"))
            buttonPressed = Input.GetButtonDown("Attack2");
        return buttonPressed;
    }
    private float Navigation()
    {
       
        return Input.GetAxis("Nav");
    }

    private void RingOutVictory()
    {
        if (!isMatchOver)
        {
            if (players[0].IsHypeHit)
            {
                ringOut.enabled = true;
               
                isPlayerOneVictory = false;
                isMatchOver = true;
            }
            else if (players[1].IsHypeHit)
            {

                ringOut.enabled = true;
               
                isPlayerOneVictory = true;
                isMatchOver = true;

            }
            if (players[0].transform.position.y < playerBounds.transform.position.y)
            {
                
                ringOut.enabled = true;
                              
                isPlayerOneVictory = false;
                isMatchOver = true;

            }
            else if (players[1].transform.position.y < playerBounds.transform.position.y)
            {
                ringOut.enabled = true;
                isPlayerOneVictory = true;
                isMatchOver = true;
               
                
                
            }
        }
    }
    private void MatchSet()
    {
        if (isMatchOver)
        {
            if (isPlayerOneVictory)
                audioSource.clip = playerOneTheme;
            else
                audioSource.clip = playerTwoTheme;
            

            //Wait X seconds
            StartCoroutine("MatchSetDelay");
        }
    }
    IEnumerator MatchSetDelay()
    {
        Debug.Log("Match");
        if(!audioSource.isPlaying)
            audioSource.Play();
        WaitForSeconds delay = new WaitForSeconds(2.0f);
        yield return delay;
        ringOut.enabled = false;
        matchSetcamera.enabled = true;
        matchSetcamera.transform.position = cameraPosition;
        matchSetcamera.fieldOfView = 20.0f;
        if (isPlayerOneVictory)
        {
            //players[0].IsWinner = true;
            StartCoroutine("VictoryTaunt", 0);
        }
        else if (!isPlayerOneVictory)
        {
           // players[1].IsWinner = true;
            StartCoroutine("VictoryTaunt", 1);
        }
    }
    IEnumerator PauseNavigation()
    {
        while (pauseMenuObject.activeSelf)
        {
            float pauseEndTime = Time.realtimeSinceStartup + 1f;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                
                yield return 0;
                PauseControls();

            }
           
        }
    }
    IEnumerator MatchSetNavigation()
    {
        MatchSetMenuObject.SetActive(true);
        var text = MatchSetMenuObject.GetComponentInChildren<Text>();
        if (isPlayerOneVictory)
        {
            players[1].gameObject.active = false;
            text.text = string.Format(players[0].name + " Wins!");
        }
        else
        {
            players[0].gameObject.active = false;
            text.text = string.Format(players[1].name + " Wins!");
        }

        nav.transform.position = (matchSetButtons[0].transform.position - new Vector3(100, 15.0f, 0));
        Time.timeScale = 0.0f;
        while (MatchSetMenuObject.activeSelf)
        {
            float pauseEndTime = Time.realtimeSinceStartup + 1f;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {

                yield return 0;
                PauseControls();

            }

        }
    }
    IEnumerator VictoryTaunt(int player)
    {

        matchSetcamera.transform.LookAt(players[player].transform.position);
        players[player].transform.LookAt(matchSetcamera.transform.position);
        players[player].IsTaunting = true;
        WaitForSeconds delay = new WaitForSeconds(2.0f);
        yield return delay;
        StartCoroutine("MatchSetNavigation");
    }
}


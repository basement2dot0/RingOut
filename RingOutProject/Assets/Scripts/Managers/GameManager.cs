using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private Text uiText;
    private Button[] pauseButtons;
    private Button quit;
    private bool isPaused;
    private GameObject pauseMenuObject;
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

    public float Match { get { return match; } set { match = value; } }
    public float Rounds { get { return rounds; } set { rounds = value; } }

    private void Awake()
    {
       
        uiText = GameObject.Find("UIText").GetComponent<Text>();
        matchTimerText = GetComponentInChildren<Text>();
        pauseMenuObject = GameObject.FindGameObjectWithTag("ShowOnPause");
        nav = GameObject.FindGameObjectWithTag("Nav");
        pauseButtons = new Button[2];
        foreach (Button button in pauseMenuObject.GetComponentsInChildren<Button>())
        {
            if (button.name.ToLower() == string.Format("resume"))
                pauseButtons[0] = button;
            else
                pauseButtons[1] = button;
        }
        pauseMenuObject.SetActive(false);
        nav.transform.position = (pauseButtons[0].transform.position - new Vector3(100, 0, 0));
    }
    private void Start()
    {

        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                players[0] = player.GetComponent<Player>();
            else
                players[1] = player.GetComponent<Player>();
            rounds = 0;
            uiText.text = "";
        }
        playersTheme = new AudioManager[2];
        foreach (var theme in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (theme.GetComponent<Player>().ID == 1)
                playersTheme[0] = theme.GetComponent<AudioManager>();
            else
                playersTheme[1] = theme.GetComponent<AudioManager>();
        }
    }

    private void Update()
    {
        RoundTimer();
        PauseMenu();
        RingOutVictory();
    }

    private void RoundTimer()
    {
        matchTimer -= Time.deltaTime;
        if (matchTimer <= 0)
        {
            //matchTimer = 0;
            UpdateTimer();
            DetermineWinner();
            Time.timeScale = 0.0f;
        }
        else if(matchTimer > 0)
            UpdateTimer();
        
    }
    private void UpdateTimer()
    {
        int seconds = (int)(matchTimer % 60);
        matchTimerText.text = seconds.ToString();
        
    }
   private void DetermineWinner()
    {
        var slider = gameObject.GetComponentInChildren<Slider>();
        if(slider.value > 50.0f)
        {
            uiText.text = "Player 1 wins!";
            playersTheme[0].StopHypeMusic();
        }
        else if(slider.value < 50.0f)
        {
            uiText.text = "Player 2 wins!";
            playersTheme[1].StopHypeMusic();
        }
        else
            uiText.text = "DRAW!";
        
    }

    private void PauseMenu()
    {
        if (PauseButton())
        {
           if(!isPaused)
            {
                Time.timeScale = 0.0f;
                isPaused = true;
                pauseMenuObject.SetActive(true);
                StartCoroutine("PauseNavigation");
            }
            
        }
    }
    private void PauseControls()
    {
            var resumeButton = (pauseButtons[0].transform.position - new Vector3(100, 0, 0));
            var quitButton = (pauseButtons[1].transform.position - new Vector3(100, 0, 0));
         

            if (Navigation() > 0.0f)
                nav.transform.position = resumeButton;
            else if (Navigation() < 0.0f)
                nav.transform.position = quitButton ;
            else if (ConfirmButton())
            {
            if (nav.transform.position == quitButton)
            {
                SceneManager.LoadScene("Main Menu");
                Time.timeScale = 1.0f;
            }
            else if (nav.transform.position == resumeButton)
            {
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
        if (players[0].IsHypeHit || players[1].IsHypeHit)
        {
            uiText.text = "RING OUT!";
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
}


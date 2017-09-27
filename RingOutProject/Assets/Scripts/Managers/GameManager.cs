using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Vector3 cameraPosition;
    private bool isMatchOver;
    private bool isPlayerOneVictory;
    private Camera matchSetcamera;
    private Camera mainCamera;
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
    [SerializeField]
    Image ringOut;
    [SerializeField]
    private GameObject playerBounds;

    public float Match { get { return match; } set { match = value; } }
    public float Rounds { get { return rounds; } set { rounds = value; } }

    private void Awake()
    {
        ringOut.enabled = false;
        
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
        playersTheme = new AudioManager[2];
        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
            {
                players[0] = player.GetComponent<Player>();
                playersTheme[0] = player.GetComponent<AudioManager>();
            }
            else
            {
                players[1] = player.GetComponent<Player>();
                playersTheme[1] = player.GetComponent<AudioManager>();
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
   private void DetermineMomentumWinner()
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
        if (!isMatchOver)
        {
            if (players[0].IsHypeHit)
            {
                ringOut.enabled = true;
                isPlayerOneVictory = false;
                mainCamera.enabled = false;
                matchSetcamera.transform.LookAt(players[0].transform.position);
                matchSetcamera.transform.position = new Vector3((players[0].transform.position.x) - (-9.96f), (players[0].transform.position.y) - (16), (players[0].transform.position.z) - (-74.2f));

            }
            else if (players[1].IsHypeHit)
            {
                ringOut.enabled = true;
                isPlayerOneVictory = true;
                mainCamera.enabled = false;
                matchSetcamera.enabled = true;
                matchSetcamera.transform.position = new Vector3((players[1].transform.position.x) - (-9.96f), (players[1].transform.position.y) - (16), (players[1].transform.position.z) - (-74.2f));
                matchSetcamera.transform.LookAt(players[1].transform.position);
            }
            if (players[0].transform.position.y < playerBounds.transform.position.y)
            {
                ringOut.enabled = true;
                isMatchOver = true;
                isPlayerOneVictory = false;

            }
            else if (players[1].transform.position.y < playerBounds.transform.position.y)
            {
                ringOut.enabled = true;
                isMatchOver = true;
                isPlayerOneVictory = true;
                Debug.Log("Ring Out");

            }
        }
    }
    private void MatchSet()
    {
        if (isMatchOver)
        {
            //Wait X seconds
            StartCoroutine("MatchSetDelay");
            //
           
        }
    }
    IEnumerator MatchSetDelay()
    {
        Debug.Log("Match");
        WaitForSeconds delay = new WaitForSeconds(2.0f);
        yield return delay;
        ringOut.enabled = false;
        matchSetcamera.enabled = true;
        matchSetcamera.transform.position = cameraPosition;
        if (isPlayerOneVictory)
        {

            matchSetcamera.transform.LookAt(players[0].transform.position);
            matchSetcamera.fieldOfView = 20.0f;
            uiText.text = string.Format("PLAYER 1 WINS");
        }
        if(!isPlayerOneVictory)
        {
            matchSetcamera.transform.LookAt(players[1].transform.position);
            matchSetcamera.fieldOfView = 20.0f;
            uiText.text = string.Format("PLAYER 2 WINS");
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


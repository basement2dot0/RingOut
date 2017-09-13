using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this class should be loaded with the stage in an empty game object
    [SerializeField]
    private float Rounds;
    [SerializeField]
    private float Match;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Text uiText;

    private Button[] pauseButtons;
    private Button quit;
    private bool isPaused;
    private GameObject pauseMenuObject;
    private GameObject nav;
    private void Awake()
    {
        uiText = GetComponentInChildren<Text>();
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
            {
                players[0] = player.GetComponent<Player>();


            }

            else
            {
                players[1] = player.GetComponent<Player>();

            }

        }
        Rounds = 0;
        uiText.text = "";
    }
    
    private void Update()
    {
       
        PauseMenu();
        RingOutVictory();
       

    }

    private void FixedUpdate()
    {
 

    }

    private void PauseMenu()
    {
        if (PauseButton())
        {
           if(!isPaused)
            {
                
                Time.timeScale = 0.0f;
                //uiText.text = string.Format("PAUSE");
                isPaused = true;
                pauseMenuObject.SetActive(true);
                StartCoroutine("PauseNavigation");
            }
            
        }
    }

    private void RingOutVictory()
    {
        if (players[0].IsHypeHit || players[1].IsHypeHit)
            uiText.text = "RING OUT!";
    }

    private void PauseControls()
    {
            var resumeButton = (pauseButtons[0].transform.position - new Vector3(100, 0, 0));
            var quitButton = (pauseButtons[1].transform.position - new Vector3(100, 0, 0));
         

        if (Navigation() > 0.0f)
            {
                Debug.Log("Resume");
            
                nav.transform.position = resumeButton;
            
            }
            if (Navigation() < 0.0f)
            {
                Debug.Log("Quit");
                nav.transform.position = quitButton ;
            }
            if (ConfirmButton())
            {
              if(nav.transform.position == quitButton)
              {

              }
              if(nav.transform.position == resumeButton)
            {
                Time.timeScale = 1.0f;
                //uiText.text = "";
                isPaused = false;
                pauseMenuObject.SetActive(false);

            }
        }

    }


    private bool PauseButton()
    {
        return Input.GetButtonDown("Submit");
    }
    private float Navigation()
    {
        return Input.GetAxis("Vertical1");
    }
    private bool ConfirmButton()
    {
        return Input.GetButtonDown("Attack1");
    }


    //Testing
    IEnumerator PauseNavigation()
    {
        while (true)
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


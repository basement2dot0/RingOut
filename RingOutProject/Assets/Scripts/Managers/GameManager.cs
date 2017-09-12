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
    [SerializeField]
    private InputManager inputManager;
    private bool isPaused;

    private void Awake()
    {
        uiText = GetComponentInChildren<Text>();
        inputManager = GetComponent<HumanInput>();
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
        Debug.Log(isPaused.ToString());
        PauseMenu();
       if (players[0].IsHypeHit|| players[1].IsHypeHit)
            uiText.text = "RING OUT!";


    }



    private void PauseMenu()
    {
        if (inputManager.PauseButton())
        {
           if(!isPaused)
            {
                Time.timeScale = 0.0f;
                uiText.text = string.Format("PAUSE");
                isPaused = true;
                Debug.Log("PAUSED");
            }
            else if(isPaused)
            {
                Time.timeScale = 1.0f;
                uiText.text = "";
                isPaused = false;
                Debug.Log("UNPAUSED");
            }
        }
    }

    private void RingOutVictory()
    {
        if (players[0].IsHypeHit || players[1].IsHypeHit)
            uiText.text = "RING OUT!";
    }
}


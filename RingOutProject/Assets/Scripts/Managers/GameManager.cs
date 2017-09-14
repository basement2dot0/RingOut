using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float matchTimer;
    [SerializeField]
    private Image[] CountDownNumbers;
    [SerializeField]
    private Text matchTimerText;
    [SerializeField]
    private float Rounds;
    [SerializeField]
    private float Match;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Text ringOutText;

    

    private void Awake()
    {
        matchTimerText = GetComponentInChildren<Text>();
        matchTimer = 60.0f;
        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                players[0] = player.GetComponent<Player>();
            else
                players[1] = player.GetComponent<Player>();
        }
    }
    private void Start()
    {

        Rounds = 0;
        ringOutText.text = "";
        
    }
    private void Update()
    {
        RoundTimer();



        if (players[1].IsHypeHit)
            Debug.Log(players[1].IsHypeHit.ToString());

        if (players[0].IsHypeHit|| players[1].IsHypeHit)
            ringOutText.text = "RING OUT!";


    }

    private void RoundTimer()
    {
        
        matchTimer -= Time.deltaTime;
        UpdateTimer();
        if (matchTimer < 0)
        {
            matchTimer = 0;
            DetermineWinner();
            Time.timeScale = 0.0f;

        }
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
            ringOutText.text = "Player 1 wins!";
        }
        else if(slider.value < 50.0f)
        {
            ringOutText.text = "Player 2 wins!";
        }
        else
        {
            ringOutText.text = "DRAW!";
        }
    }


}


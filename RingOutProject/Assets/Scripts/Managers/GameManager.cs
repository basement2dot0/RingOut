using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

    public float Match { get => match; set => match = value; }
    public float Rounds { get => rounds; set => rounds = value; }

    private void Awake()
    {
        matchTimerText = GetComponentInChildren<Text>();
        matchTimer = 10.0f;
        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                players[0] = player.GetComponent<Player>();
            else
                players[1] = player.GetComponent<Player>();
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
        smatchTimer -= Time.deltaTime;
        if (matchTimer <= 0)
        {
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
            ringOutText.text = "Player 1 wins!";
            playersTheme[0].StopHypeMusic();
        }
        else if(slider.value < 50.0f)
        {
            ringOutText.text = "Player 2 wins!";
            playersTheme[1].StopHypeMusic();
        }
        else
            ringOutText.text = "DRAW!";
        
    }


}


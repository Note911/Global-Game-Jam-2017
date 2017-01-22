using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public Player player;

    private int totalScore;
    private int currentScore;
    private int multiplier;
    float timer;

    public Text totalScoreText;
    public Text currentScoreText;
    private string emptyText = "";
    private string sCurrentScore;
    private string sTotalScore;
    private string sMarkiplier;

    
	// Use this for initialization
	void Start ()
    {
        timer = 0;
        totalScore = 0;
        currentScore = 0;
        multiplier = 1;
        sMarkiplier = "" + sMarkiplier;
        sTotalScore = "Score: " + totalScore;
        sMarkiplier = emptyText;
        sCurrentScore = currentScore + sMarkiplier;
        totalScoreText.text = sTotalScore;
        currentScoreText.text = emptyText;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check if there is a worthy markiplier
        if (multiplier > 1)
            sMarkiplier = " x " + multiplier;
        else
            sMarkiplier = emptyText;
        //Score timer tick
        

        //temp- when breached start scoring.
        if (player.playerState == Player.PlayerState.AIRBORNE)
        {
            timer += Time.deltaTime;
            if (timer > 0.33f)
            { 
                ScoreTick();
                timer = 0;
            }
        }
        if(player.playerState == Player.PlayerState.UNDERWATER)
        {
            AddToTotal(currentScore);
            currentScore = 0;
        }
        currentScoreText.text = sCurrentScore + sMarkiplier;
    }
    void AddToTotal(int _currentScore)
    {
        totalScore += _currentScore * multiplier;
        totalScoreText.text = "Score: " + totalScore;

    }

    void ScoreTick()
    {
        currentScore += 10;
        sCurrentScore = "" + currentScore;
    }
}

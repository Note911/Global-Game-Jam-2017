using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    public Player player;

    private float totalScore;
    private float currentScore;
    private float multiplier;
    float timer;
    public bool alive;
    public Text totalScoreText;
    public Text currentScoreText;
    private string emptyText = "";
    private string sCurrentScore;
    private string sTotalScore;
    private string sMarkiplier;

    
	// Use this for initialization
	void Start ()
    {
        alive = true;
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
        if(alive)
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
                multiplier = 1;
            }
            currentScoreText.text = sCurrentScore + sMarkiplier;
        }
        else
        {
            currentScoreText.text = "";
            totalScoreText.text = "";
        }
    }
    void AddToTotal(float _currentScore)
    {
        totalScore += _currentScore * multiplier;
        totalScoreText.text = "Score: " + (int)totalScore;

    }

    public void ScoreTick()
    {
        currentScore += 10;
        sCurrentScore = "" + currentScore;
    }

    public void AddMultiplier(float _multiplier)
    {
        multiplier += _multiplier;
    }

    public void AddStaticScore(int num)
    {
        currentScore += num;
    }

    public void ChangeCurrentScoreByMultiplier(float _m)
    {
        currentScore *= _m;
    }
}

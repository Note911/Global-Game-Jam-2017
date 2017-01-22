using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinalScreenSplash : MonoBehaviour {
    public Player player;
    public Text scoreText;
	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
	}
    private void OnEnable()
    {
        player.scoreManager.GetComponent<ScoreManager>().alive = false;
        
        scoreText.text = player.scoreManager.GetComponent<ScoreManager>().totalScoreText.text;
    }

	// Update is called once per frame
	void Update ()
    {

        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour {

    public Player player;
    public Camera cam;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.localScale.x >= 0)
            transform.localScale = new Vector2(player.stamina / player.maxStamina, transform.localScale.y);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    public float value = 0.02f;
    public Player player;
    public PowerFactory powerFactory;
   

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit!");
            player.stamina += value * player.maxStamina;
            powerFactory.RemoveFromWorld(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {


    public Player player;
    public PowerFactory powerFactory;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        player.stamina += 5;
        if (player.stamina > 100.0f)
            player.stamina = 100.0f;
        Destroy(gameObject);
    }
}

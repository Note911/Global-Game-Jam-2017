using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
<<<<<<< HEAD

    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        player.stamina += 5;
        if (player.stamina > 100.0f)
            player.stamina = 100.0f;
        Destroy(gameObject);
=======
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
        
>>>>>>> refs/remotes/origin/Topher-Branch-Development
    }
}

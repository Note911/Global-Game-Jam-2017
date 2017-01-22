using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
<<<<<<< HEAD


    public Player player;
    public PowerFactory powerFactory;

    // Use this for initialization
    void Start () {
		
=======

    public Player player;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);
>>>>>>> e7c384e7e0ced7719113132dd93d9b109198f714
	}
	
	// Update is called once per frame
	void Update () {
		
	}

<<<<<<< HEAD
    void OnTriggerEnter2D(Collider2D other)
    {
        player.stamina += 5;
        if (player.stamina > 100.0f)
            player.stamina = 100.0f;
        Destroy(gameObject);
    }
=======
    void OnTriggerEnter2D(Collider2D other) {
        player.stamina ++;
        if (player.stamina > 100.0f)
            player.stamina = 100.0f;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject,0.3f);
    }

>>>>>>> e7c384e7e0ced7719113132dd93d9b109198f714
}

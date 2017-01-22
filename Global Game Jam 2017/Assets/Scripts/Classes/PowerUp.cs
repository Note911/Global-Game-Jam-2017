using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public Player player;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        player.stamina ++;
        if (player.stamina > 100.0f)
            player.stamina = 100.0f;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject,0.3f);
    }

}

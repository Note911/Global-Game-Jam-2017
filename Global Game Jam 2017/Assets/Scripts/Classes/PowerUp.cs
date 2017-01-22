using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public Player player;
    public PowerFactory powerFactory;
    public ParticleSystem particle;
    
    void OnEnable()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        Invoke("Destroy", 30.0f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    // Update is called once per frame
    void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other) {
        player.stamina ++;
        if (player.stamina > 100.0f)
            player.stamina = 100.0f;
        ParticleSystem ps = Instantiate(particle);
        ps.transform.position = transform.position;
        ps.Play();
        gameObject.SetActive(false);
        powerFactory.RemoveFromWorld(gameObject);
    }

}

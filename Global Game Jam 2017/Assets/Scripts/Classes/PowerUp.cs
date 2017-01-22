using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public Player player;
    public PowerFactory powerFactory;
    public ParticleSystem particle;
    public GameObject audioSourceObj;
    public AnimationController2D animator;
    public Animation2D sprite;
    
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
    void Awake() {
         GetComponent<PowerUp>().sprite = ResourceManager.GetInstance().GetAnimationManager().GetAnimation("PowerUp");
         GetComponent<PowerUp>().animator = new AnimationController2D(GetComponent<SpriteRenderer>(), GetComponent<PowerUp>().sprite);
    }

    // Update is called once per frame
    void Update () {
        animator.Update();
        transform.Rotate(0, 0, 10);
	}
    void OnTriggerEnter2D(Collider2D other) {
        Destroy(GameObject.Instantiate(audioSourceObj), 1.0f);

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

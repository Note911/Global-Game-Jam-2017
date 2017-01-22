using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class GameEntity : MonoBehaviour {

    public bool facingRight = true;
    protected List<Animation2D> animationList;
    public AnimationController2D animator;
    public Rigidbody2D rbody;
    protected SpriteRenderer renderer;

    public float baseMoveSpeed;
    public float moveSpeed;
    public float maxSpeed;

    public Vector2 heading;

    protected virtual void Awake() {
         animationList = new List<Animation2D>();
         renderer = GetComponent<SpriteRenderer>();
         rbody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	protected virtual void Start () {
        moveSpeed = baseMoveSpeed;
        heading = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        animator.Update();
        
	}

    public virtual void FlipX()
   {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

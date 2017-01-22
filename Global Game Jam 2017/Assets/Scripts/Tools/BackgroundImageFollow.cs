using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImageFollow : MonoBehaviour {

    public Transform anchor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(anchor.position.x, transform.position.y, transform.position.z);
	}
}

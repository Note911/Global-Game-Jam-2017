using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspirationalMessager : MonoBehaviour {

    public Text textBox;
    public Transform anchor;
    public Camera cameraRef;

	// Use this for initialization
	void Start () {
        textBox.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (textBox.enabled)
        {
            textBox.fontSize += 2;
        }
        else
            textBox.fontSize = 20;
	}

    void DisableText() {
        textBox.enabled = false;
    }

     public void BadDiveMessages() {
        textBox.transform.position = anchor.position;
        textBox.enabled = true;
        textBox.color = Color.red;
        int i = Random.Range(0, 4);
        switch (i) {
            case (0):
                textBox.text = "Gross!";
                break;
            case (1):
                textBox.text = "Git Gud!";
                break;
            case (2):
                 textBox.text = "Just Bad!";
                break;
            case (3):
                textBox.text = "You Suck!";
                break;
        }
        Invoke("DisableText", 0.3f);
    }
    public void CustomMessage(string _text, Color color, Vector2 pos)
    {
        textBox.transform.position = pos;
        textBox.enabled = true;
        textBox.color = color;
        textBox.text = _text;
        Invoke("DisableText", 0.5f);
    }
    public void DiveMessages() {
        textBox.transform.position = anchor.position;
        textBox.enabled = true;
        textBox.color = Color.yellow;
        int i = Random.Range(0, 4);
        switch (i) {
            case (0):
                textBox.text = "Not bad.";
                break;
            case (1):
                textBox.text = "Ok.";
                break;
            case (2):
                 textBox.text = "Well you tried.";
                break;
            case (3):
                textBox.text = "Satasfactory.";
                break;
            case (4):
                textBox.text = "Participation!";
                break;
            case (5):
                textBox.text = "C-";
                break;
        }
        Invoke("DisableText", 0.3f);
    }

    public void GreatDiveMessages() {
        textBox.transform.position = anchor.position;
        textBox.enabled = true;
        textBox.color = Color.green;
        int i = Random.Range(0, 6);
        switch (i) {
            case (0):
                textBox.text = "Sexy!";
                break;
            case (1):
                textBox.text = "Many Score!";
                break;
            case (2):
                 textBox.text = "Egg-Salad";
                break;
            case (3):
                textBox.text = "Massive!";
                break;
            case (4):
                textBox.text = "Dank!";
                break;
            case (5):
                textBox.text = "Egg-Salad!";
                break;
            case (6):
                textBox.text = "Thick!";
                break;
            case (7):
                textBox.text = "Perfect!";
                break;
        }
        Invoke("DisableText", 0.3f);
    }

    public void WakeMessages(int i) {
        textBox.transform.position = anchor.position;
        textBox.enabled = true;
        switch (i) {
            case (0):
                textBox.color = Color.green;
                textBox.text = "Kissed it!";
                break;
            case (1):
                textBox.color = Color.blue;
                textBox.text = "Nice Wake!";
                break;
            case (2):
                textBox.color = Color.yellow;
                 textBox.text = "Steady...";
                break;
            case (3):
                textBox.color = Color.red;
                textBox.text = "Holy Shit Brah!";
                break;
            case (4):
                textBox.color = Color.magenta;
                textBox.text = "Legendary Human!";
                break;
        }
        Invoke("DisableText", 0.3f);
    }

}

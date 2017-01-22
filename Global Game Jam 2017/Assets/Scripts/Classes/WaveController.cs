using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public Camera cameraRef;
    // Use this for initialization
    public int size = 18;
    private float waveSize;
    public float amp = 1;
    public float maxAmp = 10;
    public float freq = 200;
    public float waterDepth = 15.0f;
    GameObject[] waveSections;

    public float xOffset = 0.0f;
    public float yOffset = 5f;
    
    public GameObject wave;
    float count = 0;
    void Start ()
    {
        count += xOffset;
        waveSections = new GameObject[size*2];
        waveSize = 18.0f / size;
        for (int i = 0; i < size*2; i++)
        {
            waveSections[i] = Instantiate(wave);
            waveSections[i].name = "Wave" + i;
            waveSections[i].transform.SetParent(this.transform);

            waveSections[i].transform.position = new Vector2((i*waveSize) - (size*waveSize), 0);
            waveSections[i].transform.localScale = new Vector2(waveSize, waterDepth);
        }        
    }
	
	// Update is called once per frame
	void Update ()
    {
        amp += Time.deltaTime / 100.0f;

        if (amp > maxAmp)
            amp = maxAmp;
        count += Time.deltaTime;
        float totalLength = 0;
        for (int i = 0; i < size*2; i++)
        {
            totalLength += waveSize;
            Vector2 offset = new Vector2(waveSections[i].transform.position.x, (amp * Mathf.Sin((freq * waveSections[i].transform.position.x) + count) - /*offsets the waves down*/ yOffset));
            //Vector2 offset = new Vector2(waveSections[i].transform.position.x, (float)(Mathf.Sqrt((9.8f * (float)(Math.Tanh(count * ((2.0f * (float)Mathf.PI) / freq))) / ((2.0f * (float)Mathf.PI) / freq))))- /*offsets the waves down*/ (waterDepth / 4f) - 2.0f);
            waveSections[i].transform.position = offset;
        }
        //Wrap the waves around the screen
        for (int i = 0;  i < waveSections.Length; ++i) {
            if(waveSections[i].transform.position.x < cameraRef.ScreenToWorldPoint(Vector3.zero).x - 10.0f) {
                waveSections[i].transform.position = new Vector3(waveSections[i].transform.position.x + totalLength, waveSections[i].transform.position.y, waveSections[i].transform.position.z);
            }
            else if(waveSections[i].transform.position.x > cameraRef.ScreenToWorldPoint(new Vector3(cameraRef.pixelWidth,0,0)).x + 10.0f) {
                waveSections[i].transform.position = new Vector3(waveSections[i].transform.position.x - totalLength, waveSections[i].transform.position.y, waveSections[i].transform.position.z);
            }
        } 
    }
}

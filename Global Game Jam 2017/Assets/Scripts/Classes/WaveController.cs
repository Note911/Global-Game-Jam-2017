using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public Camera cameraRef;
    // Use this for initialization
    public int size = 18;
    private float waveSize;
    public float amp = 1;
    public float freq = 200;
    public float waterDepth = 15.0f;
    GameObject[] waveSections;
    
    public GameObject wave;
    float count = 0;
    void Start ()
    {

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
        count += Time.deltaTime;
        float totalLength = 0;
        for (int i = 0; i < size*2; i++)
        {
            totalLength += waveSize;
            Vector2 offset = new Vector2(waveSections[i].transform.position.x, (amp * Mathf.Sin((freq * waveSections[i].transform.position.x) + count) - /*offsets the waves down*/ (waterDepth / 2f) - 2.0f));
            waveSections[i].transform.position = offset;
        }
        //Wrap the waves around the screen
        for (int i = 0;  i < waveSections.Length; ++i) {
            if(waveSections[i].transform.position.x < cameraRef.ScreenToWorldPoint(Vector3.zero).x) {
                waveSections[i].transform.position = new Vector3(waveSections[i].transform.position.x + totalLength, waveSections[i].transform.position.y, waveSections[i].transform.position.z);
            }
        } 
    }
}

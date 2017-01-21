using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    // Use this for initialization
    public int size = 18;
    private float waveSize;
    public float amp = 1;
    public float freq = 200;
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

            waveSections[i].transform.position = new Vector2((i*waveSize)+0.2f-(size*waveSize)/2, 0);
            waveSections[i].transform.localScale = new Vector2(waveSize, 8.0f);
        }        
    }
	
	// Update is called once per frame
	void Update ()
    {
        count += Time.deltaTime;
        for (int i = 0; i < size*2; i++)
        {
            Vector2 offset = new Vector2((i*waveSize)+0.2f - (size *waveSize), (amp * Mathf.Sin((freq * waveSections[i].transform.position.x) + count) - 1.5f));
            waveSections[i].transform.position = offset;
        }
        
    }
}

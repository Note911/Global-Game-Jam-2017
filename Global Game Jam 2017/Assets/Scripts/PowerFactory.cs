using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFactory : MonoBehaviour {

    public List<GameObject> activeList;
    public List<GameObject> inactiveList;

    public Camera cam;
    public GameObject PUType;
    public ParticleSystem particle;
    public Player player;
    public int poolSize;

    enum SpawnPattern
    {
        Scatter,
        SineWave,
        CosWave,
        LoopDaLoop
    };
	// Use this for initialization
	void Start ()
    {
        poolSize = 200;
        inactiveList = new List<GameObject>();
        activeList = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject newPowerUp = GameObject.Instantiate(PUType);
            newPowerUp.GetComponent<PowerUp>().player = player;
            newPowerUp.GetComponent<PowerUp>().particle = particle;
            newPowerUp.GetComponent<PowerUp>().powerFactory = this;
            newPowerUp.SetActive(false);
            inactiveList.Add(newPowerUp);
        }
        Debug.Log(inactiveList.Count);
        InvokeRepeating("SpawnPowerUps", 0.0f, 8.0f);
	}
	
    private void SpawnPowerUps() {
        if (inactiveList.Count > 20)
        {
            Debug.Log("Spawning");
            int i = Random.Range(0, 3);
            switch (i)
            {
                case (0):
                    SpawnScatter(15);
                    break;
                case (1):
                    SpawnSineWave(2.5f, 2.0f, 10);
                    break;
                case (2):
                    SpawnCosWave(2.5f, 0.5f, 20);
                    break;
                case (3):
                    break;
            }
        }
    }


	// Update is called once per frame
	void Update ()
    {
        
    }
    public void RemoveFromWorld(GameObject _powerUp)
    {
        inactiveList.Add(_powerUp);
        activeList.Remove(_powerUp);
    }

    Vector2 RandomPositionVector()
    {
        return new Vector2(Random.Range(transform.position.x, transform.position.x + 175.0f), Random.Range(-1.5f, 12.0f));
    }

    /// <summary>
    /// numPowers can't exceed poolSize
    /// </summary>
    /// <param name="numPowers"></param>
    void SpawnScatter(int numPowers)
    {
       
        List<GameObject> scatterList = new List<GameObject>();
        
        for(int i = numPowers; i >= 0; i--)
        {
            scatterList.Add(inactiveList[i]);
            inactiveList.RemoveAt(i);
        }
        for (int i = 0; i < scatterList.Count; i++)
        {
            for (int j = i + 1; j < scatterList.Count; j++)
            {
                if (Mathf.Abs((scatterList[j].transform.position - scatterList[i].transform.position).magnitude) < 5.0f)
                {
                    scatterList[i].transform.position = RandomPositionVector();
                }
            }

        }
        for(int i = scatterList.Count - 1; i >= 0; i--)
        {
            scatterList[i].SetActive(true);
            scatterList[i].transform.position += transform.position;
            activeList.Add(scatterList[i]);
            scatterList.RemoveAt(i);
        }
        
    }

    void SpawnSineWave(float a, float f, int numPowers)
    {
        List<GameObject> swList = new List<GameObject>();
        for(int i = numPowers; i >= 0; i--)
        {
            swList.Add(inactiveList[i]);
            inactiveList.RemoveAt(i);
        }
        for (int i = 0; i < swList.Count; ++i)
        {
            swList[i].transform.position = new Vector2(a * i + 125.0f, Mathf.Sin(f * i));
        }
        for (int i = swList.Count-1; i >= 0; i--)
        {
            swList[i].SetActive(true);
            swList[i].transform.position += transform.position;
            activeList.Add(swList[i]);
            swList.RemoveAt(i);
        }
    }
    void SpawnCosWave(float a, float f, int numPowers)
    {
        List<GameObject> cwList = new List<GameObject>();
        for (int i = numPowers; i >= 0; i--)
        {
            cwList.Add(inactiveList[i]);
            inactiveList.RemoveAt(i);
        }
        for (int i = 0; i < cwList.Count; ++i)
        {
            cwList[i].transform.position = new Vector2(a * i + 125.0f, Mathf.Cos(f * i));
        }
        for(int i = cwList.Count-1; i >= 0; i--)
        {
            cwList[i].SetActive(true);
            cwList[i].transform.position += transform.position;
            activeList.Add(cwList[i]);
            cwList.RemoveAt(i);
        }
    }
    void SpawnLoop()
    {

    }
}

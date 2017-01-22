using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFactory : MonoBehaviour {
    List<PowerUp> powerupList;

    public Camera cam;
    public GameObject PUType;
    public Player player;

    enum SpawnPattern
    {
        Scatter,
        SineWave,
        TanWave,
        LoopDaLoop
    };
	// Use this for initialization
	void Start ()
    {
        powerupList = new List<PowerUp>();
        InvokeRepeating("SpawnPowerUps", 0.0f, 2.0f);
	}
	
    private void SpawnPowerUps() {
        Debug.Log("Spawning");
        int i = Random.Range(0, 2);
        switch(i) {
            case (0):
                SpawnScatter(20);
                break;
            case (1):
                SpawnSineWave(2.5f, 2.0f);
                break;
            case (2):
                break;
            case (3):
                break;
        }
    }


	// Update is called once per frame
	void Update ()
    {
        
    }
    public void RemoveFromWorld(GameObject _powerUp)
    {
        
    }

    Vector2 RandomPositionVector()
    {
        return new Vector2(Random.Range(transform.position.x, transform.position.x + 175.0f), Random.Range(-1.5f, 12.0f));
    }

    void SpawnScatter(int numPowers)
    {
        //Instead of Instantiating- Take n unused objects
        for (int i = 0; i < numPowers; ++i)
        {
            GameObject newPowerUp = GameObject.Instantiate(PUType);
            
            newPowerUp.GetComponent<PowerUp>().player = player;
            powerupList.Add(PUType.GetComponent<PowerUp>());
        }
        foreach (PowerUp powerup in powerupList)
        {
            powerup.transform.position = RandomPositionVector();
            powerup.transform.position += transform.position;
        }
        for (int i = 0; i < powerupList.Capacity; i++)
        {
            for (int j = i + 1; j < powerupList.Capacity; j++)
            {
                Debug.Log("i: " + i + " j: " + j + "\nCapacity: " + powerupList.Capacity);
                if (Mathf.Abs((powerupList[j].transform.position - powerupList[i].transform.position).magnitude) < 5.0f)
                {
                    powerupList[i].transform.position = RandomPositionVector();
                }
            }
        }
    }

    void SpawnSineWave(float a, float f)
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject newPowerUp = GameObject.Instantiate(PUType);
            newPowerUp.transform.position = new Vector2(a * i + 125.0f, Mathf.Sin(f * i));
            newPowerUp.transform.position += transform.position;
            newPowerUp.GetComponent<PowerUp>().player = player;
            powerupList.Add(PUType.GetComponent<PowerUp>());
        }
    }
    void SpawnTanWave()
    {

    }
    void SpawnLoop()
    {
        
    }
}

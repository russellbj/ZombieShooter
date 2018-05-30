using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.RVO;
using Pathfinding.Util;
using Pathfinding;

public class GameManager : MonoBehaviour {

    private int score;
    public GameObject four;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject zero;
    public float zombieSpeed;
    public int numOfZombies;
    public GameObject[] zombieArray;

    public void setNumOfZombies(int numOfZombies)
    {
        this.numOfZombies = numOfZombies;
    }

    public int getNumOfZombies()
    {
        return numOfZombies;
    }

    // difficulty (currently only determines rate of zombie spawn, ramps up over time)
    int difficulty = 0;

    public void setScore(int score)
    {
        this.score = score;
    }

    public int getScore()
    {
        return score;
    }

    public void spawnZombie()
    {
        if (numOfZombies <= 15)
        {
            numOfZombies++;
            print(difficulty);
            print(numOfZombies);
            print("zombie spawned");
            Vector3 randPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
            GameObject zombie = Instantiate(Resources.Load("Zombie", typeof(GameObject)), randPosition, transform.rotation) as GameObject;
        }
    }

	// Use this for initialization
	void Start () {
        score = 0;
        numOfZombies = 1;
        StartCoroutine(spawnTimer());
        zombieArray = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
        zombieArray = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < zombieArray.Length - 1; i++)
        {
            AIPath zombiePath = zombieArray[i].GetComponent<AIPath>();
            zombiePath.maxSpeed = zombieSpeed;
        }
        rampUp();
        
	}

    public void ChangeHealth(int health)
    {
        if(health == 3)
        {
            four.SetActive(false);
            three.SetActive(true);
        }
        if(health == 2)
        {
            three.SetActive(false);
            two.SetActive(true);
        }
        if(health == 1)
        {
            two.SetActive(false);
            one.SetActive(true);
        }
        if(health == 0)
        {
            one.SetActive(false);
            zero.SetActive(true);
        }
    }

    void rampUp()
    {
        if(score < 5)
        {
            difficulty = 0;
        }
        else if (score > 5 && score < 10)
        {
            difficulty = 1;
        }
        else if (score > 10 && score < 15)
        {
            difficulty = 2;
        }
        else if (score > 15 && score < 25)
        {
            difficulty = 3;
        }
        else if (score > 25)
        {
            difficulty = 4;
        }
    }

    IEnumerator spawnTimer()
    {
        if (difficulty == 0)
        {
            yield return new WaitForSeconds(2);
            zombieSpeed = 2;
           
                spawnZombie();
                
                StartCoroutine(spawnTimer());
            
        }
        else if (difficulty == 1)
        {
            yield return new WaitForSeconds(1);
            zombieSpeed = 3;
                spawnZombie();
               
            
            StartCoroutine(spawnTimer());
        }
        else if (difficulty == 2)
        {
            yield return new WaitForSeconds(1);
          
                spawnZombie();
            

            zombieSpeed = 3.5f;
            StartCoroutine(spawnTimer());
        }
        else if (difficulty == 3)
        {
            yield return new WaitForSeconds(1);
           
                spawnZombie();
                spawnZombie();
            
            zombieSpeed = 3;
            StartCoroutine(spawnTimer());
        }
        else if (difficulty == 4)
        {
            yield return new WaitForSeconds(1);
            
                spawnZombie();
                spawnZombie();
                spawnZombie();
                spawnZombie();
            
            zombieSpeed = 4;
            StartCoroutine(spawnTimer());
        }
    }
}

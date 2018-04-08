using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EndlessManager;

public class Cheese_Spawner : MonoBehaviour {

    // I think I will start making an endless version of the game. 
    // I may make a version with levels later on.
    // Enjoy =D

    [Header(" Player ")]
    public Transform player;

    [Header(" Cheese Prefabs ")]
    public GameObject[] cheesePrefabs;
    public GameObject[] cheeseEnemiesPrefabs;
    public float initialYSpawn;
    public float distanceBetweenCheeses;
    public int initialCheeses;
    [Tooltip("It will start spawning enemies when the score reaches this value.")]
    public int startSpawningEnemiesAt;

    [Header(" Control Settings ")]
    public float angleModifier;
    Vector3 pressedPos;
    Vector3 actualPos;

    public static bool resetTrigger;

	// Use this for initialization
	void Start () {

        pressedPos = actualPos = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
		
        // If the amount of active cheeses is smaller than the desired one, spawn a chunk
        if(transform.childCount < initialCheeses)
        {
            SpawnCheese(GetSpawningPosition());
        }


        if (Game_Controller.gameState == Game_Controller.GameState.GAME)
        {
            // Control the rotation of all this cheese parent
            ControlRotation();
        }


        if(resetTrigger)
        {
            ResetCheeses();
            resetTrigger = false;
        }
	}


    void ResetCheeses()
    {
        // Destroy all the active cheeses
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }


    }


    void ControlRotation()
    {

        if(Input.GetMouseButtonDown(0))
        {
            pressedPos = Input.mousePosition;
            actualPos = pressedPos;
        }
        else if(Input.GetMouseButton(0))
        {
            // Store the actual pos
            actualPos = Input.mousePosition;

            // Calculate the rotation angle
            float angle = actualPos.x - pressedPos.x;
            angle *= angleModifier;

            // Rotate this on the Y axis
            transform.Rotate(new Vector3(0, angle, 0));

            // Reset the pressed position
            pressedPos = actualPos;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            pressedPos = actualPos;
        }
    }


    Vector3 GetSpawningPosition()
    {
        if(transform.childCount == 0)
        {
            return new Vector3(0, initialYSpawn, 0);
        }
        else
        {
            return new Vector3(0, transform.GetChild(transform.childCount - 1).position.y - distanceBetweenCheeses, 0);
        }
    }

    void SpawnCheese(Vector3 spawnPos)
    {
        // Choose a random cheese index
        int randomCheeseIndex = Random.Range(0, cheesePrefabs.Length);


        // Store the rotation of the prefab
        Quaternion cheeseRotation = Quaternion.identity;
        cheeseRotation.y = Random.Range(0f, Mathf.PI);

        if(transform.childCount == 0)
        {
            randomCheeseIndex = 0;
            cheeseRotation = Quaternion.identity;
        }


        GameObject cheeseInstance;

        // Set the probability of spawning an enemy
        if(Game_Controller.SCORE < startSpawningEnemiesAt)
        {
 
            // Spawn a normal one
                // Spawn the cheese prefab
            cheeseInstance = Instantiate(cheesePrefabs[randomCheeseIndex], spawnPos, cheeseRotation, transform);
            
        }
        else
        {

            // If the score is greater than 50, spawn only enemy cheese

            int rand = Random.Range(0, cheeseEnemiesPrefabs.Length);

            // Spawn the cheese enemy prefab
            cheeseInstance = Instantiate(cheeseEnemiesPrefabs[rand], spawnPos, cheeseRotation, transform);
        }




    }


}

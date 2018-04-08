using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EndlessManager;

public class Cheese_Behavior : MonoBehaviour {

    [Header(" Explode Settings ")]
    public float forceMagnitude;
    Transform player;
    bool didExplode;

    // Use this for initialization
	void Start () {
        player = GetComponentInParent<Cheese_Spawner>().player;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {

        if (player.position.y < transform.position.y - 1 && !didExplode)
        {
            // Play a perfect sound
            Perfect_Sounds_Manager.playPerfectSoundTrigger = true;

            // Increase the combo of the player
            Player_Controller.cheeseCombo++;

            Explode();
            didExplode = true;
        }
    }


    public void TurnCheeseColored(Color c)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
                transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>().material.color = c;
        }
    }

    public void Explode()
    {
        // Prevents exploding twice
        didExplode = true;

        // Unparent this
        transform.parent = null;

        // Manage the score
        ManageScore();

        // Destroy the cheese prefab after some time
        Destroy(this.gameObject, 1);

        DisableMeshColliders();

        // Before destroying, we are going to add a rigidbody to all the children
        AddRigidbodies();

        // After that, we can add a force to each one of these rigidbodies
        SplitCheese();

    }


    void ManageScore()
    {
        Game_Controller.SCORE += (2 * (Player_Controller.cheeseCombo));
    }


    void DisableMeshColliders()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).childCount > 0)
                transform.GetChild(i).GetChild(0).GetComponent<MeshCollider>().isTrigger = true;
        }
    }

    void AddRigidbodies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
                transform.GetChild(i).GetChild(0).gameObject.AddComponent<Rigidbody>();

            //ThrowPart(transform.GetChild(i));
        }
    }

    void SplitCheese()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
                ThrowPart(transform.GetChild(i).GetChild(0));
        }
    }

    void ThrowPart(Transform cheesePart)
    {
        // Store the rigidbody of the part
        Rigidbody partRigidbody = cheesePart.GetComponent<Rigidbody>();

        // Set the direction in which we are going to apply the force
        Vector3 dir = (cheesePart.GetComponent<MeshCollider>().bounds.center - transform.position);

        // Apply the force in that direction
        partRigidbody.AddForce(dir * forceMagnitude);
    }
}

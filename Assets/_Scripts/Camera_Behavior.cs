using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Behavior : MonoBehaviour {

    [Header(" Settings ")]
    public Transform player;
    public float yOffset;
    Vector3 initialPos;
    float initialDeltaY;
    float minimumPlayerY;

    public static bool resetCamera;

	// Use this for initialization
	void Start () {
        
        // Store the initial position of the camera
        initialPos = transform.position;
        initialDeltaY = player.position.y - initialPos.y;


    }
	
	// Update is called once per frame
	void Update () {

        // Move the camera according to the player movement
        MoveCamera();

        if(resetCamera)
        {
            minimumPlayerY = 0;
            transform.position = initialPos;
            resetCamera = false;
        }

	}

    void MoveCamera()
    {
        // Store the target y
        if (player.position.y < minimumPlayerY)
            minimumPlayerY = player.position.y;

        // Move the Camera accordingly
        transform.position = new Vector3(transform.position.x, minimumPlayerY - initialDeltaY + yOffset, transform.position.z);
    }
}

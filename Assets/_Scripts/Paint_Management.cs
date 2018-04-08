using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint_Management : MonoBehaviour {

    [Header(" Paint Prefab Settings ")]
    public GameObject paintSplashPrefab;
    public float paintSplashScale;

    [Header(" Particles ")]
    public ParticleSystem paintParticleSystem;

    [Header(" Trail Renderer ")]
    public TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {

        // Initialize the particle system color
        SetParticleSystemColor();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        // If we have collided with a cheese, spawn a paint
        if(collision.transform.tag == "Cheese")
        {
            // Spawn the paint
            SpawnPaint(collision);

            // Play the particle System
            paintParticleSystem.Play();
        }
    }

    void SpawnPaint(Collision c)
    {
        // Get the bounds of the cheese collided
        float halfYBound = -0.18f;

        float ySpawn = c.transform.position.y + halfYBound;

        // Set the spawn point to be the collision point
        Vector3 spawnPos = new Vector3(transform.position.x, ySpawn, transform.position.z);

        // Then spawn a paint at this position
        GameObject splashInstance = Instantiate(paintSplashPrefab, spawnPos, Quaternion.identity, c.transform);

        // Rescale the paint splash
        splashInstance.transform.localScale = Vector3.one * paintSplashScale;

        // Align the paint splash
        splashInstance.transform.forward = Vector3.up;

        // Rotate it randomly
        float randomAngle = Random.Range(0, 360);
        splashInstance.transform.localEulerAngles = new Vector3(splashInstance.transform.localEulerAngles.x, randomAngle, splashInstance.transform.localEulerAngles.z);

        // Change its color
        splashInstance.GetComponent<SpriteRenderer>().color = GetComponent<MeshRenderer>().material.color;
    }



    public void SetParticleSystemColor()
    {
        paintParticleSystem.startColor = GetComponent<MeshRenderer>().material.color;

        // Change the color of the trail renderer
        trailRenderer.startColor = paintParticleSystem.startColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [Header(" Translation Settings ")]
    public bool doLevitate;
    public float magnitude;
    public float frequency;
    Vector3 initialPos;

    [Header(" Rotation Settings ")]
    public bool doRotate;
    public Vector3 angles;

	// Use this for initialization
	void Start () {

        // Store the initial position
        initialPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        if (doLevitate)
            Levitate();

        if (doRotate)
            ApplyRotation();

	}

    void Levitate()
    {
        Vector3 pos = transform.position;
        pos.y = initialPos.y + magnitude * Mathf.Sin(Mathf.Deg2Rad * (Time.time * frequency));
        transform.position = pos;
    }

    void ApplyRotation()
    {
        transform.Rotate(angles);
    }
}

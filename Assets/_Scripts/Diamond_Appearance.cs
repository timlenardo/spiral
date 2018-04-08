using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond_Appearance : MonoBehaviour {

    [Header(" Probabilty Appearance ")]
    [Range(0,100)]
    public int appearanceChance;

	// Use this for initialization
	void Start () {

        int r = Random.Range(1, 101);

        if(r <= appearanceChance)
        {
            // The diaond will be shown
        }
        else
        {
            Destroy(this.gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

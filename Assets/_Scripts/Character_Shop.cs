using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Shop : MonoBehaviour {

    [Header(" Some Values ")]
    public int price;

    [Header(" Text for Price ")]
    Text priceText;

	// Use this for initialization
	void Start () {

        priceText = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        priceText.text = "" + price;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

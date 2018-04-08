using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Color_Change : MonoBehaviour {

    [Header(" Colors ")]
    public Color[] backgroundColors;
    public float changeAfter;
    float thisTime;
    float coroutineTime = 0;

    SpriteRenderer thisSpriteRenderer;

	// Use this for initialization
	void Start () {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(thisTime > changeAfter)
        {
            ChangeColor();
            thisTime = 0;
        }
        else
        {
            thisTime += Time.deltaTime;
        }

	}

    void ChangeColor()
    {
        StartCoroutine("ChangeColorCoroutine");
    }

    IEnumerator ChangeColorCoroutine()
    {
        float maxTime = 1;

        // Chose a random color
        Color c = backgroundColors[Random.Range(0, backgroundColors.Length)];

        while (thisSpriteRenderer.color != c)
        {
            thisSpriteRenderer.color = Color.Lerp(thisSpriteRenderer.color, c, 0.1f);

            coroutineTime += Time.deltaTime / 2;

            if(coroutineTime > maxTime)
            {
                coroutineTime = 0;
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime / 2);
        }

        thisSpriteRenderer.color = c;

        Debug.Log("Color Changed"); 

        yield return null;
    }
}

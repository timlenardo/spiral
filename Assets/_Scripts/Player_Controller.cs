using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EndlessManager;

public class Player_Controller : MonoBehaviour {

    [Header(" Bounce Settings ")]
    public Rigidbody thisRigidbody;
    public float bounceForce;
    Vector3 initialPosition;

    [Header(" Score Management ")]
    public static int cheeseCombo;

    public static bool resetTrigger;

    [Header(" Sounds ")]
    public AudioSource bounceSound;
    public AudioSource cheesePerfectExplodeSound;
    public AudioSource diamondSound;


	// Use this for initialization
	void Start () {

        initialPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(resetTrigger)
        {
            ResetPlayer();
            resetTrigger = false;
        }

	}

    private void OnCollisionEnter(Collision collision)
    {
        // When we collide with a cheese, apply the bounce force
        if(collision.transform.tag == "Cheese")
        {
            Bounce();

            // Check if the combo is greater or equal to 3 to explode the cheese
			if (cheeseCombo >= 3) {
				collision.transform.parent.GetComponentInParent<Cheese_Behavior> ().Explode ();
				collision.transform.parent.GetComponentInParent<Cheese_Behavior> ().TurnCheeseColored (new Color32 (0, 125, 255, 255));

				cheesePerfectExplodeSound.Play ();
			} else {
				iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
//				Handheld.Vibrate();
			}

            // Reset the cheese combo
            cheeseCombo = 0;
        }
        else if(collision.transform.tag == "Enemy")
        {
            // Check if the combo is greater or equal to 3 to explode the cheese
            if (cheeseCombo >= 3)
            {
                Bounce();

                collision.transform.parent.GetComponentInParent<Cheese_Behavior>().Explode();
                collision.transform.parent.GetComponentInParent<Cheese_Behavior>().TurnCheeseColored(new Color32(0, 125, 255, 255));

                cheesePerfectExplodeSound.Play();

            }
            else
            {
                //Game_Controller.SCORE = 0;
//				Handheld.Vibrate();
				iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);

                // This is a gameover unless we have passed 3 cheeses before hitting
                Game_Controller.gameoverTrigger = true;
            }

            // Reset the cheese combo
            cheeseCombo = 0;


        }
    }

    void Bounce()
    {
        thisRigidbody.velocity = (Vector3.up * bounceForce);

        bounceSound.Play();
    }

    void ResetPlayer()
    {
        transform.position = initialPosition;
    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Diamond")
        {
            // Destroy the diamond
            Destroy(other.gameObject);

            // Add one to the coins
            Game_Controller.COINS++;

            // Play the diamond sound
            diamondSound.Play();
        }
    }

}

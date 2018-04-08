using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perfect_Sounds_Manager : MonoBehaviour {

    [Header(" Perfect Sounds ")]
    public AudioSource[] perfectSounds;
    public static bool playPerfectSoundTrigger;

    [Header(" Cheese Explode ")]
    public AudioSource cheeseExplodeSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(playPerfectSoundTrigger)
        {
            PlayPerfectSound(Player_Controller.cheeseCombo);
            playPerfectSoundTrigger = false;
        }

	}

    void PlayPerfectSound(int soundIndex)
    {
        if(soundIndex < perfectSounds.Length-1)
        {
            // Play the appropriate perfect sound
            perfectSounds[soundIndex-1].Play();
        }
        else
        {
            // Play the last perfect sound
            perfectSounds[perfectSounds.Length - 1].Play();
        }

        // Play the cheese exploding sound
        //cheeseExplodeSound.Play();
    }
}

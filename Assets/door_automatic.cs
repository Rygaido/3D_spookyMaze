using UnityEngine;
using System.Collections;

public class door_automatic : MonoBehaviour {

	public GameObject player;

	public float closeRange = 5.0f;
	public float openRange = 2.0f;

	public bool isOpen = false;

	AudioSource sound;
	public AudioClip doorClose;
	public float volume = 1.0f;

	public float soundDelay = 0.5f;

	float close = 2.6f;
	float c;

	// Use this for initialization
	void Start () {
		
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = (player.transform.position - transform.position).magnitude;

		if (distance > closeRange) {
			if (isOpen) {
				isOpen = false;

				GetComponent<Animation>() ["Scene"].speed = -1;
				//GetComponent<Animation>().speed = -1;
				//GetComponent<Animation>().Play ("scene");
				GetComponent<Animation>().Play ();

				//sound.PlayOneShot(doorOpen,volume);
				sound.PlayDelayed(soundDelay * 4);
				c = close;
			}
		} else if(distance < openRange){
			if (!isOpen) {
				isOpen = true;
				
				GetComponent<Animation>() ["Scene"].speed = 1;
				//GetComponent<Animation>().speed = 1;
				//GetComponent<Animation>().Play ("scene");
				GetComponent<Animation>().Play ();
				
				//sound.PlayOneShot(doorOpen,volume);
				sound.PlayDelayed(soundDelay);
			}
		}

		if (c > 0) {
			c-=Time.deltaTime;

			if(c <= 0){
				sound.PlayOneShot(doorClose,volume);
			}
		}

	}
}

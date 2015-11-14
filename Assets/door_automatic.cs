using UnityEngine;
using System.Collections;

public class door_automatic : MonoBehaviour {

	public GameObject player;

	public float closeRange = 5.0f;
	public float openRange = 2.0f;

	public bool isOpen = false;

	// Use this for initialization
	void Start () {
	
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
			}
		} else if(distance < openRange){
			if (!isOpen) {
				isOpen = true;
				
				GetComponent<Animation>() ["Scene"].speed = 1;
				//GetComponent<Animation>().speed = 1;
				//GetComponent<Animation>().Play ("scene");
				GetComponent<Animation>().Play ();
			}
		}
	}
}

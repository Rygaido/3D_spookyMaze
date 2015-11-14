using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private float turnSpeed = 90; 		// turning speed

	public float maxVal = 45;			//highest the character can look
	public float minVal = -45;			//lowest the character can look
	private float currentVal;

	void Start(){
		currentVal = 0;
	}

	
	// Update is called once per frame
	void Update () {
	
		//If looking up
		if (Input.GetAxis ("Mouse Y") > 0) {
			if( currentVal < maxVal)
			{
				transform.Rotate (-Input.GetAxis ("Mouse Y") * turnSpeed * Time.deltaTime, 0, 0);
				currentVal += Input.GetAxis ("Mouse Y");
			}
		}
		//If looking down
		else if (Input.GetAxis ("Mouse Y") < 0) {
			if( currentVal > minVal){
				transform.Rotate (-Input.GetAxis ("Mouse Y") * turnSpeed * Time.deltaTime, 0, 0);
				currentVal += Input.GetAxis ("Mouse Y");
			}
		}


	}
}

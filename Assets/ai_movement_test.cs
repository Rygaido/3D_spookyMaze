using UnityEngine;
using System.Collections;

public class ai_movement_test : MonoBehaviour {
	
	float direction;
	float speed;
	
	float t;
	float timer;
	
	Random rand;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (t <= 0) {
			direction = Random.Range (0.01f, 360f);
			speed = Random.Range (-5, 5);
			if (speed < 0) {
				speed = 0;
			}
			timer = Random.Range (0, 5);
			t = timer;
			
			transform.Rotate(direction,0,0);
		}
		
		transform.Translate (Vector3.forward * (speed*Time.deltaTime));
		t -= Time.deltaTime;
		//direction = Vector3.Rotate(Vector3 (1, 0, 0),);
	}
}

using UnityEngine;
using System.Collections;

public class ai_movement_test : MonoBehaviour {

	float direction;
	float speed;
	public float speedMax = 4.0f;

	float rot;
	public float rotSpeed=1.0f;
	
	float t;
	float timer;
	public float timeMax = 2.0f;
	float delay;
	public float delayMax=3.0f;
	
	Random rand;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (t <= -delay) {
			direction = Random.Range (-180f, 180f);
			speed = Random.Range (-speedMax, speedMax);
			delay = Random.Range(0,delayMax);
			if (speed < 0) {
				speed = 0;
			}
			timer = Random.Range (1, timeMax);
			t = timer;
			//rotSpeed = 0.5f;
			//transform.Rotate(direction,0,0);
			//transform.Rotate(transform.forward,direction);
			transform.Rotate(transform.up,direction);
		}

		if (t > 0) {

			if(rot < Mathf.Abs(direction)){
				float d=(direction/rotSpeed) * Time.deltaTime;
				//float d=(rotSpeed/direction) * Time.deltaTime;
				rot += Mathf.Abs(d);
				transform.Rotate(transform.up, d);
			}
			transform.Translate (transform.forward * (speed * Time.deltaTime));
			//direction = Vector3.Rotate(Vector3 (1, 0, 0),);
		}
		
		t -= Time.deltaTime;
	}
}

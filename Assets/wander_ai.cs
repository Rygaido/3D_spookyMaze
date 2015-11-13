using UnityEngine;
using System.Collections;

public class wander_ai : MonoBehaviour {

	float direction;
	float speed;
	public float speedMax = 4.0f;

	float rot;
	public float rotSpeed=0.5f;
	
	float t;
	float timer;
	public float timeMax = 2.0f;
	float delay;
	public float delayMax=3.0f;
	
	Random rand;
	
	//if target is specified, ai is more likely to wander in its general direction
	public GameObject target;
	public float targetPriority = 1.0f;

	public AnimationCurve weight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (t <= -delay) {
			float weightAngle = 0.0f;
			if (targetPriority > 0 && target != null){
				//Vector3 a = (transform.position - target.transform.position);
				Vector3 a = (target.transform.position - transform.position);

				weightAngle = Vector3.Angle( Vector3.forward, a); //determine angle between up vector and mouse
				//float sign = Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(Vector3.up, a))); //get sign for rotation
				float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(Vector3.forward, a))); //get sign for rotation
				weightAngle *=sign; 
				Debug.Log(weightAngle+"\n");

				//Need to find angle to face object, currently just turning by that angle from whatever direction is facing
			}


			//direction = Random.Range (-180f, 180f);
			direction = weight.Evaluate(Random.value)*180f; //(-180f, 180f);
			if(Random.value >= 0.5f){
				direction*=-1f;
			}
			direction+=weightAngle;
			if(direction > 180){
				direction-=180;
				direction*=-1;
			}
			else if(direction < -180){
				direction += 180;
				direction*=-1;
			}
			//direction += Random.Range(0, weightAngle-direction);
			//direction += weight.Evaluate(Random.value) * weightAngle;

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
			//transform.Rotate(transform.up,direction);
			rot = 0;
		}

		if (t > 0) {

			if(rot < Mathf.Abs(direction)){
				float d=(direction/rotSpeed) * Time.deltaTime;
				//float d=(rotSpeed/direction) * Time.deltaTime;
				rot += Mathf.Abs(d);
			//	transform.Rotate(transform.up, d);
				transform.Rotate(Vector3.up, d);
			}
			//transform.Translate (transform.forward * (speed * Time.deltaTime));
			transform.Translate (Vector3.forward * (speed * Time.deltaTime));
			//direction = Vector3.Rotate(Vector3 (1, 0, 0),);
		}
		
		t -= Time.deltaTime;
	}
}

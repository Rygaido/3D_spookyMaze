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


	public Vector3 origin;
	public GameObject player;
	public bool isActivated;

	public float range = 10.0f;
	//if target is specified, ai is more likely to wander in its general direction
	Vector3 target;
	public float targetPriority = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (t <= -delay) {
			if((transform.position-player.transform.position).magnitude < range){
				isActivated = true;
			}
			else{
				isActivated = false;
			}

			float weightAngle = 0.0f;
			if (targetPriority > 0){
				if(isActivated){
					target = player.transform.position;
				}
				else{
					target = origin;
				}
				//Vector3 a = (transform.position - target.transform.position);
				//Vector3 targPos = target.transform.position;
				target.z = transform.position.z;
				Vector3 a = (target - transform.position);

				weightAngle = Vector3.Angle( transform.forward, a); //determine angle faced direction and facing target

				//float sign = Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(Vector3.up, a))); //get sign for rotation
				float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(Vector3.forward, a))); //get sign for rotation

				weightAngle *=sign; 
			}


			direction = Random.Range (-180f, 180f);
			direction += Random.Range(0, weightAngle-direction);

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

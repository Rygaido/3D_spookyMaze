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

	public float animSpeed = 2.0f;

	Random rand;


	Vector3 origin;
	public GameObject player;
	public bool isActivated;
	bool idle =false;

	public float range = 10.0f;
	//if target is specified, ai is more likely to wander in its general direction
	Vector3 target;
	public float targetPriority = 1.0f;

	AudioSource sound;
	public float volumeMax = 1.0f;
	float volumeFlux = 0.1f;
	float volume = 1.0f;
	public float soundDelay = 0.4f;
	float sd;
	public AudioClip footStep;

	public Vector3 myNormal = new Vector3(0,1,0);

	// Use this for initialization
	void Start () {
		origin = transform.position;
	}

	private void FixedUpdate(){
		float gravity = 10;
		// apply constant weight force according to enemy normal:
		GetComponent<Rigidbody>().AddForce(-gravity*GetComponent<Rigidbody>().mass*myNormal);		
	}

	// Update is called once per frame
	void Update () {
		if (transform.up != myNormal) {
			transform.rotation = Quaternion.FromToRotation(transform.up, myNormal);
		}
		if (t <= -delay) {
			if(player != null){ //detect player, or defult to origin settings
				if((transform.position-player.transform.position).magnitude < range){
					isActivated = true;
				}
				else{
					isActivated = false;
				}
			}

			//modify probability to face player
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

			//apply random direction, speed and timing
			direction = Random.Range (-180f, 180f);
			if(targetPriority >= 1){
				//direction /= targetPriority;
			}
			float dir = weightAngle-direction;
			float weight = Random.Range(0, dir);
			weight += (dir-weight) - ((dir-weight) /targetPriority);
			direction += weight;

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
			idle = false;

			//animation effect
			if(speed > 0.0f){
				GetComponent<Animation>() ["Armature.001|WalkCycle"].speed = speed/speedMax * animSpeed + 1;
				GetComponent<Animation>().Play ("Armature.001|WalkCycle");
			}

			soundDelay = soundDelay*speed/speedMax;

		}

		if (t > 0) { //walking

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

			//sound effect
			sd -= Time.deltaTime;
			if(sd <= 0){
				if(volume == volumeMax){
					volume -= volumeFlux;
				}
				else{
					volume += volumeFlux;
				}
				sound.PlayOneShot(footStep,volume);
				sd = soundDelay;
			}
		}
		else if(t<=0 && !idle){
			//GetComponent<Animation>() ["Armature.001|Idle"].speed = 1;
			//GetComponent<Animation>().Play ("Armature.001|Idle");
			//GetComponent<Animation>() [4].speed = 1;

			GetComponent<Animation>().Play ();
			idle = true;
		}
		
		t -= Time.deltaTime;
	}
}

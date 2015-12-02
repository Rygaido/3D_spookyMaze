using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterScript : MonoBehaviour {
	
	public float moveSpeed = 6;    	// move speed
	private float turnSpeed = 90; 		// turning speed (degrees/second)
	private float lerpSpeed = 5; 		// smoothing speed
	private float gravity = 10; 		// gravity acceleration
	
	private bool isGrounded;
	private bool isAlive;

	private float deltaGround = 0.2f; 	// character is grounded up to this distance
	private float jumpSpeed = 10; 		// vertical jump initial speed
	private float jumpRange = 2; 		// range to detect target wall
	private Vector3 surfaceNormal; 		// current surface normal
	private Vector3 myNormal; 			// character normal
	private Vector3 myForward;			// character forward
	private float distGround; 			// distance from character position to ground
	private bool jumping = false; 		// flag &quot;I'm jumping to wall&quot;
	private float vertSpeed = 0; 		// vertical jump current speed
	
	private Transform myTransform;
	public BoxCollider boxCollider; 	// drag BoxCollider ref in editor

	private Vector3 previousPosition;
	private Vector3 velocity;

	private void Start(){
		myNormal = transform.up; 			
		myForward = transform.forward;
		myTransform = transform;


		GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation
		// distance from transform.position to ground
		distGround = boxCollider.extents.y - boxCollider.center.y;

		isAlive = true;
	}
	
	private void FixedUpdate(){


		// apply constant weight force according to character normal:
		GetComponent<Rigidbody>().AddForce(-gravity*GetComponent<Rigidbody>().mass*myNormal);
		
	
	}
	
	private void Update(){
		//velocity = -transform.position + previousPosition;

		// jump code - jump to wall or simple jump
		if (jumping) return; // abort Update while jumping to a wall

		if (isAlive) {
		
			Ray ray;
			RaycastHit hit;


			//If player is pressing jump...
			if (Input.GetButtonDown ("Jump")) {

				//If there is a wall ahead, stick to it
				ray = new Ray (myTransform.position, myTransform.forward);
				if (Physics.Raycast (ray, out hit, jumpRange)) {
					if (hit.transform.tag == "Wall")
						JumpToWall (hit.point, hit.normal); // yes: jump to the wall
				}
			
			}


			// movement code
			myTransform.Rotate (0, Input.GetAxis ("Mouse X") * turnSpeed * Time.deltaTime, 0);


			ray = new Ray (myTransform.position, -myNormal); // cast ray downwards

			//If there is ground close enough below player, attach player to that ground
			if (Physics.Raycast (ray, out hit)) { 

				isGrounded = hit.distance <= distGround + deltaGround;
				surfaceNormal = hit.normal;

			} else {
				isGrounded = false;
				// assume usual ground normal to avoid "falling forever"
				surfaceNormal = Vector3.up;
			}





			myNormal = Vector3.Lerp (myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
			// find forward direction with new myNormal:
			myForward = Vector3.Cross (myTransform.right, myNormal);
			// align character to the new myNormal while keeping the forward direction:
			Quaternion targetRot = Quaternion.LookRotation (myForward, myNormal);
			myTransform.rotation = Quaternion.Lerp (myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
			// move the character forth/back with Vertical axis:
			myTransform.Translate (0, 0, Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime);
			myTransform.Translate (Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

			velocity =new Vector3(Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime,0,Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime);

			//automatically aquire wall directly under a corner when you step over it
			if(!isGrounded && !jumping){
				Vector3 p = myTransform.position-myNormal*(distGround+0.1f);
				float d = -Vector3.Dot(myNormal, -velocity);
				ray = new Ray (p, (myTransform.rotation*-velocity)); // cast ray backwards
				//ray = new Ray (p, -velocity + myNormal*d); // cast ray backwards
				//ray = new Ray (p, Quaternion.Inverse(myTransform.rotation * -myForward)); // cast ray backwards
				
				//If there is ground close enough below player, attach player to that ground
				if (Physics.Raycast (ray, out hit)) { 
					if (hit.transform.tag == "Wall"){
						isGrounded = true;
						//JumpToWall (myTransform.position - myTransform.up, myTransform.forward);
						JumpToWall (hit.point, hit.normal);
					}
				}
			}
		}
		//If player is dead, fall over and die
		else {




		}

		previousPosition = transform.position;
	}
	
	private void JumpToWall(Vector3 point, Vector3 normal){

		// jump to wall
		jumping = true; // signal it's jumping to wall
		GetComponent<Rigidbody>().isKinematic = true; // disable physics while jumping
		Vector3 orgPos = myTransform.position;
		Quaternion orgRot = myTransform.rotation;
		Vector3 dstPos = point + normal * (distGround + 0.0f); // will jump to 0.5 above wall
		Vector3 myForward = Vector3.Cross(myTransform.right, normal);
		Quaternion dstRot = Quaternion.LookRotation(myForward, normal);
		
		StartCoroutine (jumpTime (orgPos, orgRot, dstPos, dstRot, normal));
		//jumptime
	}
	
	private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal) {
		for (float t = 0.0f; t < 1.0f; ){
			t += Time.deltaTime;
			myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
			myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
			yield return null; // return here next frame
		}
		myNormal = normal; // update myNormal
		GetComponent<Rigidbody>().isKinematic = false; // enable physics
		jumping = false; // jumping to wall finished
		
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Enemy") {
			isAlive = false;
		}
	}
	
}
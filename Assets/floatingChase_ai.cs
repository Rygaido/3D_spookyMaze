using UnityEngine;
using System.Collections;

public class floatingChase_ai : MonoBehaviour {

	public float speed;

	public GameObject target;

	Vector3 direction;//

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		direction = target.transform.position - transform.position;

		direction.Normalize ();

		transform.translate (direction * speed);
	}
}

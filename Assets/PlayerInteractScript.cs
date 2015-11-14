using UnityEngine;
using System.Collections;

public class PlayerInteractScript : MonoBehaviour {

	public float interactDist = 1;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e")) {
			RaycastHit hitInfo;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)), out hitInfo, interactDist)) {
				if (hitInfo.collider.tag == "Door") {
					hitInfo.collider.transform.parent.GetComponent<Animation>().Play ();
				}
			}
		}
	}
}

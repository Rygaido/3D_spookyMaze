using UnityEngine;
using System.Collections;

public class PlayerInteractScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e")) {
			RaycastHit hitInfo;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)), out hitInfo, 100)) {
				if (hitInfo.collider.tag == "Door") {
					Debug.Log ("hardy-har");
					hitInfo.collider.GetComponent<Animator>().Play("Cube.001");
				}
			}
		}
	}
}

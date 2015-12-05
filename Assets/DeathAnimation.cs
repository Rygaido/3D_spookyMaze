using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour {

	bool playing = false;
	
	float frame = 0;
	public float fps = 30;
	
	Texture2D[] images;

	// Use this for initialization
	void Start () {
		images = Resources.LoadAll<Texture2D>("cutscenes/Death01");
	}
	
	void Update() {
		if (playing) {
			frame += fps * Time.deltaTime;
			
			if (frame >= images.Length) {
				Application.LoadLevel("Scene1");
			}
		}
		
		// for testing
		/*if (Input.GetKeyDown("f")) {
			Play();
		}*/
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (playing) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), images[(int)frame]);
		}
	}
	
	public void Play() {
		playing = true;
	}
}

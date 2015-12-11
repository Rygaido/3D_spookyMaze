using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour {

	bool playing = false;
	
	float frame = 0;
	public float fps = 30;
	
	Texture2D[][] images;

	int movie = 0;

	// Use this for initialization
	void Start () {
		images = new Texture2D[2][];
		images[0] = Resources.LoadAll<Texture2D>("cutscenes/Death01");
		images[1] = Resources.LoadAll<Texture2D>("cutscenes/Death02");
	}
	
	void Update() {
		if (playing) {
			frame += fps * Time.deltaTime;
			
			if (frame >= images[movie].Length) {
				Application.LoadLevel("Scene1");
			}
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (playing) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), images[movie][(int)frame]);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Enemy") {
			movieValue val = col.collider.GetComponent<movieValue>();
			if (val != null) {
				Play(val.movie);
			} else {
				Play();
			}
		}
	}


	public void Play() {
		playing = true;
	}
	public void Play(int movie) {
		this.movie = movie;
		playing = true;
	}
}

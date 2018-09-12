using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSounds : MonoBehaviour {

	AudioSource[] sounds;
	// Use this for initialization
	void Start () {
		sounds = this.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnnemyDie() {
		sounds[0].Play();
	}

	public void reduceMusicVolume() {
		sounds[1].volume -= 0.5f;
	}

	
	public void setMusicVolume() {
		sounds[1].volume -= 1f;
	}
}

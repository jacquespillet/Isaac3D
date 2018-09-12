using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource pickupKey;
	public AudioSource pickupBomb;
	public AudioSource pickupRedHeart;
	public AudioSource dieSound;
	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = this.GetComponents<AudioSource>();
		this.pickupKey = audioSources[0];
		this.pickupBomb = audioSources[1];
		this.pickupRedHeart = audioSources[2];
		this.dieSound = audioSources[3];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

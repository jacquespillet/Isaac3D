using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource pickupKey;
	public AudioSource pickupBomb;
	public AudioSource pickupRedHeart;
	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = this.GetComponents<AudioSource>();
		this.pickupKey = audioSources[0];
		this.pickupBomb = audioSources[1];
		this.pickupRedHeart = audioSources[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

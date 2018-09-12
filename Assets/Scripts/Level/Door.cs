using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	private AudioSource doorOpen;
	private AudioSource doorClose;
	// Use this for initialization
	void Awake () {
		AudioSource[] audiosources = this.GetComponents<AudioSource>();
		doorOpen = audiosources[0];
		doorClose = audiosources[1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void open() {
		this.doorOpen.Play();
		this.GetComponent<Animator>().Play("Open");
	}

	public void close() {
		this.doorClose.Play();
		this.GetComponent<Animator>().Play("Close");
	}
}

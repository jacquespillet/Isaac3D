using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialHeart : MonoBehaviour {
	public float value;
	// Use this for initialization
	void Start () {
		this.value = 1f;
	}

	public SpecialHeart setValue(float value) {
		this.value = value;
		return this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

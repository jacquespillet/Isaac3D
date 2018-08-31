using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadOnion : Bonus {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void activate(TearCaster tearCaster) {
		if(tearCaster.tearsCadence >= 0.2f) {
			tearCaster.tearsCadence -= 0.1f;
		}
		this.gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Bonus {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void activate(TearCaster tearCaster) {
		tearCaster.numShots = 3;
		tearCaster.tearsCadence += 0.1f;
		this.gameObject.SetActive(false);
	}
}

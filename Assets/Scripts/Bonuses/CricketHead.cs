using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketHead : Bonus {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void activate(TearCaster tearCaster) {
		tearCaster.tearPower += 0.5f;
		this.gameObject.SetActive(false);
	}
}

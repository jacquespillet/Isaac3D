using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonBlender : Bonus {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void activate(TearCaster tearCaster) {
		tearCaster.tearType = (GameObject) Resources.Load("Tears/HomingTear", typeof(GameObject));
		this.gameObject.SetActive(false);
	}
}

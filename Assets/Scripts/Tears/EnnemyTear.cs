using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyTear : MonoBehaviour {
	public float power;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Controls>().TakeDamage(this.power);
		}	
	}

}

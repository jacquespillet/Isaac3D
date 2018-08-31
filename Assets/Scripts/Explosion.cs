using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnParticleCollision(GameObject other)
	{
		if(other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Controls>().TakeDamage(1f);
		} else if(other.gameObject.tag == "Ennemy") {
			other.gameObject.GetComponent<Ennemy>().TakeDamage(3f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}

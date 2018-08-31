using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingTear : PlayerTear {
	
	void OnTriggerEnter(Collider other)
	{
		float currentSpeed = this.rigidBody.velocity.magnitude;
		if(other.gameObject.tag == "Ennemy") {
			this.rigidBody.velocity = (other.transform.position - this.transform.position) * currentSpeed / 10f;
		}
	}	

	IEnumerator disableTear() {
		yield return new WaitForSeconds (0.5f);
		Destroy(this.gameObject);
	}
}

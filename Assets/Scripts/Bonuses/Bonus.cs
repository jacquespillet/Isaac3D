using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour {
	private bool isActive = false;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player") {
			BonusManager bonusManager = other.gameObject.GetComponent<BonusManager>();
			bonusManager.addBonus(this);
		}	
	}


	public abstract void activate(TearCaster TearCaster);
}

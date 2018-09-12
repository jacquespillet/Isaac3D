using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Ennemy {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void preDieActions() {
		this.openTrap();
		this.popBonus();
	}

	private void openTrap() {
		((BossRoom) this.room).openTrap();
	}

	private void popBonus() {

	}

}

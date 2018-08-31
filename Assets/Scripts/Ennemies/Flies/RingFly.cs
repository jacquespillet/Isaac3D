using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFly : Ennemy {
	private float elapsedTime=0f;
	public Transform player;
	// Use this for initialization
	void Start () {
		this.body = this.GetComponent<Rigidbody>();
		this.animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDead) {
			Move();
		}
		checkIfDead();
	}


	protected override void Attack() {}

	protected override void Move() {
		if(Camera.main.transform.parent != null) {
			this.transform.LookAt(new Vector3(Camera.main.transform.parent.position.x, 2f, Camera.main.transform.parent.position.z));
			this.body.velocity = this.transform.forward * this.speed;
		}
	}
}

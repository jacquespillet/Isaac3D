using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFly : Ennemy {
	private float elapsedTime=0f;
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


	protected override void Attack() {
	}

	protected override void Move() {
		if(elapsedTime > 0.3f) {
			Vector3 velocity = this.body.velocity;
			float newX = velocity.x + Random.Range(-3, 3f);
			float newZ = velocity.z + Random.Range(-3f, 3f);
			this.body.velocity = new Vector3(newX, 0f, newZ).normalized * speed ;
			this.transform.forward = this.body.velocity.normalized;
			this.elapsedTime = 0f;
		}
		this.elapsedTime+= Time.deltaTime;
	}
}

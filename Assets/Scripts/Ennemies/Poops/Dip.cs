using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dip : Ennemy {
	float timeSinceMove = 0;

	// Use this for initialization
	void Start () {
		this.body = this.GetComponent<Rigidbody>();
		this.animator = this.transform.GetChild(0).GetComponent<Animator>();
	}

	void Update () {
		timeSinceMove += Time.deltaTime;
		if(!isDead) {
			Move();
		}
		checkIfDead();
	}

	protected override void burst() {
		
	}


	protected override void Attack() {
		// this.animator.SetTrigger("Attack");
		if(Random.Range(0f, 1f) > 0.5f) {
			this.animator.Play("DipAttack");
			this.animator.enabled = false;
			this.body.velocity = this.transform.forward * 10f + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
		}
		timeSinceMove = 0f;
	}

	protected override void Move() {
		this.animator.enabled = true;
		if(Camera.main.transform.parent != null) {
			this.transform.LookAt(new Vector3(Camera.main.transform.parent.position.x, 2f, Camera.main.transform.parent.position.z));
		}

		if(timeSinceMove >= 2f) {
			Attack();
		}
	}

	
	protected override IEnumerator changeMaterial(float timer) { 
		Material[] damagedMats = {this.damagedMat, this.damagedMat, this.damagedMat, this.damagedMat};
		Material[] mainMats = this.bodyGO.GetComponent<SkinnedMeshRenderer>().materials;

		this.bodyGO.GetComponent<SkinnedMeshRenderer>().materials = damagedMats;
		yield return new WaitForSeconds(0.1f);
		this.bodyGO.GetComponent<SkinnedMeshRenderer>().materials = mainMats;
	}
}

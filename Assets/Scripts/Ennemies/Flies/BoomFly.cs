using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomFly : Ennemy {
	public GameObject explosion;
	private bool hasExploded = false;
	private Vector3 currentDirection;
	private AudioSource explosionSound;
	// Use this for initialization
	void Start () {
		this.explosionSound = this.GetComponents<AudioSource>()[0];
		this.currentDirection = new Vector3(Random.Range(-4f, 4f), 0f, Random.Range(-4f, 4f));
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

	protected override void preDieActions() {
		if(!hasExploded) {
			this.explosionSound.Play();
			GameObject explosionGO = Instantiate(this.explosion, this.transform.position + this.transform.forward - this.transform.right * 0.5f, Quaternion.Euler(Vector3.zero), this.transform);
			hasExploded = true;
		}
	}

	protected override void collisionActions(Collision other) {
		if(other.gameObject.tag == "roomElement" ) {
			this.currentDirection = Vector3.Reflect(this.currentDirection, other.contacts[0].normal);
		}
	}

	protected override IEnumerator die() {
		yield return new WaitForSeconds (1f);
		Destroy(this.gameObject);
	}
	
	protected override void Attack() {}

	protected override void Move() {
		this.body.velocity = currentDirection * speed;
		if(Camera.main.transform.parent != null) {
			// this.transform.LookAt(new Vector3(Camera.main.transform.parent.position.x, 2f, Camera.main.transform.parent.position.z));

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : Boss {
	private float elapsedTime=0f;
	private GameObject tearPrefab;
	private AudioSource[] audiosources;

	// Use this for initialization

	void Awake()
	{
		this.damagedMat = (Material) Resources.Load("Materials/Damage");
	}

	void Start () {
		this.animator = this.GetComponent<Animator>();
		this.tearPrefab = (GameObject) Resources.Load("Tears/Ennemies/RedTear");
		this.audiosources = this.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!isDead) {
			Move();
		}
		checkIfDead();
	}

	protected override void Attack() {
		int random = Random.Range(0, 2);
		if(random >0) {
			this.jumpAttack();
		} else {
			this.spitAttack();
		}
	}

	protected override IEnumerator changeMaterial(float timer) { 
		Material[] damagedMats = {this.damagedMat, this.damagedMat, this.damagedMat, this.damagedMat};
		Material[] mainMats = this.bodyGO.GetComponent<SkinnedMeshRenderer>().materials;

		this.bodyGO.GetComponent<SkinnedMeshRenderer>().materials = damagedMats;
		yield return new WaitForSeconds(0.1f);
		this.bodyGO.GetComponent<SkinnedMeshRenderer>().materials = mainMats;
	}

	private void jumpAttack(){
		this.animator.Play("Jump");
		this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 60f, 0f) + this.transform.forward * 5f;
		this.audiosources[1].Play();
		StartCoroutine(comeBackIdle());
	}

	private void spitAttack() {
		this.animator.Play("Spit");
		for(int i=0; i<20; i++) {
			GameObject tear = Instantiate(this.tearPrefab, this.transform.position + this.transform.forward *5f - this.transform.right * 3f, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			tear.GetComponent<EnnemyTear>().power = this.attackPower;
			tear.GetComponent<Rigidbody>().velocity = this.transform.forward * 30f + new Vector3(Random.Range(-10, 10), Random.Range(15, 30), Random.Range(-10, 10));
			StartCoroutine(disableTear(0.6f, tear));		
		}
		this.audiosources[0].Play();
		StartCoroutine(comeBackIdle());
	}


	override protected void collisionActions(Collision other){
		if(other.gameObject.tag == "Floor") {
			this.audiosources[2].Play();
		}
	}


	IEnumerator comeBackIdle() {
		yield return new WaitForSeconds (1.5f);
		this.animator.Play("Idle");
	}
	IEnumerator disableTear(float timer, GameObject tear) {
		yield return new WaitForSeconds (timer);
		Destroy(tear);
	}

	protected override void Move() {
		this.transform.LookAt(Camera.main.transform.parent.position);
		this.elapsedTime+=Time.deltaTime;

		if(elapsedTime>= 5.0f) {
			Attack();
			elapsedTime = 0f;
		}
	}


}

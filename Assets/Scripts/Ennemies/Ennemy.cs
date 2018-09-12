using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ennemy : MonoBehaviour {
	public float life;
	public float speed;
	protected Rigidbody body;
	protected Animator animator;
	protected bool isDead = false;
	public bool touchDamage;
	public float attackPower;
	public Room room;
	public GameObject bodyGO;
	protected Material damagedMat;
	// Use this for initialization


	void Awake()
	{
		this.damagedMat = (Material) Resources.Load("Materials/Damage");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Tear") {
			this.TakeDamage(other.transform.GetComponent<PlayerTear>().power);
		}
		if(other.gameObject.tag == "Player" && this.touchDamage) {
			other.gameObject.GetComponent<Controls>().TakeDamage(this.attackPower);
		}
		this.collisionActions(other);	
	}

	virtual public void TakeDamage(float damage) {
			StartCoroutine(changeMaterial(0.1f));
			this.life -= damage;
	}
	

	protected void checkIfDead() {
		if(this.life <= 0) {
			this.room.currentLevel.GetComponent<LevelSounds>().EnnemyDie();
			this.preDieActions();
			this.animator.SetTrigger("isDead");
			this.isDead = true;
			StartCoroutine(die());
			this.postDieActions();
		}
	}

	virtual protected void collisionActions(Collision other){}

	virtual protected void preDieActions() {}
	virtual protected void postDieActions() {}

	virtual protected IEnumerator die() {
		burst();
		yield return new WaitForSeconds (0f);
		Destroy(this.gameObject);
	}

	virtual protected void burst() {}

	virtual protected IEnumerator changeMaterial(float timer) {
		Material mainMat = this.bodyGO.GetComponent<MeshRenderer>().material;
		this.bodyGO.GetComponent<MeshRenderer>().material = this.damagedMat;
		yield return new WaitForSeconds(timer);
		this.bodyGO.GetComponent<MeshRenderer>().material = mainMat;
	}

	abstract protected void Attack();
	abstract protected void Move();
}

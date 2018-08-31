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

	public GameObject bodyGO;
	private Material damagedMat;
	// Use this for initialization

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
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
			this.TakeDamage(1f);
		}
		if(other.gameObject.tag == "Player" && this.touchDamage) {
			other.gameObject.GetComponent<Controls>().TakeDamage(this.attackPower);
		}
		this.collisionActions(other);	
	}

	public void TakeDamage(float damage) {
			Material mainMat = this.bodyGO.GetComponent<MeshRenderer>().material;
			 this.bodyGO.GetComponent<MeshRenderer>().material = this.damagedMat;
			StartCoroutine(changeMaterial(mainMat, this.damagedMat, 0.1f));
			this.life -= damage;
	}
	

	protected void checkIfDead() {
		if(this.life <= 0) {
			this.preDieActions();
			this.animator.SetTrigger("isDead");
			this.isDead = true;
			StartCoroutine(die());
		}
	}

	virtual protected void collisionActions(Collision other){}

	virtual protected void preDieActions() {}

	virtual protected IEnumerator die() {
		yield return new WaitForSeconds (1f);
		Destroy(this.gameObject);
	}

	private IEnumerator changeMaterial(Material mainMat, Material otherMat, float timer) {
		this.bodyGO.GetComponent<MeshRenderer>().material = otherMat;
		yield return new WaitForSeconds(timer);
		this.bodyGO.GetComponent<MeshRenderer>().material = mainMat;
	}

	abstract protected void Attack();
	abstract protected void Move();
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTear : MonoBehaviour {
	protected Rigidbody rigidBody;
	public float power;

	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision other)
	{
		Destroy(this.gameObject);	
	}
	
	void Update () {
		
	}
}

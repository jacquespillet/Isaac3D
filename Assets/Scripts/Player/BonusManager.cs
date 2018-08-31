using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour {
	public List<Bonus> bonuses;
	
	// Use this for initialization
	void Start () {
		this.bonuses = new List<Bonus>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void addBonus(Bonus bonus) {
		bonus.activate(this.gameObject.GetComponent<TearCaster>());
		this.bonuses.Add(bonus);
	}
}

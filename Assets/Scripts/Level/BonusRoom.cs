using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BonusRoom : Room {

	public override void initElements(GameObject player) {
		if(!isInitialized && !isTerminated) {
			List<GameObject> elements = this.fileParser.parseEnnemies(this.level.type, this.type, roomId.ToString(), this.decorContainer.transform, this.ennemyContainer.transform, this.transform);
			this.initBonus();
			isInitialized = true;
			isTerminated = true;
		}
	}

	
	protected override void popItem() {}


	private void initBonus() {
		UnityEngine.Object[] bonuses = Resources.LoadAll("Bonuses/");
		int bonusesCount = bonuses.Length;
		int bonusIndex = UnityEngine.Random.Range(0, bonusesCount);
		GameObject bonusPrefab = (GameObject) bonuses[bonusIndex];
		// GameObject bonusPrefab = (GameObject) Resources.Load("Bonuses/SpoonBlender");
		
		Instantiate(bonusPrefab, this.transform.position + bonusPrefab.transform.position, Quaternion.Euler(0f, 0f, 0f), this.transform);
	}
}

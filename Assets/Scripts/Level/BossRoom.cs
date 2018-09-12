using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class BossRoom : Room {
	public GameObject trap;
	private AudioSource[] audiosources;

	public override void initElements(GameObject player) {
		if(!isInitialized && !isTerminated) {
			this.audiosources = this.GetComponents<AudioSource>();
			this.updateMinimap();
			player.GetComponent<Controls>().currentRoom = this;
			this.level.gameObject.GetComponent<LevelSounds>().reduceMusicVolume();
			this.audiosources[0].Play();
			this.closeDoors();
			InitBoss();
			isInitialized = true;
		}
	}

	protected override void popItem() {}

	private void InitBoss() {
		UnityEngine.Object[] bosses = Resources.LoadAll("Bosses/", typeof(GameObject));
		int bossesCount = bosses.Length;
		int bossIndex = UnityEngine.Random.Range(0, bossesCount);
		GameObject bossPrefab = (GameObject) bosses[bossIndex];
		// GameObject bonusPrefab = (GameObject) Resources.Load("Bonuses/SpoonBlender");
		
		GameObject ennemyGO = Instantiate(bossPrefab, this.transform.position + bossPrefab.transform.position, Quaternion.Euler(0f, 0f, 0f), this.ennemyContainer.transform);
		ennemyGO.GetComponent<Ennemy>().room = this;
		
		if(this.ennemyContainer.transform.childCount >0) {
			this.closeDoors();
		}
		isInitialized = true;
	}

	public void openTrap() {
		this.trap.GetComponent<Animator>().Play("Open");
	}

	public void reloadLevel() {
		this.level.reloadLevel();
	}

}

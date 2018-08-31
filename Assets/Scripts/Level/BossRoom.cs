using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BossRoom : Room {

	public override void initElements(GameObject player) {
		this.updateMinimap();
		player.GetComponent<Controls>().currentRoom = this;
		if(!isInitialized && !isTerminated) {
			this.closeDoors();
			// string path = Application.persistentDataPath + "/Rooms/" + this.level.type + "/" + this.type +  "/numRooms";
			// StreamReader reader = new StreamReader(path);
			// int numRooms = Int32.Parse(reader.ReadLine());
			// reader.Close();
			// int roomId = UnityEngine.Random.Range(1, numRooms+1);
			List<GameObject> elements = this.fileParser.parseEnnemies(this.level.type, this.type, roomId.ToString(), this.decorContainer.transform, this.ennemyContainer.transform, this.transform);
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
		
		Instantiate(bossPrefab, this.transform.position + bossPrefab.transform.position, Quaternion.Euler(0f, 0f, 0f), this.transform);

	}

}

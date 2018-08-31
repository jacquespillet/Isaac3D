using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	private string levelType, roomType;
	private Room room;
	public Door door;
	public bool hasDoor = false;
	public GameObject minimapWall;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(string levelType, string roomType, Room room) {
		this.levelType = levelType;
		this.roomType = roomType;
		this.room = room;
	}
	public void setDoor() {
		GameObject doorPrefab = Resources.Load("Levels/" + this.levelType + "/Doors/" + roomType) as GameObject;
		GameObject doorGO = Instantiate(doorPrefab, 
			this.transform.position, 
			Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
		Door door = new Door();
		door = doorGO.GetComponent<Door>();
		this.door = door;
		door.transform.SetParent(this.transform);
		door.transform.localEulerAngles= new Vector3(0f, 0f, 0f);
		this.hasDoor = true;
	}
}

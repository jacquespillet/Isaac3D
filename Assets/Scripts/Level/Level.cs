using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
	public List<Room> rooms;
	private string levelType = "Red";
	private int numRooms;
	private int roomsCreated = 0;
	private int numSpecialRooms = 0; //Boss and Bonus
	private FileParser fileParser;

	public Room currentRoom;
	public GameObject player;
	public GameObject UIManager;
	void Awake()
	{
		this.fileParser = new FileParser();
	}

	void Start () {
		InitLevel();
	}

	public void reloadLevel() {
		DontDestroyOnLoad(player.gameObject);
		player.gameObject.transform.position = new Vector3(0f, 5f, 0f);
		DontDestroyOnLoad(UIManager);
		SceneManager.LoadScene("newScene");
		// foreach (var room in rooms) 
		// {
		// 	foreach(var wall in room.walls) {
		// 		Destroy(wall.gameObject);
		// 	}
		// 	Destroy(room.gameObject);
		// }
		// this.numRooms = 0;
		// this.roomsCreated = 0;
		// this.numSpecialRooms = 0;
		// this.rooms.Clear();

		// this.player.transform.position = new Vector3(0f, 5f, 0f);
		// InitLevel();
	}

	// Update is called once per frame
	void Update () {
	}

	private void InitLevel() {
		int minRooms = 6;
		this.numRooms = UnityEngine.Random.Range(minRooms, minRooms+3);
		this.rooms = new List<Room>();
		
		this.CreateRoom(levelType, "Normal", new Vector3(0f, 0f, 0f));

		while(this.roomsCreated < this.numRooms - numSpecialRooms) {
			Vector3 roomPosition = this.getRoomPosition();
			this.CreateRoom(levelType, "Normal", roomPosition);
			this.recomputeNeighbours();
		}
		//Add Bonus
		Vector3 bonusPosition = this.getRoomPosition();
		this.CreateRoom(levelType, "Bonus", bonusPosition);
		this.recomputeNeighbours();

		//Add Boss
		Vector3 bossPosition = this.getRoomPosition();
		this.CreateRoom(levelType, "Boss", bossPosition);
		this.recomputeNeighbours();
	}


	private void CreateRoom(string levelType, string roomType, Vector3 position) {
		GameObject roomPrefab = Resources.Load("Levels/" + levelType + "/Grounds/" + roomType) as GameObject;
		GameObject roomGO = Instantiate(roomPrefab, position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
		Room room = new Room();

		if(roomType =="Normal") {
			room = roomGO.GetComponent<Room>();
			room.Init(this, position, roomsCreated, roomType);
			this.rooms.Add(room);
		} else if(roomType =="Bonus") {
			room = roomGO.GetComponent<BonusRoom>();
			room.Init(this, position, roomsCreated, roomType);
			this.rooms.Add(room);
		} else if(roomType =="Boss") {
			room = roomGO.GetComponent<BossRoom>();
			room.Init(this, position, roomsCreated, roomType);
			this.rooms.Add(room);
		}

		string path = Application.persistentDataPath + "/Rooms/" + this.type + "/" + roomType + "/numRooms";
		StreamReader reader = new StreamReader(path);
		int numRooms = Int32.Parse(reader.ReadLine());
		reader.Close();
		int roomId = UnityEngine.Random.Range(1, numRooms+1);
		room.roomId = roomId;

		List<GameObject> elements = this.fileParser.parseDecors(this.type, roomType, roomId.ToString(), room.decorContainer.transform, room.ennemyContainer.transform, room.transform);

		
		this.roomsCreated++;
	}

	private Vector3 getRoomPosition() {
		bool canAddRoom = false;
		while(!canAddRoom) {
			int roomIndex = UnityEngine.Random.Range(0, this.rooms.Count);
			Room room = this.rooms[roomIndex];
			int direction = UnityEngine.Random.Range(0, 4);
			if(!room.hasNeighbour(direction, rooms)) {
				canAddRoom = true;
				Vector3 position = room.transform.position + room.getOffset(direction);
				return position;
			}
		}
		return new Vector3(0f, 0f, 0f);
	}

	private Vector3 getBossRoomPosition() {
		return new Vector3(0f, 0f, 0f);
	}

	private void recomputeNeighbours() {
		for(int i=0; i<rooms.Count; i++) {
			for(int j=0; j<Variables.DIRECTIONS_ARRAY.Length; j++) {
				RaycastHit hit;
				int layerMask = 1 << 8;
				if (Physics.Raycast(rooms[i].transform.position + Variables.DIRECTIONS_ARRAY[j] * (Variables.ROOM_SIZE / 2 -1f), 
					Variables.DIRECTIONS_ARRAY[j], 
					out hit, 
					1.5f,
					layerMask))
				{
					rooms[i].neighbours[j] = 1;
					rooms[i].neighbourRooms[j] = hit.transform.parent.GetComponent<Room>();
				} else {
					rooms[i].neighbours[j] = 0;
				}
			}
			rooms[i].SetWalls();
		}
	}

	public List<Room> Rooms {
		get { return rooms; }
	}

	public string type {
		get { return levelType; }
	}
}

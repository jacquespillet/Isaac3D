using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Room : MonoBehaviour {
	public int[] neighbours;
	public Room[] neighbourRooms;
	public Wall[] walls;
	protected Level level;
	public Level currentLevel;

	public string type;
	private int index;
	protected FileParser fileParser;
	protected bool isInitialized = false;
	protected bool isTerminated = false;	
	
	public GameObject decorContainer;
	public GameObject ennemyContainer;

	//Minimap 
	public GameObject minimapFloor;
	public Material minimapMaterial;
	private Material currentRoomMaterial;
	public int roomId;
	void Awake () {
		this.fileParser = new FileParser();
		this.currentRoomMaterial = (Material) Resources.Load("UI/Materials/CurrentRoom", typeof(Material));
	}
	
	// Update is called once per frame
	void Update () {
		if(!isTerminated) {
			checkIfTerminated();
		}
	}

	private void checkIfTerminated() {
		if(this.ennemyContainer.transform.childCount == 0 && this.isInitialized) {
			this.isTerminated = true;
			this.openDoors();
			this.popItem();
		}
	}

	virtual protected void popItem() {
		float popProba = UnityEngine.Random.Range(0f, 1f);
		if(popProba > 0.3f) {
			UnityEngine.Object[] popables =  Resources.LoadAll("Popable", typeof(GameObject));
			int objectIndex = UnityEngine.Random.Range(0, popables.Length);
			GameObject prefab = (GameObject) popables[objectIndex]; 
			Instantiate(prefab, this.transform.position + prefab.transform.position, Quaternion.Euler(Vector3.zero), this.transform);
		}	
	}

	private void openDoors() {
		List<Door> doors = this.getDoors();
		for(int i=0; i<doors.Count; i++) {
			doors[i].open();
		}
	}
	protected void closeDoors() {
		List<Door> doors = this.getDoors();
		for(int i=0; i<doors.Count; i++) {
			doors[i].close();
		}
	}

	public void killAllEnnemies() {
		for(int i=0; i<this.ennemyContainer.transform.childCount; i++) {
			Ennemy ennemy = this.ennemyContainer.transform.GetChild(i).GetComponent<Ennemy>();
			ennemy.TakeDamage(10f);
		}
	}


	virtual public void initElements(GameObject player) {
		this.updateMinimap();
		this.level.currentRoom = this;
		this.level.player = player;

		player.GetComponent<Controls>().currentRoom = this;
		if(!isInitialized && !isTerminated) {
			List<GameObject> elements = this.fileParser.parseEnnemies(this, this.level.type, this.type, roomId.ToString(), this.decorContainer.transform, this.ennemyContainer.transform, this.transform);
			if(this.ennemyContainer.transform.childCount >0) {
				this.closeDoors();
			}
			isInitialized = true;
		}
	}



	protected void updateMinimap() {
		for(int i=0; i<this.neighbourRooms.Length; i++) {
			if(neighbourRooms[i] != null) {
				neighbourRooms[i].setMinimapMaterial(false);
			}
		}
		this.setMinimapMaterial(true);
	}

	public void setMinimapMaterial(bool isCurrent) {
		this.minimapFloor.SetActive(true);
		for(int i=0; i<this.walls.Length; i++) {
			walls[i].minimapWall.SetActive(true);
		}
		if(isCurrent) {
			this.minimapFloor.GetComponent<MeshRenderer>().material = this.currentRoomMaterial;
		} else {
			this.minimapFloor.GetComponent<MeshRenderer>().material = this.minimapMaterial;			
		}
	}

	public void Init(Level level, Vector3 position, int index, string type) {
		this.type = type;
		this.transform.position = position;
		this.neighbours = new int[]{0, 0, 0, 0};
		this.neighbourRooms = new Room[4];
		this.walls = new Wall[4];
		this.level = level;
		this.currentLevel = level;
		this.index = index;
	}

	public bool hasNeighbour(int direction, List<Room> rooms) {
		if(this.neighbours[direction] == 0) return false;
		else return true;
	}

	public Vector3 getOffset(int direction) {
		return Variables.DIRECTIONS_ARRAY[direction] * Variables.ROOM_SIZE;
	}

	private List<Door> getDoors() {
		List<Door> doors = new List<Door>();
		for(int i=0; i<4; i++) {
			if(this.walls[i] != null && this.walls[i].hasDoor) {
				doors.Add(this.walls[i].door);
			}
		}
		return doors;
	}

	public void SetWalls() {
		for(int i=0; i<Variables.DIRECTIONS_ARRAY.Length; i++) {
			if(this.walls[i] != null) {
				Destroy(this.walls[i].gameObject);
			}

			if(this.neighbours[i] == 0) {
				GameObject wallPrefab = Resources.Load("Levels/" + this.level.type + "/Walls/" + "WallWithoutDoor") as GameObject;
				GameObject wallGO = Instantiate(wallPrefab, 
					this.transform.position + Variables.DIRECTIONS_ARRAY[i] * (Variables.ROOM_SIZE / 2) + new Vector3(0f, 5f, 0f), 
					Quaternion.Euler(Variables.ROTATIONS_ARRAY[i])) as GameObject;
				Wall wall= new Wall();
				wall = wallGO.GetComponent<Wall>();
				this.walls[i] = wall;
			} 
			else if(this.neighbours[i] == 1) {
				if(this.neighbourRooms[i].walls[(i+2) % 4] != null) {
					Destroy(this.neighbourRooms[i].walls[(i+2) % 4].gameObject);
				}

				GameObject wallPrefab = Resources.Load("Levels/" + this.level.type + "/Walls/" + "WallWithDoor") as GameObject;
				GameObject wallGO = Instantiate(wallPrefab, 
					this.transform.position + Variables.DIRECTIONS_ARRAY[i] * (Variables.ROOM_SIZE / 2) + new Vector3(0f, 5f, 0f), 
					Quaternion.Euler(Variables.ROTATIONS_ARRAY[i])) as GameObject;
				Wall wall= new Wall();
				wall = wallGO.GetComponent<Wall>();
				wall.Init(this.level.type, this.type, this);
				wall.setDoor();

				this.walls[i] = wall;
				this.neighbourRooms[i].walls[(i+2) % 4] = wall;
			}
		}
	}
}

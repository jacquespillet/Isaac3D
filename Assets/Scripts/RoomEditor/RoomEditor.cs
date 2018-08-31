using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

public class RoomEditor : MonoBehaviour {
	public GameObject DecorContainer, EnnemyContainer; 
	public TextAsset Redrooms;

	public Dropdown levelTypeDropdown;
	private string currentlevelType;

	
	public Dropdown roomType;
	private string currentRoomType;

	private GameObject currentRoom;


	public Dropdown decorDropdown;
	private List<UnityEngine.Object> decorsList;

	
	public Dropdown ennemyDropdown;
	private List<UnityEngine.Object> ennemiesList;

	
	public Dropdown sceneDropdown;
	private string[] scenes;


	public Material selected;
	private GameObject selectedGameObject;
	private bool isSelected = false;
	GameObject lastSelected;

	// Use this for initialization
	void Start () {
		this.decorsList = new List<UnityEngine.Object>();
		this.ennemiesList = new List<UnityEngine.Object>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePos = new Vector3(0f, 0f, 0f);
        if (Physics.Raycast(ray, out hit)) {
			if(hit.transform.tag != "Floor") {
				if(Input.GetMouseButtonDown(0)) {
					this.isSelected = true;
					this.selectedGameObject = hit.transform.gameObject;
				} 
				if(Input.GetMouseButtonUp(0)) {
					this.lastSelected = selectedGameObject;
					this.isSelected = false;
					this.selectedGameObject = null;
				}
			}
			mousePos = new Vector3(hit.point.x, 0f, hit.point.z);
        }

		if(this.isSelected){
			this.selectedGameObject.transform.position = new Vector3(Mathf.Round(mousePos.x), this.selectedGameObject.transform.position.y, Mathf.Round(mousePos.z));
		}

		if(Input.GetKeyDown(KeyCode.Delete)) {
			Destroy(lastSelected.gameObject);
		}
	}

	public void displayRoom() {
		if(this.currentRoom != null) {
			for(int i=0; i<this.EnnemyContainer.transform.childCount; i++) {
				Destroy(this.EnnemyContainer.transform.GetChild(i).gameObject);
			}
			for(int i=0; i<this.DecorContainer.transform.childCount; i++) {
				Destroy(this.DecorContainer.transform.GetChild(i).gameObject);
			}
			Destroy(this.currentRoom.gameObject);
			this.decorsList.Clear();
			this.ennemiesList.Clear();
			this.sceneDropdown.ClearOptions();
			this.decorDropdown.ClearOptions();
			this.ennemyDropdown.ClearOptions();
		}
		
		//Instantiate Terrain
		int value = this.levelTypeDropdown.value;
		string levelType = this.levelTypeDropdown.options[value].text;
		this.currentlevelType = levelType;
		
		value = this.roomType.value;
		string roomType= this.roomType.options[value].text;
		this.currentRoomType = roomType;
		GameObject roomPrefab = Resources.Load("Levels/" + levelType + "/Grounds/" + roomType) as GameObject;
		this.currentRoom = Instantiate(roomPrefab, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;

		//Init Decor dropdown
		UnityEngine.Object[] rocks = Resources.LoadAll("Levels/" + levelType + "/Decors", typeof(GameObject));
		List<string> DecorOptions = new List<string>();
		for(int i=0; i<rocks.Length; i++) {
			DecorOptions.Add(rocks[i].name);
			decorsList.Add(rocks[i]);
		}
		this.decorDropdown.AddOptions(DecorOptions);

		
		//Init Ennemies dropdown
		UnityEngine.Object[] ennemies = Resources.LoadAll("Ennemies", typeof(GameObject));
		List<string> EnnemiesOptions = new List<string>();
		for(int i=0; i<ennemies.Length; i++) {
			EnnemiesOptions.Add(ennemies[i].name);
			ennemiesList.Add(ennemies[i]);
		}
		this.ennemyDropdown.AddOptions(EnnemiesOptions);

		//Init Scenes dropdown
		string path =	Application.persistentDataPath + "/Rooms/" + this.currentlevelType + "/" + this.currentRoomType;
		this.scenes = Directory.GetFiles(path);
		List<String> sceneOptions = new List<String>();
		for(int i=0; i<this.scenes.Length; i++) {
			sceneOptions.Add(Path.GetFileName(this.scenes[i]));
		}
		this.sceneDropdown.AddOptions(sceneOptions);
	}

	public void loadScene() {
		for(int i=0; i<this.EnnemyContainer.transform.childCount; i++) {
			Destroy(this.EnnemyContainer.transform.GetChild(i).gameObject);
		}
		for(int i=0; i<this.DecorContainer.transform.childCount; i++) {
			Destroy(this.DecorContainer.transform.GetChild(i).gameObject);
		}
		int value = this.sceneDropdown.value;
		string path = this.scenes[value];
		// string levelType, string roomType, string roomId, Transform decorContainer, Transform ennemyContainer, Transform floorTransform) {
		FileParser parser = new FileParser();
		parser.parse(path,
			this.currentlevelType,
			this.DecorContainer.transform, 
			this.EnnemyContainer.transform, 
			this.currentRoom.transform);
		for(int i=0; i<this.EnnemyContainer.transform.childCount; i++) {
			if(this.EnnemyContainer.transform.GetChild(i).GetComponent<Ennemy>() != null) {
				this.EnnemyContainer.transform.GetChild(i).GetComponent<Ennemy>().enabled =false;
			}
		}
	}

	public void addDecor() {
		int value = this.decorDropdown.value;
		GameObject toInstantiate = (GameObject) this.decorsList[value];
		Instantiate(toInstantiate, this.DecorContainer.transform);
	}

	public void addEnnemy() {
		int value = this.ennemyDropdown.value;
		GameObject toInstantiate = (GameObject) this.ennemiesList[value];
		GameObject instantiated = Instantiate(toInstantiate, this.EnnemyContainer.transform) as GameObject;
		instantiated.GetComponent<Ennemy>().enabled = false;
	}

	public void SaveNewRoom() {
		int numRoom = this.incrementNumRoom();
		string path =	Application.persistentDataPath + "/Rooms/" + this.currentlevelType + "/" + this.currentRoomType + "/" +  numRoom;
	
		StreamWriter writer = new StreamWriter(path, true);		
		writer.WriteLine("roomType;" + this.currentRoomType);
		WriteElements("decor", this.DecorContainer.transform, writer);
		WriteElements("ennemy", this.EnnemyContainer.transform, writer);
        writer.Close();
	}

	public void SaveExistingRoom() {
		int value = this.sceneDropdown.value;
		string path = this.scenes[value];
		StreamWriter writer = File.CreateText(path);
		writer.WriteLine("roomType;" + this.currentRoomType);
		WriteElements("decor", this.DecorContainer.transform, writer);
		WriteElements("ennemy", this.EnnemyContainer.transform, writer);
        writer.Close();
	}



	private void WriteElements(string title, Transform container, StreamWriter writer) {
		for(int i=0; i<container.childCount; i++) {
			GameObject instance = container.GetChild(i).gameObject;
			string prefabName = instance.name;
			int cloneIndex = prefabName.IndexOf("(Clone");
			prefabName = prefabName.Remove(cloneIndex, 7);
			writer.WriteLine(title + ";" + prefabName + ";" + instance.transform.position.x + ";" + instance.transform.position.y + ";" +instance.transform.position.z + ";");
		}
	}

	private int incrementNumRoom() {
		string path = Application.persistentDataPath + "/Rooms/" + this.currentlevelType + "/"  + this.currentRoomType +  "/numRooms";
		StreamReader reader = new StreamReader(path);
		int numRooms = Int32.Parse(reader.ReadLine());
        reader.Close();
		numRooms++;

		StreamWriter writer = File.CreateText(path);
		writer.WriteLine(numRooms);
		writer.Close();


		return numRooms;
	}
}
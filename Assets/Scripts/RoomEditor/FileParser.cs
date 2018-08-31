using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileParser {
	public List<GameObject> parseEnnemies(string levelType, string roomType, string roomId, Transform decorContainer, Transform ennemyContainer, Transform floorTransform) {
		string path =	Application.persistentDataPath + "/Rooms/" + levelType + "/" + roomType + "/" + roomId;
		StreamReader reader = new StreamReader(path);
		string text = reader.ReadToEnd();
		reader.Close();
		string[] lines = text.Split(new string[] {"\n"}, StringSplitOptions.None);
		// string roomType = "";
		List<GameObject> gos = new List<GameObject>();

		for(int i=0; i<lines.Length; i++) {
			string[] elements = lines[i].Split(new string[] {";"}, StringSplitOptions.None);
			if(elements[0] == "ennemy") {
				GameObject ennemyPrefab = Resources.Load("Ennemies/" + elements[1]) as GameObject;
				Vector3 position = new Vector3( Int32.Parse(elements[2]), Int32.Parse(elements[3]), Int32.Parse(elements[4]));
				GameObject ennemyGO =  GameObject.Instantiate(ennemyPrefab, position + floorTransform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)), ennemyContainer);
				gos.Add(ennemyGO);
			}
		}			
		return gos;
	}

	public List<GameObject> parseDecors(string levelType, string roomType, string roomId, Transform decorContainer, Transform ennemyContainer, Transform floorTransform) {
		string path =	Application.persistentDataPath + "/Rooms/" + levelType + "/" + roomType + "/" + roomId;
		StreamReader reader = new StreamReader(path);
		string text = reader.ReadToEnd();
		reader.Close();
		string[] lines = text.Split(new string[] {"\n"}, StringSplitOptions.None);
		// string roomType = "";
		List<GameObject> gos = new List<GameObject>();

		for(int i=0; i<lines.Length; i++) {
			string[] elements = lines[i].Split(new string[] {";"}, StringSplitOptions.None);
			if(elements[0] == "decor") {
				GameObject decorPrefab = Resources.Load("Levels/" + levelType + "/Decors/" + elements[1]) as GameObject;
				Vector3 position = new Vector3( Int32.Parse(elements[2]), Int32.Parse(elements[3]), Int32.Parse(elements[4]));
				GameObject decorGO =  GameObject.Instantiate(decorPrefab, position + floorTransform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)), decorContainer);
				gos.Add(decorGO);
			}
		}			
		return gos;
	}

	public List<GameObject> parse(string path, string levelType, Transform decorContainer, Transform ennemyContainer, Transform floorTransform) {
		StreamReader reader = new StreamReader(path);
		string text = reader.ReadToEnd();
		reader.Close();
		string[] lines = text.Split(new string[] {"\n"}, StringSplitOptions.None);
		// string roomType = "";
		List<GameObject> gos = new List<GameObject>();

		for(int i=0; i<lines.Length; i++) {
			string[] elements = lines[i].Split(new string[] {";"}, StringSplitOptions.None);
			if(elements[0] == "decor") {
				GameObject decorPrefab = Resources.Load("Levels/" + levelType + "/Decors/" + elements[1]) as GameObject;
				Vector3 position = new Vector3( Int32.Parse(elements[2]), Int32.Parse(elements[3]), Int32.Parse(elements[4]));
				GameObject decorGO =  GameObject.Instantiate(decorPrefab, position + floorTransform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)), decorContainer);
				gos.Add(decorGO);
			}
			if(elements[0] == "ennemy") {
				GameObject ennemyPrefab = Resources.Load("Ennemies/" + elements[1]) as GameObject;
				Vector3 position = new Vector3( Int32.Parse(elements[2]), Int32.Parse(elements[3]), Int32.Parse(elements[4]));
				GameObject ennemyGO =  GameObject.Instantiate(ennemyPrefab, position + floorTransform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)), ennemyContainer);
				gos.Add(ennemyGO);
			}
		}			
		return gos;
	}
}

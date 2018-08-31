using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundListener : MonoBehaviour {
	public Room room;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
			room.initElements(col.gameObject);
        }
    }
}

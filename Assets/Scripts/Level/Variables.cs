using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables {
	public static float ROOM_SIZE = 35f;

	public static Vector3 TOP_DIRECTION = new Vector3(0f, 0f, 1f);
	public static Vector3 RIGHT_DIRECTION = new Vector3(1f, 0f, 0f);
	public static Vector3 BOTTOM_DIRECTION = new Vector3(0f, 0f, -1f);
	public static Vector3 LEFT_DIRECTION = new Vector3(-1f, 0f, 0f);
	
	public static Vector3[] DIRECTIONS_ARRAY = new Vector3[] {
		TOP_DIRECTION,
		RIGHT_DIRECTION,
		BOTTOM_DIRECTION,
		LEFT_DIRECTION
	};

	public static Vector3[] ROTATIONS_ARRAY = new Vector3[] {
		new Vector3(0f, 0f, 0f),
		new Vector3(0f, 90f, 0f),
		new Vector3(0f, 0f, 0f),
		new Vector3(0f, 90f, 0f)
	};
}

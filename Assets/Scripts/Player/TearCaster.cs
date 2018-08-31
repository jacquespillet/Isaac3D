using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearCaster : MonoBehaviour {

	private float timeBetweenShots;
	private bool isCasting;
	public int numShots;
	public float tearsCadence;
	public float shotSpeed;
	public float range;
	public List<GameObject> tears;
	private AudioSource tearCastSound;
	public GameObject tearType;
	public bool isPlayer;

	// Use this for initialization
	void Start () {
		tears = new List<GameObject> ();
		// this.tearType = (GameObject) Resources.Load("Tears/Tear", typeof(GameObject));
		AudioSource[] audioSources = this.GetComponents<AudioSource>();
		this.tearCastSound = audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayer) {
			if (Input.GetMouseButton (0)) {
				isCasting = true;
			} else {
				isCasting = false;
			}
		} else {
			int randomNumber = Random.Range(0, 2);
			if(randomNumber>0) {
				isCasting = true;
			} else {
				isCasting = false;				
			}
		}

		if (isCasting) {
			CastTears ();
		}
	}

	void CastTears() {
		timeBetweenShots += Time.deltaTime;
		if (timeBetweenShots > tearsCadence) {
			this.tearCastSound.Play();
			getTears();
			timeBetweenShots = 0f;
		}
	}

	private void getTears() {
		if(this.numShots == 1) {
			GameObject tear = Instantiate (tearType,this.transform.position + transform.forward*2f  , Quaternion.Euler(Vector3.zero)) as GameObject;
			tears.Add (tear);
			tear.GetComponent<Rigidbody> ().velocity = transform.forward * shotSpeed;
			StartCoroutine(disableTear (range, tear));
		} else if(this.numShots ==2) {
			for(float i=-0.5f; i<=0.5f; i+=1) {
				Vector3 tearPosition = this.transform.position + transform.forward*2f + transform.right * i; 
				GameObject tear = Instantiate (
					tearType, 
					tearPosition, 
					Quaternion.Euler(Vector3.zero)
				) as GameObject;
				
				tears.Add (tear);
				tear.GetComponent<Rigidbody> ().velocity = transform.forward * shotSpeed;
				StartCoroutine(disableTear (range, tear));
			}
		} else if(this.numShots ==3) {
			for(int i=-1; i<2; i++) {
				Vector3 tearPosition = this.transform.position + transform.forward*2f ; 
				GameObject tear = Instantiate (
					tearType, 
					tearPosition, 
					Quaternion.Euler(Vector3.zero)
				) as GameObject;
				tears.Add (tear);
				tear.GetComponent<Rigidbody> ().velocity = (transform.forward + (transform.right * 0.2f) * i) * shotSpeed;
				StartCoroutine(disableTear (range, tear));
			}
		}
	}

	IEnumerator disableTear(float timer, GameObject tear) {
		yield return new WaitForSeconds (timer);
		Destroy(tear);
	}
}

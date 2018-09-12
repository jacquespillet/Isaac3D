using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
	public float maxLife;
	private float currentLife;
	public float speed = 2f;
	public float MAX_FORWARD_Y = 0.80f;
	public CursorLockMode wantedMode;
	private AudioSource[] hurtSound;
	public GameObject redScreen;
	public bool isRedScreen;
	public GameObject UILifeContainer;
	public SoundManager soundManager;

	public Room currentRoom;

	public MainMenu menu;

	private bool isDead;
	private bool isInMenu;
	public GameObject inGameUI;

	//For blue and black hearts
	public List<SpecialHeart> additiveHearts;    
	
	public bool created = false;

    void Awake()
    {
		Debug.Log("CREATE");
        
		// DontDestroyOnLoad(this.gameObject);
		this.isDead = false;
		this.isInMenu = false;
		this.additiveHearts = new List<SpecialHeart>();

		this.currentLife = maxLife;
		this.updateLifeUI();
		Cursor.lockState = wantedMode;

		menu.controls = this;

		AudioSource[] audioSources = this.GetComponents<AudioSource>();
		this.hurtSound = new AudioSource[] {
			audioSources[1],
			audioSources[2],
			audioSources[3]
		};
    }

	void Start () {
	}

	public void TakeDamage(float damage) {
		if(!this.isRedScreen) {
			if(this.additiveHearts.Count == 0) {
				this.currentLife -= damage;
			} else {
				SpecialHeart heart = this.additiveHearts[this.additiveHearts.Count-1]; 
				heart.value -= damage;
				if(heart.value <= 0) {
					if(heart.GetType() == typeof(BlackHeart)) {
						this.currentRoom.killAllEnnemies();
					}
					this.additiveHearts.Remove(heart);
				}
			}


			if(this.currentLife <=0) {
				this.die();				
			} else {
				int index = Random.Range(0, this.hurtSound.Length);
				this.hurtSound[index].Play();
				this.redScreen.SetActive(true);
				this.isRedScreen = true;
				this.updateLifeUI();
				StartCoroutine(unableObject(this.redScreen, 0.3f));
			}
		}
	}

	private void die() {
		this.updateLifeUI();
		this.inGameUI.SetActive(false);
		this.soundManager.dieSound.Play();
		this.menu.gameObject.SetActive(true);
		this.menu.isDead = true;
		this.isDead = true;
		this.currentRoom.ennemyContainer.SetActive(false);
	}
	

	private void updateLifeUI() {
		int i=0;
		for(i=0; i<this.UILifeContainer.transform.childCount; i++) {
			Destroy(this.UILifeContainer.transform.GetChild(i).gameObject);
		}

		int numLifes = (int) System.Math.Truncate(this.currentLife);
		GameObject heartPrefab = (GameObject) Resources.Load("UI/Hearts/Heart", typeof(GameObject));

		//add full hearts
		for(i=0; i<numLifes; i++) {
			GameObject redHeartGO = Instantiate(heartPrefab, UILifeContainer.transform);
			redHeartGO.GetComponent<RawImage>().texture = (Texture) Resources.Load("UI/Hearts/FullHeart");
			redHeartGO.GetComponent<RectTransform>().localPosition= new Vector3(-75 + 30 * i, 15f, 0f);
		}

		//add half heart
		if((float) currentLife - numLifes > 0) {
			GameObject halfHeartGO = Instantiate(heartPrefab, UILifeContainer.transform);
			halfHeartGO.GetComponent<RawImage>().texture = (Texture) Resources.Load("UI/Hearts/HalfHeart");
			halfHeartGO.GetComponent<RectTransform>().localPosition= new Vector3(-75 + 30 * i, 15f, 0f);	
			i++;		
		}

		// //add empty hears
		int numEmpty = (int) System.Math.Truncate(maxLife - currentLife);
		for(int j=0; j<numEmpty; j++, i++) {
			GameObject emptyHeartGO = Instantiate(heartPrefab, UILifeContainer.transform);
			emptyHeartGO.GetComponent<RawImage>().texture = (Texture) Resources.Load("UI/Hearts/EmptyHeart");
			emptyHeartGO.GetComponent<RectTransform>().localPosition= new Vector3(-75 + 30 * i, 15f, 0f);						
		}

		for(int j=0; j<this.additiveHearts.Count; j++, i++) {
			GameObject emptyHeartGO = Instantiate(heartPrefab, UILifeContainer.transform);
			if( this.additiveHearts[j].GetType() == typeof(BlueHeart)) {
				emptyHeartGO.GetComponent<RawImage>().texture = (this.additiveHearts[j].value == 1) ?  (Texture) Resources.Load("UI/Hearts/BlueFullHeart") :  (Texture) Resources.Load("UI/Hearts/BlueHalfHeart");
			}
			if( this.additiveHearts[j].GetType() == typeof(BlackHeart)) {
				emptyHeartGO.GetComponent<RawImage>().texture = (this.additiveHearts[j].value == 1) ?  (Texture) Resources.Load("UI/Hearts/BlackFullHeart") :  (Texture) Resources.Load("UI/Hearts/BlackHalfHeart");
			}
			emptyHeartGO.GetComponent<RectTransform>().localPosition= new Vector3(-75 + 30 * i, 15f, 0f);									
		}
	}

	private IEnumerator unableObject(GameObject gameObject, float timer) {
		yield return new WaitForSeconds (timer);
		gameObject.SetActive(false);
		this.isRedScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!this.isDead) {
			float horizontaltranslation = Input.GetAxis("Horizontal");
			float verticalTranslation =  Input.GetAxis("Vertical");
			this.GetComponent<Rigidbody>().velocity = new Vector3(this.transform.forward.x* verticalTranslation * this.speed, this.GetComponent<Rigidbody>().velocity.y, this.transform.forward.z* verticalTranslation* this.speed);
			this.GetComponent<Rigidbody>().velocity += this.transform.right * horizontaltranslation * this.speed;
			

			// Get mouse movment and rotate
			float y = Input.GetAxis("Mouse X");
			float x = Input.GetAxis("Mouse Y");			

			Vector3 rotation = new Vector3(x, y * -1, 0 );
			transform.eulerAngles = transform.eulerAngles - rotation;
			if(this.transform.forward.y > MAX_FORWARD_Y) {
				this.transform.forward = new Vector3(this.transform.forward.x, MAX_FORWARD_Y, this.transform.forward.z);
			} else if (this.transform.forward.y < -MAX_FORWARD_Y) {
				this.transform.forward = new Vector3(this.transform.forward.x, -MAX_FORWARD_Y, this.transform.forward.z);

			}

			if(Input.GetKeyDown(KeyCode.Escape)) {
				if(!isInMenu) {
					this.menu.gameObject.SetActive(true);
					this.inGameUI.SetActive(false);
					Time.timeScale = 0;
					isInMenu = true;
				} else {
					this.menu.gameObject.SetActive(false);
					this.inGameUI.SetActive(true);
					Time.timeScale = 1;
					isInMenu = false;	
					Cursor.lockState = wantedMode;				
				}
			}
		}
	}

	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "RedHeart") {
			if(this.currentLife < this.maxLife) {
				this.currentLife = (this.currentLife+1 > this.maxLife) ? this.maxLife : this.currentLife+1;
				this.updateLifeUI();
				this.soundManager.pickupRedHeart.Play();
				Destroy(other.gameObject);
			}
		}
		if(other.gameObject.tag == "BlueHeart") {
			this.additiveHearts.Add(new BlueHeart().setValue(1f));
			this.updateLifeUI();
			this.soundManager.pickupRedHeart.Play();
			Destroy(other.gameObject);
		}
		// if(other.gameObject.tag == "WhiteHeart") {
		// 	if(this.currentLife < this.maxLife) {
		// 		this.additiveHearts.Add("White");
		// 		this.updateLifeUI();
		// 		this.soundManager.pickupRedHeart.Play();
		// 		Destroy(other.gameObject);
		// 	}
		// }
		if(other.gameObject.tag == "BlackHeart") {
			this.additiveHearts.Add(new BlackHeart().setValue(1f));
			this.updateLifeUI();
			this.soundManager.pickupRedHeart.Play();
			Destroy(other.gameObject);
		}
	}	
}

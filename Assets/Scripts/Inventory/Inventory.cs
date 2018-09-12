using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	private int numKeys;
	private int numBombs;
	private int numCoins;

	public SoundManager soundManager;

	public Text bombNumberText;
	public Text keyNumberText;
	public Text coinNumberText;
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

	void Start()
	{
	}


	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Key") {
			this.numKeys++;
			this.soundManager.pickupKey.Play();
			this.keyNumberText.text = (this.numKeys < 10) ? "0" + this.numKeys.ToString() : this.numKeys.ToString();
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "Bomb") {
			this.numBombs++;
			this.soundManager.pickupBomb.Play();
			this.bombNumberText.text = (this.numBombs < 10) ? "0" + this.numBombs.ToString() : this.numBombs.ToString();
			Destroy(other.gameObject);
		}
	}	
}

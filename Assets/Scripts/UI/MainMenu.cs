using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : Menu {
	public int currentChoice;
	public string[] choices = {"Restart", "Quit"};
	public RawImage image;
	public int numChoices;

	public bool isDead;
	public Controls controls;
	public UIManager uiManager;
	// Use this for initialization
	void Start () {
		this.currentChoice = 0;
		this.numChoices = choices.Length;
		this.isDead = false;
	}
	

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			this.currentChoice--;
			this.currentChoice %= numChoices;
			this.currentChoice = Mathf.Abs(currentChoice);
			updateText();
		} 
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			this.currentChoice++;
			this.currentChoice %= numChoices;
			this.currentChoice = Mathf.Abs(currentChoice);
			updateText();
		} 
		if(Input.GetKeyDown(KeyCode.Return)) {
			if(choices[currentChoice].IndexOf("Restart") != -1) {
				this.controls.inGameUI.SetActive(true);
				this.gameObject.SetActive(false);
				this.controls.gameObject.transform.position = new Vector3(0f, 5f, 0f);
				this.controls.created = false;
				Destroy(this.controls.gameObject);
				Destroy(this.uiManager.gameObject);
				SceneManager.LoadScene("Scene");
				Time.timeScale = 1;
			} else if(choices[currentChoice].IndexOf("Quit") != -1){
				Application.Quit();
			}
		}
	}

	private void updateText() {
		for(int i=0; i<image.transform.childCount; i++) {
			Text text = image.transform.GetChild(i).GetComponent<Text>();
			if(i != currentChoice) {
				text.text = "   " + choices[i];
			} else {
				text.text = "> " + choices[i];
			}
		}
	}
}

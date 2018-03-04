using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour {
	public static LoadingPanel instance;

	void Awake(){
		instance = this;
	}

	void Start(){
	
		waitFor (0.5f);
	}

	public void waitFor(float inputSec){

		StartCoroutine (waitFor_IE(inputSec));
	
	}

	[SerializeField]
	GameObject blackPanel;

	IEnumerator waitFor_IE(float inputSec){
	
		blackPanel.SetActive (true);
		yield return new WaitForSeconds (inputSec);
		blackPanel.SetActive (false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputField_controller : MonoBehaviour {

	public enum validationType{
	
		NONE,INT,FLOAT
	};

	[SerializeField]
	validationType thisValidation;
	[SerializeField]
	GameObject targetInput_obj;
	InputField targetInput;
	[SerializeField]
	Text targetText;

	// Use this for initialization
	void Start () {

		targetInput = targetInput_obj.GetComponent<InputField> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void saveText(){
		if (checkValidation ()) {
			targetText.text = targetInput.text;
			targetInput_obj.SetActive (false);
		} else {
			targetInput.text = "Invalid";
		}
	}

	bool checkValidation(){
		switch(thisValidation){

		case validationType.NONE:
			return true;
			break;
		case validationType.INT:
			int resultInt = 0;
			if (int.TryParse (targetInput.text, out resultInt)) {
				return true;
			} else {			
				return false;
			}
			break;
		case validationType.FLOAT:
			float resultFloat = 0;
			if (float.TryParse (targetInput.text, out resultFloat)) {
				targetInput.text = resultFloat.ToString ("F2");
				return true;
			} else {			
				return false;
			}
			break;

		}

		return false;
	}



	//UI control

	int pressCounter = 0;
	public void ButtonPress(){

		if (pressCounter > 0) {
			displayInputField ();
		} else {		
			StartCoroutine (countDown());
		}
	}

	IEnumerator countDown(){

		pressCounter++;
		yield return new WaitForSeconds (1);
		pressCounter --;
	}


	void displayInputField(){	
		targetInput_obj.SetActive (true);
		targetInput.text = targetText.text;
	}
}

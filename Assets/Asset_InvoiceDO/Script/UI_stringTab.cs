using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_stringTab : MonoBehaviour {

	public bool independent = false;
	public bool dateTime = false;
	public int index;
	public Text myText;

	void Update(){
	
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
			if (targetInput.gameObject.activeSelf)
				SaveButton ();
		}			
	}

	public void setText(string inputString){
		myText.text = inputString;
	}

	public void buttonClick(){

		StartCoroutine (ClickCount_IE());
	}

	[SerializeField]
	InputField targetInput;

	int ClickCounter = 0;

	bool selected = false;

	IEnumerator ClickCount_IE(){

		ClickCounter++;

		if (ClickCounter > 1) {
			Debug.Log ("Double Click");
			targetInput.transform.gameObject.SetActive (true);
			targetInput.text = myText.text;
			ClickCounter = 0;
			selected = true;
			transform.parent.BroadcastMessage ("Unselect",transform);
			yield return null;
		} else {
			yield return new WaitForSeconds (0.5f);
			if(ClickCounter > 0)
				ClickCounter--;
		}
	}

	public void Unselect(Transform theTransform){
		if (theTransform != transform) {
			targetInput.transform.gameObject.SetActive (false);
			selected = false;
		}
	}

	public void SaveButton(){
		Debug.Log (targetInput.textComponent.preferredWidth);

		if (targetInput.text != "X") {
			
			if (dateTime)
				myText.text = formatString (targetInput.text);
			else
				myText.text = targetInput.text;
			
			if(!independent)
				transform.parent.GetComponent<UI_genericList> ().modified (index,myText.text);
			targetInput.transform.gameObject.SetActive (false);
			selected = false;
		} else {
			if (independent) {
				targetInput.transform.gameObject.SetActive (false);
				selected = false;
			}
			else
				deleteThis ();
		}

	}

	public void deleteThis(){
		transform.parent.GetComponent<UI_genericList> ().removeItem (index);
	}

	string getDateNow(){

		return System.DateTime.Now.ToString ("dd/MM/yyyy");	
	}

	string formatString(string inputString){
		
		System.DateTime outputDate;
		//System.DateTime.TryParse (inputString,out outputDate);
		System.DateTime.TryParseExact (inputString,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out outputDate);

		string outputString = outputDate.ToString("dd/MM/yyyy");
		if (outputString == "01/01/0001")
			outputString = getDateNow ();

		return outputString;	
	}
		

	public void checkTooLong(){
	
		Debug.Log (targetInput.textComponent.fontSize + " Check Too Long : " + targetInput.textComponent.resizeTextMaxSize);
		if (targetInput.textComponent.fontSize < targetInput.textComponent.resizeTextMaxSize) {
			targetInput.text.Remove (targetInput.text.Length-1);
		}
	}
}

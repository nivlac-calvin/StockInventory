using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartSuggestion_Price : MonoBehaviour {

	[SerializeField]
	InputField targetInputField;
	public Text priceText;

	public void chooseThis(){
		targetInputField.text = priceText.text;
		transform.parent.gameObject.SetActive (false);
	}


}

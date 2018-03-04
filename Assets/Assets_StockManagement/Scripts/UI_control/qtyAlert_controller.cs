using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class qtyAlert_controller : MonoBehaviour {

	[SerializeField]
	ItemPrefabManager thisItemPrefab;
	[SerializeField]
	GameObject qty_al_Input_obj;
	[SerializeField]
	InputField qty_al_Input_;
	[SerializeField]
	Text qtyText;

	[SerializeField]
	Image targetImage;

	bool boolControl = true;

	[SerializeField]
	GameObject saveButton;

	public void editAlert(){

		if (boolControl) {
			refreshAlert ();
			qty_al_Input_obj.SetActive (true);
			boolControl = false;
		} else {

			if (qty_al_Input_obj.activeSelf) {
				saveAlert ();
			}
		}

		if (boolControl) {
			saveButton.SetActive (true);
		}
		else {
			saveButton.SetActive (false);
		}
	}

	public void saveAlert(){
		int alertInput = 0;

		if (int.TryParse (qty_al_Input_.text, out alertInput)) {
			currentAlert = alertInput;
			qty_al_Input_obj.SetActive (false);
			boolControl = true;
			refreshAlert ();
		} else {
			qty_al_Input_.text = "Invalid";
			boolControl = false;
		}

	}
	public int currentAlert = 0;

	public void refreshAlert(){
		qty_al_Input_.text = currentAlert.ToString ();;
		if (int.Parse(qtyText.text) < currentAlert) {
			targetImage.color = Color.red;
		} else {
			targetImage.color = Color.white;
		}
	}
}

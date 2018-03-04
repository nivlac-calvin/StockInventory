using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemDetailsTab : MonoBehaviour {
	[HideInInspector]
	public ItemsGroupManager theManager;

	public invoiceItem thisInvoiceItem;

	[SerializeField]
	Text descpComp;
	[SerializeField]
	Text priceComp;
	public int ID;

	[SerializeField]
	InputField targetInput;

	int ClickCounter = 0;

	bool selected = false;

	public void buttonClick(){
	
		StartCoroutine (ClickCount_IE());
	}

	void Update(){
	
		if (selected) {
		
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
			
				theManager.moveUp (ID,thisInvoiceItem);
			}


			if (Input.GetKeyDown (KeyCode.DownArrow)) {

				theManager.moveDown (ID,thisInvoiceItem);
			}
		}
	}
		
	IEnumerator ClickCount_IE(){
	
		ClickCounter++;

		if (ClickCounter > 1) {
			Debug.Log ("Double Click");
			targetInput.transform.gameObject.SetActive (true);
			targetInput.text = thisInvoiceItem.itemDescription;
			ClickCounter = 0;
			selected = true;
			theManager.transform.BroadcastMessage ("Unselect",transform);
			yield return null;
		} else {
			yield return new WaitForSeconds (0.5f);
			if(ClickCounter > 0)
				ClickCounter--;
		}
	}

	public void removeMe(){
		theManager.RemoveAt(ID);
	}

	public void saveThis(){
		Refresh_Invoice ();
		theManager.saveThis (ID,thisInvoiceItem);
	}

	public void SaveButton(){
		if (targetInput.text != "") {
			thisInvoiceItem.itemDescription = targetInput.text;
			saveThis ();
			targetInput.transform.gameObject.SetActive (false);
			selected = false;
		} 
		else
			removeMe ();

	}

	public void Refresh_Invoice(){

		descpComp.text = thisInvoiceItem.itemDescription;

	}

	public void Unselect(Transform theTransform){
		if (theTransform != transform) {
			targetInput.transform.gameObject.SetActive (false);
			selected = false;
			Refresh_Invoice ();
		}

	}
}

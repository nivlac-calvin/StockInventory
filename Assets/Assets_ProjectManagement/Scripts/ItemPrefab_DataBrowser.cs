using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPrefab_DataBrowser : MonoBehaviour {

	public Item thisItem = new Item();

	[SerializeField]
	Color normalColor;
	[SerializeField]
	Color selectedColor;

	[SerializeField]
	Image targetImage;
	[SerializeField]
	Text itemName_text;
	[SerializeField]
	Text itemFrom_text;

	public void assignThis(Item input_item,string tableName){
	
		thisItem = input_item;
		string text = "";
		if (thisItem.brand_name != null)
			text += thisItem.brand_name;

		text += " " + thisItem.part_no;
		itemName_text.text = text;	
	}

	public void DestroyMe(){
	
		Destroy(this.gameObject);
	}

	public void broadcast(){
	
		transform.parent.BroadcastMessage ("SelectThis",this.transform);
	}

	public void SelectThis(Transform me){

		if (me != this.transform) {
			targetImage.color = normalColor;
		} else
			targetImage.color = selectedColor;
	}


}

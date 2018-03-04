using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPrefabManager : MonoBehaviour {

	public Item thisItem;
	public int indexID;

	public void assignItem(Item inputItem){

		thisItem = inputItem;
		UpdateUI_element ();
	}

	[SerializeField]
	Text IndexText;
	[SerializeField]
	Text BrandText;
	[SerializeField]
	Text PartNumberText;
	[SerializeField]
	Text DescriptionText;
	[SerializeField]
	Text QtyText;
	[SerializeField]
	Text U_priceText;
	[SerializeField]
	Text T_priceText;

	[SerializeField]
	qtyAlert_controller qtyAlert_controller;

	public void UpdateUI_element(){

		IndexText.text = (indexID+1).ToString ();;
		BrandText.text = thisItem.brand_name;
		PartNumberText.text = thisItem.part_no;
		DescriptionText.text = thisItem.description;
		QtyText.text = thisItem.qty_all.ToString ();
		qtyAlert_controller.currentAlert = thisItem.alert_qty;
		qtyAlert_controller.refreshAlert ();
		U_priceText.text = thisItem.U_price.ToString ("F2");
		T_priceText.text = thisItem.T_price.ToString ("F2");

	}

	public void saveCurrentInfo(){

		thisItem.brand_name = BrandText.text;
		thisItem.part_no = PartNumberText.text;
		thisItem.description = DescriptionText.text;
		thisItem.qty_all = int.Parse(QtyText.text);
		thisItem.alert_qty = qtyAlert_controller.currentAlert;
		thisItem.U_price = float.Parse(U_priceText.text);
		thisItem.T_price = float.Parse(T_priceText.text);
		UpdateUI_element ();
	}

	public allItemPrefabs_controller myController;
	public void deleteThis(){
	
		myController.deleteItem(indexID);
	}
}

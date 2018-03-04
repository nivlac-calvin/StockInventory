using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

	public string ID;
	public string brand_name;
	public string part_no;
	public string description;
	public int qty_all;
	public float U_price;
	public float T_price;
	public int alert_qty;

	public Item(){

		ID = "";
		brand_name = "";
		part_no = "";
		description = "";
		qty_all = 0;
		U_price = 0;
		T_price = U_price * (qty_all);
	}

	public Item(string input_ID){

		ID = input_ID;
		brand_name = "";
		part_no = "";
		description = "";
		qty_all = 0;
		U_price = 0;
		T_price = U_price * (qty_all);
	}


	public Item(string brandName_input,string part_no_input,string description_input,int qtyALL_input,float u_price){

		ID = brandName_input + part_no_input;
		brand_name = brandName_input;
		part_no = part_no_input;
		description = description_input;
		qty_all = qtyALL_input;
		U_price = u_price;
		T_price = u_price * qty_all;
	}

	public void calculatePrice(){

		T_price = U_price * qty_all;
	}

	public void refreshID(){

		ID = brand_name + part_no;
	}

	public Item getCopy(){
		return (Item)this.MemberwiseClone ();
	}

};

[System.Serializable]
public class DataContainer {

	public string tableName;
	public int indexID;
	public List<Item> itemList = new List<Item>();

	public void saveToThis(List<Item> itemList_input){

		itemList.Clear ();
		for(int i = 0 ; i < itemList_input.Count ; i++){
			itemList.Add (itemList_input[i].getCopy());
		}
	}

	public List<Item> getFromThis(){
	
		List<Item> outputItemList = new List<Item> ();
		for(int i = 0 ; i < itemList.Count ; i++){
			outputItemList.Add (itemList[i].getCopy());
		}
		return itemList;
	}
}

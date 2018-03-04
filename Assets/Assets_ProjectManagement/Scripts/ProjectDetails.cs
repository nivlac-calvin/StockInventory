using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ProjectItem {

	public string UniqueID;
	public string OrderFrom;
	public string Brand;
	public string PartNumber;
	public string Description;

	public Item thisItem;
	public int qty_Need;
	public int qty_Balance;
	public int qty_Took; //from stock
	public int qty_Buy; //direct buy
	public string deliveryOn;
	public string invdo_No;
	public bool enough;

	public ProjectItem(){

		UniqueID = "UniqueID";
		OrderFrom = "";

	}

	void took(int input){

		qty_Took += input;
		if (input < 0) {
		
			thisItem.qty_all += input;
		}
		refreshItemDetails ();	
	}

	void buy(int input){

		qty_Buy += input;
		refreshItemDetails ();
	}

	void refreshItemDetails(){
	
		Brand = thisItem.brand_name;
		PartNumber = thisItem.part_no;
		Description = thisItem.description;
		qty_Balance = thisItem.qty_all;
	}

};


public enum progressState{

	INPROGRESS,COMPLETED
};

public class ProjectDetails {

	string projectID;
	public progressState P_state;
	public string invoiceID;
	DateTime inv_dt;

	public string customerName;
	string projectName;
	string otherDescription;

	public List<ProjectItem> projectItemList = new List<ProjectItem>();

	public ProjectDetails(){
		P_state = progressState.INPROGRESS;
		invoiceID = "00752";
		customerName = "ImCustomer";
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

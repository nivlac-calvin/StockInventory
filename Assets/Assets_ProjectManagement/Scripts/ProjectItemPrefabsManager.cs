using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectItemPrefabsManager : MonoBehaviour {

	public ProjectItem thisProjectItem;
	public int indexID;

	public void assignItem(ProjectItem inputItem){

		thisProjectItem = inputItem;
		UpdateUI_element ();
	}

	[SerializeField]
	Text OrderFromText;
	[SerializeField]
	Text BrandText;
	[SerializeField]
	Text PartNumberText;
	[SerializeField]
	Text DescriptionText;
	[SerializeField]
	Text NeedText;
	[SerializeField]
	Text BalanceText;
	[SerializeField]
	Text TookText;
	[SerializeField]
	Text BuyText;	
	[SerializeField]
	Text DeliveryOnText;	
	[SerializeField]
	Text InvDoNoText;


	public void UpdateUI_element(){

		OrderFromText.text = thisProjectItem.OrderFrom;
		BrandText.text = thisProjectItem.Brand;
		PartNumberText.text = thisProjectItem.PartNumber;
		DescriptionText.text = thisProjectItem.Description;
		NeedText.text = thisProjectItem.qty_Need.ToString ();
		BalanceText.text = thisProjectItem.qty_Balance.ToString ();
		TookText.text = thisProjectItem.qty_Took.ToString ();
		BuyText.text = thisProjectItem.qty_Buy.ToString ();
		DeliveryOnText.text = thisProjectItem.deliveryOn;
		InvDoNoText.text = thisProjectItem.invdo_No;
	}

	public void saveCurrentInfo(){

		thisProjectItem.OrderFrom = OrderFromText.text;
		thisProjectItem.Brand = BrandText.text;
		thisProjectItem.PartNumber = PartNumberText.text;
		thisProjectItem.Description = DescriptionText.text;
		thisProjectItem.qty_Need = int.Parse(NeedText.text);
		thisProjectItem.qty_Balance = int.Parse(BalanceText.text);
		thisProjectItem.qty_Took = int.Parse(TookText.text);
		thisProjectItem.qty_Buy = int.Parse(BuyText.text);
		thisProjectItem.deliveryOn = DeliveryOnText.text;
		thisProjectItem.invdo_No = InvDoNoText.text;
		UpdateUI_element ();
	}
}

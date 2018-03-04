using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditPanelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void displayItem(){
		brand_name_input.text = thisTarget.targetItem.brand_name;
		part_no_input.text = thisTarget.targetItem.part_no;
		description_input.text = thisTarget.targetItem.description;
		qty_all_input.text = thisTarget.targetItem.qty_all.ToString();
		price_input.text = thisTarget.targetItem.U_price.ToString ();
		alert_input.text = thisTarget.targetItem.alert_qty.ToString ();
	}

	public void saveItem(){
		thisTarget.targetItem.brand_name = brand_name_input.text;
		thisTarget.targetItem.part_no = part_no_input.text;
		thisTarget.targetItem.description = description_input.text;
		thisTarget.targetItem.qty_all  = int.Parse(qty_all_input.text);
		thisTarget.targetItem.U_price = float.Parse (price_input.text);
		thisTarget.targetItem.alert_qty = int.Parse (alert_input.text);
		thisTarget.refreshCard ();
	}

	[SerializeField]
	InputField stock_IN;

	public void stockIn(){

		int in_result = 0;

		if (stock_IN.text != "" && int.TryParse (stock_IN.text,out in_result) == true) {
		
			thisTarget.targetItem.qty_all += in_result;
			stock_IN.text = null;
		}

		thisTarget.refreshCard ();
		displayItem ();
	}
	[SerializeField]
	InputField stock_OUT;

	public void stockOut(){

		int out_result = 0;
		if (stock_OUT.text != "" && int.TryParse (stock_OUT.text,out out_result) == true) {

			thisTarget.targetItem.qty_all -= out_result;
			stock_OUT.text = null;
		}

		thisTarget.refreshCard ();
		displayItem ();
	}

	public ItemCard thisTarget;

	[SerializeField]
	InputField brand_name_input;
	[SerializeField]
	InputField part_no_input;
	[SerializeField]
	InputField description_input;
	[SerializeField]
	InputField qty_all_input;
	[SerializeField]
	InputField price_input;
	[SerializeField]
	InputField alert_input;
}

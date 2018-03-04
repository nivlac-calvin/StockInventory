using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewPanelManager : MonoBehaviour {


	void OnEnable(){
	
		brand_name_input.text = "";
		part_no_input.text = "";
		description_input.text = "";
		qty_all_input.text = "";
		U_price_input.text = "";
		alert_input.text = "";
	}

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void saveItem(){
		Item thisTarget = new Item ();
		thisTarget.brand_name = brand_name_input.text;
		thisTarget.part_no = part_no_input.text;
		thisTarget.description = description_input.text;
		thisTarget.qty_all  = int.Parse(qty_all_input.text);
		thisTarget.U_price = float.Parse(U_price_input.text);
		thisTarget.alert_qty = int.Parse (alert_input.text);
		thisTarget.ID = target_DataMng.mainDataMNG.ID_generator.GenerateNewID (target_DataMng.tableName.text);
		target_DataMng.AddNewItem (thisTarget);
	}

	[SerializeField]
	InputField brand_name_input;
	[SerializeField]
	InputField part_no_input;
	[SerializeField]
	InputField description_input;
	[SerializeField]
	InputField qty_all_input;
	[SerializeField]
	InputField U_price_input;
	[SerializeField]
	InputField alert_input;

	public DataManager target_DataMng;
}

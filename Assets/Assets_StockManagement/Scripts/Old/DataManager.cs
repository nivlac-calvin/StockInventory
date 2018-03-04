using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using UnityEngine.UI;



public class DataManager : MonoBehaviour {

	public Text tableName;
	public List<Item> itemList = new List<Item>();
	public MainDataManager mainDataMNG;
	public int thisIndex;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddNewItem(Item inputItem){
		inputItem.T_price = inputItem.U_price * inputItem.qty_all;
		Debug.Log ("Add New : " + inputItem.brand_name);
		itemList.Add (inputItem);
	}

//	void hideNotMatch(){
//
//		int hideCount = 0;
//
//		for(int i = 0; i < PreObjList.Count; i++){
//
//			if (!checkKeyWord (PreObjList[i].GetComponent<ItemCard>().targetItem,findButtonScript.keywords_input.text)) {
//
//				PreObjList [i].SetActive (false);
//				hideCount++;
//			}
//		}
//		rect_Tran.sizeDelta = new Vector2 (rect_Tran.sizeDelta.x, (itemList.Count - hideCount) * (35));	
//	}
//
	bool checkKeyWord(Item thisItem,string keyword){

		string brand_ = thisItem.brand_name.ToLower ();
		string descp_ = thisItem.description.ToLower ();
		string partNo_ = thisItem.part_no.ToLower ();


		if (brand_.Contains (keyword.ToLower ()))
			return true;
		
		if (descp_.Contains (keyword.ToLower ()))
			return true;

		if (partNo_.Contains (keyword.ToLower ()))
			return true;

		return false;

	}

	public void saveThis(){
		mainDataMNG.saveData (thisIndex);
	}
}

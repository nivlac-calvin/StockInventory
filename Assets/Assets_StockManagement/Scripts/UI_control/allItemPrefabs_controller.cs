using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allItemPrefabs_controller : MonoBehaviour {

	public DataTab_controller targetDataTab_;

	List<Item> itemList = new List<Item>();
	List<GameObject> itemPrefabsList = new List<GameObject> ();

	[SerializeField]
	GameObject itemPrefabs;

	void Start(){
//		List<Item> itemList_input = new List<Item>();
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());	
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());	
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());	
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());	
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//		itemList_input.Add (new Item ());
//
//
//		LoadItemList (itemList_input);
	}

	public void LoadItemList(List<Item> itemList_input){
		Debug.Log ("Load This List : " + itemList_input.Count);
		clearAndDestroy ();
		for(int  i = 0 ; i < itemList_input.Count ; i++){
			itemList.Add (itemList_input [i]);
			GameObject newPrefab = Instantiate (itemPrefabs,this.transform) as GameObject;
			newPrefab.SetActive (true);
			itemPrefabsList.Add (newPrefab);
			newPrefab.GetComponent<ItemPrefabManager> ().indexID = i;
			newPrefab.GetComponent<ItemPrefabManager> ().assignItem (itemList [i]);


			Debug.Log ("Instantiate : " + itemList [i].description);
		}
		this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x,(itemList.Count +1) * (22));
	}

	public void addNewItem(){
		itemList.Add (new Item());
		GameObject newPrefab = Instantiate (itemPrefabs,this.transform) as GameObject;
		newPrefab.SetActive (true);
		itemPrefabsList.Add (newPrefab);
		newPrefab.GetComponent<ItemPrefabManager> ().assignItem (new Item());
		newPrefab.GetComponent<ItemPrefabManager> ().UpdateUI_element ();
		localRefresh ();
	}

	public void saveItemList(){
		localRefresh ();
		targetDataTab_.saveThis ();
	}

	void localRefresh(){
	
		for(int  i = 0 ; i < itemPrefabsList.Count ; i++){
			itemPrefabsList [i].GetComponent<ItemPrefabManager> ().indexID = i;
			itemPrefabsList [i].GetComponent<ItemPrefabManager> ().saveCurrentInfo();
			itemList [i] = itemPrefabsList [i].GetComponent<ItemPrefabManager> ().thisItem;
		}

		this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x,(itemList.Count +1) * (22));
	}

	public void deleteItem(int inputIndex){

		itemList.RemoveAt (inputIndex);
		Destroy (itemPrefabsList [inputIndex]);
		itemPrefabsList.RemoveAt (inputIndex);
		localRefresh ();
	
	}

	void clearAndDestroy(){
		itemList.Clear ();
		for(int i  = 0 ; i < itemPrefabsList.Count ; i++){
			Destroy (itemPrefabsList[i]);
		}
		itemPrefabsList.Clear ();
	}

	public List<Item> getItemList(){
	
		localRefresh ();
		return itemList;
	}
}

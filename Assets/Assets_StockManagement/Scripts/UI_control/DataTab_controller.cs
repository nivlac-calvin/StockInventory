using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTab_controller : MonoBehaviour {
	public DataContainersManager thisDataContainersManager;

	public DataContainer thisDataContainer;
	[SerializeField]
	allItemPrefabs_controller targetItemsController;
	[SerializeField]
	Text dataTab_text;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void assignData(DataContainer inputDataContainer){
		thisDataContainer = inputDataContainer;
		dataTab_text.text = thisDataContainer.tableName;
	}

	public void loadThis(){
		targetItemsController.targetDataTab_ = this;
		targetItemsController.LoadItemList (thisDataContainer.getFromThis());
	}

	public void saveThis(){
		thisDataContainer.saveToThis (targetItemsController.getItemList());
		thisDataContainersManager.saveData (thisDataContainer.indexID);
	}
}

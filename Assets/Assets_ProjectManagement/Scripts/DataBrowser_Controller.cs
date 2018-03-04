using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBrowser_Controller : MonoBehaviour {

	public DataContainer_Loader thisDataConLoader;

	[SerializeField]
	GameObject browserPrefab;
	[SerializeField]
	Transform contentTransform;

	List<GameObject> prefabList = new List<GameObject>();

	void OnEnable(){
		StartCoroutine (preparing());
	}

	IEnumerator preparing(){
		while (thisDataConLoader.loaded == false) {
			Debug.Log("Loading");
			yield return null;

		}
		clearAndDesroy ();
		displayGroupedItems ();
	}

	void displayGroupedItems(){

		float height_new = 0;

		for(int i = 0 ; i < thisDataConLoader.dataList.Count ; i ++){

			for(int j = 0 ; j < thisDataConLoader.dataList[i].itemList.Count ; j++){
				GameObject ThisObject = Instantiate (browserPrefab, contentTransform) as GameObject;
				prefabList.Add (ThisObject);
				ThisObject.SetActive (true);
				ItemPrefab_DataBrowser targetPrefab = ThisObject.GetComponent<ItemPrefab_DataBrowser> ();
				targetPrefab.assignThis (thisDataConLoader.dataList[i].itemList[j],thisDataConLoader.dataList[i].tableName);
				height_new += 30;
			}


		}

		contentTransform.GetComponent<RectTransform>().sizeDelta = new Vector2 (contentTransform.GetComponent<RectTransform>().rect.width,height_new);;
	}

	void clearAndDesroy(){
	
		if (prefabList.Count > 0) {
			contentTransform.BroadcastMessage ("DestroyMe");
			prefabList.Clear ();
		}
	}
}

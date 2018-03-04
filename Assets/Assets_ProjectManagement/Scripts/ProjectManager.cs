using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectManager : MonoBehaviour {

	[SerializeField]
	GameObject ButtonPrefab;
	// Use this for initialization
	void Start () {

		refreshButtons ();
	
	}

	public List<string> inProgressList = new List<string>();
	public List<string> completedList = new List<string>();

	// Update is called once per frame
	void Update () {
	
	}

	[SerializeField]
	Transform inPro_trans;
	[SerializeField]
	Transform Com_trans;

	List<GameObject> PreObjList_A = new List<GameObject>();
	List<GameObject> PreObjList_B = new List<GameObject>();

	public void refreshButtons(){
		clearPre ();

		for(int i  = 0; i < inProgressList.Count ; i++){

			GameObject thisObj = Instantiate (ButtonPrefab,inPro_trans) as GameObject;
			thisObj.SetActive (true);
			//thisObj.GetComponent<ProjectButton> ().projectName.text = inProgressList[i];
			PreObjList_A.Add (thisObj);
		}

		for(int i  = 0; i < completedList.Count ; i++){

			GameObject thisObj = Instantiate (ButtonPrefab,Com_trans) as GameObject;
			thisObj.SetActive (true);
			//thisObj.GetComponent<ProjectButton> ().projectName.text = completedList[i];
			PreObjList_B.Add (thisObj);
		}
	


	}

	void clearPre(){

		if (PreObjList_A != null) {
			for (int i = 0; i < PreObjList_A.Count; i++) {
				Destroy (PreObjList_A [i]);
			}
			PreObjList_A.Clear ();
		}

		if (PreObjList_B != null) {
			for (int i = 0; i < PreObjList_B.Count; i++) {
				Destroy (PreObjList_B [i]);
			}

			PreObjList_B.Clear ();
		}
	}
}

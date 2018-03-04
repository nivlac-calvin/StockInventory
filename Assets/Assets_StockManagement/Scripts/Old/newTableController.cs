using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class newTableController : MonoBehaviour {

	[SerializeField]
	DataContainersManager mainDataCTN;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[SerializeField]
	InputField table_name_input;
	[SerializeField]
	GameObject table_name_button;
	public void show_inputName(){
		table_name_input.gameObject.SetActive (true);
		table_name_button.SetActive (true);
	}

	[SerializeField]
	GameObject confirm_Panel;
	[SerializeField]
	Text tableName;

	public void confirmationPanel(){
		confirm_Panel.SetActive (true);
		tableName.text = table_name_input.text;
	}

	public void yes(){

		mainDataCTN.createNewItemList (tableName.text);
		confirm_Panel.SetActive (false);
		table_name_input.gameObject.SetActive (false);
		table_name_button.SetActive (false);
	}
}

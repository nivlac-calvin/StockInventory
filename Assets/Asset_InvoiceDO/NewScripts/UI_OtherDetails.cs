using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OtherDetails : MonoBehaviour {
	public int thisInvoiceID;
	public InvoiceRecords_Details thisInvoiceRecords = new InvoiceRecords_Details();
	[SerializeField]
	InputField project_input;
	[SerializeField]
	UI_DateFormation targetDate;
	[SerializeField]
	InputField sales_input;
	[SerializeField]
	InputField po_input;

	void OnEnable(){	
		StartCoroutine (load_coroutine());
	}

	IEnumerator load_coroutine(){
		
		while (DataManager_New.instance == null) {
			yield return null;
		}
		while(!DataManager_New.instance.loaded_partner){
			yield return null;
		}
		thisInvoiceRecords = DataManager_New.instance.invoiceList [thisInvoiceID];
		loadOtherDetails ();
	}

	void loadOtherDetails(){
		project_input.text = thisInvoiceRecords.projectTitle;
		sales_input.text = thisInvoiceRecords.salesEng_Code;
		po_input.text = thisInvoiceRecords.PurchaseOrder;
		targetDate.currentDateString = thisInvoiceRecords.invoiceDate;
		targetDate.initialize ();

		if(thisInvoiceRecords.clientData_ID != "empty")
			Partner_CurrentID = int.Parse (thisInvoiceRecords.clientData_ID);

		Partner_reloadList ();
	}

	public void saveOtherDetails(){
		thisInvoiceRecords.projectTitle = project_input.text;
		thisInvoiceRecords.salesEng_Code = sales_input.text;
		thisInvoiceRecords.PurchaseOrder = po_input.text;
		thisInvoiceRecords.invoiceDate = targetDate.currentDateString;

		DataManager_New.instance.saveInvoice_target (thisInvoiceRecords);
		loadOtherDetails ();
	}



	[Header("Partner Related")]

	public List<clientDataDetails> this_clientList = new List<clientDataDetails> ();

	[SerializeField]
	Text partnerText_accNo_main;
	[SerializeField]
	Text partnerText_add_main;
	[SerializeField]
	Text partnerText_term_main;

	public int Partner_CurrentID = -1;
	clientDataDetails currentClientData = new clientDataDetails();

	[SerializeField]
	GameObject PartnerBrowset_Obj;
	[SerializeField]
	Transform parentOfBrowser;
	[SerializeField]
	GameObject browserObj_Prefab;
	List<GameObject> prefabList = new List<GameObject>();

	[SerializeField]
	InputField preview_accNo;
	[SerializeField]
	InputField preview_add;
	[SerializeField]
	InputField preview_term;

	void Partner_reloadList(){

		this_clientList.Clear ();
		clientDataDetails[] clientData_temp = new clientDataDetails[DataManager_New.instance.clientList.Count];
		DataManager_New.instance.clientList.CopyTo(clientData_temp);

		for(int i = 0 ; i < clientData_temp.Length ; i++){
			this_clientList.Add(clientData_temp [i]);
		}

		if (Partner_CurrentID > -1) {
			currentClientData = this_clientList [Partner_CurrentID];
			partnerText_accNo_main.text = currentClientData.clientAccNo;
			partnerText_add_main.text = currentClientData.clientAddress;	
			partnerText_term_main.text = currentClientData.clientTerm;
		} else {
		
			partnerText_accNo_main.text = "";
			partnerText_add_main.text = "";
			partnerText_term_main.text = "";
		}
	}

	public void Partner_activateBrowser(){
		Partner_reloadList ();
		PartnerBrowset_Obj.SetActive (true);
		Partner_refreshBrowserScroll ();
		Partner_updatePreview (); 
	}

	void Partner_refreshBrowserScroll(){
		for(int i = 0; i< prefabList.Count ; i++){
			prefabList [i].SetActive (false);
		}

		for(int i = 0 ; i <this_clientList.Count ; i++){

			if (i > (prefabList.Count-1)) {
				GameObject newObj = Instantiate (browserObj_Prefab, parentOfBrowser);
				newObj.GetComponent<UI_partner_tab> ().thisID = int.Parse(this_clientList [i].clientId_inDB);
				newObj.GetComponent<UI_partner_tab> ().target_otherDetails = this;
				newObj.GetComponent<UI_partner_tab> ().refreshThis ();
				newObj.SetActive (true);
				prefabList.Add (newObj);
			} else {
				prefabList [i].SetActive (true);
				prefabList [i].GetComponent<UI_partner_tab> ().thisID = int.Parse(this_clientList [i].clientId_inDB);
				prefabList [i].GetComponent<UI_partner_tab> ().target_otherDetails = this;
				prefabList [i].GetComponent<UI_partner_tab> ().refreshThis ();
			}
		}
	}

	public void Partner_updatePreview(){
		if (Partner_CurrentID > -1) {
			preview_accNo.text = this_clientList [Partner_CurrentID].clientAccNo;
			preview_add.text = this_clientList [Partner_CurrentID].clientAddress;	
			preview_term.text = this_clientList [Partner_CurrentID].clientTerm;
		}
	}

	public void Partner_saveEditing(){
		if (Partner_CurrentID > -1) {
			clientDataDetails editedData = new clientDataDetails ();
			editedData = this_clientList [Partner_CurrentID];
			editedData.clientAccNo = preview_accNo.text;
			editedData.clientAddress = preview_add.text;
			editedData.clientTerm = preview_term.text;
			this_clientList [Partner_CurrentID] = editedData;
			DataManager_New.instance.partner_updateTargetClient (this_clientList [Partner_CurrentID]);
			thisInvoiceRecords.clientData_ID = Partner_CurrentID.ToString ();
		}
		Partner_reloadList ();
		Partner_refreshBrowserScroll ();
	}

	public void Partner_addNewClient(){
		Debug.Log ("Save Other Details");
		DataManager_New.instance.addNewPartner ();
		Partner_reloadList ();
		Partner_refreshBrowserScroll ();
	}
}

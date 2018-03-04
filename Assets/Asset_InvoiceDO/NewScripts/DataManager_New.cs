using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

[System.Serializable]
public struct InvoiceRecords_Details{

	public string invoiceID;
	public string projectTitle;
	public string clientData_ID;
	public string invoiceDate;
	public string salesEng_Code;
	public string PurchaseOrder;

	public List<InvoiceItemRecord> itemDetails_List;
};

[System.Serializable]
public struct clientDataDetails{
	public string clientId_inDB;
	public string clientAccNo;
	public string clientAddress;
	public string clientTerm;
}

[System.Serializable]
public struct InvoiceItemRecord{
	public List<string> quantity_List;
	public List<string> description_List;
	public List<string> unitPrice_List;
	public List<string> nettAmount_List;
};

public class DataManager_New : MonoBehaviour {
	string fileName_partner = "PartnersData";
	string fileName_invoice = "InvoiceData";

	public static DataManager_New instance;

	void Awake(){
		instance = this; 
	}

	public void OnEnable(){
		_FileLocation = Application.dataPath + "//InvoiceDataFolder";
		LoadPartner ();
		LoadInvoices ();
	}


	[Header("Invoice Realted")]

	public int startFrom = 629;

	//---------------------------------------------Invoice------------------------------------------------------
	public List<InvoiceRecords_Details> invoiceList = new List<InvoiceRecords_Details>();
	public bool loaded_invoice = false;

	void LoadInvoices () {		

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
		}

		string dataString = LoadXML (fileName_invoice);

		//if no such data recreate and load again
		if (dataString == null || dataString == "notExist") {
			addNewInvoice ();
			dataString = LoadXML (fileName_invoice);
		}

		InvoiceRecords_Details[] recordsArr = DeserializeObject_invoice (dataString) as InvoiceRecords_Details[];
		invoiceList.Clear ();

		for (int i = 0; i < recordsArr.Length; i++) {
			invoiceList.Add (recordsArr [i]);
		}

		if (invoiceList.Count < (startFrom + 1))
			StartCoroutine (fillRequirement());
		else
			loaded_invoice = true;
	}

	IEnumerator fillRequirement(){

		while (invoiceList.Count < startFrom + 1) {
			invoiceList.Add (NewEmptyInvoice ());
		}
		saveCurrentInvoice ();
		yield return null;
		LoadInvoices ();
	}

	InvoiceRecords_Details NewEmptyInvoice(){
		InvoiceRecords_Details newInvoice = new InvoiceRecords_Details ();
		newInvoice.invoiceID = (invoiceList.Count).ToString("0000");
		Debug.Log ("Here Invoice ID : " + newInvoice.invoiceID);
		newInvoice.projectTitle = "New Project " + newInvoice.invoiceID;
		newInvoice.clientData_ID = "empty";

		//Create a newRecords for one page
		InvoiceItemRecord newInvoiceItems = new InvoiceItemRecord();
		newInvoiceItems = newEmptyRecord ();

		newInvoice.itemDetails_List = new List<InvoiceItemRecord> ();
		newInvoice.itemDetails_List.Add (newInvoiceItems);

		return newInvoice;
	}

	InvoiceItemRecord newEmptyRecord(){
		InvoiceItemRecord newInvoiceItems = new InvoiceItemRecord();

		newInvoiceItems.description_List = new List<string> ();
		newInvoiceItems.quantity_List = new List<string> ();
		newInvoiceItems.unitPrice_List = new List<string> ();
		newInvoiceItems.nettAmount_List = new List<string> ();

		for(int i = 0 ; i < 17 ; i++){
			newInvoiceItems.description_List.Add ("");
			newInvoiceItems.quantity_List.Add ("");
			newInvoiceItems.unitPrice_List.Add ("");
			newInvoiceItems.nettAmount_List.Add ("");
		}
		return newInvoiceItems;
	}

	public void saveInvoice_target(InvoiceRecords_Details invoiceData_input){
		invoiceList [int.Parse(invoiceData_input.invoiceID)] = invoiceData_input;
		saveInvoiceFile (invoiceList);	
	}

	public void saveInvoiceFile(List<InvoiceRecords_Details> invoiceData_input){
		InvoiceRecords_Details[] recordsArr = invoiceData_input.ToArray ();
		string data_string = SerializeObject_invoice (recordsArr);
		CreateXML (fileName_invoice,data_string);	
	}

	public void saveCurrentInvoice(){
		saveInvoiceFile (invoiceList);
	}

	public void addNewInvoice(){	
		invoiceList.Add(NewEmptyInvoice());
		saveInvoiceFile (invoiceList);	
	}

	public void addNewPage(int targetInvoice){	
		invoiceList[targetInvoice].itemDetails_List.Add(newEmptyRecord());
		saveInvoiceFile (invoiceList);	
	}

	public void copyData(int from_,int to_){
		Debug.Log ("COPY DATA START");
		InvoiceItemRecord[] newInvoiceItemRecord =  new InvoiceItemRecord[invoiceList [from_].itemDetails_List.Count];
		invoiceList [from_].itemDetails_List.CopyTo (newInvoiceItemRecord);
		invoiceList [to_].itemDetails_List.Clear ();

		for(int i = 0 ;i < newInvoiceItemRecord.Length;i++){
			invoiceList [to_].itemDetails_List.Add (newEmptyRecord());
			invoiceList [to_].itemDetails_List [i].quantity_List.Clear ();
			invoiceList [to_].itemDetails_List [i].description_List.Clear ();
			invoiceList [to_].itemDetails_List [i].unitPrice_List.Clear ();
			invoiceList [to_].itemDetails_List [i].nettAmount_List.Clear ();

			for(int j = 0 ; j < 17 ; j++){
				string newString;

				//List<string> newStringList_qty = new List<string> ();
				newString = newInvoiceItemRecord [i].quantity_List [j];
				invoiceList [to_].itemDetails_List[i].quantity_List.Add (newString);

				//List<string> newStringList_des = new List<string> ();
				newString = newInvoiceItemRecord [i].description_List [j];
				invoiceList [to_].itemDetails_List[i].description_List.Add (newString);

				//List<string> newStringList_uPrice = new List<string> ();
				newString = newInvoiceItemRecord [i].unitPrice_List [j];
				invoiceList [to_].itemDetails_List[i].unitPrice_List.Add (newString);

				//List<string> newStringList_nett = new List<string> ();
				newString = newInvoiceItemRecord [i].nettAmount_List [j];
				invoiceList [to_].itemDetails_List[i].nettAmount_List.Add (newString);
			
			}
		}	
		saveInvoiceFile (invoiceList);
	}

	[Header("Partner Realted")]

	//---------------------------------------------Partner------------------------------------------------------

	public List<clientDataDetails> clientList = new List<clientDataDetails>();
	public bool loaded_partner = false;

	void LoadPartner () {

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);

		}

		string dataString = LoadXML (fileName_partner);

		//if no such data recreate and load again
		if (dataString == null || dataString == "notExist") {
			clientList.Add (NewEmptyPartner());
			savePartnerFile (clientList);
			dataString = LoadXML (fileName_partner);
		}

		clientDataDetails[] recordsArr = DeserializeObject_partner (dataString) as clientDataDetails[];
		clientList.Clear ();

		for (int i = 0; i < recordsArr.Length; i++) {
			clientList.Add (recordsArr [i]);
		}

		loaded_partner = true;
	}

	public void addNewPartner(){
		clientList.Add (NewEmptyPartner());
		savePartnerFile (clientList);
		LoadPartner ();
	}

	clientDataDetails NewEmptyPartner(){
		clientDataDetails newClient = new clientDataDetails ();

		newClient.clientId_inDB = (clientList.Count).ToString("0000");

		newClient.clientAccNo = "New Client ";
		newClient.clientAddress = "New Address ";

		return newClient;
	}

	public void savePartnerFile(List<clientDataDetails> clientsData_input){
		clientDataDetails[] recordsArr = clientsData_input.ToArray ();
		string data_string = SerializeObject_partner (recordsArr);
		CreateXML (fileName_partner,data_string);	
	}

	//when not exist return -999
	public int partner_checkExistByAccNo(string inputAccNo){

		for (int i = 0; i < clientList.Count; i++) {
			if (clientList [i].clientAccNo == inputAccNo)
				return i;			
		}

		return -999;
	}

	public void partner_updateTargetClient(clientDataDetails inputData){
		int inputID = int.Parse(inputData.clientId_inDB);
		clientList [inputID] = inputData;
		savePartnerFile (clientList);
	}

	public clientDataDetails getPartnerByID(int inputID){
		clientDataDetails thisclient = clientList [inputID];
		return thisclient;
	}

	//----------------------------------Data Serilization-------------------------------------------------
	string _FileLocation;

	// Here we serialize our UserData object of myData 
	string SerializeObject_partner(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(clientDataDetails[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	object DeserializeObject_partner(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(clientDataDetails[])); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 

	// Here we serialize our UserData object of myData 
	string SerializeObject_invoice(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(InvoiceRecords_Details[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	object DeserializeObject_invoice(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(InvoiceRecords_Details[])); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 

	/* The following metods came from the referenced URL */ 
	string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 

	byte[] StringToUTF8ByteArray(string pXmlString) 
	{ 
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
		return byteArray; 
	} 
		

	// Finally our save and load methods for the file itself 
	void CreateXML(string fileName,string _data) 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(_FileLocation+"\\"+ fileName + ".xml"); 
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
		} 
		else 
		{ 
			t.Delete(); 
			writer = t.CreateText(); 
		}

		writer.Write(_data); 
		writer.Close(); 
	} 

	string LoadXML(string fileName) 
	{ 
		string fullFilePath = _FileLocation+"\\"+ fileName + ".xml";
		if (File.Exists (fullFilePath)) {
			StreamReader r = File.OpenText (_FileLocation + "\\" + fileName + ".xml"); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			return _info; 
		} else {
			return "notExist";		
		}
	} 


}

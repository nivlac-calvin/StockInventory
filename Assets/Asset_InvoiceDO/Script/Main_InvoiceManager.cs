using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


public struct InvoiceRecordsDetails{
	
	public string invoiceID;
	public string projectTitle;
	public string clientAccNo;
	public string clientAddress;
	public string invoiceDate;
	public string term;
	public string salesEng_Code;
	public string PurchaseOrder;

	public List<string> invoiceItem_List;
	public List<string> itemNo_List;
	public List<string> quantity_List;
	public List<string> unitPrice_List;
	public List<string> nettAmount_List;
};

public class Main_InvoiceManager : MonoBehaviour {

	public static Main_InvoiceManager instance;

	void Awake(){
	
		instance = this;
	}

	public static string fileName = "InvoiceRecordsFile";
	public List<InvoiceRecordsDetails> currentInvoiceList = new List<InvoiceRecordsDetails> ();

	public void addNewEmptyProject(){

		loadFiles ();
		currentInvoiceList.Add (NewEmptyInvoice());
		saveFile (currentInvoiceList);
	}

	public void addNewProject(InvoiceRecordsDetails inputInvoiceData){

		loadFiles ();
		currentInvoiceList.Add (inputInvoiceData);
		saveFile (currentInvoiceList);
	}

	public InvoiceRecordsDetails getInvoice(int id){

		loadFiles ();
		return currentInvoiceList [id];
	
	}	

	public void ModifyList(int inputIndex,InvoiceRecordsDetails inputInvoiceRecords){
		Debug.Log ("Modify : " + inputIndex);
		loadFiles ();
		currentInvoiceList [inputIndex] = inputInvoiceRecords;
		saveFile (currentInvoiceList);	
	}


	InvoiceRecordsDetails NewEmptyInvoice(){
		InvoiceRecordsDetails newInvoice = new InvoiceRecordsDetails ();

		newInvoice.invoiceID = (currentInvoiceList.Count).ToString("0000");
		Debug.Log ("New Invoice : " + newInvoice.invoiceID);
		newInvoice.projectTitle = "New Project " + newInvoice.invoiceID;

		newInvoice.invoiceItem_List = new List<string> ();
		newInvoice.invoiceItem_List.Add ("Item001");
		newInvoice.invoiceItem_List.Add ("Item002");
		newInvoice.itemNo_List = new List<string> ();
		newInvoice.quantity_List = new List<string> ();
		newInvoice.unitPrice_List = new List<string> ();
		newInvoice.nettAmount_List = new List<string> ();

		return newInvoice;
	}

	void display(){
		loadFiles ();
		for(int i = 0 ; i < currentInvoiceList.Count ; i++){

			Debug.Log (currentInvoiceList[i].invoiceID);

		}
	}


	//=====================Save and Load==========================

	public void loadFiles(){
		_FileLocation = Application.dataPath + "//InvoiceRecordsFile";

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
			currentInvoiceList.Add (NewEmptyInvoice());
			saveFile (currentInvoiceList);
		}

		currentInvoiceList = new List<InvoiceRecordsDetails> ();

		string dataString = LoadXML (fileName);
		Debug.Log("Data String");
		if (dataString != null) {
			InvoiceRecordsDetails[] recordsArr = DeserializeObject (dataString) as InvoiceRecordsDetails[];
			currentInvoiceList.Clear ();

			for (int i = 0; i < recordsArr.Length; i++) {
				currentInvoiceList.Add (recordsArr[i]);
			}
		}
	}

	public void saveFile(List<InvoiceRecordsDetails> invoicRecordList){
		InvoiceRecordsDetails[] recordsArr = invoicRecordList.ToArray ();
		string data_string = SerializeObject (recordsArr);
		CreateXML (fileName,data_string);	
	}

	//----------------------------------Data Serilization-------------------------------------------------

	static string _FileLocation;

	/* The following metods came from the referenced URL */ 
	static string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 

	static byte[] StringToUTF8ByteArray(string pXmlString) 
	{ 
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
		return byteArray; 
	} 

	// Here we serialize our UserData object of myData 
	static string SerializeObject(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(InvoiceRecordsDetails[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	static object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(InvoiceRecordsDetails[])); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 


	// Finally our save and load methods for the file itself 
	static void CreateXML(string fileName,string _data) 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(_FileLocation+"//"+ fileName + ".xml"); 
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

	static string LoadXML(string fileName) 
	{
		StreamReader r = File.OpenText(_FileLocation+"//"+ fileName + ".xml"); 
		Debug.Log ("File : " + _FileLocation+"//"+ fileName + ".xml");
		if (r == null) 
		{ 
			Debug.Log ("File not found"); 
			return null; 
		}
		string _info = r.ReadToEnd(); 
		r.Close(); 
		return _info; 
	} 
}

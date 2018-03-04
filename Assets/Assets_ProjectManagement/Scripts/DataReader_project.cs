using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class DataReader_project : MonoBehaviour {

	public struct dataStruct{

		public string fileName;
		public Item[] itemArr;

	}

	string data_FileLocation;

	List<dataStruct> List_dataStruct = new List<dataStruct> ();
	// Use this for initialization
	void Start () {
		data_FileLocation = Application.dataPath + "//DataFile";

		if (Directory.Exists(data_FileLocation) == false) {
			Directory.CreateDirectory (data_FileLocation);
		}

		getFile ();	
		displayDataList ();
		Item thisItem = searchByID ("Main_000000");
		Debug.Log (thisItem.ID + "   " + thisItem.brand_name);
	}

	// Update is called once per frame
	void Update () {

	}

	public Item searchByID(string ID_input){

		string[] splited_ID = ID_input.Split (new string[] { "_" },System.StringSplitOptions.None);

		for(int i =0 ; i <  List_dataStruct.Count ; i++){

			if (List_dataStruct [i].fileName == splited_ID[0] ) {

				for(int j = 0 ; j < List_dataStruct[i].itemArr.Length ; j++){

					if (List_dataStruct [i].itemArr [j].ID == ID_input) {
					
						return List_dataStruct [i].itemArr [j];
					}

				}

			}
		}

		Debug.Log ("GG got bug");
		return null;
	}



	void getFile(){
		List_dataStruct.Clear ();
		string[] filePaths;

		filePaths = Directory.GetFiles (data_FileLocation); 

		for (int i = 0; i < filePaths.Length; i++) {

			string fileName = Path.GetFileName (filePaths[i]);

			if (fileName.Contains (".xml") && fileName.Contains("meta") == false) {
				loadData(fileName);
				Debug.Log ("File : " + fileName);
			}
		}
	}

	void loadData(string fileName_){

		string _data = LoadXML_data (fileName_);
		if(_data.ToString() != "") 
		{ 
			Item[] itemData_this = (Item[])DeserializeObject(_data); 
			dataStruct thisDataStruct;
			string[] splited = fileName_.Split ('.');
			thisDataStruct.fileName = splited[0];
			thisDataStruct.itemArr = itemData_this;
			List_dataStruct.Add (thisDataStruct);
		}
	}

	void displayDataList(){
	
		for(int i= 0; i < List_dataStruct.Count ; i++){

			Debug.Log ("------------------------" + List_dataStruct[i].fileName + "------------------------" );
			for(int j = 0 ; j < List_dataStruct[i].itemArr.Length ; j++){

				Debug.Log(List_dataStruct [i].itemArr [j].ID);
				
			}		
		}

	}

	public void saveAll(){

		for(int i = 0 ; i < List_dataStruct.Count; i++){

			Item[] itemData_ = List_dataStruct[i].itemArr;
			string this_data = SerializeObject(itemData_); 
			CreateXML_data( List_dataStruct[i].fileName,this_data);
		}
	}

	//----------------------------------Data Serilization-------------------------------------------------


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

	// Here we serialize our UserData object of myData 
	string SerializeObject(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Item[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(Item[])); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 

	// Finally our save and load methods for the file itself 
	//----------------------------------------------------------------------------------------------
	void CreateXML_data(string fileName,string _data) 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(data_FileLocation+"\\"+ fileName + ".xml"); 
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

	string LoadXML_data(string fileName) 
	{ 
		StreamReader r = File.OpenText(data_FileLocation+"\\"+ fileName); 
		string _info = r.ReadToEnd(); 
		r.Close(); 
		return _info; 
	} 

}

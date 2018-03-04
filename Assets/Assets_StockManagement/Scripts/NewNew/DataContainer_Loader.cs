using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class DataContainer_Loader : MonoBehaviour {

	public bool loaded = false;
	public void OnEnable(){
	
		LoadLoad ();
	}

	// Use this for initialization
	void LoadLoad () {
		loaded = false;
		_FileLocation = Application.dataPath + "//DataContainer";

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
		}

		getFile ();		
	}

	public List<DataContainer> dataList = new List<DataContainer> ();

	void getFile(){
		//clearAndDestroy ();
		string[] filePaths;

		filePaths = Directory.GetFiles (_FileLocation); 
		int listIndex = 0;
		for (int i = 0; i < filePaths.Length; i++) {

			string fileName = Path.GetFileName (filePaths[i]);

			if (fileName.Contains (".xml") && fileName.Contains("meta") == false) {
				Debug.Log (fileName);
				dataList.Add(LoadDatabase (fileName));
				listIndex++;
			}

		}
		loaded = true;
	}
		
	DataContainer LoadDatabase(string fileName_){
		DataContainer targetDataCtn = new DataContainer();

		string _data = LoadXML (fileName_);
		Debug.Log ("Start Load : " + _data);
		if(_data.ToString() != "") 
		{ 
			Item[] itemData_this = (Item[])DeserializeObject(_data); 

			string[] splited = fileName_.Split ('.');
			targetDataCtn.tableName = splited[0];

			List<Item> itemList_loaded = new List<Item>();
			for(int i = 0; i < itemData_this.Length;i++){

				itemList_loaded.Add (itemData_this [i]);
			}

			targetDataCtn.saveToThis (itemList_loaded);
		}

		return targetDataCtn;

	}

	//----------------------------------Data Serilization-------------------------------------------------
	string _FileLocation;

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
		StreamReader r = File.OpenText(_FileLocation+"\\"+ fileName); 
		string _info = r.ReadToEnd(); 
		r.Close(); 
		return _info; 
	} 

}

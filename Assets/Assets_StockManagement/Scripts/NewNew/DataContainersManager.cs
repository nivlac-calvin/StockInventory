using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class DataContainersManager : MonoBehaviour {

	List<GameObject> buttonObjList;

	[SerializeField]
	GameObject buttonPrefab;
	List<GameObject> buttonPrefabList;
	[SerializeField]
	Transform buttonParent_transform;

	// Use this for initialization
	void Start () {

		_FileLocation = Application.dataPath + "//DataContainer";

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
		}

		getFile ();		
	}

	void getFile(){
		clearAndDestroy ();
		string[] filePaths;

		filePaths = Directory.GetFiles (_FileLocation); 
		int listIndex = 0;
		for (int i = 0; i < filePaths.Length; i++) {

			string fileName = Path.GetFileName (filePaths[i]);

			if (fileName.Contains (".xml") && fileName.Contains("meta") == false) {

				LoadDataTab(fileName,listIndex);
				listIndex++;
			}

		}
	}

	void LoadDataTab(string fileName_,int index_){
		
		string _data = LoadXML (fileName_);
		Debug.Log ("Start Load : " + _data);
		if(_data.ToString() != "") 
		{ 
			Item[] itemData_this = (Item[])DeserializeObject(_data); 
			GameObject newPrefab = Instantiate (buttonPrefab,buttonParent_transform) as GameObject;
			newPrefab.SetActive (true);
			DataContainer targetDataCtn = new DataContainer();
			string[] splited = fileName_.Split ('.');
			targetDataCtn.tableName = splited[0];
			targetDataCtn.indexID = index_;

			List<Item> itemList_loaded = new List<Item>();
			for(int i = 0; i < itemData_this.Length;i++){

				itemList_loaded.Add (itemData_this [i]);
			}

			targetDataCtn.saveToThis (itemList_loaded);
			newPrefab.GetComponent<DataTab_controller> ().thisDataContainersManager = this;
			newPrefab.GetComponent<DataTab_controller> ().assignData (targetDataCtn);
			buttonPrefabList.Add (newPrefab);
		}

	}

	void clearAndDestroy(){
		if (buttonPrefabList != null) {
			for(int i = 0 ;i < buttonPrefabList.Count;i++){

				Destroy (buttonPrefabList[i]);

			}
			buttonPrefabList.Clear ();
		} else {
			buttonPrefabList = new List<GameObject> ();
		}

	}




	public IDgenerator ID_generator;

	//----------------Creat New------------------------------



	public void createNewItemList(string itemListName){

		Item[] itemList_input_new = new Item[1];
		itemList_input_new [0] = new Item (ID_generator.GenerateNewID(itemListName));
		string dataInput = SerializeObject (itemList_input_new);
		CreateXML (itemListName,dataInput);
		getFile ();
	}

	public void saveData(int target_index){
		Debug.Log ("Target : " + target_index);
		DataContainer targetDataCTN = buttonPrefabList [target_index].GetComponent<DataTab_controller> ().thisDataContainer;

		Item[] itemData_ = new Item[targetDataCTN.itemList.Count];

		itemData_ = targetDataCTN.itemList.ToArray();

		for(int i = 0; i < itemData_.Length;i++){

			Debug.Log (" " + i + " " + itemData_[i].brand_name);
		}
		// Time to creat our XML! 
		string this_data = SerializeObject(itemData_); 
		// This is the final resulting XML from the serialization process 
		CreateXML( targetDataCTN.tableName,this_data);
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

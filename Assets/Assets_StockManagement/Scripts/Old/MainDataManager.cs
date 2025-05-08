using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class MainDataManager : MonoBehaviour {

	List<DataManager> data_list;

	// Use this for initialization
	void Start () {
		_FileLocation = Application.dataPath + "//DataFile";

		if (Directory.Exists(_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
		}

		getFile ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void getFile(){
		
		string[] filePaths;

		filePaths = Directory.GetFiles (_FileLocation); 
		int listIndex = 0;
		clearPre ();
		for (int i = 0; i < filePaths.Length; i++) {

			string fileName = Path.GetFileName (filePaths[i]);

			if (fileName.Contains (".xml") && fileName.Contains("meta") == false) {
				
				loadToDataManager(fileName,listIndex);
				listIndex++;
			}

		}
	}

	[SerializeField]
	GameObject itemList_prefab;
	[SerializeField]
	Transform itemList_Tabs;

	public List<GameObject> prefabList;

	void clearPre(){
		for (int i = 0; i < prefabList.Count; i++) {
			Destroy (prefabList [i]);
		}
		prefabList.Clear ();
	}

	void loadToDataManager(string fileName_,int index_){
		
		string _data = LoadXML (fileName_);
		if(_data.ToString() != "") 
		{ 
			Item[] itemData_this = (Item[])DeserializeObject(_data); 
			GameObject newPrefab = Instantiate (itemList_prefab,itemList_Tabs) as GameObject;
			newPrefab.SetActive (true);
			DataManager targetDataMng = newPrefab.GetComponent<DataManager> ();
			string[] splited = fileName_.Split ('.');
			targetDataMng.tableName.text = splited[0];
			targetDataMng.thisIndex = index_;

			targetDataMng.itemList.Clear ();

			for(int i = 0; i < itemData_this.Length;i++){

				targetDataMng.itemList.Add (itemData_this [i]);
			}

			prefabList.Add (newPrefab);
		}
	}

	public IDgenerator ID_generator;

	public void createNewItemList(string itemListName){
	
		Item[] itemList_input_new = new Item[1];
		itemList_input_new [0] = new Item (ID_generator.GenerateNewID(itemListName));
		string dataInput = SerializeObject (itemList_input_new);
		CreateXML (itemListName,dataInput);
		getFile ();
	}

	public void saveData(int target_index){
		Debug.Log ("Target : " + target_index);
		Item[] itemData_ = new Item[prefabList [target_index].GetComponent<DataManager> ().itemList.Count];
		itemData_ = prefabList [target_index].GetComponent<DataManager> ().itemList.ToArray();

		for(int i = 0; i < itemData_.Length;i++){

			Debug.Log (" " + i + " " + itemData_[i].brand_name);
		}
		// Time to creat our XML! 
		string this_data = SerializeObject(itemData_); 
		// This is the final resulting XML from the serialization process 
		CreateXML( prefabList [target_index].GetComponent<DataManager> ().tableName.text,this_data);
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

	string LoadXML(string fileName) 
	{ 
		StreamReader r = File.OpenText(_FileLocation+"//"+ fileName); 
		string _info = r.ReadToEnd(); 
		r.Close(); 
		return _info; 
	} 
		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

[System.Serializable]
public struct ItemRecordDetails{

	public string ItemName;
	public List<float> priceList;
};

public class ItemRecords_Manager: MonoBehaviour {

	public static string fileName = "ItemRecordsFile";
	public static List<ItemRecordDetails> currentItemsRecord_List = new List<ItemRecordDetails> ();

	void Start(){
		
		_FileLocation = Application.dataPath + "//ItemRecordsFiles";

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
		}

//		List<ItemRecordDetails> itemRecordList_temp = new List<ItemRecordDetails> ();
//		ItemRecordDetails newItemRecord = new ItemRecordDetails();
//
//		newItemRecord.ItemName = "AirBag";
//		newItemRecord.priceList = new List<float> ();
//		newItemRecord.priceList.Add (10.00f);
//		itemRecordList_temp.Add (newItemRecord);
//		newItemRecord.ItemName = "WaterBAg";
//		itemRecordList_temp.Add (newItemRecord);
//		newItemRecord.ItemName = "FireBa";
//		itemRecordList_temp.Add (newItemRecord);
//		newItemRecord.ItemName = "WindBag";
//		itemRecordList_temp.Add (newItemRecord);
//		newItemRecord.ItemName = "AlibabaBag";
//		itemRecordList_temp.Add (newItemRecord);
//
//		saveFile (itemRecordList_temp);

		loadFiles ();
	}

	public static List<ItemRecordDetails> searchByName(string input_name){

		List<ItemRecordDetails> matchedList = new List <ItemRecordDetails>();

		for(int i = 0 ; i < currentItemsRecord_List.Count ; i++){

			if (currentItemsRecord_List [i].ItemName.ToLower ().Contains (input_name.ToLower())) {
			
				matchedList.Add (currentItemsRecord_List[i]);

				if (matchedList.Count > 5)
					break;
			}
		
		}

		return matchedList;
	}

	public static void addNew(string inputName, float inputPrice){

		if (inputName != "") {
			bool new_= true;
			for(int i = 0 ; i < currentItemsRecord_List.Count ; i++){

				if (currentItemsRecord_List [i].ItemName.ToLower ().Trim () == inputName.ToLower ().Trim ()) {
					if (checkResultNew (currentItemsRecord_List [i], inputPrice)) {
						currentItemsRecord_List [i].priceList.Add (inputPrice);
					}
					new_ = false;
					break;
				} 
			}

			if (new_) {
				ItemRecordDetails newItem = new ItemRecordDetails ();
				newItem.ItemName = inputName;
				newItem.priceList = new List<float> ();
				newItem.priceList.Add (inputPrice);
				currentItemsRecord_List.Add (newItem);
				saveFile (currentItemsRecord_List);
			}
		}
	}

	static bool checkResultNew(ItemRecordDetails inputRecord,float inputPrice){


		for(int i = 0; i < inputRecord.priceList.Count ; i++){

			if (inputPrice == inputRecord.priceList [i]) {

				return false;
			}
		}

		return true;	
	}

	//=====================Save and Load==========================

	static void loadFiles(){

		string dataString = LoadXML (fileName);
		if (dataString != null) {
			ItemRecordDetails[] recordsArr = DeserializeObject (dataString) as ItemRecordDetails[];
			List<ItemRecordDetails> recordsList = new List<ItemRecordDetails> ();
			currentItemsRecord_List.Clear ();

			for (int i = 0; i < recordsArr.Length; i++) {
				currentItemsRecord_List.Add (recordsArr[i]);
			}
		}
	}

	static void saveFile(List<ItemRecordDetails> itemRecordList){
		ItemRecordDetails[] recordsArr = itemRecordList.ToArray ();
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
		XmlSerializer xs = new XmlSerializer(typeof(ItemRecordDetails[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	static object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(ItemRecordDetails[])); 
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
		string _info = r.ReadToEnd(); 
		r.Close(); 
		return _info; 
	} 
}

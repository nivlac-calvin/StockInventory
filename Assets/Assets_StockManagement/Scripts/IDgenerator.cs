using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text;

public class ID_details{

	public string full_ID;
	public int number;
	public string fileName;

	public ID_details(){
		
	}

	public ID_details(string input_fileName,int input_number){

		number = input_number;
		fileName = input_fileName;	
		full_ID = fileName +"_"+numberGenerate(number);
		Debug.Log (full_ID);
	}

	string numberGenerate(int input_num){

		if (99999 < input_num) {
			return input_num.ToString ();
		} else if (9999 < input_num) {
			return "0"+ input_num.ToString ();
		} else if (999 < input_num) {
			return "00"+ input_num.ToString ();
		} else if (99 < input_num) {
			return "000"+ input_num.ToString ();
		} else if (9 < input_num) {
			return "0000"+ input_num.ToString ();
		} else {
			return "00000"+ input_num.ToString ();
		}
	}
}

public class IDgenerator : MonoBehaviour {

	List<ID_details> idList = new List<ID_details> ();

	ID_details id01;

	string IDdata_FolderLocation;
	string IDdata_FileLocation;
	// Use this for initialization
	void Start () {


		IDdata_FolderLocation = Application.dataPath + "//ID_File";
		IDdata_FileLocation = IDdata_FolderLocation +  "\\ID_data.xml";

		if (Directory.Exists(IDdata_FolderLocation) == false) {
			Directory.CreateDirectory (IDdata_FolderLocation);
		}
		getFile ();	
	}

	// Update is called once per frame
	void Update () {
		
	}

	public string GenerateNewID(string fileName_){
		int num_ = 0;
		for (int i = 0; i < idList.Count; i++) {
		
			if (idList [i].fileName == fileName_) {
				num_ = idList [i].number;
				num_++;
			}

		}
		idList.Add (new ID_details(fileName_,num_));
		saveData ();
		display ();
		return idList [idList.Count-1].full_ID;
	}

	void display(){
	
		for(int i = 0 ; i < idList.Count ; i++){

			Debug.Log("Display : " + idList [i].full_ID);

		}

	}

	void getFile(){
		idList.Clear ();
		string _data = LoadXML_data ();
		if (_data.ToString () != "") { 
			ID_details[] ID_Data_this = (ID_details[])DeserializeObject (_data); 

			for (int i = 0; i < ID_Data_this.Length; i++) {
				idList.Add (ID_Data_this [i]);
				Debug.Log ("Loaded : " + ID_Data_this [i].full_ID);
			}
		} else {
			saveData ();
			getFile ();
		}
	}

	public void saveData(){
		ID_details[] ID_data = new ID_details[idList.Count];
		ID_data = idList.ToArray();

		// Time to creat our XML! 
		string this_data = SerializeObject(ID_data); 
		// This is the final resulting XML from the serialization process 
		CreateXML_data(this_data);
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
		XmlSerializer xs = new XmlSerializer(typeof(ID_details[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(ID_details[])); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 

	// Finally our save and load methods for the file itself 
	//----------------------------------------------------------------------------------------------
	void CreateXML_data(string _data) 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(IDdata_FileLocation); 
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

	string LoadXML_data() 
	{ 
		if (File.Exists (IDdata_FileLocation)) {
			StreamReader r = File.OpenText (IDdata_FileLocation); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			return _info; 
		} else
			return "";

	}
}

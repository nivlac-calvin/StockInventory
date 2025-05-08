using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class DataManager_project : MonoBehaviour {

	public struct projectData{
	
		public string fileName;
		public ProjectDetails projectDetails;
	}

	string project_FileLocation;

	List<projectData> List_projectData = new List<projectData>();

	// Use this for initialization
	void Start () {
		project_FileLocation = Application.dataPath + "//ProjectFile";

		if (Directory.Exists(project_FileLocation) == false) {
			Directory.CreateDirectory (project_FileLocation);
		}
		string this_data = SerializeObject(new ProjectDetails()); 
		getFile ();	
		displayDataList ();
	}

	// Update is called once per frame
	void Update () {

	}

	void getFile(){
		string[] filePaths;

		filePaths = Directory.GetFiles (project_FileLocation); 

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
			projectData thisProData;
			thisProData.projectDetails = (ProjectDetails)DeserializeObject(_data); 
			thisProData.fileName = fileName_;
			List_projectData.Add (thisProData);
		}
	}

	[SerializeField]
	ProjectManager P_manager;
	void displayDataList(){
		P_manager.inProgressList.Clear ();
		P_manager.completedList.Clear ();
		for(int i= 0; i < List_projectData.Count ; i++){

			if (List_projectData [i].projectDetails.P_state == progressState.INPROGRESS) {
			
				P_manager.inProgressList.Add (List_projectData [i].fileName);
			}
			else if (List_projectData [i].projectDetails.P_state == progressState.COMPLETED) {

				P_manager.completedList.Add (List_projectData [i].fileName);
			}
		}
		P_manager.refreshButtons ();
	}

//	public void saveAll(){
//
//		for(int i = 0 ; i < List_dataStruct.Count; i++){
//
//			Item[] itemData_ = List_dataStruct[i].itemArr;
//			string this_data = SerializeObject(itemData_); 
//			CreateXML_data( List_dataStruct[i].fileName,this_data);
//		}
//	}

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
		XmlSerializer xs = new XmlSerializer(typeof(ProjectDetails)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(ProjectDetails)); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 

	// Finally our save and load methods for the file itself 
	//----------------------------------------------------------------------------------------------
	void CreateXML_data(string fileName,string _data) 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(project_FileLocation+"//"+ fileName + ".xml"); 
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
		StreamReader r = File.OpenText(project_FileLocation+"//"+ fileName); 
		string _info = r.ReadToEnd(); 
		r.Close(); 
		return _info; 
	} 

}

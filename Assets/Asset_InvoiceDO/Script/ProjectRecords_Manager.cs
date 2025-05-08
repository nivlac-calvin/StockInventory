using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


public struct ProjectRecordsDetails{
	public string projectName;
	public List<string> projectItem_List;
};

public class ProjectRecords_Manager : MonoBehaviour {

	public static ProjectRecords_Manager instance;

	public static string fileName = "ProjectRecordsFile";
	public List<ProjectRecordsDetails> currentProject_List = new  List<ProjectRecordsDetails>();

	void Awake(){

		instance = this;

	}

	static string _FileLocation;

	void Start(){
//		ProjectRecordsDetails newProject = new ProjectRecordsDetails();
//		newProject.projectName = "MIXER BATCHING PLANT CONTROL PANEL SC-130W(2)";
//		newProject.projectItem_List = new List<string> ();
//		newProject.projectItem_List.Add ("NES 3P MCCB 175A");
//		newProject.projectItem_List.Add ("NES 3P MCCB 185A");
//		newProject.projectItem_List.Add ("NES 3P MCCB 198A");
//		newProject.projectItem_List.Add ("NES 3P MCCB 1000A");
//
//		currentProject_List = new List<ProjectRecordsDetails> ();
//		currentProject_List.Add (newProject);
//		newProject.projectName = "Ali Project";
//		currentProject_List.Add (newProject);
//		saveFile (currentProject_List);
	}


	public void addNewProject(ProjectRecordsDetails inputProject){

		loadFiles ();
		currentProject_List.Add (inputProject);
		saveFile (currentProject_List);
	}

	//=====================Save and Load==========================

	public void loadFiles(){
		_FileLocation = Application.dataPath + "//ProjectRecordsFile";

		if (Directory.Exists (_FileLocation) == false) {
			Directory.CreateDirectory (_FileLocation);
		}

		currentProject_List = new List<ProjectRecordsDetails> ();

		string dataString = LoadXML (fileName);
		if (dataString != null) {
			ProjectRecordsDetails[] recordsArr = DeserializeObject (dataString) as ProjectRecordsDetails[];
			List<ProjectRecordsDetails> recordsList = new List<ProjectRecordsDetails> ();
			currentProject_List.Clear ();

			for (int i = 0; i < recordsArr.Length; i++) {
				currentProject_List.Add (recordsArr[i]);
			}
		}
	}

	public void saveFile(List<ProjectRecordsDetails> itemRecordList){
		ProjectRecordsDetails[] recordsArr = itemRecordList.ToArray ();
		string data_string = SerializeObject (recordsArr);
		CreateXML (fileName,data_string);	
	}

	//----------------------------------Data Serilization-------------------------------------------------

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
		XmlSerializer xs = new XmlSerializer(typeof(ProjectRecordsDetails[])); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	static object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(ProjectRecordsDetails[])); 
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

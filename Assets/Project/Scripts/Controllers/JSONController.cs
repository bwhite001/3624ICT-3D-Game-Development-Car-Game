using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class JSONController : MonoBehaviour {
	public TextAsset file;
	private JSONNode programs;
	private string filePath = "programs";

	public JSONController () {
		parseFile ();
	}
	void parseFile()
	{
		file = (TextAsset) Resources.Load(filePath);
		if (file == null)
		{
			Debug.Log("File Not Loaded");
		}
		else 
		{
			Debug.Log("JSON File Loaded");
			programs = JSON.Parse(file.text);
		}
	}

	public JSONNode getJSONNode()
	{
		return programs;
	}

	public string[] getPrograms(string type)
	{
		string[] programNames = new string[programs.Count];
		if(type == "name" || type == "code")
			for (int i = 0; i<programs.Count; i++) 
			{
				programNames[i] = programs[i][type];
			}

		return programNames;
	}

	public void setCurrentProgram(string programCode)
	{
		for (int i = 0; i<programs.Count; i++) 
		{
			string code = programs[i]["code"];
			if(code == programCode)
			{
				ProgramObjectController.setProgram(programs[i]);
				return;
			}
		}

		ProgramObjectController.setProgram(null);
	}

}

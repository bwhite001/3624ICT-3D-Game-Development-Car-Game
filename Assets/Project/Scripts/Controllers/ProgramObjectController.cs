using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ProgramObjectController : MonoBehaviour {
	public static JSONNode program;
	public static JSONNode currentMajor;

	public ProgramObjectController() {

	}

	public static void setProgram(JSONNode currentProgram)
	{
		program = currentProgram;
	}

	public static JSONNode core()
	{
		return program["coreCourses"];
	}

	public static string[] getProgram()
	{
		return new string[2] {program["code"], program["name"]};
	}
}

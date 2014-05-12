using UnityEngine;
using System.Collections;

public class ProgramListMenu : MonoBehaviour {
	public GUIStyle buttonStyle;
	private int screenW;
	private int screenH;

	private float buttonW = 500f;
	private float buttonH = 100f;

	private JSONController jsonController;

	private string[] programNames;
	private string[] programCodes;

	void Start() {
		screenW = Screen.width;
		screenH = Screen.height;

		jsonController = new JSONController();

		programNames = jsonController.getPrograms ("name");
		programCodes = jsonController.getPrograms ("code");
	}

	void OnGUI(){
		float fullH = (0.5f * screenH) - programNames.Length * (buttonH + 10) / 2;
		Rect buttonBox = new Rect ((0.5f * screenW) - buttonW/2, fullH, buttonW, buttonH);


		for(int i = 0; i<programNames.Length; i++)
		{
			string buttonName = programCodes[i]+" - "+programNames[i];

			Rect newButtonBox = buttonBox;

			newButtonBox.y = buttonBox.y + i * (buttonBox.height + 10);

			if (GUI.Button(newButtonBox,buttonName, buttonStyle)) 
			{
				goToProgram(programCodes[i]);
			}
		}

	}

	void goToProgram (string code)
	{
		jsonController.setCurrentProgram (code);

		if (ProgramObjectController.program != null) 
		{
			Application.LoadLevel(1);
		}
	}
}

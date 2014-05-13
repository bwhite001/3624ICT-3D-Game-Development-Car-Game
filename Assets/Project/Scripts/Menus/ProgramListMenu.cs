using UnityEngine;
using System.Collections;

public class ProgramListMenu : MonoBehaviour {
	public GUIStyle buttonStyle;
	public GUIStyle backbuttonStyle;

	private int screenW;
	private int screenH;

	public float buttonW = 500f;
	public float buttonH = 100f;
	public float bbuttonW = 500f;
	public float bbuttonH = 100f;


	public Rect backBtn;

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

		backBtn = buttonBox;
		backBtn.width = bbuttonW;
		backBtn.height = bbuttonH;

		backBtn.y = buttonBox.y + (programNames.Length) * (buttonH + 10);
		backBtn.x = (0.5f * screenW) - buttonW/1.5f;

		if(GUI.Button(backBtn,"Back", backbuttonStyle))
			Application.LoadLevel(0);

	}

	void goToProgram (string code)
	{
		jsonController.setCurrentProgram (code);

		if (ProgramObjectController.program != null) 
		{
			Application.LoadLevel(2);
		}
	}
}

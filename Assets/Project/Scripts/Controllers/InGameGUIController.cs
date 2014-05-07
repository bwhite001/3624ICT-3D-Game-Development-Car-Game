using UnityEngine;
using System.Collections;

public class InGameGUIController : MonoBehaviour {
	private bool showMajorMenu = false;

	public GUIStyle buttonStyle;

	private int screenW;
	private int screenH;
	
	private float buttonW = 200f;
	private float buttonH = 50f;
	
	private string[] majorNames;

	private int largestChar = 0;

	void Start() {
		screenW = Screen.width;
		screenH = Screen.height;

	}

	void OnGUI(){
		if(showMajorMenu)
		{
			createSelectMajorMenu();
		}
	}

	public void displayMajorMenu(string[] btnNames, bool show)
	{
		majorNames = btnNames;
		showMajorMenu = show;

		foreach(string str in majorNames)
		{
			if(str.Length > largestChar)
				largestChar = str.Length;
		}
	}

	void createSelectMajorMenu()
	{
		buttonW = largestChar * buttonStyle.fontSize / 1.5f;
		float fullH = (0.5f * screenH) - majorNames.Length * (buttonH + 10) / 2;
		Rect buttonBox = new Rect ((0.5f * screenW) - buttonW/2, fullH, buttonW, buttonH);
		Rect fullBox = new Rect (buttonBox);
		fullBox.width = buttonW + buttonW/2;
		fullBox.x = fullBox.x - buttonW/4;
		fullBox.height = fullH*2 + fullH/2;
		fullBox.y = fullBox.y - fullH / 4; 

		GUI.Box (fullBox, "Pick A Major");

		for(int i = 0; i<majorNames.Length; i++)
		{
			string buttonName = majorNames[i];
			
			Rect newButtonBox = buttonBox;
			
			newButtonBox.y = buttonBox.y + i * (buttonBox.height + 10);
			
			if (GUI.Button(newButtonBox,buttonName, buttonStyle)) 
			{
				showMajorMenu = false;
				GameObject.FindGameObjectWithTag("GameController").SendMessage("selectMajor", buttonName);
			}
		}
	}
}

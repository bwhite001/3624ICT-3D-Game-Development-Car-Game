using UnityEngine;
using System.Collections;

public class InGameGUIController : MonoBehaviour {
	private bool showMajorMenu = false;

	public GUIStyle buttonStyle;
	private int screenW;
	private int screenH;
	
	private float buttonW = 500f;
	private float buttonH = 100f;
	
	private string[] majorNames;
	
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
	}

	void createSelectMajorMenu()
	{
		float fullH = (0.5f * screenH) - majorNames.Length * (buttonH + 10) / 2;
		Rect buttonBox = new Rect ((0.5f * screenW) - buttonW/2, fullH, buttonW, buttonH);
		
		for(int i = 0; i<majorNames.Length; i++)
		{
			string buttonName = majorNames[i];
			
			Rect newButtonBox = buttonBox;
			
			newButtonBox.y = buttonBox.y + i * (buttonBox.height + 10);
			
			if (GUI.Button(newButtonBox,buttonName, buttonStyle)) 
			{
				//goToProgram(majorNames[i]);
			}
		}
	}
}

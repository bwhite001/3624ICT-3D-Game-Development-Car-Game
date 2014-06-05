using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUIStyle buttonStyle;
	private int screenW;
	private int screenH;
	
	public float buttonW = 300f;
	public float buttonH = 60f;
	public float iconS = 30f;
	public float icontop;
	public float iconleft;


	private JSONController jsonController;
	
	public string[] buttonNames;
	public Texture[] Icons;

	void Start() {
		screenW = Screen.width;
		screenH = Screen.height;
	}
	
	void OnGUI(){
		float fullH = (0.5f * screenH) - buttonNames.Length * (buttonH +4) / 2;
		Rect buttonBox = new Rect ((0.5f * screenW) - buttonW/2, fullH, buttonW, buttonH);
		
		
		for(int i = 0; i<buttonNames.Length; i++)
		{
			string buttonName = buttonNames[i];
			
			Rect newButtonBox = buttonBox;
			
			newButtonBox.y = buttonBox.y + i * (buttonBox.height + 10);

			Rect iconBox = new Rect(newButtonBox.x + iconleft, newButtonBox.y + icontop, iconS, iconS); 
			

			if (GUI.Button(newButtonBox,buttonName, buttonStyle)) 
			{
				goToProgram(buttonName);
			}

			if(Icons[i])
				GUI.DrawTexture(iconBox, Icons[i]);
		}
		
	}
	
	void goToProgram (string code)
	{
		GameObject.FindGameObjectWithTag ("MenuController").SendMessage ("setScene", code);
	}
}

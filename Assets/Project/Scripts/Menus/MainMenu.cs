using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUIStyle buttonStyle;
	private int screenW;
	private int screenH;
	
	public float buttonW = 500f;
	public float buttonH = 100f;
	public float iconS = 100f;
	public float icontop;
	public float iconleft;


	private JSONController jsonController;
	
	public string[] buttonNames;
	public int[] buttonLocations;
	public Texture[] Icons;

	void Start() {
		screenW = Screen.width;
		screenH = Screen.height;
	}
	
	void OnGUI(){
		float fullH = (0.5f * screenH) - buttonNames.Length * (buttonH + 10) / 2;
		Rect buttonBox = new Rect ((0.5f * screenW) - buttonW/2, fullH, buttonW, buttonH);
		
		
		for(int i = 0; i<buttonNames.Length; i++)
		{
			string buttonName = buttonNames[i];
			
			Rect newButtonBox = buttonBox;
			
			newButtonBox.y = buttonBox.y + i * (buttonBox.height + 10);

			Rect iconBox = new Rect(newButtonBox.x + iconleft, newButtonBox.y + icontop, iconS, iconS); 
			

			if (GUI.Button(newButtonBox,buttonName, buttonStyle)) 
			{
				if(buttonLocations[i] != 0)
					goToProgram(buttonLocations[i]);
			}

			if(Icons[i])
				GUI.DrawTexture(iconBox, Icons[i]);
		}
		
	}
	
	void goToProgram (int code)
	{
		Debug.Log (code);
		Application.LoadLevel(code);
	}
}

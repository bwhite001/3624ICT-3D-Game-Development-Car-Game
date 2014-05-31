using UnityEngine;
using System.Collections;

public class PickCarColorMenu : MonoBehaviour {
	public GameObject CarPicker;
	public Material[] carMaterals;
	public string[] colors = new string[] {"Blue","Green","Orange","Purple","Red","Yellow"};
	public string[] nav = new string[] {"Go Back", "Play Game"};
	public GUIStyle buttonStyle;
	public GUIStyle selectedButtonStyle;
	public GUIStyle navButtonStyle;
	private int screenW;
	private int screenH;
	
	public float buttonW = 500f;
	public float buttonH = 100f;
	public float offsetW = 0f;
	public float offsetH = 0f;

	public int currentColor = 0;

	void Start()
	{
		screenW = Screen.width;
		screenH = Screen.height;

		setCarColor (currentColor);
	}
	public void setCarColor(int selectedColor)
	{
		currentColor = selectedColor;

		if(carMaterals[currentColor] != null)
		{
			CarPicker.renderer.material = carMaterals[currentColor];
		}
	}

	void OnGUI(){
		float fullH = (0.5f * screenH) - colors.Length * (buttonH + 10) / 2;
		Rect buttonBox = new Rect ((0.5f * screenW) - buttonW/2 + offsetW, fullH+offsetH, buttonW, buttonH);
		
		
		for(int i = 0; i<colors.Length; i++)
		{
			GUIStyle currentBtnStyle = buttonStyle;
			string buttonName = colors[i];

			if(i == currentColor)
			{
				currentBtnStyle = selectedButtonStyle;
				buttonName += "\n Selected";
			}

			
			Rect newButtonBox = buttonBox;
			
			newButtonBox.y = buttonBox.y + i * (buttonBox.height + 10);

			if (GUI.Button(newButtonBox,buttonName,currentBtnStyle)) 
			{
				setCarColor(i);
			}
		}

		Rect navBox = new Rect (10, screenH-buttonH-10, buttonW, buttonH);

		for(int i = 0; i<nav.Length; i++)
		{
			string buttonName = nav[i];
			
			Rect newButtonBox = navBox;

			if(i == 1)
				newButtonBox.x = screenW-buttonW-10;
			
			if (GUI.Button(newButtonBox,buttonName,navButtonStyle)) 
			{
				if(i == 0)
					Application.LoadLevel(1);
				else {
					CarColors.setColor(currentColor);
					Application.LoadLevel(3);
				}
			}
		}
	}
}

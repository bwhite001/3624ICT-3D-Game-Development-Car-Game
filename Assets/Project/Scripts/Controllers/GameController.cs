using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameController : MonoBehaviour {

	private WaypointController waypointController;
	private GUIController guiController;
	private InGameGUIController inGameGuiController;
	private ArrowController arrowController;
	private JSONController jsonController;

	private JSONNode CurrentProgram;

	private int pickMajorIndex;
	private int pickMajorYear;

	private int currentCourseIndex = 0;
	private int currentYearIndex = 0;
	private int completedCourses = 0;

	private string[,] allCourses = new string[3,8];
	private string currentCourse;

	void Start()
	{
		if (ProgramObjectController.program == null) 
		{
			Debug.LogError("Program Not Defined!");
			Debug.Break();
			Application.Quit();
		}
		else
		{
			instantiateControllers ();
			insertCore();
			playerStart();
		}

	}
	void instantiateControllers()
	{
		waypointController = GameObject.FindGameObjectWithTag ("WaypointController").GetComponent<WaypointController>();
		waypointController.SendMessage ("createWaypoints", 40);
		
		guiController = GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIController>();

		inGameGuiController = GameObject.FindGameObjectWithTag ("GUIController").GetComponent<InGameGUIController>();

		Debug.Log (inGameGuiController);

		guiController.setProgram (ProgramObjectController.getProgram(), null);
	}

	void insertCore()
	{
		string[] firstYear = ProgramObjectController.getCoreClasses (1);
		string[] secondYear = ProgramObjectController.getCoreClasses (2);
		string[] thirdYear = ProgramObjectController.getCoreClasses (3);

		for (int i = 0; i<allCourses.GetLength(0); i++) 
		{
			for(int j = 0; j<allCourses.GetLength(1); j++)
			{
				string[] courseName = null;

				if(i == 0)
					courseName = firstYear;
				else if(i == 1)
					courseName = secondYear;
				else if(i == 2)
					courseName = thirdYear;

				if(j < courseName.Length)
				{
					allCourses[i,j] = courseName[j];

					Debug.Log (courseName[j] + " @ " + i + "," + j);
				}
			}
		}
	}

	void playerStart()
	{
		waypointController.SendMessage ("selectWaypoint");

		currentCourseIndex = 0;
		currentYearIndex = 0;
		completedCourses = 0;

		currentCourse = allCourses [0, 0];

		updateGui ();


	}

	void updateGui()
	{
		Waypoint waypoint = waypointController.getSelectedWaypoint();
		guiController.setLocation (waypoint.waypointName);
		guiController.setCourse (currentCourse);
		guiController.setInfo (currentYearIndex + 1,(currentCourseIndex/4 +1), completedCourses);
	}

	void playerAtWaypoint()
	{
		//selectMajor (ProgramObjectController.getMajors () [0]);
		int tmpI = currentYearIndex;
		int tmpJ = currentCourseIndex + 1;

		if(currentCourseIndex >= allCourses.GetLength(1)-1)
		{
			tmpI = currentYearIndex+1;
			tmpJ = 0;
		}

		completedCourses++;
		
		if(completedCourses == allCourses.Length)
			//End Game
			Application.Quit();

		if (allCourses [tmpI, tmpJ] == null)
		{
			inGameGuiController.displayMajorMenu(ProgramObjectController.getMajors(), true);
			return;
		}


		if(currentCourseIndex >= allCourses.GetLength(1)-1)
		{
			currentYearIndex++;
			currentCourseIndex = 0;
		}
		else
			currentCourseIndex++;



		currentCourse = allCourses[currentYearIndex, currentCourseIndex];
		waypointController.SendMessage ("selectWaypoint");
		updateGui();

	}

	void selectMajor(string majorName)
	{
		Debug.Log (majorName);

		ProgramObjectController.setMajor (majorName);

		string[] f = ProgramObjectController.getMajorClasses (1);
		string[] s = ProgramObjectController.getMajorClasses (2);
		string[] t = ProgramObjectController.getMajorClasses (3);

		foreach(string str in f)
		{
			Debug.Log(str);
		}

		int length = f.Length + s.Length + t.Length;
		Debug.Log(length);

		string[] majorCourses = new string[length];

		f.CopyTo (majorCourses, 0);
		s.CopyTo (majorCourses, f.Length);
		t.CopyTo (majorCourses, s.Length);

		int index = 0;

		for (int i = 0; i<allCourses.GetLength(0); i++) 
		{
			for(int j = 0; j<allCourses.GetLength(1); j++)
			{
				if(allCourses[i,j] == null)
				{
					allCourses[i,j] = majorCourses[index];
					index++;
				}

			}
		}
		playerAtWaypoint ();
		guiController.setProgram (ProgramObjectController.getProgram(), majorName);


	}
}

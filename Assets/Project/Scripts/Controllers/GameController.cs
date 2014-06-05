using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameController : MonoBehaviour {

	private WaypointController waypointController;
	private GUIBoxes guiController;
	private InGameGUIController inGameGuiController;
	private ArrowController arrowController;
	private JSONController jsonController;
	private TimingScript timer;

	private int failedGrades = 0;

	private Waypoint currentWaypoint;

	public GameObject Player;

	void Start()
	{
		instantiateControllers ();

		if (ProgramObjectController.program == null) 
		{
			Debug.Log("Program Not Defined!");
			jsonController.setCurrentProgram("1109BIT");

		}

		playerStart();
	}

	void Update()
	{
		getPlayerDist ();
	}


	void instantiateControllers()
	{
		jsonController = new JSONController();


		Player = GameObject.FindGameObjectWithTag ("Player");

		Player.SendMessage ("setCarColor", CarColors.getColor ());

		arrowController = GameObject.FindGameObjectWithTag ("ArrowController").GetComponent<ArrowController>();
		waypointController = GameObject.FindGameObjectWithTag ("WaypointController").GetComponent<WaypointController>();
		
		guiController = GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIBoxes>();

		inGameGuiController = GameObject.FindGameObjectWithTag ("InGameGUIController").GetComponent<InGameGUIController>();

		timer = gameObject.GetComponent<TimingScript>();
	}
	
	void playerStart()
	{
		currentWaypoint = waypointController.selectWaypoint();

		placePlayer ();

		string[] program = ProgramObjectController.getProgram();
		guiController.currentProgram = program[0] + " - " + program[1];
		guiController.currentMajor = "Undeclared";

		Courses.init();

		guiController.newSemester();

		Semester(true);
	}

	void updateGui()
	{
		arrowController.changeTarget(currentWaypoint.transform);

		guiController.currentCourseIndex = Courses.currentCourse;
		Vector2 place = Courses.getYearTime();
		guiController.currentSemester = (int)place.x;
		guiController.currentYear = (int)place.y;
		guiController.currentGPA = GPAScoring.calulateGPA();


		string[] course = Courses.getCurrentCourse();

		guiController.currentCourse = course[0] + " - " + course[1];
		guiController.currentBuilding = currentWaypoint.getName();

		timer.setTime(Vector3.Distance(Player.transform.position, currentWaypoint.transform.position));
		timer.startTimer();

	}

	public void playerAtWaypoint()
	{
		currentWaypoint.selected = false;

		waypointController.selectWaypoint();
		currentWaypoint = waypointController.getSelectedWaypoint ();
		string[] course = Courses.getCurrentCourse();

		if(Courses.currentCourse < 3)
		{
			int grade = getGrade();
			timer.pauseTimer();

			GPAScoring.addScore(Courses.currentCourse,Courses.currentSemseter, grade, course);

			guiController.semesterGrades[Courses.currentCourse] = grade+"";

			Courses.currentCourse++;

			updateGui();
		}
		else
		{
			Semester(false);
		}
	}

	public void playerFailedCourse()
	{
		if(failedGrades < 3)
		{
			currentWaypoint.selected = false;
			
			waypointController.selectWaypoint();

			currentWaypoint = waypointController.getSelectedWaypoint ();
			string[] course = Courses.getCurrentCourse();
			
			if(Courses.currentCourse < 3)
			{
				int grade = 3;
				timer.stopTimer();
				
				GPAScoring.addScore(Courses.currentCourse,Courses.currentSemseter, grade, course);
				
				guiController.semesterGrades[Courses.currentCourse] = "F";
				
				Courses.currentCourse++;
				
				updateGui();
			}
			else
			{
				Semester(false);
			}
			failedGrades++;
		}
		else
		{
			inGameGuiController.currentMenu = 4;
			inGameGuiController.showMenu = true;
		}
	}

	void placePlayer()
	{
		GameObject[] StartingPoints = GameObject.FindGameObjectsWithTag("StartingPoint");

		int index = (int)Random.Range (0, StartingPoints.Length - 1);

		Player.transform.position = StartingPoints [index].transform.position;

		Player.transform.LookAt (currentWaypoint.transform);
	}

	void getPlayerDist(
	{
		float dist = Vector3.Distance(Player.transform.position, currentWaypoint.transform.position);

		if(dist <= 10)
		{
			playerAtWaypoint();
		}

		guiController.setPercent(timer.timePercentage, timer.currentTime);

		if(timer.timePercentage == 0)
		{
			playerFailedCourse();
		}
	}

	void Semester(bool start)
	{
		int menu = (start) ? 0 : 3;
		inGameGuiController.currentMenu = menu;
		inGameGuiController.showMenu = true;
	}

	int getGrade()
	{
		return (int)(Mathf.Ceil((GPAScoring.maxGrade - GPAScoring.minGrade) * timer.timePercentage) + GPAScoring.minGrade);
	}

	public void playerInGame()
	{
		updateGui();
	}

	public void playerPaused()
	{
		timer.pauseTimer();
	}
	public void playerUnPaused()
	{
		timer.startTimer();
	}
}

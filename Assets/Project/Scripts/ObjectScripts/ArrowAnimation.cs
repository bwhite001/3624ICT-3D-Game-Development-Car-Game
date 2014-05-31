using UnityEngine;
using System.Collections;

public class ArrowAnimation : MonoBehaviour {

	public float spinSpeed = 1.0f;
	private Vector3 axis = new Vector3(0,1,0);
	private Vector3 pos1;
	private Vector3 pos2;
	private Vector3 offset;
	public float moveSpeed = 0.05f;
	private Vector3 moveTo;

	// Use this for initialization
	void Start () {
		axis = new Vector3(0,1,0);

		offset = Vector3.down;
		
		pos1 = transform.position;
		
		pos2 = transform.position + offset; 
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(axis * Time.deltaTime * (spinSpeed * 360));

		if(transform.position == pos1)
			
		{
			
			moveTo = pos2;
			
		}
		
		if(transform.position == pos2)
			
		{
			
			moveTo = pos1;
			
		}
		
		transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed);
	}
}

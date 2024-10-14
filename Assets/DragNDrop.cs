using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour {

	public bool FollowMode;
	float radius=.01f;
	public Vector3 InitialPosition;
	
	public Transform x;
	//public Transform Circle;
	
	public int CellsLayerMask = 8;
		
    //RaycastHit hit;
	//Vector3 rayPos;
	//bool enableRay;
		
	Vector3 pos;
	Collider2D coll;
	Cell C;
	
	//public enum Type {red, bloo, green, yello};
	public Cell.Type type;

	public void Start()
	{
		InitialPosition = transform.position;
		CellsLayerMask = 1<<CellsLayerMask;
	}
	
	public void Drag()
	{
		//transform.position = 
	}
	
	void Update()
	{
		//transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		if(FollowMode)
		{
			pos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
			transform.position = new Vector3(pos.x,pos.y,-9);			
		}/*
		if(enableRay)
		{
			Debug.DrawRay(rayPos,new Vector3(0,0,-1) *10);
		}*/
	}
	
	void OnMouseDown()
	{
		FollowMode = true;
	}
	
	
	void OnMouseUp()
	{
		FollowMode = false;
		x.position = new Vector3(transform.position.x,transform.position.y,-13f);
		//Circle.position = new Vector3(transform.position.x,transform.position.y,-2f);
		//Circle.localScale = new Vector3(radius,0.01f,radius);
		coll = Physics2D.OverlapCircle(transform.position,radius,CellsLayerMask);
		if(coll)
		{
			C = coll.GetComponent<Cell>();
			if (C)
				if(C.type != type)
				{
					Debug.Log("coll : "+coll + " | C.type : "+C.type);
					C.StartPlague(C.type,type);
					Manager.OneMove();
				}					
		}	
		/*		
		pos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;	
		rayPos = new Vector3(pos.x,pos.y,10);
		Physics.Raycast(rayPos,new Vector3(0,0,-1)*10,out hit);
		enableRay=true;
		if(hit)
		{
			Debug.Log(hit);
			if(hit.transform.gameObject)
			{
				coll = hit.transform.gameObject.GetComponent<Collider2D>();
				if(coll)
				{
					C = coll.GetComponent<Cell>();
					if (C)
						if(C.type != type)
						{
							Debug.Log("coll : "+coll + " | C.type : "+C.type);
							C.Infect(type);
							Manager.moves++;
						}					
				}
			}
		}
		*/
		transform.position = InitialPosition ;
	}
	/*
	public void Folow()
	{
		FollowMode = true;
	}
	
	public void UnFolow()
	{
		FollowMode = false;
	}*/
}

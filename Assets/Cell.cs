using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    public float InfectionRadius;
    public float Seconds;
	
	public enum Type {red, bloo, green, yello};
	public Type type;	
	//public Type initialType;
	
	public SpriteRenderer sprite;
	public bool thisOne;
	
	public Manager manager;
	
	public Collider2D[] CellsToInfect ;//= new Collider2D[10];
	//int l;
	
	public static int cells=0;
	public static int cellsR=0;
	public static int cellsB=0;
	public static int cellsG=0;
	public static int cellsY=0;
	
	
	public static bool OnGoingPlague;
	public static float WaitTime = 0.15f;
	
	Cell C;
	
	void Awake()
	{
		Cell.cells++;
	}
	
	void Start () 
	{
		//sprite = GetComponent<SpriteRenderer>();
		Infect(type);
		switch(type)
		{
			case Type.red   : cellsR++;  break;
			case Type.bloo  : cellsB++;  break;
			case Type.green : cellsG++;  break;
			case Type.yello : cellsY++;  break;
		}
		manager.UpdateRatios();
	}
	
	// Update is called once per frame
	void Update () 
	{
		{ //Keyboard Controls
		if(Input.GetKeyDown(KeyCode.I) && thisOne)			//InfectNeighbours(Type.green);
		{
			StartPlague(type,Type.green);
			Manager.OneMove();
		}

		if(Input.GetKeyDown(KeyCode.O) && thisOne)//InfectNeighbours(Type.red);	
		{
			StartPlague(type,Type.red);		
			Manager.OneMove();
		}		
	
		if(Input.GetKeyDown(KeyCode.K) && thisOne)//InfectNeighbours(Type.red);
		{
			StartPlague(type,Type.bloo);	
			Manager.OneMove();
		}			
	
		if(Input.GetKeyDown(KeyCode.L) && thisOne)//InfectNeighbours(Type.red);
		{
			StartPlague(type,Type.yello);	
			Manager.OneMove();
		}		
		}
		
	}
	
	public IEnumerator Plague(Type initialType,Type TargetType)
    {
		
		while(OnGoingPlague) 
		yield return new WaitForSeconds(Cell.WaitTime);
		
		Cell.OnGoingPlague = true;
		CellsToInfect = Physics2D.OverlapCircleAll(transform.position, InfectionRadius);
		//l = CellsToInfect.Length;//= Physics2D.OverlapCircleNonAlloc(transform.position, InfectionRadius,CellsToInfect);
		
		//yield return new WaitForSeconds(Seconds);	
	    
		for (int i = 0; i < CellsToInfect.Length; i++)
        {
            C = CellsToInfect[i].gameObject.GetComponent<Cell>();
            if (C != null)
            {
				if(C.type == initialType)
                {
					C.Infect(TargetType);
					C.StartPlague(initialType,TargetType);
					//C.InfectNeighbours(initialType,TargetType);	
				}
            }			
        }
		Cell.OnGoingPlague = false;
		
		yield return new WaitForSeconds(Seconds);
		
    }
	
	public void StartPlague(Type initialType,Type TargetType)
	{
		StartCoroutine(Plague(initialType,TargetType));
		//InfectNeighbours(initialType,TargetType);
	}
	
	public void Infect(Type TargetType)
	{
		//StartCoroutine(Plague());		
		switch(type)
		{
			case Type.red   : cellsR--;  break;
			case Type.bloo  : cellsB--;  break;
			case Type.green : cellsG--;  break;
			case Type.yello : cellsY--;  break;
		}
		type = TargetType;
		switch(type)
		{
			case Type.red   : sprite.color = Color.red;    cellsR++; break;
			case Type.bloo  : sprite.color = Color.blue;   cellsB++; break;
			case Type.green : sprite.color = Color.green;  cellsG++; break;
			case Type.yello : sprite.color = Color.yellow; cellsY++; break;
		}
		manager.UpdateRatios();
	}
	/*
	public void InfectNeighbours(Type initialType,Type TargetType)
	{
		CellsToInfect = Physics2D.OverlapCircleAll(transform.position, InfectionRadius);
		//l = CellsToInfect.Length;//= Physics2D.OverlapCircleNonAlloc(transform.position, InfectionRadius,CellsToInfect);
		
        for (int i = 0; i < CellsToInfect.Length; i++)
        {
            C = CellsToInfect[i].gameObject.GetComponent<Cell>();
            if (C != null)
            {
				if(C.type == initialType)
                {
					C.Infect(TargetType);
					C.InfectNeighbours(initialType,TargetType);
				}
            }			
        }		
	}*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	[Header("Percentages")]
	public Text TextRed;
	public Text TextBlue;
	public Text TextGreen;
	public Text TextYellow;
		
	[Space]
	public Text TipText;
	
	[Space]
	public RectTransform SecondaryNextLevelButton;
	public RectTransform GameOverPanel;
	public Text GameOverText;	
	public Text GameFinishedText;
	
	[Header("Score")]
	public Text MovesText;	
	public static int moves = 0;
	
	[Space]
	public Transform[] Levels;
	public int CurrentLevel = -1;
	
	[Space]
	public bool DebugLevel;
	
	int R,B,G,Y;
	
	static Manager m; //for local static calls
	
	void Awake () 
	{	
		m = gameObject.GetComponent<Manager>();
		NextLevelShit();
	}
	
	void Start () 
	{	
	}
	
	void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
	public void UpdateRatios()
	{
		
		if(Cell.cells!=0)
		{
			R = (100*Cell.cellsR/Cell.cells);
			B = (100*Cell.cellsB/Cell.cells);
			G = (100*Cell.cellsG/Cell.cells);
			Y = (100*Cell.cellsY/Cell.cells);
		}
		else
		{
			R = 0;
			B = 0;
			G = 0;
			Y = 0;
		}
		
		TextRed.text    = R.ToString();
		TextBlue.text   = B.ToString();
		TextGreen.text  = G.ToString();
		TextYellow.text = Y.ToString();
	
		if(!DebugLevel)
		{
			if(R==100)
			{
				GameOverText.text = "Red Dominated Game over";
				GameOverText.color = Color.red;
			GameOverPanel.gameObject.SetActive(true);
			}
			else if(B==100)
			{ 
				GameOverText.text = "Blue Dominated Game over";
				GameOverText.color = Color.blue;
			GameOverPanel.gameObject.SetActive(true);
			}
			else if(G==100)
			{
				GameOverText.text = "Green Dominated Game over";
				GameOverText.color = Color.green;
			GameOverPanel.gameObject.SetActive(true);
			}
			else if(Y==100)
			{
				GameOverText.text = "Yellow Dominated Game over";
				GameOverText.color = Color.yellow;
			GameOverPanel.gameObject.SetActive(true);
			}
			else 
			GameOverText.text = "";			
		}
	}
	
	// Obsolete : use OneMove instead , unless the call is local
	public void UpdateMovesText()
	{
		MovesText.text = "Moves : " + moves.ToString();
	}
	
	public static void OneMove()
	{
		moves++;
		m.UpdateMovesText();
	}
	
	public void NextLevelShit()
	{
		if(CurrentLevel==-1 || CurrentLevel==0)
		{
			DebugLevel=true;
			SecondaryNextLevelButton.gameObject.SetActive(true);
			TipText.gameObject.SetActive(true);
			TipText.text=" Drag and drop a color on a cell to color it \n Press [>] when you're ready";
		}
		else if(CurrentLevel==1)
		{
			DebugLevel=false;
			SecondaryNextLevelButton.gameObject.SetActive(false);
			TipText.gameObject.SetActive(true);
			TipText.text="Now solve the puzzle to get to the next levels! \n Infect all the cells with the same color";
		}
		else
		{
			DebugLevel=false;
			SecondaryNextLevelButton.gameObject.SetActive(false);
			TipText.gameObject.SetActive(false);
			TipText.text="";
		}
		
		if(CurrentLevel != -1)
		Levels[CurrentLevel].gameObject.SetActive(false);
	
		if(CurrentLevel < Levels.Length-1)
		{
			//reset
			GameOverPanel.gameObject.SetActive(false);
			GameOverText.text = "";			
			Cell.cells = 0;	
			Cell.cellsR = 0;	
			Cell.cellsB = 0;	
			Cell.cellsG = 0;	
			Cell.cellsY = 0;
			moves = 0;
			UpdateMovesText();
			//go to next level
			CurrentLevel++;
			Levels[CurrentLevel].gameObject.SetActive(true);
			UpdateRatios();
		}
		else 
		{
			GameFinishedText.gameObject.SetActive(true);
			GameOverPanel.gameObject.SetActive(false);
			GameOverText.text = "";			
		}
	}	
	
}

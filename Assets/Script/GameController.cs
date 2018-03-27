using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static public GameObject ball;
	static public GameObject camera;
	static public int goalsForMatch = 3;

	//Placar do Gol 1
	public GameObject[] goals1;
	public int goals1Count;
	//Placar do Gol 2
	public GameObject[] goals2;
	public int goals2Count;

	public void addGoalOn(int id){
		if (id == 1)
			goals1 [goals1Count++].SetActive (true);
		
		if (id == 2)
			goals2 [goals2Count++].SetActive (true);

		if (goals1Count >= goalsForMatch || goals2Count >= goalsForMatch)
			resetGoalsCount ();
	}

	public void resetGoalsCount(){
		goals1Count = goals2Count = 0;
		int i = -1;
		while (++i<goals1.Length) {
			goals1[i].SetActive(false);
			goals2[i].SetActive(false);
		}
	}

	void Start () {
		ball = GameObject.Find ("Ball");
		camera = GameObject.Find ("Camera");
		resetGoalsCount ();
	}


	void Update () {
	

	}
}

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static public GameObject ball;
	static public GameObject field;
	static public GameObject goal1;
	static public GameObject goal2;

	static public byte bitTeamA = 0x00;

	//Registra a quantidade de gols por partida
	static public int goalsForMatch = 3;
	//Registra a quantidade de movimentos para cada turno
	static public int movementsForTurn = 3;

	//Controla se a vez de jogar: time 1 ou time 2
	static public bool isTurnOfTeam1 = false;
	//Controla permissao para movimentar alguma peça
	static public bool ishalted = false;

	//Registra o numero de movimentos de um time por vez
	static private int movementCount = 0;
	//TODO //Refatorar este codigo
	static public int MovementCount {
		get { return movementCount; }
		set { movementCount = value;
			  if (movementCount>=movementsForTurn){
			  	 movementCount=0;
				 isTurnOfTeam1 = !isTurnOfTeam1;
			  }
			}
	}

	//Exibe a seta que indica o sentido da jogada
	static public IEnumerator showArrowTurn(  ){
		if(MovementCount==0){
			ishalted = true;
			yield return new WaitForSeconds (1.00f);
			while(ball.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
				yield return new WaitForSeconds (0.50f);

			Instantiate (Resources.Load<GameObject> ("Prefab/ArrowTurn"), (Vector2)Camera.main.transform.position, Quaternion.identity);
			ishalted = false;
		}
	}

	//Registra as posiçoes iniciais de cada peça do time
	public GameObject[] team1Pieces;
	public Vector2[] team1Position;
	public GameObject[] team2Pieces;
	public Vector2[] team2Position;

	//Placar do Gol 1
	public GameObject[] goals1;
	public int goals1Count;
	//Placar do Gol 2
	public GameObject[] goals2;
	public int goals2Count;

	//Setas de orietaçao para o turno de cada time
	public GameObject arrowTurn;

	public void addGoalOn(int id){
		if (id == 1) {
			//Exibe um score a mais
			goals1 [goals1Count++].SetActive (true);
			///Habilita o turno do time 1
			isTurnOfTeam1 = true;
		}
		if (id == 2) {
			//Exibe um score a mais
			goals2 [goals2Count++].SetActive (true);
			///Habilita o turno do time 2
			isTurnOfTeam1 = false;
		}
		movementCount = 0;

		GetComponent<CameraControl>(). focusOn (3);

		//Se a partida encerrar por numero de gols
		if (goals1Count >= goalsForMatch || goals2Count >= goalsForMatch) {
			resetGoalsCount ();
		}


		//Organizar as peças
		int i = -1;
		while (++i<team1Pieces.Length) team1Pieces[i].transform.position = team1Position[i];
		i = -1;
		while (++i<team2Pieces.Length) team2Pieces[i].transform.position = team2Position[i];
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
		ball  = GameObject.Find ("Ball");
		field = GameObject.Find ("Field");
		goal1 = GameObject.Find ("Goal1");
		goal2 = GameObject.Find ("Goal2");
		resetGoalsCount ();

	}

}

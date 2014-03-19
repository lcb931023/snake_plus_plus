/***************GameManager**************
 * Keeps track of game objects: 
 * food, worm, player's snake
 ***************************************/

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	/****Public variables: assigned in Unity****/
	public float leaderFollowWt;
	public float leaderFollowDistance;
	
	public float wanderWt;
	public float wanderRadius;//of the constraining sphere
	public float wanderDistance;//between constrain sphere and agent
	public float wanderJitter;//random displacement each update
	
	public float fleeWt;
	public float fleeRadius;
	
	public float stayInBoundWt;
	
	//The centre of active area and area's radius
	public Vector3 areaCentre;
	public float areaRadius;
	
	//object amount management
	public int numberOfFood;
	public int numberOfWorm;
	public int numberOfInitSnakeBody;
	//object prefab management
	public Object foodPrefab;
	public Object wormPrefab;
	public Object snakeBodyPrefab;
	public GameObject snakeHead;
	
	/****Private variables****/
	//Array for food
	private GameObject[] foods;
	public GameObject[] Foods {get{return foods;}}
	//Array for worms
	private GameObject[] worms;
	public GameObject[] Worms {get{return worms;}}
	//Array for snake body
	private GameObject[] snakeParts;
	public GameObject[] SnakeParts {get{return snakeParts;}}
	
	//make the flockmanager accessible to other scripts
	private static GameManager thisClass;
	public static GameManager ThisClass {get{return thisClass;}}

	void Start () {
		thisClass = this;//make the flockmanager accessible to other scripts blahblah
		
		//create worms and food based on values from Unity
		initiateFood();
		initiateWorm();
		initiateSnake();
	}
	
	void Update () {
	}
	
	private void initiateFood(){
		foods = new GameObject[numberOfFood];
		for(int i=0; i < numberOfFood;i++){
			Vector3 foodPos = new Vector3(
				Random.Range(areaCentre.x-areaRadius, areaCentre.x+areaRadius),
				Random.Range(areaCentre.y-areaRadius, areaCentre.y+areaRadius),
				Random.Range(areaCentre.z-areaRadius, areaCentre.z+areaRadius));
			foods[i] = (GameObject)Instantiate( foodPrefab,
												foodPos,
												Quaternion.identity);
		}
	}
	
	private void initiateWorm(){
		worms = new GameObject[numberOfWorm];
		//SnakeBodySteering sBS;
		for(int i=0; i < numberOfWorm;i++){
			Vector3 wormPos = new Vector3(
				Random.Range(areaCentre.x-areaRadius, areaCentre.x+areaRadius),
				Random.Range(areaCentre.y-areaRadius, areaCentre.y+areaRadius),
				Random.Range(areaCentre.z-areaRadius, areaCentre.z+areaRadius));
			worms[i] = (GameObject)Instantiate(wormPrefab,
													wormPos,
													Random.rotation);
			//sBS = worms[i].GetComponent<SnakeBodySteering>();
			//sBS.OwnIndex = i;
		}
	}
	
	private void initiateSnake(){
		snakeParts = new GameObject[numberOfInitSnakeBody];
		SnakeBodySteering sBS;
		for(int i=0; i < numberOfInitSnakeBody;i++){
			Vector3 snakeBodyPos = new Vector3(0f,0f,- i * 1.6f);
			snakeParts[i] = (GameObject)Instantiate(snakeBodyPrefab,
													snakeBodyPos,
													Quaternion.identity);
			sBS = snakeParts[i].GetComponent<SnakeBodySteering>();
			sBS.OwnIndex = i;
		}
	}
	
}

/***************WormSteering***************
 * Wandering
 * fleeing/evading the snake head when close
 **************************************/
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Steering))]

public class WormSteering : MonoBehaviour {
	//Pre-requisite
	private CharacterController characterController;
	private Steering steering;
	private GameManager gameManager;
	//reference to self in gm's array
	private int ownIndex = -1;
	public int OwnIndex { get { return ownIndex; } set { ownIndex = value; }}
	//steering calculation
	private Vector3 moveDirection;
	private Vector3 steeringForce;
	//wander's initializatio nand initial direction
	private Vector3 wanderRadius;
	// Use this for initialization
	void Start () {
		characterController = gameObject.GetComponent<CharacterController>();
		steering = gameObject.GetComponent<Steering>();
		gameManager = GameManager.ThisClass;
		//initialize wander
		wanderRadius = this.transform.forward;
		wanderRadius *= gameManager.wanderRadius;
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForce ();
		ClampSteering ();
		moveDirection = transform.forward * steering.Speed;
		// movedirection equals velocity
		//add acceleration
		moveDirection += steeringForce * Time.deltaTime;
		//update speed
		steering.Speed = moveDirection.magnitude;
		if (steering.Speed != moveDirection.magnitude) {
			moveDirection = moveDirection.normalized * steering.Speed;
		}
		//orient transform
		if (moveDirection != Vector3.zero)
			transform.forward = moveDirection;
		// the CharacterController moves us subject to physical constraints
		characterController.Move (moveDirection * Time.deltaTime);
		
		detectCollision();
		
	}
	private void CalcSteeringForce ()//add steering behaviors here
	{
		steeringForce = Vector3.zero;
		//proritize fleeing snakeHead than other two behaviors
		Vector3 fleeSnake = gameManager.fleeWt * FleeSnake(gameManager.snakeHead.transform.position, gameManager.fleeRadius);
		if(fleeSnake == Vector3.zero){
			steeringForce += gameManager.wanderWt * Wander(gameManager.wanderDistance, gameManager.wanderJitter);
			steeringForce += gameManager.stayInBoundWt * steering.StayInBound(gameManager.areaCentre, gameManager.areaRadius);
			steering.maxSpeed = 5;
		}else{
			steering.maxSpeed = 15;
			steeringForce += fleeSnake;
		}
	}
	private void ClampSteering ()
	{
		if (steeringForce.magnitude > steering.maxForce) {
			steeringForce.Normalize ();
			steeringForce *= steering.maxForce;
		}
	}
	
	public Vector3 Wander (float wanderDistance, float wanderJitter)
	{
		//find dv, desired velocity
		Vector3 dv = Vector3.zero;
		wanderRadius = Quaternion.AngleAxis(Random.Range(-wanderJitter,wanderJitter),transform.up) * wanderRadius;
		wanderRadius = Quaternion.AngleAxis(Random.Range(-wanderJitter,wanderJitter),transform.right) * wanderRadius;
		wanderRadius = Quaternion.AngleAxis(Random.Range(-wanderJitter,wanderJitter),transform.forward) * wanderRadius;
		Vector3 sphereDist = transform.forward * wanderDistance;
		dv = sphereDist + wanderRadius;
		return dv;
	}
	
	public Vector3 FleeSnake (Vector3 snakeHeadPos,float fleeRadius)
	{
		if(Vector3.Distance(transform.position, snakeHeadPos) < fleeRadius){
			return steering.Flee (snakeHeadPos);
		}else{
			return Vector3.zero;
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Snake Body" || hit.gameObject.tag == "Snake Head"){
			Destroy(gameObject);
		}
	}
	
	private void detectCollision(){//detect collision with all snake bodyparts
		for(int i=0; i < gameManager.numberOfInitSnakeBody;i++){
			Vector3 bodyPos = gameManager.SnakeParts[i].transform.position;
			if (Vector3.Distance (transform.position, bodyPos) < 4){
				Destroy(gameObject);
			}
		}
	}
	
	//debug
	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(transform.position, transform.position + steeringForce);
	}
}

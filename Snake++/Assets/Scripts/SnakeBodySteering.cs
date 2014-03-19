/***************SnakeBodySteering***************
 * Leader following one by one
 * keep distance with the one in front and the one in the back (by arriving the tail of the one in front)
 **************************************/
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Steering))]
public class SnakeBodySteering : MonoBehaviour {
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
	// Use this for initialization
	void Start () {
		characterController = gameObject.GetComponent<CharacterController>();
		steering = gameObject.GetComponent<Steering>();
		gameManager = GameManager.ThisClass;
		Physics.IgnoreLayerCollision(8,8);
	}
	
	// Update is called once per frame
	public void Update ()
	{
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
	}
	private void CalcSteeringForce ()//add steering behaviors here
	{
		steeringForce = Vector3.zero;
		//snake leader-following
		if(ownIndex>0){
			steeringForce += gameManager.leaderFollowWt * leaderFollowing (gameManager.SnakeParts[ownIndex-1]);
		}else{
			steeringForce += gameManager.leaderFollowWt * leaderFollowing (gameManager.snakeHead);
		}
	}
	private void ClampSteering ()
	{
		if (steeringForce.magnitude > steering.maxForce) {
			steeringForce.Normalize ();
			steeringForce *= steering.maxForce;
		}
	}
	private Vector3 leaderFollowing (GameObject target){
		return steering.SnakeFollow(target,gameManager.leaderFollowDistance);
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Snake Head" && ownIndex > 4){
			Destroy(hit.gameObject);
		}
		if (hit.gameObject.tag == "Worm"){
			Destroy(hit.gameObject);
		}
	}
	
	//debug
	/*void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(transform.position, transform.position + steeringForce);
	}*/
}

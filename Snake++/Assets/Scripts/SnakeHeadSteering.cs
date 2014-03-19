/*     TODO
	Figure out the difference between different rotations, and how to do it locally
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]


public class SnakeHeadSteering : MonoBehaviour {
	
	public float moveSpeed;
	
	private Vector3 moveDirection;
	private CharacterController characterController;
	
	
	// The rotation factor, this will control the speed we rotate at.
	public float rotationSensitvity = 500.0f;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;//Hide the mouse
		moveDirection = transform.forward;
		
		characterController = gameObject.GetComponent<CharacterController>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		moveDirection = transform.forward * moveSpeed;
		//and that's all you need to make the snake head go with your mouse's direction.
		//boom. done. go home now.
		//HOWEVER things get a little complicated when it's not the camera that's 
		//getting moved by mouse, but 
		//the object camera follows
		
		steerWithMouse();
		
		characterController.Move(moveDirection * Time.deltaTime);//makes the head move forward
	
	}
	
	//calculate mouse rotation on each timestamp
	void steerWithMouse(){
		//the mouse x will apply to rotation around y axis, while 
		//the mouse y will apply to rotation around x axis
		//z is buying soysauce, so he has no business here
		
		Vector3 mouseChange 
			= new Vector3(
				Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSensitvity * -1.0f,//the mouse y has reversed factor upon rotation around x axis (up and down). Need further study
				Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensitvity,
				0.0f
			);
	
		
		//lad and gents, take note, this is how you steer in space. 
		//In order to rotate to the right rotation axis reference, we specitfy using Space.self
		transform.Rotate(mouseChange, Space.Self);
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Food" || hit.gameObject.tag == "Worm"){
			Destroy(hit.gameObject);
		}
		
	}
	
	
}

using UnityEngine;
using System.Collections;

public class snakeBodyRotate : MonoBehaviour {
	
	private float rotateAmt;
	// Use this for initialization
	void Start () {
		int flipACoin = Random.Range(0,2) * 2 - 1;
		rotateAmt = Random.Range(10.0f,20.0f) * flipACoin;
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.Rotate(this.transform.forward, rotateAmt * Time.deltaTime);
	}
}

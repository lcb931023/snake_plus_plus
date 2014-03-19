using UnityEngine;
using System.Collections;

public class WeightControl : MonoBehaviour {
	
	private GameManager gameManager;
	
	public void Start(){
		gameManager = GetComponent<GameManager> ();
		
	}
	
	void OnGUI () {
		// snake body following
		gameManager.leaderFollowWt = GUI.HorizontalSlider(new Rect(25,25,200,10),gameManager.leaderFollowWt,0.0f,10.0f);
		gameManager.leaderFollowDistance = GUI.HorizontalSlider(new Rect(25,50,200,10),gameManager.leaderFollowDistance,0.0f,10.0f);
		gameManager.wanderRadius = GUI.HorizontalSlider(new Rect(25,75,200,10),gameManager.wanderRadius,0.0f,10.0f);
		gameManager.wanderDistance = GUI.HorizontalSlider(new Rect(25,100,200,10),gameManager.wanderDistance,0.0f,10.0f);
		gameManager.wanderJitter = GUI.HorizontalSlider(new Rect(25,125,200,10),gameManager.wanderJitter,0.0f,10.0f);
	}
	
}
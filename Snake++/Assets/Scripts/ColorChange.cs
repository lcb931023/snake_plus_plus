using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {
	
	public bool changeColor = false;
	
	public float changeColorSpeed = 0.05f;
	
	private Color 	red  = new Color(1F, 0.3f, 0.3f),//r, g, b
					pink = new Color(1F, 0.3f, 1F),
					blue = new Color(0.3f, 0.3f, 1F),
					cyan = new Color(0.3f, 1F, 1F),
					green= new Color(0.3f, 1F, 0.3f),
					yellow=new Color(1F, 1F, 0.3f);
	
	private Color newColor = new Color();
	
	// Use this for initialization
	void Start () {
		Color[] randomColor = new Color[] {red,pink,blue,cyan,green,yellow};
		
		newColor = randomColor[Random.Range(0,6)];
		
		renderer.material.color = newColor;
	}
	
	// Update is called once per frame
	void Update () {
		if(changeColor){
			if (//red => pink
				newColor.r == 1F &&
				newColor.g <= 0.3f &&
				newColor.b < 1F
				){
				newColor.g = 0.3f;
				newColor.b += changeColorSpeed;
			}
			if  (//pink => blue
				newColor.r > 0.3f &&
				newColor.g == 0.3f &&
				newColor.b >= 1F
				){
				newColor.b = 1F;
				newColor.r -= changeColorSpeed;
			}
			if (//blue => cyan
				newColor.r <= 0.3f &&
				newColor.g < 1F &&
				newColor.b == 1F
				){
				newColor.r = 0.3f;
				newColor.g += changeColorSpeed;
			}
			if (//cyan => green
				newColor.r == 0.3f &&
				newColor.g >= 1F &&
				newColor.b > 0.3f
				){
				newColor.g = 1F;
				newColor.b -= changeColorSpeed;
			}
			if (//green => yellow
				newColor.r < 1F &&
				newColor.g == 1F &&
				newColor.b <= 0.3f
				){
				newColor.b = 0.3f;
				newColor.r += changeColorSpeed;
			}
			if (//yellow => red
				newColor.r >= 1F &&
				newColor.g > 0.3f &&
				newColor.b == 0.3f
				){
				newColor.r = 1f;
				newColor.g -= changeColorSpeed;
			}
			
			
			
			renderer.material.color = newColor;
		}
	}
}

using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {
    float timeRemaining = 10;
    public GUIStyle style;
	// Use this for initialization
	void Start () {
        style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
	}
    void OnGUI() {
        if (timeRemaining > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 80, 200, 100), "Time remaining: " + (int)timeRemaining, style);
        }
        else {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100), "Time's up! Sorry!", style);
            //Application.LoadLevel("GameOverScene"); 
        }
    }
}

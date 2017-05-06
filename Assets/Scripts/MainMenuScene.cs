using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{

	void Start ()
	{
		string[] buttonNames = new string[] {
			"ButtonNew",
			"ButtonLoad",
			"ButtonExit",
		};

		foreach (string buttonName in buttonNames) {
			GameObject buttonObject = GameObject.Find (buttonName);
			Button button = buttonObject.GetComponent<Button> ();
			button.onClick.AddListener (delegate() {
				OnClick (buttonObject);
			});
		}
	}

	void OnClick (GameObject sender)
	{
		switch (sender.name) {
		case "ButtonNew":
			Application.LoadLevel("MainScene");
			break;
		case "ButtonLoad":
			Debug.Log ("Sorry, Load is not avilable for now.");
			break;
		case "ButtonExit":
			Application.Quit();
			break;
		default:
			Debug.Log ("Name Error.");
			break;
		}
	}

	void Update ()
	{
		
	}
}

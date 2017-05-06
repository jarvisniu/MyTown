using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {
	public int selectIndex = 0;
	private int lastIndex = 0;
	Vector2 lastScreenSize;
	RectTransform toolbetRectTransform;
	RectTransform toolselRectTransform;

	// Use this for initialization
	void Start () {
		lastScreenSize = new Vector2 (Screen.width, Screen.height);
		toolbetRectTransform = GameObject.Find ("Toolbet Background").GetComponent<RectTransform>();
		toolselRectTransform = GameObject.Find ("ToolSelectBorder").GetComponent<RectTransform>();

		refresh ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastScreenSize.x != Screen.width || lastScreenSize.y != Screen.height || lastIndex != selectIndex) {
			refresh();
			lastScreenSize = new Vector2 (Screen.width, Screen.height);
			lastIndex = selectIndex;
		}

		 // Mouse wheel
		float mouseWheelDelta = Input.GetAxis ("Mouse ScrollWheel");
		selectIndex -= (int)(mouseWheelDelta * 10);
		while (selectIndex < 0)
			selectIndex += 9;
		while (selectIndex > 8)
			selectIndex -= 9;
		
		// Exit to main menu
		if (Input.GetKey (KeyCode.Escape))
			ExitToMainmenu ();

		// 1-9
		if (Input.GetKey (KeyCode.Alpha1))
			selectIndex = 0;
		else if (Input.GetKey (KeyCode.Alpha2))
			selectIndex = 1;
		else if (Input.GetKey (KeyCode.Alpha3))
			selectIndex = 2;
		else if (Input.GetKey (KeyCode.Alpha4))
			selectIndex = 3;
		else if (Input.GetKey (KeyCode.Alpha5))
			selectIndex = 4;
		else if (Input.GetKey (KeyCode.Alpha6))
			selectIndex = 5;
		else if (Input.GetKey (KeyCode.Alpha7))
			selectIndex = 6;
		else if (Input.GetKey (KeyCode.Alpha8))
			selectIndex = 7;
		else if (Input.GetKey (KeyCode.Alpha9))
			selectIndex = 8;
	}

	void ExitToMainmenu() {
		Cursor.visible = true;
		Application.LoadLevel ("MainMenu");
	}

	void refresh() {
		toolbetRectTransform.position = new Vector3 (Screen.width / 2, toolbetRectTransform.rect.height / 2, 0);
		toolselRectTransform.position = new Vector3 (Screen.width / 2 + 60 * (selectIndex - 4), toolselRectTransform.rect.height / 2 - 3, 0);
	}
}

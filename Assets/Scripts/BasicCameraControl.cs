using UnityEngine;
using System.Collections;

public class BasicCameraControl : MonoBehaviour
{
	float yaw = 0;         // 偏航角，绕Y轴旋转的角度
	float pitch = 0;       // 俯仰角，绕Z轴旋转的角度
	const float roll = 0;  // 翻滚角，绕X轴旋转的角度

	void FixedUpdate ()
	{
		// Move forward or backward
		if (Input.GetKey (KeyCode.W)) 
			transform.position += Vector3.ClampMagnitude (transform.forward, 0.1f);
		else if (Input.GetKey (KeyCode.S)) 
			transform.position -= Vector3.ClampMagnitude (transform.forward, 0.1f);

		// Move left or right
		if (Input.GetKey (KeyCode.A)) 
			transform.position -= Vector3.ClampMagnitude (transform.right, 0.1f);
		else if (Input.GetKey (KeyCode.D)) 
			transform.position += Vector3.ClampMagnitude (transform.right, 0.1f);

		// Move up or down
		if (Input.GetKey (KeyCode.Space)) 
			transform.position += new Vector3 (0, 0.1f, 0);
		else if (Input.GetKey (KeyCode.LeftShift)) 
			transform.position -= new Vector3 (0, 0.1f, 0);

		// Rotate
		pitch -= Input.GetAxis ("Mouse Y") * 5;
		pitch = Mathf.Clamp (pitch, -90, 90);
		yaw += Input.GetAxis ("Mouse X") * 5;
		transform.rotation = Quaternion.Euler (pitch, yaw, roll);
	}
}

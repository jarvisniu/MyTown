using UnityEngine;
using System.Collections;

public class AddBlock : MonoBehaviour
{
	public Vector3 targetAddPos = new Vector3(0, 0, 0);
	public Vector3 targetRemovePos = new Vector3(0, 0, 0);

	GameObject placeholderBlock;
	BlockManager blockManager;
	Gui gui;

	Vector3 normalX, normalY, normalZ;

	void Start ()
	{
		Cursor.visible = false;

		normalX = new Vector3(1, 0, 0);
		normalY = new Vector3(0, 1, 0);
		normalZ = new Vector3(0, 0, 1);

		gui = GameObject.Find ("Game Logic").GetComponent<Gui> ();
		blockManager = GameObject.Find ("Game Logic").GetComponent<BlockManager> ();
		placeholderBlock = GameObject.Find ("Place Block Indicator");
	}


	void Update ()
	{
		refreshEditBlock();
		placeholderBlock.transform.position = targetAddPos;

		// Place or remove block
		if (Input.GetMouseButtonDown (0)) {
			blockManager.Remove(targetRemovePos);
		} else if (Input.GetMouseButtonDown (1)) {
			blockManager.Add(targetAddPos, gui.selectIndex);
		}

	}

	Vector3 getTargetPosInCenter() {
		var screenCenterPos = new Vector3 (Screen.width / 2, Screen.height / 2, 4);
		var cubeCenterPos = Camera.main.ScreenToWorldPoint (screenCenterPos);
		cubeCenterPos = new Vector3 (
			Mathf.Round (cubeCenterPos.x),
			Mathf.Round (cubeCenterPos.y),
			Mathf.Round (cubeCenterPos.z));
		return cubeCenterPos;
	}

	void refreshEditBlock() {
		float REACH_DISTANCE = 8;
		var screenCenterPos = new Vector3 (Screen.width / 2, Screen.height / 2, 4);
		var standPos = roundVec3(Camera.main.transform.position);
		var forward = Camera.main.transform.forward;
		var direction = new Vector3(
			Mathf.Sign(forward.x),
			Mathf.Sign(forward.y),
			Mathf.Sign(forward.z));
		var ray = Camera.main.ScreenPointToRay(screenCenterPos);

		float rayDistance;
		float tmpDistance = 0;
		Vector3 targetPos = new Vector3(0, 0, 0);
		Vector3 backPos = new Vector3(0, 0, 0);
		Vector3 nearestTarget = new Vector3(0, 0, 0);
		Vector3 nearestBack = new Vector3(0, 0, 0);
		float nearestDistance = 1000;

		for (var i = 0; i < 5; i ++) {
			var planeX = new Plane(normalX, standPos + new Vector3(direction.x * (i + 0.5f), 0, 0));
			planeX.Raycast(ray, out rayDistance);
			if (rayDistance < REACH_DISTANCE) {
				targetPos = ray.GetPoint(rayDistance) - new Vector3(direction.x * 0.5f, 0, 0);
				targetPos = roundVec3(targetPos);
				if (!blockManager.hasBlock(targetPos)) {
					backPos = targetPos + new Vector3(direction.x, 0, 0);
					tmpDistance = (targetPos - standPos).magnitude;
					if (blockManager.hasBlock(backPos) && tmpDistance < nearestDistance) {
						nearestTarget = targetPos;
						nearestBack = backPos;
						nearestDistance = tmpDistance;
					}
				}
			}

			var planeY = new Plane(normalY, standPos + new Vector3(0, direction.y * (i + 0.5f), 0));
			planeY.Raycast(ray, out rayDistance);
			if (rayDistance < REACH_DISTANCE) {
				targetPos = ray.GetPoint(rayDistance) - new Vector3(0, direction.y * 0.5f, 0);
				targetPos = roundVec3(targetPos);
				if (!blockManager.hasBlock(targetPos)) {
					backPos = targetPos + new Vector3(0, direction.y, 0);
					tmpDistance = (targetPos - standPos).magnitude;
					if (blockManager.hasBlock(backPos) && tmpDistance < nearestDistance) {
						nearestTarget = targetPos;
						nearestBack = backPos;
						nearestDistance = tmpDistance;
					};
				}
			}

			var planeZ = new Plane(normalZ, standPos + new Vector3(0, 0, direction.z * (i + 0.5f)));
			planeZ.Raycast(ray, out rayDistance);
			if (rayDistance < REACH_DISTANCE) {
				targetPos = ray.GetPoint(rayDistance) - new Vector3(0, 0, direction.z * 0.5f);
				targetPos = roundVec3(targetPos);
				if (!blockManager.hasBlock(targetPos)) {
					backPos = targetPos + new Vector3(0, 0, direction.z);
					tmpDistance = (targetPos - standPos).magnitude;
					if (blockManager.hasBlock(backPos) && tmpDistance < nearestDistance) {
						nearestTarget = targetPos;
						nearestBack = backPos;
						nearestDistance = tmpDistance;
					};
				}
			}
		}

		targetAddPos = nearestTarget;
		targetRemovePos = nearestBack;
	}

	Vector3 roundVec3(Vector3 vec) {
		return new Vector3 (Mathf.Round (vec.x), Mathf.Round (vec.y), Mathf.Round (vec.z));
	}
}

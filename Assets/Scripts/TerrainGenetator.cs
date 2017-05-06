using UnityEngine;
using System.Collections;

public class TerrainGenetator : MonoBehaviour
{
	BlockManager blockManager;
	bool terrainGenerated = false;
	int range = 24;

	void Start ()
	{
		blockManager = GameObject.Find ("Game Logic").GetComponent<BlockManager> ();
	}

	void Update ()
	{
		if (terrainGenerated == false) {
			int x = 0, y = 0, z = 0;
			blockManager.Add(x, -1, z, 2);
			for (var i = -range; i < range; i += 2) {
				for (var j = -range; j < range; j++) {
					x = i + j % 2;
					y = 0;
					z = j;
					blockManager.Add(y, x, z, 8);
				}
			}
			for (var i = -range; i < range; i += 2) {
				for (var j = -range; j < range; j++) {
					x = i + j % 2;
					y = 0;
					z = j;
					blockManager.Add(z, x, y, 8);
				}
			}
			for (var i = -range; i < range; i += 2) {
				for (var j = -range; j < range; j++) {
					x = i + j % 2;
					y = 0;
					z = j;
					blockManager.Add(z, y, x, 8);
				}
			}
			terrainGenerated = true;
		}
	}
}

using UnityEngine;
using System.Collections;

// 所有块体的添加都通过这里进行
public class BlockManager : MonoBehaviour
{

	bool[,,] blockStates;
	GameObject[,,] blockObjects;
	TextureManager textureManager;

	void Start ()
	{
		blockStates = new bool[200, 200, 200];
		blockObjects = new GameObject[200, 200, 200];

		textureManager = GameObject.Find ("Game Logic").GetComponent<TextureManager> ();
	}

	void Update ()
	{
	
	}

	public void Add (Vector3 pos, int blockTypeId)
	{
		Add (Mathf.RoundToInt (pos.x), Mathf.RoundToInt (pos.y), Mathf.RoundToInt (pos.z), blockTypeId);
	}

	public void Add (int x, int y, int z, int blockTypeId)
	{
		if (getBlockState (x, y, z) == false) {
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.position = new Vector3 (x, y, z);
			cube.GetComponent<MeshRenderer> ().material.mainTexture = textureManager.getTexture (blockTypeId);

			
			if (blockTypeId == 4)
				TextureManager.SetMaterialRenderingMode (cube.GetComponent<MeshRenderer> ().material, TextureManager.RenderingMode.Transparent);
//			else
//				TextureManager.SetMaterialRenderingMode (cube.GetComponent<MeshRenderer> ().material, TextureManager.RenderingMode.Opaque);
			
			setBlockObject(x, y, z, cube);
			setBlockState (x, y, z, true);
		} else {
			Debug.Log ("Block [" + x + ", " + y + ", " + z + "] is ocupied.");
		}
	}

	public void Remove (Vector3 pos)
	{
		Remove (Mathf.RoundToInt (pos.x), Mathf.RoundToInt (pos.y), Mathf.RoundToInt (pos.z));
	}

	public void Remove(int x, int y, int z) {
		GameObject.Destroy (getBlockObject (x, y, z));
		setBlockObject(x, y, z, null);
		setBlockState (x, y, z, false);
	}
	
	bool getBlockState (int x, int y, int z)
	{
		return blockStates [x + 100, y + 100, z + 100];
	}
	
	void setBlockState (int x, int y, int z, bool state)
	{
		blockStates [x + 100, y + 100, z + 100] = state;
	}

	public bool hasBlock(Vector3 pos) {
		if (Mathf.Abs(pos.x) > 99 || Mathf.Abs(pos.y) > 99 || Mathf.Abs(pos.z) > 99 ) return false;
		
		return getBlockState(
			Mathf.RoundToInt(pos.x),
			Mathf.RoundToInt(pos.y),
			Mathf.RoundToInt(pos.z));
	}
	
	GameObject getBlockObject (int x, int y, int z)
	{
		return blockObjects [x + 100, y + 100, z + 100];
	}
	
	void setBlockObject (int x, int y, int z, GameObject obj)
	{
		blockObjects [x + 100, y + 100, z + 100] = obj;
	}
}

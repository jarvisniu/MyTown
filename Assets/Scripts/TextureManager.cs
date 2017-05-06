using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class TextureManager : MonoBehaviour
{
	String[] textureFilenames;
	Texture[] textures;

	void Start ()
	{
		textureFilenames = new String[]{
			"brick.png",
			"cobblestone.png",
			"diamond_block.png",
			"diamond_ore.png",
			"glass_green.png",
			"grass_side.png",
			"hardened_clay_stained_pink.png",
			"planks_oak.png",
			"stonebrick.png",
		};
		textures = new Texture[9];

		for (int i = 0; i < textureFilenames.Length; i ++) {
			textures [i] = loadTexture (textureFilenames [i]);
		}

	}
	
	void Update ()
	{
		
	}

	public Texture getTexture(int id) {
		return textures [id];
	}

	Texture2D loadTexture (string filename)
	{
		Texture2D texture = new Texture2D (16, 16);
		byte[] pngBytes = File.ReadAllBytes (Application.streamingAssetsPath + "\\" + filename);
		texture.LoadImage (pngBytes);
		texture.filterMode = FilterMode.Point;
		return texture;
	}
	
	public enum RenderingMode
	{
		Opaque,
		Cutout,
		Fade,
		Transparent,
	}
	
	public static void SetMaterialRenderingMode (Material material, RenderingMode renderingMode)
	{
		switch (renderingMode) {
		case RenderingMode.Opaque:
			material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			material.SetInt ("_ZWrite", 1);
			material.DisableKeyword ("_ALPHATEST_ON");
			material.DisableKeyword ("_ALPHABLEND_ON");
			material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = -1;
			break;
		case RenderingMode.Cutout:
			material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			material.SetInt ("_ZWrite", 1);
			material.EnableKeyword ("_ALPHATEST_ON");
			material.DisableKeyword ("_ALPHABLEND_ON");
			material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 2450;
			break;
		case RenderingMode.Fade:
			material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetInt ("_ZWrite", 0);
			material.DisableKeyword ("_ALPHATEST_ON");
			material.EnableKeyword ("_ALPHABLEND_ON");
			material.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 3000;
			break;
		case RenderingMode.Transparent:
			material.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			material.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetInt ("_ZWrite", 0);
			material.DisableKeyword ("_ALPHATEST_ON");
			material.DisableKeyword ("_ALPHABLEND_ON");
			material.EnableKeyword ("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 3000;
			break;
		}
	}
}

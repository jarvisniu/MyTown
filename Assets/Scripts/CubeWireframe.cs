using UnityEngine;
using System.Collections;
 
public class CubeWireframe : MonoBehaviour
{
	public Color lineColor = new Color (0.0f, 1.0f, 1.0f);

	float size = 1;

	Material lineMaterial ;
	float hsize;
     
	void Start ()
	{
		hsize = size / 2;
		//lineMaterial = new Material (Shader.Find ("Lines/Cube Border"));
		lineMaterial = new Material ("Shader \"Lines/Colored Blended\" {" +
		                             "SubShader { Pass {" +
		                             "   BindChannels { Bind \"Color\",color }" +
		                             "   Blend SrcAlpha OneMinusSrcAlpha" +
		                             "   ZWrite on Cull Off Fog { Mode Off }" +
		                             "} } }");
		//lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		//lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}

	void OnRenderObject ()
	{
		lineMaterial.SetPass (0);
             
		GL.Begin (GL.LINES);
		GL.Color (lineColor);

		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		Vector3[] vetices = {
			new Vector3 (x - hsize, y - hsize, z - hsize),
			new Vector3 (x + hsize, y - hsize, z - hsize),
			new Vector3 (x + hsize, y - hsize, z + hsize),
			new Vector3 (x - hsize, y - hsize, z + hsize),
			new Vector3 (x - hsize, y + hsize, z - hsize),
			new Vector3 (x + hsize, y + hsize, z - hsize),
			new Vector3 (x + hsize, y + hsize, z + hsize),
			new Vector3 (x - hsize, y + hsize, z + hsize),
		};
		
		GL.Vertex (vetices [0]);
		GL.Vertex (vetices [1]);
		GL.Vertex (vetices [1]);
		GL.Vertex (vetices [2]);
		GL.Vertex (vetices [2]);
		GL.Vertex (vetices [3]);
		GL.Vertex (vetices [3]);
		GL.Vertex (vetices [0]);
		
		GL.Vertex (vetices [4]);
		GL.Vertex (vetices [5]);
		GL.Vertex (vetices [5]);
		GL.Vertex (vetices [6]);
		GL.Vertex (vetices [6]);
		GL.Vertex (vetices [7]);
		GL.Vertex (vetices [7]);
		GL.Vertex (vetices [4]);
		
		GL.Vertex (vetices [0]);
		GL.Vertex (vetices [4]);
		GL.Vertex (vetices [1]);
		GL.Vertex (vetices [5]);
		GL.Vertex (vetices [2]);
		GL.Vertex (vetices [6]);
		GL.Vertex (vetices [3]);
		GL.Vertex (vetices [7]);

		GL.End ();
	}
}
 

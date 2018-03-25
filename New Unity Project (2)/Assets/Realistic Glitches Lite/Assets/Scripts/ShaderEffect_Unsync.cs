using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_Unsync : MonoBehaviour {

	public enum Movement {JUMPING_FullOnly, SCROLLING_FullOnly, STATIC};
	public Movement movement = Movement.STATIC;
	public float speed = 1;
	private float position = 0;
	private Material material;

	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/VUnsync") );
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (speed < 10)
			speed += 0.1f;
		else
			speed = 0;
		position = speed * 0.1f;

		material.SetFloat("_ValueX", position);
		Graphics.Blit (source, destination, material);
	}
}

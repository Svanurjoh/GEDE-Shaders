using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestShader : MonoBehaviour {
	public float distance = 1;
	public float intensity = -44f;
	private Material material;

	private float hauntedCooldown = 4.0f;
	private float hauntedTime = 1.0f;
	private float lastHaunt = 0f;
	private int frame = 0;
	private bool haunt = false;
	private bool hauntOnCd = false;

	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/BleedingColors") );
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		frame = Time.frameCount;
		if (!haunt && !hauntOnCd) {    //Random.Range(-2f,2f);
			haunt = true;
			hauntOnCd = true;
			lastHaunt = 0.0f;
		}
		if (haunt) {
			if (frame % 4 == 0)
				distance = -2f;
			else
				distance = 0f;
		}	
		if (lastHaunt >= hauntedTime) {
			haunt = false;
			distance = 0.0f;
		}
		if (lastHaunt >= hauntedCooldown) {
			hauntOnCd = false;
		}
		lastHaunt += Time.deltaTime;

		material.SetFloat("_ValueX", distance);
		material.SetFloat ("_Intensity", 100f * distance);
		Graphics.Blit (source, destination, material);
	}
}

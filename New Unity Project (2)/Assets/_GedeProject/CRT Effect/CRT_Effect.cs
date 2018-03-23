using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CRT_Effect : MonoBehaviour {

	private Material mat;

	private float linePos = 1;
	public bool m_Enable = true;
	public Texture2D noise;
	[Range(0.01f, 7f)] public float m_Static = 0.5f;
	[Range(1f, 5f)] public float lineWidth = 1f;
	[Range(1f, 5f)] public float lineSpeed = 1f;

	void Start()
	{
		mat = new Material( Shader.Find("GEDE/CRT_Effect") );
		mat.SetTexture ("_NoiseTex", noise);
	}

	void Update()
	{
		linePos -= Time.deltaTime / (6-lineSpeed);
		if (linePos < 0) {
			linePos = 1;
		}

		float x = Random.Range (0f, 1f);
		float y = Random.Range (0f, 1f);

		mat.SetFloat ("_LinePos", linePos);
		mat.SetFloat ("_LineWidth", lineWidth);
		mat.SetVector ("_Rnd", new Vector4 (x, y, 0f, 0f));
		mat.SetFloat ("_Static", m_Static);

	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (m_Enable) {
			Graphics.Blit (source, destination, mat);
		} else {
			Graphics.Blit (source, destination);
		}
	}
}

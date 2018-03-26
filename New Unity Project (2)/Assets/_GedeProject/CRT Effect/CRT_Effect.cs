using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CRT_Effect : MonoBehaviour {

	private Material mat;
	private Material mat2;

	private float linePos = 1;
	[Header("Settings")]
	public bool m_Enable = true;
	[Header("Static Effect")]
	public Texture2D noise;
	[Range(0.01f, 7f)] public float m_Static = 0.5f;
	[Range(1f, 5f)] public float lineWidth = 1f;
	[Range(1f, 5f)] public float lineSpeed = 1f;

	[Range(0.01f, 0.05f)] public float yExtra = 0f;
	/*[Header("Fly Effect")]
	public Texture2D flySilhouette;
	[Range(0f, 100f)] public float flySize = 10f;
	[Range(0, 10)] public int maxFlies = 1;
	[Range(1f, 10f)] public float spawnDelay = 3f;
	[Range(2f, 6f)] public float duration = 3f;
	[Range(0f, 360f)] 
	public float rotation;*/

	void Start()
	{
		mat = new Material( Shader.Find("GEDE/CRT_Effect"));
		mat.SetTexture ("_NoiseTex", noise);

		mat2 = new Material( Shader.Find("GEDE/CRT_Glitch") );
		//mat.SetTexture ("_FlyTex", flySilhouette);
	}

	void Update()
	{
		linePos -= Time.deltaTime / (6-lineSpeed);
		if (linePos < 0) {
			linePos = 1;
		}
		//var rad = rotation * Mathf.PI / 180;
		//mat.SetFloat ("_Radian", rad);

		float x = Random.Range (0f, 1f);
		float y = Random.Range (0f, 1f);

		mat.SetFloat ("_LinePos", linePos);
		mat.SetFloat ("_LineWidth", lineWidth);
		mat.SetVector ("_Rnd", new Vector2 (x, y));
		mat.SetFloat ("_Static", m_Static);
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (m_Enable) {
			yExtra = Random.Range (0.01f, 0.05f); 
			Graphics.Blit (source, source, mat);
			mat2.SetFloat("_ValueY", yExtra);
			Graphics.Blit (source, source, mat2);

			mat2.SetFloat("_ValueY", -yExtra);
			Graphics.Blit (source, destination, mat2);
		} else {
			Graphics.Blit (source, destination);
		}
	}
}

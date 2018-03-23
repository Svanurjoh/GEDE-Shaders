using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CameraEffect : MonoBehaviour
{
	public Material m_Mat;
	public bool m_Enable = true;
	[Range(0.1f, 0.8f)] public float m_ThermalHigh = 0.3f;
	[Range(0.1f, 1f)] public float m_BlurAmount = 0.5f;
	[Range(0.1f, 1f)] public float m_DimensionsX = 0.5f;
	[Range(0.1f, 1f)] public float m_DimensionsY = 0.5f;

    void Update ()
	{
		float x = Random.Range (0f, 1f);
		float y = Random.Range (0f, 1f);
		m_Mat.SetVector ("_Rnd", new Vector4 (x, y, 0f, 0f));
		m_Mat.SetVector ("_Dimensions", new Vector4 (m_DimensionsX, m_DimensionsY, 0f, 0f));
		m_Mat.SetFloat ("_HotLight", m_ThermalHigh);
		m_Mat.SetFloat ("_BlurAmount", m_BlurAmount);
	}
	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		if (m_Enable)
		{
			Graphics.Blit (src, dst, m_Mat, 0);
		}
		else
		{
			Graphics.Blit (src, dst);
		}
	}
}

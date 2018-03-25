using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class NarrowFieldOfView : MonoBehaviour {

	[SerializeField]
	private Color m_NVColor = new Color(1f,1f,1f,0f);

	[SerializeField]
	private Color m_TargetBleachColor = new Color(1f,1f,1f,0f);

	[Range(0f, 0.1f)]
	private float m_baseLightingContribution = 0;

	[Range(0f, 128f)]
	private float m_LightSensitivityMultiplier = 0;

	private float m_FOV = 0;

	Material m_Material;
	Shader m_Shader;

	public bool useVignetting = true;

	public Shader NightVisionShader
	{
		get { return m_Shader; }
	}

	private void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			DestroyImmediate(mat);
			mat = null;
		}
	}

	private void CreateMaterials()
	{
		if (m_Shader == null)
		{
			m_Shader = Shader.Find("Custom/DeferredNightVisionShader");
		}
		
		if (m_Material == null && m_Shader != null && m_Shader.isSupported)
		{
			m_Material = CreateMaterial(m_Shader);
		}
	}

	private Material CreateMaterial(Shader shader)
	{
		if (!shader)
			return null;
		Material m = new Material(shader);
		m.hideFlags = HideFlags.HideAndDontSave;
		return m;
	}

	void OnDisable()
	{
		DestroyMaterial(m_Material); m_Material = null; m_Shader = null;
	}

	[ContextMenu("UpdateShaderValues")]
	public void UpdateShaderValues()
	{
		if(m_FOV < 5)
			m_FOV += 0.001f;
		if (m_Material == null)
			return;

		m_Material.SetVector("_NVColor", m_NVColor);

		m_Material.SetVector("_TargetWhiteColor", m_TargetBleachColor);

		m_Material.SetFloat("_BaseLightingContribution", m_baseLightingContribution);

		m_Material.SetFloat("_LightSensitivityMultiplier", m_LightSensitivityMultiplier);

		m_Material.SetFloat ("_FOV", m_FOV);
	
		m_Material.shaderKeywords = null;

		if(useVignetting)
		{
			Shader.EnableKeyword("USE_VIGNETTE");
		} else {
			Shader.DisableKeyword("USE_VIGNETTE");
		}

	}
	
	
	void OnEnable()
	{
		CreateMaterials();
		UpdateShaderValues();
	}
	
	public void ReloadShaders()
	{
		OnDisable();
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{		
		UpdateShaderValues();
		CreateMaterials();
		
		Graphics.Blit(source, destination, m_Material);
	}
}

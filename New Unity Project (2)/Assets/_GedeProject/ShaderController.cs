using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour {

	private CRT_Effect crt;
	private NarrowFieldOfView nfv;
	private TestShader haunt;

	// Use this for initialization
	void Start () {
		crt = GetComponent<CRT_Effect> ();
		nfv = GetComponent<NarrowFieldOfView> ();
		haunt = GetComponent<TestShader> ();

		DisableAll ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ToggleCrt ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			ToggleHaunt ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			ToggleFoV ();
		}

		UpdateShaderValues ();
	}

	private void UpdateShaderValues () {
		if (crt.enabled) {
			if (Input.GetKey (KeyCode.Keypad8))
				crt.glitch += 0.1f * Time.deltaTime;

			if (Input.GetKey (KeyCode.Keypad5))
				crt.glitch -= 0.1f * Time.deltaTime;
			
			if (Input.GetKey (KeyCode.Keypad7))
				crt.m_Static -= 1f * Time.deltaTime;

			if (Input.GetKey (KeyCode.Keypad9))
				crt.m_Static += 1f * Time.deltaTime;

			if (Input.GetKeyDown (KeyCode.Keypad4))
				crt.lineWidth -= 1f;

			if (Input.GetKeyDown (KeyCode.Keypad6))
				crt.lineWidth += 1f;

			if (Input.GetKeyDown (KeyCode.Keypad1))
				crt.lineSpeed -= 1f;

			if (Input.GetKeyDown (KeyCode.Keypad3))
				crt.lineSpeed += 1f;

			crt.glitch = Mathf.Clamp (crt.glitch, 0f, 0.1f);
			crt.m_Static = Mathf.Clamp (crt.m_Static, 0.01f, 10f);
			crt.lineWidth = Mathf.Clamp (crt.lineWidth, 1f, 5f);
			crt.lineSpeed = Mathf.Clamp (crt.lineSpeed, 1f, 5f);
		}

		if (haunt.enabled) {
			if (Input.GetKey (KeyCode.Keypad7))
				haunt.hauntedCooldown -= 1f * Time.deltaTime;

			if (Input.GetKey (KeyCode.Keypad9))
				haunt.hauntedCooldown += 1f * Time.deltaTime;

			if (Input.GetKey (KeyCode.Keypad4))
				haunt.hauntedTime -= 1f * Time.deltaTime;

			if (Input.GetKey (KeyCode.Keypad6))
				haunt.hauntedTime += 1f * Time.deltaTime;


			haunt.hauntedCooldown = Mathf.Clamp (haunt.hauntedCooldown, 1f, 8f);
			haunt.hauntedTime = Mathf.Clamp (haunt.hauntedTime, 1f, 6f);
		}

		if (nfv.enabled) {
			if (Input.GetKey (KeyCode.Keypad7))
				nfv.m_FOV -= 0.5f * Time.deltaTime;

			if (Input.GetKey (KeyCode.Keypad9))
				nfv.m_FOV += 0.5f * Time.deltaTime;

			if (Input.GetKeyDown (KeyCode.Keypad8))
				nfv.useVignetting = !nfv.useVignetting;

			nfv.m_FOV = Mathf.Clamp (nfv.m_FOV, 0f, 5f);
		}
	}

	private void ToggleCrt() {
		if (crt.enabled) {
			crt.enabled = false;
		} else {
			crt.enabled = true;
		}
		nfv.enabled = false;
		haunt.enabled = false;
	}

	private void ToggleFoV() {
		if (nfv.enabled) {
			nfv.enabled = false;
		} else {
			nfv.enabled = true;
		}
		crt.enabled = false;
		haunt.enabled = false;
	}

	private void ToggleHaunt() {
		if (haunt.enabled) {
			haunt.enabled = false;
		} else {
			haunt.enabled = true;
		}
		crt.enabled = false;
		nfv.enabled = false;
	}

	private void DisableAll() {
		crt.enabled = false;
		nfv.enabled = false;
		haunt.enabled = false;
	}
}

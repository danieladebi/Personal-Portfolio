using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour {

	public GameObject[] atoms;

	void Start() {
		for (int i = 1; i < atoms.Length; i++) {
			atoms [i].SetActive (false);
		}
		atoms [0].SetActive (true);
	}

	void LateUpdate() {
		if (Input.GetKeyDown ("left")) {
			atoms [AtomStructureManager.number-1].SetActive (true);
			atoms [AtomStructureManager.number].SetActive (false);
		}
		if (Input.GetKeyDown ("right")) {
			atoms [AtomStructureManager.number-1].SetActive (true);
			atoms [AtomStructureManager.number-2].SetActive (false);
		}
	}

	public void PreviousDiagram() {
		atoms [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number < atoms.Length)
			atoms [AtomStructureManager.number].SetActive (false);
	}

	public void NextDiagram() {
		atoms [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number > 1)
			atoms [AtomStructureManager.number-2].SetActive (false);
	}
}

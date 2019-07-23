using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalDiagramManager : MonoBehaviour {

	public GameObject[] atomOrbitals;

	void Start() {
		for (int i = 1; i < atomOrbitals.Length; i++) {
			atomOrbitals [i].SetActive (false);
		}
		atomOrbitals [0].SetActive (true);
	}

	void LateUpdate() {
		if (Input.GetKeyDown ("left")) {
			atomOrbitals [AtomStructureManager.number-1].SetActive (true);
			atomOrbitals [AtomStructureManager.number].SetActive (false);
		}
		if (Input.GetKeyDown ("right")) {
			atomOrbitals [AtomStructureManager.number-1].SetActive (true);
			atomOrbitals [AtomStructureManager.number-2].SetActive (false);
		}
	}

	public void PreviousDiagram() {
		atomOrbitals [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number < atomOrbitals.Length)
			atomOrbitals [AtomStructureManager.number].SetActive (false);
	}


	public void NextDiagram() {
		atomOrbitals [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number > 1)
			atomOrbitals [AtomStructureManager.number-2].SetActive (false);
	}

}

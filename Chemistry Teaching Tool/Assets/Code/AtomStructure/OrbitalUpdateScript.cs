using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalUpdateScript : MonoBehaviour {

	public GameObject[] orbitalsNew;
	public GameObject[] orbitalsFilled;

	void Start() {
		for (int i = 1; i < orbitalsNew.Length; i++) {
			orbitalsNew [i].SetActive (false);
		}
		orbitalsNew [0].SetActive (true);

		for (int i = 1; i < orbitalsFilled.Length; i++) {
			orbitalsFilled [0].SetActive (false);
		}
		orbitalsFilled [0].SetActive (true);
	}

	void LateUpdate() {
		if (Input.GetKeyDown ("left")) {
			orbitalsNew [AtomStructureManager.number-1].SetActive (true);
			orbitalsNew [AtomStructureManager.number].SetActive (false);
			orbitalsFilled [AtomStructureManager.number-1].SetActive (true);
			orbitalsFilled [AtomStructureManager.number].SetActive (false);
		}
		if (Input.GetKeyDown ("right")) {
			orbitalsNew [AtomStructureManager.number-1].SetActive (true);
			orbitalsNew [AtomStructureManager.number-2].SetActive (false);
			orbitalsFilled [AtomStructureManager.number-1].SetActive (true);
			orbitalsFilled [AtomStructureManager.number-2].SetActive (false);
		}
	}

	public void PreviousOrbitals() {
		orbitalsNew [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number < orbitalsNew.Length)
			orbitalsNew [AtomStructureManager.number].SetActive (false);

		orbitalsFilled [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number < orbitalsFilled.Length)
			orbitalsFilled [AtomStructureManager.number].SetActive (false);
	}

	public void NextOrbitals() {
		orbitalsNew [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number > 1)
			orbitalsNew [AtomStructureManager.number-2].SetActive (false);

		orbitalsFilled [AtomStructureManager.number-1].SetActive (true);
		if (AtomStructureManager.number > 1)
			orbitalsFilled [AtomStructureManager.number-2].SetActive (false);
	}
}

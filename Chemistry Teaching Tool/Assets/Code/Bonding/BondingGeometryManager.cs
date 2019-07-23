using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BondingGeometryManager : MonoBehaviour {

	public Button previousButton;
	public Button nextButton;

	public Text geometryTitle;
	public Text bondingGeometryExplanation;

	public GameObject[] moleculeShapes;
	public GameObject[] shapeStats;

	public static int number;

	// Use this for initialization
	void Start () {
		number = 1;
		ChangeInfo (1);
		for (int i = 1; i < moleculeShapes.Length; i++) {
			moleculeShapes [i].SetActive (false);
			shapeStats [i].SetActive (false);
		}
		moleculeShapes [0].SetActive (true);
		shapeStats [0].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (number <= 1) {
			previousButton.gameObject.SetActive (false);
		} else {
			previousButton.gameObject.SetActive (true);
			if (Input.GetKeyDown ("left")) {
				number -= 1;
				ChangeInfo (number);
			}
			moleculeShapes [number-1].SetActive (true);
			shapeStats [number - 1].SetActive (true);

			if (number < 5) {
				moleculeShapes [number].SetActive (false);
				shapeStats [number].SetActive (false);
			}

		}

		if (number >= 5) {
			nextButton.gameObject.SetActive (false);
		} else {
			nextButton.gameObject.SetActive (true);
			if (Input.GetKeyDown ("right")) {
				number += 1;
				ChangeInfo (number);
			}
			moleculeShapes [number-1].SetActive (true);
			shapeStats [number - 1].SetActive (true);
			if (number > 1) {
				moleculeShapes [number - 2].SetActive (false);
				shapeStats [number - 2].SetActive (false);
			}
		}
	}

	public void NextBGInfo() {
		moleculeShapes [number-1].SetActive (true);
		shapeStats [number - 1].SetActive (true);

		if (number < 5) {
			number += 1;
			moleculeShapes [number-2].SetActive (false);
			shapeStats [number - 2].SetActive (false);

			ChangeInfo (number);
		}
	}

	public void PreviousBGInfo() {
		moleculeShapes [number - 1].SetActive (true);
		shapeStats [number - 1].SetActive (true);

		if (number > 1) {
			number -= 1;
			ChangeInfo (number);
			moleculeShapes [number].SetActive (false);
			shapeStats [number].SetActive (false);

		} 
	}

	public void ChangeInfo(int n) {
		switch (n) {
		case 1:
			geometryTitle.text = "Linear";
			bondingGeometryExplanation.text = "For molecules with linear geometry, both the electron domain " +
				"geometry and the molecular geometry are the same.\n";
			break;
		case 2:
			geometryTitle.text = "Trigonal Planar";
			bondingGeometryExplanation.text = "No electron domains on the central atom allows the non-central atoms " +
			"to move away from each other to form a triangular shape. But when we replace one atom with an electron domain, " +
			"the other 2 atoms feel a slight repulsion, reducing the bond angle by a little bit.";
			break;
		case 3:
			geometryTitle.text = "Tetrahedral";
			bondingGeometryExplanation.text = "For molecules with tetrahedral geometry, there can either be 0, 1, or 2 " +
				"electron domains. One electron domain results in a trigonal pyramidal molecular structure, while two " +
				"electron domains result in a bent shape with a smaller angle than that formed from the trigonal planar electron domain " +
				"structure.";
			break;
		case 4:
			geometryTitle.text = "Trigonal Bipyramidal (Advanced)";
			bondingGeometryExplanation.text = "Molecules with trigonal bipyramidal geometry have two different bond angles depending " +
				"on the position of the atom. This is one of the rarer configurations.";
			break;
		case 5:
			geometryTitle.text = "Octahedral (Advanced)";
			bondingGeometryExplanation.text = "Molecules with octahedral geometry take the shape of an octahedron, where there are " +
			"a total of six electron domains. This is another rare configuration, but elements like Sulfur (S) sometimes have six atoms " +
			"attached to form an octahedral shape."; 
			break;
		}
	}
}

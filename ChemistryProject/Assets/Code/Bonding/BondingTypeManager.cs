using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BondingTypeManager : MonoBehaviour {

	public Button previousButton;
	public Button nextButton;

	public Text bTTitle;
	public Text bTExplanation;

	public GameObject[] bondingTypes;

	public static int number;

	// Use this for initialization
	void Start () {
		number = 1;
		ChangeInfo (1);
		for (int i = 1; i < bondingTypes.Length; i++) {
			bondingTypes [i].SetActive (false);
		}
		bondingTypes [0].SetActive (true);
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
			bondingTypes [number-1].SetActive (true);
			if (number < 4)
				bondingTypes [number].SetActive (false);
		}

		if (number >= 4) {
			nextButton.gameObject.SetActive (false);
		} else {
			nextButton.gameObject.SetActive (true);
			if (Input.GetKeyDown ("right")) {
				number += 1;
				ChangeInfo (number);
			}
			bondingTypes [number-1].SetActive (true);
			if (number > 1)
				bondingTypes [number-2].SetActive (false);
		}
	}

	public void NextBTInfo () {
		bondingTypes [number-1].SetActive (true);

		if (number < 4) {
			number += 1;
			bondingTypes [number-2].SetActive (false);

			ChangeInfo (number);
		}
	}

	public void PreviousBTInfo () {
		bondingTypes [number - 1].SetActive (true);

		if (number > 1) {
			number -= 1;
			ChangeInfo (number);
			bondingTypes [number].SetActive (false);

		} 
	}

	public void ChangeInfo(int n) {
		switch (n) {
		case 1:
			bTTitle.text = "Ionic Bonding";
			bTExplanation.text = "Ionic bonds occur when there is a complete transfer of electrons. " +
			"Typically, ionic bonds are formed between a metal and a non-metal. Electronegativity difference " +
			"should be greater than 1.7.";
			break;

		case 2:
			bTTitle.text = "Polar Covalent Bonding";
			bTExplanation.text = "Polar Covalent bonds occur when electrons are unevenly shared between two atoms." +
				"The electrons will stay near the atom with the higher electronegativity, but this electronegativity difference " +
				"isn't high enough to create an ionic bond. Electronegativity difference is usually between 0.4 and 1.7.";
			break;

		case 3:
			bTTitle.text = "Nonpolar Covalent Bonding";
			bTExplanation.text = "Nonpolar Covalent bonds occur when electrons are evenly shared among two atoms." +
				"This usually happens for diatomic atoms such as Hydrogen or Oxygen, but it can also happen with two atoms that" +
				"have a small electronegativity difference (< 0.4).";
			break;

		case 4: 
			bTTitle.text = "Metallic Bonding";
			bTExplanation.text = "Metallic bonds occur between two metals in the form of alloys. The electrons among " +
				"the atoms are shared among the entire metallic structure. Metals can also form alloys with non-metals.";
			break;
		}
	}
}

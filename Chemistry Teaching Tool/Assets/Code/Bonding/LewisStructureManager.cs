using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LewisStructureManager : MonoBehaviour {

	public Button previousButton;
	public Button nextButton;

	public const int sectionCount = 5;

	public Text sectionTitle;
	public Text sectionInfo;
	public GameObject[] sectionImages;

	public static int number;

	// Use this for initialization
	void Start () {
		number = 1;
		ChangeInfo (number);
		for (int i = 1; i < sectionImages.Length; i++) {
			sectionImages [i].SetActive (false);
		}
		sectionImages [0].SetActive (true);
	}
	
	void Update() {
		if (number <= 1) {
			previousButton.gameObject.SetActive (false);
		} else {
			previousButton.gameObject.SetActive (true);
			if (Input.GetKeyDown ("left")) {
				number -= 1;
				ChangeInfo (number);
			}
			sectionImages [number-1].SetActive (true);
			if (number < sectionCount)
				sectionImages [number].SetActive (false);
		}

		if (number >= sectionCount) {
			nextButton.gameObject.SetActive (false);
		} else {
			nextButton.gameObject.SetActive (true);
			if (Input.GetKeyDown ("right")) {
				number += 1;
				ChangeInfo (number);
			}
			sectionImages [number-1].SetActive (true);
			if (number > 1)
				sectionImages [number-2].SetActive (false);
		}

	} 

	public void NextLSInfo() {
		sectionImages [number-1].SetActive (true);

		if (number < sectionCount) {
			number += 1;
			sectionImages [number-2].SetActive (false);
			ChangeInfo (number);
		}
	}

	public void PreviousLSInfo() {
		sectionImages [number-1].SetActive (true);

		if (number > 1) {
			number -= 1;
			ChangeInfo (number);
			sectionImages [number].SetActive (false);
		} 
	}

	private void ChangeInfo (int n) {
		switch (n) {
		case 1:
			sectionTitle.text = "Octet Rule";
			sectionInfo.text = "The octet rule refers to the fact that atoms tend to prefer to have 8 valence electrons in " +
			"its outer shell, as the atom is more stable in this scenario. These electrons can be shared among multiple atoms. " +
			"This is a general rule that applies to most atoms. Hydrogen is a common exception to this rule, " +
			"as it can only hold a maximum of 2 electrons. Transition metals also tend to break this rule very often.";
			break;
		case 2:
			sectionTitle.text = "Tips for Creating Lewis Structures";
			sectionInfo.text = "When creating a Lewis Structure, start by counting the amount of valence electrons " +
				"in the compound. Assume all bonds are single bonds at first, and then make sure every atom has" +
			"eight electrons (except Hydrogen), remembering that some of the electrons can be shared. Any electrons " +
			"in a bond between two atoms count towards both elements. An example of a Lewis Structure being created is displayed below.";
			break;
		case 3:
			sectionTitle.text = "Double Bond Example";
			sectionInfo.text = "Sometimes, just adding a single bond won't be enough to have all atoms become stable. When this happens," +
			"you'll have to move at least two more electrons to add another bond in order for all atoms to become stable. Molecules with" +
			"double bonds are much sturdier than those with triple bonds. Below is an example of a molecule with a double bond.";
			break;
		case 4:
			sectionTitle.text = "More Complex Examples";
			sectionInfo.text = "Usually, the bonds you will work with are either single or double bonds. But sometimes," +
				"you will work with triple bonds, the strongest type of bond. And different chemical bonds can exist within the " +
				"same molecule. Below is an example of a more complicated molecule.";
			break;
		case 5:
			sectionTitle.text = "Ionic Bond Examples";
			sectionInfo.text = "Ionic bonds are represented differently in Lewis Structures. To represent an ionic bond between " +
			"two atoms, brackets are required between each of the atoms that have a charge, with the charge being notified at " +
			"the top right hand corner of the bracket. To represent an ionic compound, a compound that has a charge, you must " +
			"have brackets around the entire molecule, and then note the charge in the same format as you would with a single ion. " +
			"A negative charge means electrons were gained, and a positive charge means electrons were lost.";
			break;
		}
	}
}

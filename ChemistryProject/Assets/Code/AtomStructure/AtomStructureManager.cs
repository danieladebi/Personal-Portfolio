using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomStructureManager : MonoBehaviour {

	public Button previousButton;
	public Button nextButton;

	public Text name;
	public Text atomicMass;
	public Text massNumber;
	public Text stateOfMatter;
	public Text atomicNumber;
	public Text aNumberWLabel;
	public Text symbol;
	public Text elementTitle;
	public Text electronConfig;

	public GameObject element;

	public static int number;

	char one = '\u00b9';
	char two = '\u00b2'; 
	char three = '\u00b3';
	char four = '\u2074';
	char five = '\u2075';
	char six = '\u2076';
	char seven = '\u2077';
	char eight = '\u2078';
	// char nine = '\u2079';
	char zero = '\u2070';

	// Use this for initialization
	void Start () {
		number = 1;
		electronConfig.text = symbol.text + " = 1s" + one;
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
		}

		if (number >= 36) {
			nextButton.gameObject.SetActive (false);
		} else {
			nextButton.gameObject.SetActive (true);
			if (Input.GetKeyDown ("right")) {
				number += 1;
				ChangeInfo (number);
			}
		}
	
	} 
		
	public void NextElementInfo() {
		if (number < 36) {
			number += 1;
			ChangeInfo (number);
		}
	}

	public void PreviousElementInfo() {
		if (number > 1) {
			number -= 1;
			ChangeInfo (number);
		} 
	}

	private void ChangeInfo(int n) {
		switch (n) {
			case 1:
				name.text = elementTitle.text = "Hydrogen";
				atomicMass.text = "Atomic Mass: 1.01";
				massNumber.text = "1";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "H";
				electronConfig.text = symbol.text + " = 1s" + one;
				break;
			case 2:
				name.text = elementTitle.text = "Helium";
				atomicMass.text = "Atomic Mass: 4.00";
				massNumber.text = "4";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "He";
				electronConfig.text = symbol.text + " = 1s" + two;
				break;
			case 3:
				name.text = elementTitle.text = "Lithium";
				atomicMass.text = "Atomic Mass: 6.94";
				massNumber.text = "7";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Li";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + one;
				break;
			case 4:
				name.text = elementTitle.text = "Beryllium";
				atomicMass.text = "Atomic Mass: 9.01";
				massNumber.text = "9";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Be";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two;
				break;
			case 5:
				name.text = elementTitle.text = "Boron";
				atomicMass.text = "Atomic Mass: 10.81";
				massNumber.text = "11";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "B";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + one;
				break;
			case 6:
				name.text = elementTitle.text = "Carbon";
				atomicMass.text = "Atomic Mass: 12.01";
				massNumber.text = "12";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "C";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + two;
				break;
			case 7:
				name.text = elementTitle.text = "Nitrogen";
				atomicMass.text = "Atomic Mass: 14.01";
				massNumber.text = "14";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "N";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + three;
				break;
			case 8:
				name.text = elementTitle.text = "Oxygen";
				atomicMass.text = "Atomic Mass: 16.00";
				massNumber.text = "16";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "O";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + four;
				break;
			case 9:
				name.text = elementTitle.text = "Fluorine";
				atomicMass.text = "Atomic Mass: 19.00";
				massNumber.text = "19";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "F";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + five;
				break;
			case 10:
				name.text = elementTitle.text = "Neon";
				atomicMass.text = "Atomic Mass: 20.18";
				massNumber.text = "20";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ne";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six;
				break;
			case 11:
				name.text = elementTitle.text = "Sodium";
				atomicMass.text = "Atomic Mass: 22.99";
				massNumber.text = "23";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Na";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + one;
				break;
			case 12:
				name.text = elementTitle.text = "Magnesium";
				atomicMass.text = "Atomic Mass: 24.31";
				massNumber.text = "24";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Mg";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two;
				break;
			case 13:
				name.text = elementTitle.text = "Aluminum";
				atomicMass.text = "Atomic Mass: 26.98";
				massNumber.text = "27";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Al";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + one;
				break;
			case 14:
				name.text = elementTitle.text = "Silicon";
				atomicMass.text = "Atomic Mass: 28.09";
				massNumber.text = "28";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Si";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two;
				break;
			case 15:
				name.text = elementTitle.text = "Phosphorus";
				atomicMass.text = "Atomic Mass: 30.97";
				massNumber.text = "31";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "P";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + three;
				break;
			case 16:
				name.text = elementTitle.text = "Sulfur";
				atomicMass.text = "Atomic Mass: 32.07";
				massNumber.text = "32";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "S";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + four;
				break;
			case 17:
				name.text = elementTitle.text = "Chlorine";
				atomicMass.text = "Atomic Mass: 35.45";
				massNumber.text = "35";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Cl";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + five;
				break;
			case 18:
				name.text = elementTitle.text = "Argon";
				atomicMass.text = "Atomic Mass: 39.95";
				massNumber.text = "40";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ar";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + six;
				break;
			case 19:
				name.text = elementTitle.text = "Potassium";
				atomicMass.text = "Atomic Mass: 39.10";
				massNumber.text = "39";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "K";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + one;
				break;
			case 20:
				name.text = elementTitle.text = "Calcium";
				atomicMass.text = "Atomic Mass: 40.08";
				massNumber.text = "40";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ca";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two;
				break;
			case 21:
				name.text = elementTitle.text = "Scandium";
				atomicMass.text = "Atomic Mass: 44.96";
				massNumber.text = "45";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Sc";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one;
				break;
			case 22:
				name.text = elementTitle.text = "Titanium";
				atomicMass.text = "Atomic Mass: 47.87";
				massNumber.text = "48";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ti";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + two;
				break;
			case 23:
				name.text = elementTitle.text = "Vanadium";
				atomicMass.text = "Atomic Mass: 50.94";
				massNumber.text = "51";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "V";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + three;
				break;
			case 24:
				name.text = elementTitle.text = "Chromium";
				atomicMass.text = "Atomic Mass: 52.00";
				massNumber.text = "52";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Cr";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + one + " 3d" + five;
				break;
			case 25:
				name.text = elementTitle.text = "Manganese";
				atomicMass.text = "Atomic Mass: 54.94";
				massNumber.text = "55";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Mn";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + five;
				break;
			case 26:
				name.text = elementTitle.text = "Iron";
				atomicMass.text = "Atomic Mass: 55.85";
				massNumber.text = "56";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Fe";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + six;
				break;
			case 27:
				name.text = elementTitle.text = "Cobalt";
				atomicMass.text = "Atomic Mass: 58.93";
				massNumber.text = "59";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Co";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + seven;
				break;
			case 28:
				name.text = elementTitle.text = "Nickel";
				atomicMass.text = "Atomic Mass: 58.69";
				massNumber.text = "59";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ni";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + eight;
				break;
			case 29:
				name.text = elementTitle.text = "Copper";
				atomicMass.text = "Atomic Mass: 63.55";
				massNumber.text = "64";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Cu";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + one + " 3d" + one + zero;
				break;
			case 30:
				name.text = elementTitle.text = "Zinc";
				atomicMass.text = "Atomic Mass: 65.38";
				massNumber.text = "65";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Zn";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero;
				break;
			case 31:
				name.text = elementTitle.text = "Gallium";
				atomicMass.text = "Atomic Mass: 69.72";
				massNumber.text = "70";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ga";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero + " 4p" + one;
				break;
			case 32:
				name.text = elementTitle.text = "Germanium";
				atomicMass.text = "Atomic Mass: 72.64";
				massNumber.text = "73";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Ge";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero + " 4p" + two;
				break;
			case 33:
				name.text = elementTitle.text = "Arsenic";
				atomicMass.text = "Atomic Mass: 74.92";
				massNumber.text = "75";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "As";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero + " 4p" + three;
				break;
			case 34:
				name.text = elementTitle.text = "Selenium";
				atomicMass.text = "Atomic Mass: 78.96";
				massNumber.text = "79";
				stateOfMatter.text = "State of Matter: Solid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Se";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero + " 4p" + four;
				break;
			case 35:
				name.text = elementTitle.text = "Bromine";
				atomicMass.text = "Atomic Mass: 79.90";
				massNumber.text = "80";
				stateOfMatter.text = "State of Matter: Liquid";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Br";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero + " 4p" + five;
				break;
			case 36:
				name.text = elementTitle.text = "Krypton";
				atomicMass.text = "Atomic Mass: 83.80";
				massNumber.text = "84";
				stateOfMatter.text = "State of Matter: Gas";
				atomicNumber.text = number.ToString ();
				aNumberWLabel.text = "Atomic Number\n" + number;
				symbol.text = "Kr";
				electronConfig.text = symbol.text + " = 1s" + two + " 2s" + two + " 2p" + six + " 3s" + two + " 3p" + two + " 4s" + two + " 3d" + one + zero + " 4p" + six;					
				break;
		}

	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrendTextUpdate : MonoBehaviour {

	public Text trendExplanationText;

	public bool isIE;
	public bool isEN;
	public bool isAS;

	public GameObject IEAtomsImages;
	public GameObject ENAtomsImages;
	public GameObject ASAtomsImages;

	public Text nobleGasDisclaimerText;

	// Use this for initialization
	void Awake () {
		trendExplanationText.text = "";
		IEAtomsImages.SetActive (false);
		ENAtomsImages.SetActive (false);
		ASAtomsImages.SetActive (false);
		nobleGasDisclaimerText.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	public void DisplayText () {
		if (isIE) {
			trendExplanationText.text = "<color=red><b>Ionization Energy</b></color>\n\n " +
				"The Ionization Energy is the energy required to remove a valence electron from a neutral atom, forming a cation. " +
				"The more filled the outer electron shell is, the higher the ionization energy, which is why the trend increases from left " +
				"to right. Also, as the atom gets larger, the ionization energy decreases, as the atom struggles to maintain all of its valence electrons," +
				"and this is why the ionization energy increases as you go up the periodic table. " +
				"\n\nSo an atom like Chlorine (Cl) has a higher ionization energy than Sodium (Na) because Chlorine has more valence electrons than Sodium.";
			IEAtomsImages.SetActive (true);
			ENAtomsImages.SetActive (false);
			ASAtomsImages.SetActive (false);
			nobleGasDisclaimerText.gameObject.SetActive (true);
		}
		else if (isEN) {
			trendExplanationText.text = "<color=#ff3e00ff><b>Electronegativity</b></color>\n\n " +
				"Electronegativity measures an atom's tendency to attract electrons. The more valence electrons an atom has, the " +
				"less likely it is to want to lose its electrons. Electron affinity is related to electronegativity, as it's the " +
				"measure of the energy change that occurs when a neutral atom gains an electron, forming an anion. Fluorine (F) is " +
				"the most electronegative atom on the periodic table, as it has a small atomic radius, and a nearly filled outer " +
				"electron shell.";
			IEAtomsImages.SetActive (false);
			ENAtomsImages.SetActive (true);
			ASAtomsImages.SetActive (false);
			nobleGasDisclaimerText.gameObject.SetActive (true);
		} 
		else if (isAS) {
			trendExplanationText.text = "<color=#00ffffff><b>Atomic Radius</b></color>\n\n " +
				"The atomic radius is exactly one half of the distance of the nuclei of two atoms. The atomic radius tend to be smaller, from" +
				"the left of the periodic table to the right, when an atom has more electrons. The atomic radius increases as you go down the periodic table," +
				" due to an effect called electron shielding, which prevents electrons from getting too close to the nucleus, which in turn, results in a larger" +
				"atomic radius. Helium (He) has the smallest atomic radius, while Francium (Fr) has the largest.";

			IEAtomsImages.SetActive (false);
			ENAtomsImages.SetActive (false);
			ASAtomsImages.SetActive (true);
			nobleGasDisclaimerText.gameObject.SetActive (false);
		}
	} 
}

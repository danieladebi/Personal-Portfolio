using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	public Text elementTypeInfo;

	// Each of these booleans will determine which text will show.
	public bool isAlkali;
	public bool isAlkalineEarth;
	public bool isTransition;
	public bool isPoorMetal;
	public bool isMetalloid;
	public bool isNonmetal;
	public bool isHalogen;
	public bool isNobleGas;
	public bool isLanthanide;
	public bool isActinide;

	void Start() {
		elementTypeInfo.text = "";
	}

	public void DisplayText() {
		if (isAlkali) {
			elementTypeInfo.text =
				"<color=red><b>Alkali Metals</b></color> - The Alkali Metals, found in group 1 of the periodic table, " +
				"are very reactive metals. These metals are also shiny and very soft. " +
				"They each only have one valence electron and are most" +
				"likely to react with the halogens. In ionic form, alkali metals have a charge of +1.";
		} else if (isAlkalineEarth) {
			elementTypeInfo.text = 
				"<color=orange><b>Alkaline Earth Metals</b></color> - The Alkaline Earth Metals, found in group 2 of the periodic " +
				"table, are very reactive metals, although not as reactive as alkali metals. These metals are " +
				"very soft, with less luster than alkaline metals. Alkaline metals have two valence electrons, and in " +
				"ionic form, alkaline metals have a charge of +2.";
		} else if (isTransition) {
			elementTypeInfo.text = 
				"<color=yellow><b>Transition Metals</b></color> - The Transition Metals, found in groups 3-12 of the periodic table, " +
				"are both ductile and malleable, and are very good at conducting electricity. These elements " +
				"also generally have a high luster. Many of these metals have multiple charges in ionic form, but" +
				"zinc (Zn), silver (Ag), and cadmium (Cd) are exceptions to this rule.";
		} else if (isPoorMetal) {
			elementTypeInfo.text = 
				"<color=green><b>Poor Metals</b></color> - The Poor Metals, found in groups 13-16 <i>under the staircase</i>, are more " +
				"soft and brittle metals, although they aren't as soft as alkali or alkaline earth metals. " +
				"They can conduct electricity, but they are poor conductors of heat. These metals have " +
				"varying charges in ionic form, with the exception of Aluminum (Al).";
		} else if (isMetalloid) {
			elementTypeInfo.text = 
				"<color=cyan><b>Metalloids/Semimetals</b></color> - The Metalloids, or the Semimetals, found in groups 13-16 <i>along</i> " +
				"the staircase, are elements that have properties of both metals and nonmetals. Some of these elements are semiconductors, " +
				"which means they are only electrically conductive in certain conditions.";
		} else if (isNonmetal) {
			elementTypeInfo.text =
				"<color=blue><b>Nonmetals</b></color> - The Nonmetals, which technically includes <i>all</i> elements above and to the " +
				"right of the staircase, are elements that are poor electricity and heat conductors. These elements are very brittle, and have " +
				"little to no luster. Nonmetals usually form an ion with a negative charge, except for hydrogen, which can be positive or " +
				"negative. ";
		} else if (isHalogen) {
			elementTypeInfo.text = 
				"<color=#ff66ffff><b>Halogens</b></color> - The Halogens, found in group 17 of the periodic table, are nonmetals " +
				"that are known to react with the alkali and alkaline earth metals to form salts. The halogens contain two gases (Fluorine (F) " +
				"and Chlorine (Cl) ), one liquid (Bromine (Br) ), and two solids (Iodine (I) and Astatine (At) )." +
				"Each of these elements have 7 valence electrons, and in ionic form, have a charge of -1.";
		} else if (isNobleGas) {
			elementTypeInfo.text = 
				"<color=purple><b>Noble Gases</b></color> - The Noble Gases, found in group 18 of the periodic table, are inert gases " +
				"because they have all 8 valence electrons in their outer shell, or 2 electrons in Helium's (He) case. Because of their high stability," +
				"the noble gases generally don't react with any other elements, and as a result, they all have a charge of 0.";
		} else if (isLanthanide) {
			elementTypeInfo.text = 
				"<color=#ff99ffff><b>Lanthanides</b></color> - The Lanthanides, starting with Lanthanum (La) going through Lutetium (Lu), " +
				"are the first row of rare earth elements. These elements are all metals, and they all have a silvery shine, but they" +
				"quickly tarnish in air. Lanthanides are technically transition metals, and are pretty reactive.";
		} else if (isActinide) {
			elementTypeInfo.text = 
				"<color=#ff00aaff><b>Actinides</b></color> - The Actinides, starting from Actinium (Ac) going through Lawrencium (Lr), " +
				"are the second row of rare earth elements. Like the Lanthanides, these elements are all metals, but they are much less" +
				"stable. In fact, all of the actinides are radioactive, and none of the elements after Uranium are found in nature.";
		}
	}
}

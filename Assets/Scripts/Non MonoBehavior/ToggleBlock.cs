using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Holds the label value and state of a toggle in a toggle
 inspector block. The label value depends on the toggle state */
public class ToggleBlock : InteractionBlock {
	private string labelOff;
	private string labelOn;

	private bool state;

	public string LabelOff {
		get { return labelOff; }
	}

	public string LabelOn {
		get { return labelOn; }
	}

	public bool State {
		get { return state; }
		set { state = value; }
	}

	public ToggleBlock(string on, string off, bool startState) {
		labelOff = off;
		labelOn = on;
		state = startState;
	}
}
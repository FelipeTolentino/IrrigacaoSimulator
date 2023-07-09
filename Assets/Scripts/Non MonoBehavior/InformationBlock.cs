using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Holds information to be displayed in a information inspector block */
public class InformationBlock : IBlock {
	private string label;
	private string value;

	public string Label {
		get { return label; }
	}

	public string Value {
		get { return value; }
		set { this.value = value; }
	}
	public InformationBlock(string _label) {
		label = _label;
	}

	public InformationBlock(string _label, string _value) {
		label = _label;
		value = _value;
	}
}

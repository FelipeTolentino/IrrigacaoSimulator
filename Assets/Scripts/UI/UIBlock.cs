using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBlock : MonoBehaviour {
	[SerializeField] protected TMP_Text label;

	protected Element element;
	protected int contentQueuePos;

	public Element Element {
		set { element = value; }
	}

	public int ContentQueuePos {
		set { contentQueuePos = value; }
	}

	// Start is called before the first frame update
	void Start() { }

	// Update is called once per frame
	void Update() { }
}
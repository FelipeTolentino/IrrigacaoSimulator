using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInformarionBlock : UIBlock {

	[SerializeField] private TMP_Text value;
	// Start is called before the first frame update
	void Start() {
		var content = (InformationBlock) element.GetBlockContent(contentQueuePos);
		label.text = content.Label;
	}

	// Update is called once per frame
	void Update() {
		var content = (InformationBlock) element.GetBlockContent(contentQueuePos);
		value.text = content.Value;
	}
}
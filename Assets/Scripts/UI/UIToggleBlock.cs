using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UIElements.Toggle;

public class UIToggleBlock : UIInteractionBlock {
	// Start is called before the first frame update
	void Start() {
		var content = (ToggleBlock)element.GetBlockContent(contentQueuePos);
		label.text = content.State ? content.LabelOn : content.LabelOff;
	}

	// Update is called once per frame
	void Update() {
		var content = (ToggleBlock)element.GetBlockContent(contentQueuePos);
		label.text = content.State ? content.LabelOn : content.LabelOff;
	}

	public void Toggle() {
		element.ToggleBlockResponse(contentQueuePos);
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour {
	private static Inspector Instance;

	[SerializeField] private GameObject inspector;
	[SerializeField] private GameObject infBlockPrefab, toggleBlockPrefab;
	[SerializeField] private TMP_Text title;

	private Element currElement;
	private bool active;
	private List<GameObject> blocks;

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	// Start is called before the first frame update
	void Start() {
		blocks = new List<GameObject>();
	}

	// Update is called once per frame
	void Update() {
		if (active) {
		}
	}

	public static Inspector GetInstance() {
		return Instance;
	}

	public void Show(Element element) {
		RemoveBlocks();

		currElement = element;
		title.text = currElement.DisplayName;
		
		var blocksCotent = currElement.BlocksContent;
		for (int i = 0; i < blocksCotent.Count; i++) {
			var content = blocksCotent[i];
			GameObject block;
			if (content.GetType() == typeof(InformationBlock)) {
				block = Instantiate(infBlockPrefab, transform);
				block.transform.SetParent(inspector.transform);
			}
			else /*if (content.GetType() == typeof(ToggleBlock))*/ {
				block = Instantiate(toggleBlockPrefab, transform);
				block.transform.SetParent(inspector.transform);
			}
			block.GetComponent<UIBlock>().Element = element;
			block.GetComponent<UIBlock>().ContentQueuePos = i;
			blocks.Add(block);
		}
		
		inspector.SetActive(active = true);
		LayoutRebuilder.ForceRebuildLayoutImmediate(inspector.GetComponent<RectTransform>());
	}

	public void Hide() {
		if (active) {
			currElement = null;
			RemoveBlocks();
			inspector.SetActive(active = false);
		}
	}

	void RemoveBlocks() {
		foreach (var block in blocks) {
			Destroy(block);
		}
	}
	
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.Cursor;

public enum ToolType {
	None,
	IPipe,
	LPipe,
}

public class Toolbar : MonoBehaviour {
	private static Toolbar Instance;
	[SerializeField] private GameObject toolbar;
	[SerializeField] private List<Button> elementButtons;
	[SerializeField] private List<Button> toolButtons;
	[SerializeField] private Color highlightColor, selectedColor;

	[SerializeField] private Texture2D inspectCursor, removeCursor, placeCursor;
	
	
	private int selectedElement = 0;
	private int selectedTool = 1;

	public int SelectedElement {
		get { return selectedElement; }
		set { selectedElement = value; }
	}

	public int SelectedTool {
		get { return selectedTool; }
	}

	private void Awake() {
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	// Start is called before the first frame update
	void Start() {

		SelectTool(1);
		
		foreach (var button in elementButtons) {
			var colorBlock = button.colors;
			colorBlock.highlightedColor = highlightColor;
			button.colors = colorBlock;
		}
		
		foreach (var button in toolButtons) {
			var colorBlock = button.colors;
			colorBlock.highlightedColor = highlightColor;
			button.colors = colorBlock;
		}
	}

	// Update is called once per frame
	void Update() { }

	public static Toolbar GetInstance() {
		return Instance;
	}

	public void SelectElement(int element) {
		var colorBlock = elementButtons[selectedElement].colors;
		colorBlock.normalColor = Color.white;
		elementButtons[selectedElement].colors = colorBlock;

		if (selectedTool > 0) {
			colorBlock = toolButtons[selectedTool - 1].colors;
			colorBlock.normalColor = Color.white;
			toolButtons[selectedTool - 1].colors = colorBlock;	
		}
		selectedTool = 0;

		selectedElement = element;

		colorBlock = elementButtons[selectedElement].colors;
		colorBlock.normalColor = selectedColor;
		elementButtons[selectedElement].colors = colorBlock;
		
		Cursor.SetCursor(placeCursor, new Vector2(placeCursor.width / 2, placeCursor.height / 2), CursorMode.Auto);
		
		LayoutRebuilder.ForceRebuildLayoutImmediate(toolbar.GetComponent<RectTransform>());
	}

	public void SelectTool(int tool) {
		ColorBlock colorBlock;
		if (selectedTool > 0) {
			colorBlock = toolButtons[selectedTool - 1].colors;
			colorBlock.normalColor = Color.white;
			toolButtons[selectedTool - 1].colors = colorBlock;
		}
		
		selectedTool = tool;

		colorBlock = toolButtons[selectedTool - 1].colors;
		colorBlock.normalColor = selectedColor;
		toolButtons[selectedTool - 1].colors = colorBlock;

		if (selectedTool == 1) {
			Cursor.SetCursor(inspectCursor, Vector2.zero, CursorMode.Auto);
		}
		else if (selectedTool == 2) {
			Cursor.SetCursor(removeCursor, Vector2.zero, CursorMode.Auto);
		}
		
		LayoutRebuilder.ForceRebuildLayoutImmediate(toolbar.GetComponent<RectTransform>());
	}
}
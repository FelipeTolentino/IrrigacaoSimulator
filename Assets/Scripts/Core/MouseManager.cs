using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MouseManager : MonoBehaviour {
	// Start is called before the first frame update
	void Start() { }

	// Update is called once per frame
	void Update() {
		RightClick();
		RightPress();
		LeftClick();
	}
	
	/* Casts a ray in the clicked position, if it hits a cell collider that
	 contains an element and the inspect tool is selected, open the inspector. 
	 If the cell has nothing, hide the inspector */
	void RightClick() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray);

			foreach (var hit in hits) {
				if (hit.collider.gameObject.CompareTag("Cell")) {
					var cell = hit.collider.gameObject.GetComponent<Cell>();
					var toolbar = Toolbar.GetInstance();
					if (toolbar.SelectedTool == 1) {
						if (cell.HasElement())
							Inspector.GetInstance().Show(cell.GetElement());
						else
							Inspector.GetInstance().Hide();
						
						Grid.GetInstance().SelectCell(cell.ID);
					}
				}
			}
		}
	}

	/* Casts a ray in the clicked position, if it hits a cell collider and:
		    No tool selected: then set the selected element in the cell
				Remove tool selected: remove the element the cell is holding */
	void RightPress() {
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			if (hit.collider?.gameObject.CompareTag("Cell") == true) {
				var cell = hit.collider.gameObject.GetComponent<Cell>();
				var toolbar = Toolbar.GetInstance();
				if (toolbar.SelectedTool == 0)
					cell.SetElement(Toolbar.GetInstance().SelectedElement);
				else if (toolbar.SelectedTool == 2) {
					cell.RemoveElement();
				}
			}
		}	
	}

	/* Casts a ray in the clicked position, if it hits a cell collider,
	 rotate the element the cell is holding */
	void LeftClick() {
		if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits;
			hits = Physics.RaycastAll(ray);

			foreach (var hit in hits) {
				if (hit.collider.gameObject.CompareTag("Cell")) {
					var cell = hit.collider.gameObject.GetComponent<Cell>();
					cell.RotateElement();
				}
			}
		}
	}
}
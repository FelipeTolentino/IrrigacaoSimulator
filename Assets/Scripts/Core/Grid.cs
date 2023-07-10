using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Grid : MonoBehaviour {
	private static Grid Instance;

	[SerializeField] private GameObject plane;
	[SerializeField] GameObject cellPrefab;
	[SerializeField] private int width, lenght;
	[SerializeField] private Color color1, color2;							// Colors for chess pattern
	[SerializeField] private Color colorSelected;								// Color for a selected cell

	[SerializeField] private List<GameObject> elementsPrefabs;  // Prefab of all possible element a cell may hold

	private List<GameObject> cells;															// List of all cells in the grid
	private Color defaultColor;                                 // Original color of a selected cell
	private int selectedCell = -1;															// Selected cell's ID
	
	public List<GameObject> Prefabs {
		get { return elementsPrefabs; }
	}

	public int Width {
		get { return width; }
	}

	void Awake() {
		/* Initializing the singleton */
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	// Start is called before the first frame update
	void Start() {
		cells = new List<GameObject>();
		AlignPlane();
		CreateGrid();
	}

	// Update is called once per frame
	void Update() { }

	public static Grid GetInstance() {
		return Instance;
	}
	
	/* Create a grid of cells with chess pattern */
	void CreateGrid() {
		int idPool = -1;

		for (int i = 0; i < lenght; i++) {
			for (int j = 0; j < width; j++) {
				GameObject cell = Instantiate(cellPrefab,
					new Vector3(0f + j, 0f, 0f + i),
					Quaternion.identity);
				cell.name = $"Cell {j + 1}x{i + 1}";
				if (i % 2 == 0) cell.GetComponent<Cell>().Initialize(++idPool, idPool % 2 == 0 ? color2 : color1);
				else cell.GetComponent<Cell>().Initialize(++idPool, idPool % 2 == 0 ? color1 : color2);

				cell.transform.parent = plane.transform;
				cells.Add(cell);
			}
		}
	}

	void AlignPlane() {
		plane.transform.position = new Vector3(width / 2, 0, lenght / 2);
	}

	/* Change the color of the chosen cell to indicate its selected. Before doing it,
	 if there is already a selected cell, change its color back to normal */
	public void SelectCell(int cellId) {
		if (selectedCell != -1) {
			cells[selectedCell].GetComponent<Cell>().Color = defaultColor;
		}
		selectedCell = cellId;
		var selected = cells[selectedCell].GetComponent<Cell>();
		defaultColor = selected.Color;
		selected.Color = colorSelected;
	}
	
	/* Check if a grid position (cell id) is valid */
	public bool IsPositionValid(int cellId) {
		return cellId >= 0 && cellId < cells.Count;
	}

	public Cell GetCell(int cellId) {
		return cells[cellId].GetComponent<Cell>();
	}
}
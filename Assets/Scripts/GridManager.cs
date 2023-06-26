using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{
    private static GridManager Instance;
    
    [SerializeField] GameObject cellPrefab;
    [SerializeField] private int width, lenght;
    [SerializeField] private Color color1, color2;
    [SerializeField] private Color colorSelected;

    private List<GameObject> cells;
    private int selectedId = -1;
    private Color defaultColor;

    public int SelectedCell
    {
        get { return selectedId; }
    }

    public List<GameObject> Cells
    {
        get { return cells; }
    }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cells = new List<GameObject>();
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GridManager GetInstance()
    {
        return Instance;
    }

    public void SelectCell(int cellId)
    {
        if (selectedId != cellId)
        {
            if (selectedId != -1)
                cells[selectedId].GetComponentInChildren<SpriteRenderer>().color = defaultColor;
            
            selectedId = cellId;
            defaultColor = cells[selectedId].GetComponentInChildren<SpriteRenderer>().color;
            cells[selectedId].GetComponentInChildren<SpriteRenderer>().color = colorSelected;
            
            //Gizmos.DrawWireCube(cells[selectedCell].transform.position, new Vector3(1f, 1f, 1f));
        }

        //Gizmos.DrawWireCube(cells[selectedCell].transform.position, new Vector3(1f, 1f, 1f));
    }
    
    void CreateGrid()
    {
        int idPool = -1;
        
        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject cell = Instantiate(cellPrefab, 
                    new Vector3(0f + j, 0f, 0f + i), 
                    Quaternion.identity);
                cell.name = $"Cell {j + 1}x{i + 1}";
                if (i % 2 == 0) cell.GetComponent<CellBehavior>().Initialize(++idPool, idPool % 2 == 0 ? color2 : color1);
                else cell.GetComponent<CellBehavior>().Initialize(++idPool, idPool % 2 == 0 ? color1 : color2);
                
                cells.Add(cell);
            }
        }
    }
}

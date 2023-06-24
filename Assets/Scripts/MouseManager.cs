using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MouseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Cell"))
                {
                    int cellId = hit.collider.gameObject.GetComponent<CellBehavior>().CellID;
                    GridManager.GetInstance().SelectCell(cellId);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);
            
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Pipe"))
                    {
                        var pipe = hit.collider.gameObject.GetComponent<PipeBehavior>();
                        pipe.RotatePipe();
                        GridManager.GetInstance().SelectCell(pipe.OnCell);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            int selectedCell = GridManager.GetInstance().SelectedCell;
            var cells = GridManager.GetInstance().Cells;
            cells[selectedCell].GetComponent<CellBehavior>().SetPipeI();
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            int selectedCell = GridManager.GetInstance().SelectedCell;
            var cells = GridManager.GetInstance().Cells;
            cells[selectedCell].GetComponent<CellBehavior>().SetPipeL();
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            int selectedCell = GridManager.GetInstance().SelectedCell;
            var cells = GridManager.GetInstance().Cells;
            cells[selectedCell].GetComponent<CellBehavior>().SetPipePlus();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            int selectedCell = GridManager.GetInstance().SelectedCell;
            var cells = GridManager.GetInstance().Cells;
            cells[selectedCell].GetComponent<CellBehavior>().SetPipeT();
        }
    }

}

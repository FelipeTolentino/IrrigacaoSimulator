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
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Cell"))
                {
                    int cellId = hit.collider.gameObject.GetComponent<CellBehavior>().CellID;
                    StartCoroutine(GridManager.GetInstance().SelectCell(cellId));
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit;
            
            hit = Physics.RaycastAll(ray);

            GameObject cell = null;

            if (hit.Length > 0)
            {
                
                // Busca a celula do pipe no hit
                foreach (var i in hit)
                {
                    if (i.collider.gameObject.CompareTag("Cell"))
                        cell = i.collider.gameObject;
                }
                
                // Busca o pipe no hit
                foreach (var i in hit)
                {
                    if (i.collider.gameObject.CompareTag("Pipe"))
                    {
                        if(cell != null)
                        {
                            int cellId = cell.GetComponent<CellBehavior>().CellID;
                            StartCoroutine(GridManager.GetInstance().SelectCell(cellId));
                        }
                        
                        StartCoroutine(i.collider.gameObject.GetComponent<PipeBehavior>().RotatePipe());
                    }
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                if (hit.collider.gameObject.tag == "Cell")
                {
                    int cellId = hit.collider.gameObject.GetComponent<CellBehavior>().CellID;
                    StartCoroutine(GridManager.GetInstance().SelectCell(cellId));
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.CompareTag("Cell"))
                    {
                        int cellId = hits[i].collider.gameObject.GetComponent<CellBehavior>().CellID;
                        StartCoroutine(GridManager.GetInstance().SelectCell(cellId));
                    }
                    if (hits[i].collider.gameObject.CompareTag("Pipe"))
                    {
                        StartCoroutine(hits[i].collider.gameObject.GetComponent<PipeBehavior>().RotatePipe());
                    }
                }
            }
        }   
    }

}

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
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Pipe")
                {
                    StartCoroutine(hit.collider.gameObject.GetComponent<PipeBehavior>().RotatePipe());
                }
            }
        }
    }

}

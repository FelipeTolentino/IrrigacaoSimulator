using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pipeIPrefab, pipeLPrefab;
    
    private int cellId;
    private bool hasPipe = false;

    public int CellID
    {
        get { return cellId; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPipeI()
    {
        if (hasPipe) return;
        hasPipe = true;
        Instantiate(pipeIPrefab, 
            new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z), 
            Quaternion.identity);
    }

    public void SetPipeL()
    {
        if (hasPipe) return;
        Instantiate(pipeLPrefab, 
            new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z),
            Quaternion.identity);
    }

    public void Initialize(int id, Color color)
    {
        cellId = id;
        GetComponentInChildren<SpriteRenderer>().color = color;
    }
}

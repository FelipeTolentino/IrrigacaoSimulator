using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pipeIPrefab, pipeLPrefab;
    
    private int cellId;
    private int pipeType;

    private GameObject myPipe;

    public int CellID
    {
        get { return cellId; }
    }

    public int PipeType
    {
        get { return pipeType; }
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
        if (pipeType == 1) return;
        if (pipeType == 2)
            Destroy(myPipe);
        pipeType = 1;
        GameObject pipe = Instantiate(pipeIPrefab, 
            new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z), 
            Quaternion.identity);

        pipe.GetComponent<PipeBehavior>().OnCell = cellId;
        myPipe = pipe;
    }

    public void SetPipeL()
    {
        if (pipeType == 2) return;
        if (pipeType == 1)
            Destroy(myPipe);
        pipeType = 2;
        GameObject pipe = Instantiate(pipeLPrefab, 
            new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z),
            Quaternion.identity);
        
        pipe.GetComponent<PipeBehavior>().OnCell = cellId;
        myPipe = pipe;
    }

    public void Initialize(int id, Color color)
    {
        cellId = id;
        GetComponentInChildren<SpriteRenderer>().color = color;
    }
}

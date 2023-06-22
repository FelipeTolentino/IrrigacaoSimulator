using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBehavior : MonoBehaviour
{
    private int onCell;

    public int OnCell
    {
        get { return onCell; }
        set { onCell = value;  }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotatePipe()
    {
        transform.Rotate(Vector3.up, 90f);
        if (transform.rotation.y >= 360)
            transform.Rotate(Vector3.up, -360);
    }
}

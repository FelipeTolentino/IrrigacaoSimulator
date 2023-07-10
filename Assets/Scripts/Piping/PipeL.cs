using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeL : Piping {
	// Start is called before the first frame update
	protected override void Start() {
		displayName = "Joelho";
		hasConnection = new bool[4];
		
		/* Initial positioning of the L pipe allow connection only
		   in the left and right top */
		hasConnection[0] = hasConnection[1] = true;
		
		base.Start();
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
	}

	/* Rotates the pipe and changes is points of connection accordingly.
		 Then do the flow update sequence and update the neighbors */
	public override void Rotate() {
		base.Rotate();
		
		hasConnection = new bool[4];
		if (rotation == 0)
			hasConnection[0] = hasConnection[1] = true;
		else if (rotation == 90)
			hasConnection[1] = hasConnection[2] = true;
		else if (rotation == 180) 
			hasConnection[2] = hasConnection[3] = true;
		else if (rotation == 270) 
			hasConnection[0] = hasConnection[3] = true;

		CheckIncomingFlow();
		CheckFlow();
		CheckOutgoingFlow();
		prevFlowOrientation = (FlowOrientation[]) flowOrientation.Clone();
		UpdateNeighbors();
	}
}
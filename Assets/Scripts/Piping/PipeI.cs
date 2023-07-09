using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeI : Piping {
	// Start is called before the first frame update
	protected override void Start() {
		displayName = "Tubo";
		hasConnection = new bool[4];
		
		/* Initial positioning of the I pipe allow connection only
		   in the left and right sides */
		hasConnection[0] = hasConnection[2] = true;
		
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
		if (rotation == 90 || rotation == 270) 
			hasConnection[1] = hasConnection[3] = true;
		else
			hasConnection[0] = hasConnection[2] = true;

		CheckIncomingFlow();
		CheckFlow();
		CheckOutgoingFlow();
		prevFlowOrientation = (FlowOrientation[]) flowOrientation.Clone();
		UpdateNeighbors();
	}
}
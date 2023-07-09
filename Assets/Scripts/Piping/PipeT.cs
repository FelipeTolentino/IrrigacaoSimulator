using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeT : Piping {
	// Start is called before the first frame update
	protected override void Start() {
		displayName = "Conexão T";
		
		/* Initial positioning of the T pipe allow connection
		   in all sides but the left */
		hasConnection = Enumerable.Repeat(true, 4).ToArray();
		hasConnection[0] = false;
		
		
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
		
		hasConnection = Enumerable.Repeat(true, 4).ToArray();
		if (rotation == 0)
			hasConnection[0] = false;
		else if (rotation == 90)
			hasConnection[1] = false;
		else if (rotation == 180)
			hasConnection[2] = false;
		else if (rotation == 270)
			hasConnection[3] = false;
		
		CheckIncomingFlow();
		CheckFlow();
		CheckOutgoingFlow();
		prevFlowOrientation = (FlowOrientation[]) flowOrientation.Clone();
		UpdateNeighbors();
	}
}
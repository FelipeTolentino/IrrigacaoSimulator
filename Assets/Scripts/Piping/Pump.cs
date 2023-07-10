using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pump : Piping {
	private bool on = true;
	
// Start is called before the first frame update
	protected override void Start() {
		displayName = "Bomba";
		source = true;
		
		/* Defining the grid positions in the L, U, R, D directions */
		var grid = Grid.GetInstance();
		adjacentPos = new int[4];
		adjacentPos[0] = position - 1;
		adjacentPos[1] = position + grid.Width;
		adjacentPos[2] = position + 1;
		adjacentPos[3] = position - grid.Width;
		
		/* Initial positioning of the T pipe allow connection only
		   in the right side */
		hasConnection = new bool[4];
		hasConnection[2] = true;
		
		/* Pump always has flow */
		hasFlow = true;
		
		/* Pump starts with flow coming out in the right side */
		flowOrientation = new FlowOrientation[4];
		flowOrientation[2] = FlowOrientation.ComingOut;

		blocksContent = new List<IBlock>();
		blocksContent.Add(new InformationBlock("Informação", "00000"));
		blocksContent.Add(new ToggleBlock("Ligada", "Desligada", on));
		
		UpdateNeighbors();
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
	}

	/* Switch on and off then update neighbors*/
	public override void ToggleBlockResponse(int blockContentRef) {
		((ToggleBlock)blocksContent[blockContentRef]).State = on = !on;

		if (on) {
			for (int side = 0; side < 4; side++) {
				if (hasConnection[side]) {
					flowOrientation[side] = FlowOrientation.ComingOut;
					hasFlow = true;
				}
			}
		}
		else {
			flowOrientation = new FlowOrientation[4];
			hasFlow = false;
		}
		
		UpdateNeighbors();
	}

	/* Rotates the pipe and changes is points of connection and
	   flow orientation accordingly. Then do the flow update sequence 
	   and update the neighbors */
	public override void Rotate() {
		base.Rotate();
		
		hasConnection = new bool[4];
		flowOrientation = new FlowOrientation[4];
		
		if (rotation == 0) {
			hasConnection[2] = true;
			flowOrientation[2] = FlowOrientation.ComingOut;
		}
		if (rotation == 90) {
			hasConnection[3] = true;
			flowOrientation[3] = FlowOrientation.ComingOut;
		}
		else if (rotation == 180) {
			hasConnection[0] = true;
			flowOrientation[0] = FlowOrientation.ComingOut;
		}
		else if (rotation == 270) {
			hasConnection[1] = true;
			flowOrientation[1] = FlowOrientation.ComingOut;
		}
		
		UpdateNeighbors();
	}
}
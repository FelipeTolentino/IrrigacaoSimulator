using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FlowOrientation {
	None,
	ComingIn,
	ComingOut,
	Source
}

public class Piping : Element {
	[SerializeField] protected List<GameObject> wetParts;
	[SerializeField] protected Material emptyMaterial, filledMaterial, currentMaterial;

	
	protected bool hasFlow;                           // Indicates if this pipe has flow
	protected bool[] hasConnection;                   // Indicates in which directions (L, U, R, D) the pipe can connect
	protected FlowOrientation[] flowOrientation;      // Indicates the flow orientation on the pipe side (L, U, R, D) */
	protected bool source;                            // Indicates if the pipe is a flow source
	protected FlowOrientation[] prevFlowOrientation;  // Holds the previous flow orientation after the flow updates
	
	
	public bool HasFlow {
		get { return hasFlow; }
		set { hasFlow = value; }
	}

	// Start is called before the first frame update
	protected override void Start() {
		base.Start();
		
		/* Add information to be displayed in the inspector */
		blocksContent.Add(new InformationBlock("Vazão", "0,000 m³/h"));
		
		/* Initial flow check and set */
		CheckIncomingFlow();
		CheckFlow();
		CheckOutgoingFlow();
		prevFlowOrientation = (FlowOrientation[]) flowOrientation.Clone();
		UpdateNeighbors();
	}

	// Update is called once per frame
	protected virtual void Update() {
		/* Changes the pipe material to match its flow state */
		if (hasFlow && currentMaterial != filledMaterial) {
			GetWet();
		}
		else if (!hasFlow && currentMaterial != emptyMaterial){
			GetDry();
		}
	}

	/* Check in the directions (L, U, R, D) in which the pipe can connect, if
	   neighbor pipes (if any) have flow coming out. If they do, set the pipe
	   flow in that direction as Coming In. */
	protected void CheckIncomingFlow() {
		flowOrientation = new FlowOrientation[4];
		var grid = Grid.GetInstance();
		//
		for (int side = 0; side < 4; side++) {
			if (grid.IsPositionValid(adjacentPos[side]) && hasConnection[side]) {
				int adjCellSide = side < 2 ? side + 2 : side - 2;
				var adjCellPiping = grid.GetCell(adjacentPos[side]).GetPiping();
				if (adjCellPiping?.hasConnection[adjCellSide] == true &&
				    adjCellPiping.flowOrientation[adjCellSide] == FlowOrientation.ComingOut) {
					flowOrientation[side] = FlowOrientation.ComingIn;
				}
			}
		}
	}
	
	/* Set the flow in the pipe to true if there is any flow coming in and false
	   otherwise */
	protected void CheckFlow() {
		for (int side = 0; side < 4; side++) {
			if (flowOrientation[side] == FlowOrientation.ComingIn) {
				hasFlow = true;
				InvokeRepeating(nameof(UpdateBlocksContent), 0, 1f);
				return;
			}
		}

		hasFlow = false;
		CancelInvoke();
	}

	/* If the pipe has flow, sets the flow orientation to 'Coming Out' in all
	   directions where its not coming in */
	protected void CheckOutgoingFlow() {
		for (int side = 0; side < 4; side++) {
			if (hasFlow)
				if (flowOrientation[side] != FlowOrientation.ComingIn)
					flowOrientation[side] = FlowOrientation.ComingOut;
		}
	}

	/* Look in all directions (L, U, R, D), if it has a pipe, make it update */
	protected void UpdateNeighbors() {
		var grid = Grid.GetInstance();
		for (int side = 0; side < 4; side++) {
			if (grid.IsPositionValid(adjacentPos[side])) {
				var adjacentPiping = grid.GetCell(adjacentPos[side]).GetPiping();
				if (adjacentPiping?.source == false) {
					adjacentPiping?.UpdateFlow(position, position);
				}
			}
		}
	}

	/* Do the flow update sequence (check flow coming out in neighbors, define
	   if has flow and where it will coming out). If there is any change in flow,
	   update it's neighbors (but not the neighbor that updated it, and the update
	   chain starter) */
	protected void UpdateFlow(int callerPos, int chainStartPos) {
		CheckIncomingFlow();
		CheckFlow();
		CheckOutgoingFlow();

		if (!CheckChanges()) return;
			prevFlowOrientation = (FlowOrientation[]) flowOrientation.Clone();
		
		var grid = Grid.GetInstance();
		for (int side = 0; side < 4; side++) {
			if (grid.IsPositionValid(adjacentPos[side]) &&
			    adjacentPos[side] != callerPos &&
			    adjacentPos[side] != chainStartPos) {
				var adjacentPiping = grid.GetCell(adjacentPos[side]).GetPiping();
				if (adjacentPiping?.source == false) {
					adjacentPiping?.UpdateFlow(position, chainStartPos);
				}
			}
		}
	}

	/* Check if there is any change in flow orientation */
	bool CheckChanges() {
		return !Enumerable.SequenceEqual(prevFlowOrientation, flowOrientation);
	}
	
	protected virtual void UpdateBlocksContent() {
		foreach (InformationBlock blockContent in blocksContent) {
			blockContent.Value = Random.Range(0.6234f, 0.6314f).ToString("0.0000", CultureInfo.InvariantCulture) + " m³/h";
		}
	}

	public override void Rotate() {
		base.Rotate();
	}

	/* Change the pipe material to indicate its filled */
	protected virtual void GetWet() {
		currentMaterial = filledMaterial;
		foreach (var part in wetParts) {
			part.GetComponent<Renderer>().material = filledMaterial;
		}
	}

	/* Change the pipe material to indicate its empty */
	protected void GetDry() {
		currentMaterial = emptyMaterial;
		foreach (var part in wetParts) {
			part.GetComponent<Renderer>().material = emptyMaterial;
		}
	}

	/* Reset flow orientation in all directions, update neighbors and
	   destroy itself */
	public override void AutoDestroy() {
		flowOrientation = new FlowOrientation[4];
		UpdateNeighbors();
		base.AutoDestroy();
	}
}
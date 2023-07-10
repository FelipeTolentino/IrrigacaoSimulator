using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Valve : Piping {
	[SerializeField] private GameObject handle;

	private bool open = true;
	
	// Start is called before the first frame update
	protected override void Start() {
		displayName = "Registro";
		hasConnection = new bool[4];
		hasConnection[0] = hasConnection[2] = true;
		
		base.Start();
		
		blocksContent.Add(new ToggleBlock("Aberto", "Fechado", true));
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
	}
	
	protected internal override void UpdateFlow(int callerPos, int chainStartPos) {
		CheckIncomingFlow();
		CheckFlow();
		CheckOutgoingFlow();

		if (!open && !CheckChanges()) return;
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

	protected override void UpdateBlocksContent() {
		foreach (var blockContent in blocksContent) {
			if (blockContent.GetType() == typeof(InformationBlock))
				((InformationBlock)blockContent).Value = Random.Range(
					0.6234f, 
					0.6314f).ToString("0.0000", CultureInfo.InvariantCulture) + 
				  " m³/h";
		}
	}

	/* Open and close than update neighbors */
	public override void ToggleBlockResponse(int blockContentRef) {
		((ToggleBlock)blocksContent[blockContentRef]).State = open = !open;

		if (open) {
			CheckOutgoingFlow();
			GetWet();
		}
		else {
			for (int side = 0; side < 4; side++) {
				if (flowOrientation[side] != FlowOrientation.ComingIn) {
					flowOrientation[side] = FlowOrientation.None;
				}
			}
			GetDry();
			GetWet();
		}
		UpdateNeighbors();
		TurnHandle();
	}
	
	protected override void GetWet() {
		currentMaterial = filledMaterial;
		for (int i = 0; i < (open ? 2 : 1); i++) {
			wetParts[i].GetComponent<Renderer>().material = filledMaterial;
		}
	}
	
	void TurnHandle() {
		handle.transform.Rotate(Vector3.up, handle.transform.rotation.y == 0 ? 90 : -90);
	}
}
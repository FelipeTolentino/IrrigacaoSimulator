using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Element : MonoBehaviour {
	protected int position;									// Position of the element in the grid (cell id)
	protected int rotation;									// Current degree of rotation
	protected int[] adjacentPos;						// Positions in the grid (cells id) in the L, U, R, D directions
	protected string displayName;						// Name displayed in the inspector
	protected List<IBlock> blocksContent;   // Content of the blocks to be displayed in the inspector

	public List<IBlock> BlocksContent {
		get { return blocksContent; }
	}
	
	// Start is called before the first frame update
	protected virtual void Start() {
		/* Defining the grid positions in the L, U, R, D directions */
		var grid = Grid.GetInstance();
		adjacentPos = new int[4];
		adjacentPos[0] = position - 1;
		adjacentPos[1] = position + grid.Width;
		adjacentPos[2] = position + 1;
		adjacentPos[3] = position - grid.Width;

		blocksContent = new List<IBlock>();
	}

	public string DisplayName {
		get { return displayName; }
	}
	
	// Update is called once per frame
	void Update() { }

	public void Initialize(int cellId) {
		position = cellId;
	}

	/* Increases the object rotation 90º each call until it reaches
	 270º, then resets to 0 */
	public virtual void Rotate() {
		rotation += (rotation + 90 >= 360) ? -270 : 90;
		transform.Rotate(Vector3.up, rotation != 0 ? 90 : -270);
	}
	
	public IBlock GetBlockContent(int queuePos) {
		return blocksContent[queuePos];
	}
	
	/* Called when a toggle in the inspector is pressed */
	public virtual void ToggleBlockResponse(int blockContentRef) {}

	public virtual void AutoDestroy() {
		Destroy(gameObject);
	}
}
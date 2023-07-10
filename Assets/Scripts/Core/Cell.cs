using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Cell : MonoBehaviour {
	[SerializeField] private SpriteRenderer sprite;
	
	private int id;
	private GameObject currElement;			// Current element the cell its holding
	private float rotation;							// Current degree of rotation
	private int currElementType = -1;

	public int ID {
		get { return id; }
	}
	
	public Color Color {
		get { return sprite.color; }
		set { sprite.color = value; }
	}
	
	// Start is called before the first frame update
	void Start() { }

	// Update is called once per frame
	void Update() {
	}

	public void Initialize(int id, Color color) {
		this.id = id;
		GetComponentInChildren<SpriteRenderer>().color = color;
	}

	/* Spawn the chosen element (if the current element is different) centralized
	   above the cell and set it as cell children. If there is already any element
	   in the cell, destroy it before the new one is set */
	public void SetElement(int chosenElementType) {
		// If the cell already contains the same element, do nothing
		if (currElementType == chosenElementType) return;

		// Instantiate the element above the cell centralized
		Destroy(currElement);
		var prefab = Grid.GetInstance().Prefabs[chosenElementType];
		// Height offset for the objects...
		float heightOffset = chosenElementType != 5 ? 0.05f : 0.082f;
		currElement = Instantiate(
			prefab,
			new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z),
			Quaternion.identity);

		currElement.GetComponent<Element>().Initialize(id);
		currElement.transform.parent = transform;
		currElementType = chosenElementType;
	}

	public void RotateElement() {
		currElement?.GetComponent<Element>().Rotate();
	}

	public Piping GetPiping() {
		if (currElementType < 0) return null;
		return currElement.GetComponent<Piping>();
	}

	public bool HasElement() {
		return currElement;
	}

	public Element GetElement() {
		return currElement?.GetComponent<Element>();
	}
	
	public void RemoveElement() {
		currElement?.GetComponent<Element>().AutoDestroy();
		currElement = null;
		currElementType = -1;
	}
}
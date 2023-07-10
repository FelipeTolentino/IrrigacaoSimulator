using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipePlus : Piping {
	// Start is called before the first frame update
	protected override void Start() {
		displayName = "Conexão Cruz";
		
		/* The plus pipe can connect in all sides */
		hasConnection = Enumerable.Repeat(true, 4).ToArray();
		
		
		base.Start();
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
	}

	/* Rotation of the plus pipe doesn't change its points
	   of connection */
	public override void Rotate() {
		base.Rotate();
	}
}
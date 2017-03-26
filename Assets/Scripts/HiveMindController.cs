using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMindController : MonoBehaviour {

	private Vector2 input;
	private PlayerController[] children;

	// Use this for initialization
	void Start () {
		children = GetComponentsInChildren<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!AnyoneMoving()) {
			input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
			{
				input.y = 0;
			}
			else
			{
				input.x = 0;
			}
			foreach (PlayerController pc in children) {
				if (pc.isPossessed) {
					pc.TryToMove (input);
				}
			}
		}
	}

	public bool AnyoneMoving() {
		foreach (PlayerController pc in children) {
			if (pc.isMoving) {
				return true;
			}
		}
		return false;
	}

	public void CheckHasWon() {
		foreach (PlayerController pc in children) {
			if (!pc.isPossessed) {
				return;
			}
		}

	}
}

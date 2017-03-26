using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiveMindController : MonoBehaviour {

	public PlayerController exitPerson { get; set; }

	private Vector2 input;
	private PlayerController[] children;
	private bool isWinChecking;

	void Awake() {
		children = GetComponentsInChildren<PlayerController> ();
		isWinChecking = false;
		exitPerson = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit ();
		}
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
		if (!isWinChecking) {
			isWinChecking = true;
			if (exitPerson == null) {
				isWinChecking = false;
				return;
			}
			foreach (PlayerController pc in children) {
				if (!pc.isPossessed) {
					isWinChecking = false;
					return;
				}
				pc.isChecked = false;
			}

			if (exitPerson != null && exitPerson.ChildrenAround () == children.Length) {
				
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);

			}
			isWinChecking = false;
		}
	}
}

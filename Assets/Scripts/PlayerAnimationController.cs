using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

	// Animations go North, South, East, West
	public AnimationClip[] standingUnpossessed;
	public AnimationClip[] standingPossessed;
	public AnimationClip[] walkingPossessed;

	private PlayerController pc;
	private Animation animate;

	void Awake () {
		pc = GetComponent<PlayerController> ();
		if (standingPossessed.Length != 4 || standingPossessed.Length != 4 || walkingPossessed.Length != 4) {
			Debug.LogError ("Not enough animations in PlayerAnimationController for player at " + transform.position.x.ToString () + ", " + transform.position.y.ToString ());
		}
		animate = GetComponent<Animation> ();
	}

	// Use this for initialization
	void Start () {
		animate.AddClip (standingPossessed [0], "Standing Possessed North");
		animate.AddClip (standingPossessed [1], "Standing Possessed South");
		animate.AddClip (standingPossessed [2], "Standing Possessed East");
		animate.AddClip (standingPossessed [3], "Standing Possessed West");
		animate.AddClip (standingUnpossessed [0], "Standing Unpossessed North");
		animate.AddClip (standingUnpossessed [1], "Standing Unpossessed South");
		animate.AddClip (standingUnpossessed [2], "Standing Unpossessed East");
		animate.AddClip (standingUnpossessed [3], "Standing Unpossessed West");
		animate.AddClip (walkingPossessed [0], "Walking Possessed North");
		animate.AddClip (walkingPossessed [1], "Walking Possessed South");
		animate.AddClip (walkingPossessed [2], "Walking Possessed East");
		animate.AddClip (walkingPossessed [3], "Walking Possessed West");
		Debug.Log(animate.GetClipCount ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetAnimation () {
		string animationName = "";
		if (!pc.isMoving) {
			animationName += "Standing ";
			if (pc.isPossessed) {
				animate.clip = standingPossessed [pc.currentDir.IntVal ()];
				animate.Play ();
//				animationName += "Possessed ";
//				animationName += pc.currentDir.ToString ();
//				animate.Play (animationName);
			} else {
				animate.clip = standingUnpossessed [pc.currentDir.IntVal ()];
				animate.Play ();
//				animationName += "Unpossessed ";
//				animationName += pc.currentDir.ToString ();
//				animate.Play (animationName);
			}
		} else {
			animate.clip = walkingPossessed [pc.currentDir.IntVal ()];
			animate.Play ();
//			animationName += "Walking Possessed ";
//			animationName += pc.currentDir.ToString ();
//			animate.Play (animationName);
		}
	}
}

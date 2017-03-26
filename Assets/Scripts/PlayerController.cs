﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public Direction startingDirection;
    public bool startPossessed;

    public Direction currentDir { get; set; }
    public bool isPossessed { get; set; }

    private SpriteRenderer spRender;
	private Animator anim;

    private Vector2 input;
	public bool isMoving { get; set; }
	private bool wasMoving;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;

    // Use this for unconditional initialization
    private void Awake()
    {
        this.isMoving = false;
        this.spRender = this.gameObject.GetComponent<SpriteRenderer>();
		this.anim = GetComponent<Animator> ();
        this.currentDir = startingDirection;
		this.stand ();
        if (this.startPossessed)
        {
            this.BecomePossessed();
        }
        else
        {
            this.isPossessed = false;
        }
    }

    // Use this for conditional initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (this.isPossessed && !this.isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }
			if (input != Vector2.zero) {
                Direction newDir = Direction.West;
				if (input.x < 0) {
					newDir = Direction.West;
				}
				if (input.x > 0) {
					newDir = Direction.East;
				}
				if (input.y < 0) {
					newDir = Direction.South;
				}
				if (input.y > 0) {
					newDir = Direction.North;
				}
                if (currentDir != newDir) {
                    wasMoving = false;
                }
                currentDir = newDir;


				if (!this.HasObstacle (currentDir)) {
					switch (currentDir) {
					case Direction.North:
						if (!wasMoving) {
							anim.SetTrigger ("north");
						}
                            // play the North walking animation
						break;

					case Direction.South:
						if (!wasMoving) {
							anim.SetTrigger ("south");
						}
                            // play the South walking animation
						break;
					case Direction.East:
						if (!wasMoving) {
							anim.SetTrigger ("east");
						}
                            // play the East walking animation
						break;
					case Direction.West:
						if (!wasMoving) {
							anim.SetTrigger ("west");
						}
                            // play the West walking animation
						break;
					}

					StartCoroutine (Move (this.transform));
				}

			} else if (Input.GetKeyDown (KeyCode.Space)) {
				RaycastHit2D hit = Physics2D.Raycast (this.transform.position +
                    new Vector3(currentDir.GetVector().x, currentDir.GetVector().y, 0), currentDir.GetVector ());
				if (hit.collider != null) {
					GameObject otherThing = hit.collider.gameObject;
					if (otherThing.CompareTag ("Person") &&
					                   otherThing.GetComponent<PlayerController> ().currentDir == this.currentDir.Opposite ()) {
						otherThing.GetComponent<PlayerController> ().BecomePossessed ();
					}
				}
			} else if (wasMoving) {
				wasMoving = false;
				this.stand ();
			}

        }

	}

    public IEnumerator Move (Transform entity)
    {
        this.isMoving = true;
        this.startPos = entity.position;
        this.t = 0;

        this.endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

        while (t < 1.0f)
        {
            t += Time.deltaTime * walkSpeed;
            entity.position = Vector3.Lerp(this.startPos, this.endPos, t);
            yield return null;
        }

        isMoving = false;
		wasMoving = true;
        yield return 0;
    }

    public void BecomePossessed ()
    {
        this.isPossessed = true;
    }


	public bool HasObstacle(Direction dir) {
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position + 
			new Vector3(currentDir.GetVector().x, currentDir.GetVector().y, 0), currentDir.GetVector(), 0.4f);
		if (hit.collider == null) {
			return false;
		} else if (hit.collider.gameObject.CompareTag ("Person") &&
		         hit.collider.gameObject.GetComponent<PlayerController> ().isPossessed) {
			return hit.collider.gameObject.GetComponent<PlayerController> ().HasObstacle (dir);
		} else {
			return true;
		}
	}

	private void stand() {
        switch (currentDir) {
            case Direction.North:
                anim.SetTrigger("standNorth");
                break;
            case Direction.South:
                anim.SetTrigger("standSouth");
                break;
            case Direction.East:
                anim.SetTrigger("standEast");
                break;
            default:
                anim.SetTrigger("standWest");
                break;
        }
	}
}


public enum Direction
{
    North,
    South,
    East,
    West
}

public static class DirectionMethods
{
    public static Direction Opposite(this Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            default:
                return Direction.East;
        }
    }

    public static Vector2 GetVector(this Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                return Vector2.up;
            case Direction.South:
                return Vector2.down;
            case Direction.East:
                return Vector2.right;
            default:
                return Vector2.left;
        }
    }
}
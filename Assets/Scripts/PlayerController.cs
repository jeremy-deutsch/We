using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public Direction startingDirection;
    public bool startPossessed;

    public Direction currentDir { get; set; }
    public bool isPossessed { get; set; }

    private SpriteRenderer spRender;

    private Vector2 input;
    private bool isMoving;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;

    // Use this for unconditional initialization
    private void Awake()
    {
        this.isMoving = false;
        this.spRender = this.gameObject.GetComponent<SpriteRenderer>();
        this.currentDir = startingDirection;
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
            if (input != Vector2.zero)
            {
                if (input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if (input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, currentDir.GetVector(), 1.0f);
                if (hit.collider == null)
                {
                    switch (currentDir)
                    {
                        case Direction.North:
                            // play the North walking animation
                            break;
                        case Direction.South:
                            // play the South walking animation
                            break;
                        case Direction.East:
                            // play the East walking animation
                            break;
                        case Direction.West:
                            // play the West walking animation
                            break;
                    }

                    StartCoroutine(Move(this.transform));
                }

            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, currentDir.GetVector());
                if (hit.collider != null)
                {
                    GameObject otherThing = hit.collider.gameObject;
                    if (otherThing.CompareTag("Person") && 
                        otherThing.GetComponent<PlayerController>().currentDir == this.currentDir.Opposite())
                    {
                        otherThing.GetComponent<PlayerController>().BecomePossessed();
                    }
                }
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
        yield return 0;
    }

    public void BecomePossessed ()
    {
        this.isPossessed = true;
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
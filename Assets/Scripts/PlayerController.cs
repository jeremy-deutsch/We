using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public Direction startingDirection;
    public bool startPossessed;

    private SpriteRenderer spRender;

    private bool possessed;
    private Direction currentDir;
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
        if (this.startPossessed)
        {
            this.BecomePossessed();
        }
        else
        {
            this.possessed = false;
        }
    }

    // Use this for conditional initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (this.possessed && !this.isMoving)
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
        this.possessed = true;
    }
}


public enum Direction
{
    North,
    South,
    East,
    West
}
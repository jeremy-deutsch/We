using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;

    Direction currentDir;
    private Vector2 input;
    private bool isMoving;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;

    // Use this for unconditional initialization
    private void Awake()
    {
        this.isMoving = false;
    }

    // Use this for conditional initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (!this.isMoving)
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
                StartCoroutine(Move(this.transform));
            }
        }

	}

    public IEnumerator Move(Transform entity)
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
}


enum Direction
{
    North,
    South,
    East,
    West
}
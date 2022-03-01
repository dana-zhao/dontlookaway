using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement2 : MonoBehaviour
{
    public CharacterController controller;
    public bool isSeen;
    public bool isFacing;
    // public float speed = 5f;
    public GameObject target;
    public string targetName = "Player";
    float timeLeft = 1.5f;
    float flippDelay = 1.5f;

    public GameStatus gameStatus;

    // keep track of player's path
    private List<Vector3> Q = new List<Vector3>();

    // boolean for monster condition
    public bool flipp = false;

    // Keeping track of the last player position seen
    Vector3 lastPositionSeen;

    // Check if in line of sight
    public bool isVisible = true;
    private Vector3 oldPosition;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        lastPositionSeen = target.transform.position;
        flipp = false;
        //controller.enabled = false;
        gameStatus = GameObject.FindObjectOfType<GameStatus>();

    }

    void controllerMove(Vector3 position, Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - position;
        Vector3 movement = direction.normalized * speed;
        if (movement.magnitude > direction.magnitude) movement = direction;
        controller.Move(movement);
        transform.LookAt(target.transform);
    }

    void movement(float desiredSpeed)
    {
        if (!isSeen)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, desiredSpeed * Time.deltaTime);
            transform.LookAt(target.transform);
            flippMovement(desiredSpeed);
        }
        return;
        if (isFacing && !isSeen)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                transform.position = target.transform.position - target.transform.forward * 50;
            }
        }
        else timeLeft = 1.5f;
    }

    void flippMovement(float desiredSpeed)
    {
        // add burst to prevent stuck
        float dist = Vector3.Distance(target.transform.position, transform.position);
        float actualSpeed = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        if (actualSpeed < desiredSpeed) speed *= 1.27f;
        else speed = desiredSpeed;
        oldPosition = transform.position;

        if (isSeen && desiredSpeed > 0)
        {
            speed *= 2.57f;
            speed += .7f;
        }
        // if the player is in direct line of sight, then move straight to the player
        if (isVisible)
        {
            Q.Clear();
            print("yes");
            lastPositionSeen = target.transform.position + new Vector3(0, 0, 0); ;
            controllerMove(transform.position, lastPositionSeen, speed * Time.deltaTime);

        }
        if (Q.Count == 0) Q.Add(lastPositionSeen);
        else if (Q.Count == 1) Q.Add(target.transform.position);
        else
        {
            //if (isInSight(Q[Q.Count - 2], target)) Q[Q.Count - 1] = target.transform.position;
            //else if (Vector3.Distance(target.transform.position, Q[Q.Count - 1]) > 0.1)
            //    Q.Add(target.transform.position);

            bool isAdded = false;
            for (int i = 1; i < Q.Count - 1; i++)
            {
                if (isInSight(Q[i - 1], target))
                {
                    Q[i + 1] = target.transform.position + new Vector3(0, 0, 0);
                    Q.RemoveRange(i + 2, Q.Count - i - 2);
                    isAdded = true;
                    break;
                }
            }
            if (!isAdded && Vector3.Distance(target.transform.position, Q[Q.Count - 1]) > 0.1)
                Q.Add(target.transform.position);
        }
        Vector3 destination = Q[0];
        controllerMove(transform.position, destination, speed * Time.deltaTime * 1.7f);
        if (transform.position.x - destination.x < 0.5f && transform.position.z - destination.z < 0.5f)
            Q.RemoveAt(0);
        //if (Vector3.Distance(transform.position, destination) < 1f) Q.RemoveAt(0);
    }


    private bool isBodyInSight()
    {
        RaycastHit hit;
        foreach (Transform t in transform)
        {
            var rayDirection = target.transform.position - t.transform.position;
            if (Physics.Raycast(t.transform.position, rayDirection, out hit, Mathf.Infinity))
            {
                if (!hit.transform.name.Contains(targetName)) return false;
            }
            else return false;
        }
        return true;
    }

    private bool isInSight(Vector3 position, GameObject target)
    {
        RaycastHit hit;
        Vector3 tempPos1, tempPos2;
        float off = .7f;

        float[] off1 = new float[] { 0f, -off, -off, off, off, 0f, -off, 0f, off };
        float[] off2 = new float[] { 0f, off, off, off, off, off, 0f, off, 0f };

            for (int i = 0; i < 9; i++)
        {
            tempPos1 = target.transform.position;
            tempPos1.y += off1[i];
            tempPos2 = position;
            tempPos2.y += off2[i];
            var rayDirection = tempPos1 - tempPos2;
            if (Physics.Raycast(tempPos2, rayDirection, out hit, Mathf.Infinity))
            {
                if (!hit.transform.name.Contains(targetName)) return false;
            }
            else return false;
        }
        return true;
    }

    void stickGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, Mathf.Infinity))
        {
            float groundLevel = hit.transform.GetComponent<MeshRenderer>().bounds.max.y;
            if (transform.position.y > groundLevel)
                transform.position = new Vector3(transform.position.x, groundLevel + 1.5f, transform.position.z);
        }
        return;
        foreach (Transform t in transform)
        {
            if (Physics.Raycast(t.position, new Vector3(0, -1, 0), out hit, Mathf.Infinity))
            {
                float groundLevel = hit.transform.GetComponent<MeshRenderer>().bounds.max.y;
                if (transform.position.y < groundLevel)
                    transform.position = new Vector3(transform.position.x, groundLevel + 1.5f, transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.y > target.transform.position.y) stickGround();
        isVisible = isInSight(transform.position, target);
        //manually flipping the movement condition
        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("Monster condition flipped!");
            flipp = !flipp;
        }

        if (gameStatus.allCollected())
            flipp = true;

        float dist = Vector3.Distance(target.transform.position, transform.position);
        float desiredSpeed = dist;

        if (flipp && flippDelay > 0)
        {
            flippDelay -= Time.deltaTime;
            desiredSpeed = 0f;
        }
        else if (flipp)
            desiredSpeed = desiredSpeed / 2.57f;

        if (flipp) flippMovement(desiredSpeed);
        else movement(desiredSpeed / 1.67f);
        if (transform.position.y > target.transform.position.y) stickGround();
        //if (!controller.isGrounded)
        //transform.position = transform.position - new Vector3(0, 0.88f, 0);
    }
}

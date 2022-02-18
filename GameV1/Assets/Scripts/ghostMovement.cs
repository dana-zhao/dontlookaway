using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour
{
    public CharacterController controller;
    public bool isSeen;
    public bool isFacing;
    // public float speed = 5f;
    public GameObject target;
    public string targetName = "Player";
    float timeLeft = 1.5f;

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

    }

    void controllerMove(Vector3 position, Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - position;
        Vector3 movement = direction.normalized * speed;
        if (movement.magnitude > direction.magnitude) movement = direction;
        controller.Move(movement);
        transform.LookAt(target.transform);
    }

    void movement(float speed)
    {
        if (!isFacing && !isSeen)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
        if (isFacing && !isSeen)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                transform.position = target.transform.position - target.transform.forward * 50; ;
            }
        }
        else timeLeft = 1.5f;
    }

    void flippMovement(float desiredSpeed)
    {
        // add burst to prevent stuck
        float dist = Vector3.Distance(target.transform.position, transform.position);
        float actualSpeed = Vector3.Distance(oldPosition, transform.position)/ Time.deltaTime;
        if (actualSpeed < desiredSpeed) speed *= 1.27f;
        else speed = desiredSpeed;
        oldPosition = transform.position;

        if (isSeen)
        {
            speed *= 2.7f;
            speed += .7f;
        }
        // if the player is in direct line of sight, then move straight to the player
        if (isVisible)
        {
            Q.Clear();
            controllerMove(transform.position, target.transform.position, speed * Time.deltaTime);
            lastPositionSeen = target.transform.position;
            
            return;
        }
        if (Q.Count == 0) Q.Add(lastPositionSeen);
        else if (Q.Count == 1) Q.Add(target.transform.position);
        else
        {
            if (isInSight(Q[Q.Count - 2], target)) Q[Q.Count - 1] = target.transform.position;
            else if (Vector3.Distance(target.transform.position, Q[Q.Count - 1]) > 0.1)
                Q.Add(target.transform.position);

            //bool isAdded = false;
            //for (int i = 1; i < Q.Count - 1; i++)
            //{
            //    if (isInSight(Q[i - 1], target))
            //    {
            //        Q[i + 1] = target.transform.position;
            //        Q.RemoveRange(i + 2, Q.Count - i - 2);
            //        isAdded = true;
            //        break;
            //    }
            //}
            //if (!isAdded && Vector3.Distance(target.transform.position, Q[Q.Count - 1]) > 0.1)
                //Q.Add(target.transform.position);
        }
        Vector3 destination = Q[0];
        controllerMove(transform.position, destination, speed * Time.deltaTime * 1.7f);
        if (Vector3.Distance(transform.position, destination) < 0.5) Q.RemoveAt(0);
    }

    private bool isInSight(Vector3 position, GameObject target)
    {
        RaycastHit hit;
        Vector3 tempPos1, tempPos2;
        float off = .8f;

        float[] off1 = new float[] { 0f, -off, -off, off, off, 0f, -off, 0f, off };
        float[] off2 = new float[] { 0f, -off, off, -off, off, -off, 0f, off, 0f };
        for (int i = 0; i < 5; i++)
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

    // Update is called once per frame
    void Update()
    {
        isVisible = isInSight(transform.position, target);
        //manually flipping the movement condition
        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("Monster condition flipped!");
            flipp = !flipp;
        }
        float dist = Vector3.Distance(target.transform.position, transform.position);
        float desiredSpeed = dist;
        if (flipp) flippMovement(desiredSpeed / 1.77f);
        else movement(desiredSpeed);
    }
}


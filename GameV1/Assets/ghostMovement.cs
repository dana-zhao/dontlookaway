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
    public string targetName = "PlayerBody";
    float timeLeft = 1.5f;

    // keep track of player's path
    private List<Vector3> Q = new List<Vector3>();

    // boolean for monster condition
    public bool flipp = false;

    // temp variable for chasing speed when condition is flipped
    public float chaseSpeed = 0.1f;

    // Keeping track of the last player position seen
    Vector3 lastPositionSeen;

    // Check if in line of sight
    public bool isVisible = true;

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
    }

    void movement(float speed)
    {
        if (!isFacing && !isSeen) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (isFacing && !isSeen)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                transform.position = target.transform.position - target.transform.forward * 50; ;
            }
        }
        else timeLeft = 3f;
    }

    void flippMovement(float speed)
    {
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
        controllerMove(transform.position, destination, speed * Time.deltaTime * 2f);
        if (Vector3.Distance(transform.position, destination) < 0.1) Q.RemoveAt(0);
    }

    private bool isInSight(Vector3 position, GameObject target)
    {
        RaycastHit hit;
        var rayDirection = target.transform.position - position;
        if (Physics.Raycast(position, rayDirection, out hit, Mathf.Infinity))
        {
            if (hit.transform.name == targetName) return true;
        }
        return false;
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
        float speed = dist;
        if (flipp) flippMovement(speed);
        else movement(speed);
    }
}

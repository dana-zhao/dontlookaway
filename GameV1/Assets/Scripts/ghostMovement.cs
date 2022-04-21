using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ghostMovement : MonoBehaviour
{
    public CharacterController controller;
    public UnityEngine.AI.NavMeshAgent agent;
    public NavMeshQuery navMeshQuery;
    public bool teleportable = false;
    public bool isSeen;
    public bool isFacing;
    public bool lastTickSeen;
    //public float speed = 5f;
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

    private Vector3 stopBy = Vector3.zero;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        lastPositionSeen = target.transform.position;
        flipp = false;
        //controller.enabled = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        //agent.radius = 0.5f;
        agent.autoBraking = false;
        agent.acceleration = 177f;
    }

    //void controllerMove(Vector3 position, Vector3 targetPosition, float speed)
    //{
        //Vector3 direction = targetPosition - position;
        //Vector3 movement = direction.normalized * speed;
        //if (movement.magnitude > direction.magnitude) movement = direction;
        //controller.Move(movement);
        //transform.LookAt(target.transform);
    //}

    void movement(float speed)
    {
        Vector3 dest = target.transform.position;
        //if (stopBy == Vector3.zero) dest = target.transform.position;
        //else dest = stopBy;
        if (!isSeen)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            agent.destination = dest;
            //agent.speed = speed + stopBy.magnitude * Time.deltaTime * 17.7f;
            agent.speed = speed;
            transform.LookAt(agent.nextPosition);
        }
        else
        {
            agent.speed = 0;
            agent.destination = dest;
        }
        if (!agent.pathPending && agent.remainingDistance < 2f) stopBy = Vector3.zero;
        return;
        //if (isFacing && !isSeen)
        //{
        //    timeLeft -= Time.deltaTime;
        //    if (timeLeft < 0)
        //    {
        //        //transform.position = target.transform.position - target.transform.forward * 50; 
        //       agent.destination = target.transform.position;

        //    }
        //}
        //if (isFacing && isSeen)
        //{
        //    agent.destination = transform.position;
        //}
        //else timeLeft = 1.5f;

        agent.speed = speed;
    }

    void flippMovement(float desiredSpeed)
    {
        agent.destination = target.transform.position;
        //transform.LookAt(target.transform);
        if (isFacing)
        {
            agent.speed = desiredSpeed;
        }
        if (!isFacing)
        {
            agent.speed = desiredSpeed/2;
        }
        // add burst to prevent stuck
        //float dist = Vector3.Distance(target.transform.position, transform.position);
        //float actualSpeed = Vector3.Distance(oldPosition, transform.position)/ Time.deltaTime;
        //if (actualSpeed < desiredSpeed) speed *= 1.27f;
        //else speed = desiredSpeed;
        //oldPosition = transform.position;

        //if (isSeen && desiredSpeed > 0)
        //{
            //speed *= 2.57f;
            //speed += .7f;
        //}
        // if the player is in direct line of sight, then move straight to the player
        //if (isVisible)
        //{
            //Q.Clear();
            //controllerMove(transform.position, target.transform.position, speed * Time.deltaTime);
            //lastPositionSeen = target.transform.position;
            
            //return;
        //}
        //if (Q.Count == 0) Q.Add(lastPositionSeen);
        //else if (Q.Count == 1) Q.Add(target.transform.position);
        //else
        //{
            //if (isInSight(Q[Q.Count - 2], target)) Q[Q.Count - 1] = target.transform.position;
            //else if (Vector3.Distance(target.transform.position, Q[Q.Count - 1]) > 0.1)
                //Q.Add(target.transform.position);

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
        //}
        //Vector3 destination = Q[0];
        //controllerMove(transform.position, destination, speed * Time.deltaTime * 1.7f);
        //if (Vector3.Distance(transform.position, destination) < 0.5) Q.RemoveAt(0);

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

    private void teleport(int depth = 70)
    {
        if (!teleportable) return;
        depth -= 1;
        //Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(20f, 100f)));
        Vector3 v3Pos = Camera.main.transform.position + new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        navMeshQuery = new NavMeshQuery(NavMeshWorld.GetDefaultWorld(), Unity.Collections.Allocator.Persistent);
        NavMeshLocation location = navMeshQuery.MapLocation(v3Pos, new Vector3(100f, 100f, 100f), 0);
        navMeshQuery.Dispose();
        if (depth == 0)
        {
            transform.position = location.position;
            return;
        }
        RaycastHit hit;
        var rayDirection = Camera.main.transform.position - location.position;
        if (Vector3.Distance(location.position, Camera.main.transform.position) < 12.7f)
        {
            teleport(depth);
            return;
        }
        if (Physics.Raycast(location.position, rayDirection, out hit, Mathf.Infinity) && hit.transform.name.Contains(targetName))
        {
            teleport(depth);
            return;
        }
        transform.position = location.position;
        //stopBy = location.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    teleport();
        //}

        if (lastTickSeen && !isSeen)
        {
            if (Random.Range(0f, 1f) < 0.17f) teleport();
        }
        //isVisible = isInSight(transform.position, target);
        //manually flipping the movement condition
        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    Debug.Log("Monster condition flipped!");
        //    flipp = !flipp;
            
        //}

        if (!flipp && gameStatus.allCollected())
        {
            flipp = true;
            AkSoundEngine.SetState("States", "MonsterFlippy");
            AkSoundEngine.PostEvent("ConditionFlip",gameObject);

            // var textMeshPro = FloatingTextPrefab.GetComponent<popUpTextSelf>();
            // textMeshPro.setText("Collect and run!!", 500);
            // ShowFloatingText();
        }

        float dist = Vector3.Distance(target.transform.position, transform.position);
        float desiredSpeed = dist;

        if (flipp && flippDelay > 0)
        {
            flippDelay -= Time.deltaTime;
            desiredSpeed = 0f;
            
        }
        else if (flipp)
            desiredSpeed = desiredSpeed / 1.77f;

        if (flipp) flippMovement(desiredSpeed);


        else movement(desiredSpeed);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour
{
    public bool isSeen;
    public bool isFacing;
    // public float speed = 5f;
    public GameObject target;
    float timeLeft = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        float speed = dist;
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
}

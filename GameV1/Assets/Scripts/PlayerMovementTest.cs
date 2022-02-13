using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    private float speed = 0.05f;
    [SerializeField]
    private float maxSpeed = 7f;
    [SerializeField]
    private Rigidbody rbody;

    private int objectsPickedUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Our max speed
        if (rbody.velocity.magnitude > maxSpeed) {
            return;
        }

        // WASD MOVEMENT
        if (Input.GetKey(KeyCode.W)){
            Vector3 direction = new Vector3(0, 0, 1.0f);
            rbody.velocity += direction * speed;
        }
        if (Input.GetKey(KeyCode.A)){
            Vector3 direction = new Vector3(-1.0f, 0, 0);
            rbody.velocity += direction * speed;
        }
        if (Input.GetKey(KeyCode.S)){
            Vector3 direction = new Vector3(0, 0, -1.0f);
            rbody.velocity += direction * speed;
        }
        if (Input.GetKey(KeyCode.D)){
            Vector3 direction = new Vector3(1.0f, 0, 0);
            rbody.velocity += direction * speed;
        }
        if (Input.GetKey(KeyCode.Space)){
            Vector3 direction = new Vector3(0, 1.0f, 0);
            rbody.velocity += direction * speed;
        }
    }

    // What to do when we collide with an object marked for pickup with the "Pick Up" tag
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            objectsPickedUp += 1;
            Debug.Log("Objects picked up: " + objectsPickedUp.ToString());
        }
    }

}

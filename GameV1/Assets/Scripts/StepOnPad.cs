using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOnPad : MonoBehaviour
{
    public GameObject Door;
    private bool DoorClosed = true;

    private Vector3 _originialRotation;
    private Vector3 _originialPosition;

    private Vector3 _newRotation = new Vector3(0f, 260f, 0f);
    private Vector3 _newPosition = new Vector3(8.5f, 0f, 1.5f);

    // Start is called before the first frame update
    void Start()
    {
        _originialRotation = Door.transform.eulerAngles;
        _originialPosition = Door.transform.position;
        
    }

    void OpenDoor()
    {
        Door.transform.localPosition = _newPosition;
        Door.transform.eulerAngles = _newRotation;
        DoorClosed = !DoorClosed;
    }

    void CloseDoor()
    {
        Door.transform.localPosition = _originialPosition;
        Door.transform.eulerAngles = _originialRotation;
        DoorClosed = !DoorClosed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "Player" | other.name == "Ghost")
        {
            if (DoorClosed) {
                OpenDoor();
            } else {
                CloseDoor();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.name == "Player" | other.name == "Ghost")
        {
            if (DoorClosed) {
                OpenDoor();
            } else {
                CloseDoor();
            }
        }
    }

}

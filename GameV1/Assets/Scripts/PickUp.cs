using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{   
    public float detectionRange;
    public bool closeEnough;
    private GameObject player = null;

    private TasksCompletion _TasksCompletion;
 
    // Start is called before the first frame update
    void Start()
    {
        _TasksCompletion = GameObject.FindObjectOfType<TasksCompletion>();
        if (!_TasksCompletion) {
            Debug.Log("not found");
        }

        detectionRange = 5;
        if (player == null)
             player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void OnMouseOver()
    {   
        closeEnough = false;
        if( Vector3.Distance( player.transform.position, this.gameObject.transform.position) <= detectionRange ){
            closeEnough = true;
        }
        if (closeEnough && Input.GetKeyDown(KeyCode.E)){
            this.gameObject.SetActive(false);
            _TasksCompletion.TaskComplete(this.gameObject.name);
        }
    }
}
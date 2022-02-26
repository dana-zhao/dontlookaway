using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameStatus script;
    public GameObject player;
    private bool closeEnough1;
    private float detectionRange;
    // Start is called before the first frame update
    void Start()
    {
        detectionRange = 5;
    }

    // Update is called once per frame
    void Update()
    {
 
        if( Vector3.Distance( player.transform.position, this.transform.position) <= detectionRange ){
            closeEnough1 = true;
        }
        if (closeEnough1 && script.allCollect && Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene("Hallway1");
        }
    }
}
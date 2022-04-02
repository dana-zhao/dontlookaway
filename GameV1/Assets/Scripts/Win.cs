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
        detectionRange = 3;
        script = GameObject.FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script.allCollect && Vector3.Distance(player.transform.position, this.transform.position) <= detectionRange)
        {
            AkSoundEngine.StopAll();
            SceneManager.LoadScene("Hallway1");
        }
    }
}
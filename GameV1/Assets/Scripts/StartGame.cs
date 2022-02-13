using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    
    public GameObject startGameObj;
    public GameObject TimerObj;
    public GameObject PauseButton;
    public GameObject TaskList;

    private Text _timerText;
    private float time = 0.0f;
    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        startGameObj.SetActive(true);
        TimerObj.SetActive(false);
        PauseButton.SetActive(false);
        TaskList.SetActive(false);

        _timerText = TimerObj.GetComponent<Text>();
        _timerText.text = "Time: 0";
        
    }

    public void Pause()
    {
        pause = !pause;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space") && startGameObj.activeSelf)
        {
            startGameObj.SetActive(false);
            TimerObj.SetActive(true);
            PauseButton.SetActive(true);
            TaskList.SetActive(true);
        }
        
        if (!startGameObj.activeSelf && !pause)
        {
            time += Time.deltaTime;
            _timerText.text = "Time: " + Mathf.Round(time * 10.0f) * 0.1f;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    
    public GameObject startGameObj;
    public GameObject TimerObj;
    public GameObject PausePanel;
    public GameObject TaskList;


    private Text _timerText;
    private float time = 0.0f;
    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        startGameObj.SetActive(true);
        TimerObj.SetActive(false);
        PausePanel.SetActive(false);
        TaskList.SetActive(true);

        _timerText = TimerObj.GetComponent<Text>();
        _timerText.text = "Time: 0";

        Time.timeScale = 0;
        
    }

    private void Pause()
    {
        PausePanel.SetActive(true);
        pause = !pause;
        Time.timeScale = 0;
    }

    private void Resume()
    {
        PausePanel.SetActive(false);
        pause = !pause;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // start
        if (startGameObj.activeSelf && Input.GetKeyDown("space"))
        {
            startGameObj.SetActive(false);
            TimerObj.SetActive(true);
            TaskList.SetActive(true);
            TaskList.GetComponent<TasksCompletion>().Close();
            Time.timeScale = 1;
        }

        //pause resume
        if (Input.GetKeyDown("escape") && !startGameObj.activeSelf){
            if(pause) {
                Debug.Log("Resume");
                Resume();
            } else {
                Debug.Log("Pause");
                Pause();
            }
        }

        //timer
        time += Time.deltaTime;
        _timerText.text = "Time: " + Mathf.Round(time * 10.0f) * 0.1f;
        
    }
}

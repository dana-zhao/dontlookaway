using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TasksCompletion : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, string>> LevelTasks = new Dictionary<string, Dictionary<string, string>>(){
        {"Gift_Shop", new Dictionary<string, string>
            {
                {"Blue crystal", "Blue Crystal 1"},
                {"Blue crystal (1)", "Blue Crystal 2"},
                {"Blue crystal (2)", "Blue Crystal 3"},
                {"Clear crystal", "Clear Crystal 1"},
                {"Key", "Key for exit"}
            }
        },
        {"Library", new Dictionary<string, string>
            {
                {"Scept eye", "Scept eye"},
                {"Scept stick", "Scept stick"},
                {"Scept head", "Scept head"},
                {"Key", "Key for exit"},
                {"Battery", "Battery for flashlight"},
            }
        },
        {"Statue", new Dictionary<string, string>
            {
                {"Jar", "Canopic Jar 1"},
                {"Jar (1)", "Canopic Jar 2"},
                {"Jar (2)", "Canopic Jar 3"},
                {"Jar (3)", "Canopic Jar 4"},
                {"Key", "Key for exit"},
                {"Battery", "Battery for flashlight"},
            }
        },
    };
    private Dictionary<string, string> tasks;

    public GameObject Simple;
    public GameObject ToogleGroup;
    public GameObject TaskTemplate; 

    public int count = 0;


    private Dictionary<string, GameObject> simpleGameObject = new Dictionary<string, GameObject>();
    public GameStatus gameStatus;

    void Start()
    {
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
        gameStatus.Reset();
        LoadTask();

        foreach(KeyValuePair<string, string> task in tasks){
            GameObject t = Instantiate(TaskTemplate);
            t.name = task.Key;
            t.GetComponentInChildren<Text> ().text = task.Value;
            t.transform.SetParent(ToogleGroup.transform, false);
            t.SetActive(true);
            simpleGameObject[task.Key] = t;
            gameStatus.newTask();

            count++;
        }

        Simple.SetActive(true);

        // gameStatus.currentTasks = 4;

    }

    public void TaskComplete(string taskname){

        simpleGameObject[taskname].GetComponent<Toggle>().isOn = true;
        gameStatus.collect();
        

    }

    private void LoadTask(){
        string scene = SceneManager.GetActiveScene().name;
        tasks = LevelTasks[scene];
    }

}
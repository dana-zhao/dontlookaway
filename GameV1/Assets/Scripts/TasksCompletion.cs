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
                {"Clear crystal (1)", "Clear Crystal 2"},
                {"Clear crystal (2)", "Clear Crystal 3"},
            }
        },
    };
    private Dictionary<string, string> tasks;

    public GameObject Simple;
    public GameObject Complex;

    public GameObject ToogleGroup;
    public GameObject TaskTemplate; 

    public GameObject Objective;
    public GameObject ObjectiveTemplate;

    private bool openI = false;

    private Dictionary<string, GameObject> simpleGameObject = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> complexGameObject = new Dictionary<string, GameObject>();

    void Start()
    {
        LoadTask();

        foreach(KeyValuePair<string, string> task in tasks){
            GameObject t = Instantiate(TaskTemplate);
            t.name = task.Key;
            t.GetComponentInChildren<Text> ().text = task.Value;
            t.transform.SetParent(ToogleGroup.transform, false);
            t.SetActive(true);
            simpleGameObject[task.Key] = t;
        }

        foreach(KeyValuePair<string, string> task in tasks){
            GameObject t = Instantiate(ObjectiveTemplate);
            t.name = task.Key;
            t.GetComponentInChildren<Text> ().text = task.Value;
            t.transform.SetParent(Objective.transform, false);
            t.SetActive(true);
            complexGameObject[task.Key] = t;
        }

        Complex.SetActive(false);
        Simple.SetActive(false);

    }

    public void TaskComplete(string taskname){
        // GameObject t = ToogleGroup.transform.Find(taskname).gameObject;
        // t.GetComponent<Toggle>().isOn = true;
        simpleGameObject[taskname].GetComponent<Toggle>().isOn = true;
        complexGameObject[taskname].SetActive(false);
    }

    private void LoadTask(){
        string scene = SceneManager.GetActiveScene().name;
        tasks = LevelTasks[scene];
    }

    private void Open(){
        Complex.SetActive(true);
        Simple.SetActive(false);
        openI = true;
    }

    public void Close(){
        Complex.SetActive(false);
        Simple.SetActive(true);
        openI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")){
            if(!openI) {
                Open();
            } else {
                Close();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksCompletion : MonoBehaviour
{
    // <taskname, task description>
    private Dictionary<string, string> tasks = new Dictionary<string, string>(){
        {"TestObject1", "Collect object1"},
        {"TestObject2", "Collect object2"},
        {"TestObject3", "Collect object3"},
        {"TestObject4", "Collect object4"},
        {"battery", "Battery"},
        {"RoomKey", "Key"},
    };

    public GameObject Simple;
    public GameObject Complex;

    public GameObject ToogleGroup;
    public GameObject TaskTemplate; 

    public GameObject Objective;
    public GameObject ObjectiveTemplate;

    private bool openI = false;

    void Start()
    {
        foreach(KeyValuePair<string, string> task in tasks){
            GameObject t = Instantiate(TaskTemplate);
            t.name = task.Key;
            t.GetComponentInChildren<Text> ().text = task.Value;
            t.transform.SetParent(ToogleGroup.transform, false);
            t.SetActive(true);
        }

        foreach(KeyValuePair<string, string> task in tasks){
            GameObject t = Instantiate(ObjectiveTemplate);
            t.name = task.Key;
            t.GetComponentInChildren<Text> ().text = task.Value;
            t.transform.SetParent(Objective.transform, false);
            t.SetActive(true);
        }

        Complex.SetActive(false);
        Simple.SetActive(true);

    }

    public void TaskComplete(string taskname){
        GameObject t = ToogleGroup.transform.Find(taskname).gameObject;
        t.GetComponent<Toggle>().isOn = true;
    }

    public void loadTask(Dictionary<string, string> t){
        tasks = t;
    }

    private void Open(){
        Complex.SetActive(true);
        Simple.SetActive(false);
        openI = !openI;
    }

    private void Close(){
        Complex.SetActive(false);
        Simple.SetActive(true);
        openI = !openI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")){
            if(!openI) {
                Debug.Log("open inventory");
                Open();
            } else {
                Debug.Log("close inventory");
                Close();
            }
        }

    }
}

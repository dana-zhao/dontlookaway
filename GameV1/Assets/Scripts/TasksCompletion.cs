using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksCompletion : MonoBehaviour
{
    private Dictionary<string, string> tasks = new Dictionary<string, string>(){
        {"TestObject", "Collect test object"},
        {"TestObject1", "Collect test object 1"}
    };

    public GameObject ToogleGroup;
    public GameObject TaskTemplate; 

    void Start()
    {
        foreach(KeyValuePair<string, string> task in tasks){
            GameObject t = Instantiate(TaskTemplate);
            t.name = task.Key;
            t.GetComponentInChildren<Text> ().text = task.Value;
            t.transform.parent = ToogleGroup.transform;
            t.SetActive(true);
        }

    }

    public void TaskComplete(string taskname){
        GameObject t = ToogleGroup.transform.Find(taskname).gameObject;
        t.GetComponent<Toggle>().isOn = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

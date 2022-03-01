using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{   
    public float detectionRange;
    public bool closeEnough;
    private GameObject player = null;
    private Flashlight our_light;
    public GameObject FloatingTextPrefab;
    private TasksCompletion _TasksCompletion;
    private bool tutDisplayed = false;
    public GameObject Objective;
    public bool isTut = false;

    [SerializeField] public AudioSource sfx_CrystalSfxPickUp;
 
    // Start is called before the first frame update
    void Start()
    {
        _TasksCompletion = GameObject.FindObjectOfType<TasksCompletion>();
        // Our light
        our_light = GameObject.FindObjectOfType<Flashlight>();
        if (!_TasksCompletion) {
            Debug.Log("not found");
        }

        detectionRange = 5;
        if (player == null)
             player = GameObject.FindGameObjectWithTag("Player");
    }

    void ShowFloatingText()
    {
        if (!isTut) return;
        Vector3 temp = new Vector3(Camera.main.transform.forward.x * 0.1f, transform.position.y+0.7f, Camera.main.transform.forward.z * 0.1f);
        Vector3 temp2 = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
        Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
    }  


    bool checkItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, detectionRange, 1 << 4))
        {
            if (hit.transform == transform) return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {   
        closeEnough = false;
        //if( Vector3.Distance( player.transform.position, this.gameObject.transform.position) <= detectionRange ){
        //    closeEnough = true;
         //   if (!tutDisplayed)
         //   {
         //       ShowFloatingText();
        //        tutDisplayed = true;
        //    }
        //}

        if (checkItem())
        {
            closeEnough = true;

        }
        if (Objective.transform.childCount <= 3)
        {
            var textMeshPro = FloatingTextPrefab.GetComponent<popUpTextSelf>();
            textMeshPro.setText("Collect and run!!", 500);
            ShowFloatingText();
        }
        else if (closeEnough)
        {
            if (!tutDisplayed)
            {
                ShowFloatingText();
                tutDisplayed = true;
            }
        }
        if (closeEnough && Input.GetKeyDown(KeyCode.E)){
            sfx_CrystalSfxPickUp.Play();
            this.gameObject.SetActive(false);
            _TasksCompletion.TaskComplete(this.gameObject.name);

            // Code to refresh battery TODO: ADD BATTERY ITEM
            if (this.gameObject.tag == "Battery"){
                float amount = 0.01f;
                our_light.ReplaceBattery(amount);
            }
        }
    }
}
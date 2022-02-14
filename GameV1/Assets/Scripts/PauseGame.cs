using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private StartGame _startGame;

    public GameObject PauseButton;

    // Start is called before the first frame update
    void Start()
    {
        _startGame = GameObject.FindObjectOfType<StartGame>();
        
        Button btn = PauseButton.GetComponent<Button>();
        btn.onClick.AddListener(() => TaskOnClick());
    }

    void Update()
    {
       

        
    }

    void TaskOnClick(){
        Debug.Log("click");

	}
}

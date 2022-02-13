using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private StartGame _startGame;

    public Button PauseButton;
    public GameObject ButtonText;


    private Text _buttonText;

    // Start is called before the first frame update
    void Start()
    {
        _startGame = GameObject.FindObjectOfType<StartGame>();
        Button btn = PauseButton.GetComponent<Button>();
        _buttonText = ButtonText.GetComponent<Text>();
		btn.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        // if (Input.GetKey("escape"))
        // {
        //     TaskOnClick();
        // }

        
    }

    void TaskOnClick(){
        
		_startGame.Pause();
        if (_buttonText.text == "Pause"){
            _buttonText.text = "Resume";
        }else{
            _buttonText.text = "Pause";
        }
	}
}

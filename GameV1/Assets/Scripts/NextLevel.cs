using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameStatus script;
    private bool closeEnough;
    private float detectionRange;
    private string level;
    public string nextLevel;
    
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        detectionRange = 5;
        level = GameStatus.currentLevel;
        if (level == null) {
            level = "Gift_Shop";
        }
        else if (level == "Gift_Shop"){
            level = "Library";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( Vector3.Distance( player.transform.position, this.transform.position) <= detectionRange ){
            closeEnough = true;
        }


        if(closeEnough){
            AkSoundEngine.StopAll();
            SceneManager.LoadScene(nextLevel);
        }
    }
}

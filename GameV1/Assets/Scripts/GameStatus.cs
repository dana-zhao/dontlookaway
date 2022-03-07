using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static string currentLevel;
    public int currentInventory = 0;
    public int currentTasks = 0;
    //public const int MAXLIFE = 5;
    //public int currentLife = MAXLIFE;
    public bool allCollect = false;

    public Slider healthbar;

    public void collect() {
        currentInventory ++ ;
        allCollect = currentInventory == currentTasks;
    }

    public void newTask(){
        currentTasks ++; 
    }

    //public void dead() {
        //currentLife --;
        //healthbar.value = (float)currentLife / MAXLIFE;
    //}

    public bool allCollected(){
        return allCollect;
    }


    void Start(){
        //healthbar.value = currentLife / MAXLIFE;
        currentLevel = SceneManager.GetActiveScene().name;
    }
}

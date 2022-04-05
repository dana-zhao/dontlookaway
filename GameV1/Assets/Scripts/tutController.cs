using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutController : MonoBehaviour
{
    public GameObject Objective;
    public GameObject FloatingTextPrefab;
    public GameStatus gameStatus;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameObject.FindObjectOfType<GameStatus>();
    }

    void ShowFloatingText()
    {
        //if (!Input.GetKeyUp(KeyCode.T)) return;
        Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus.allCollected())
        {
            var textMeshPro = FloatingTextPrefab.GetComponent<popUpTextSelf>();
            textMeshPro.setText("EXIT HERE", 500);
            ShowFloatingText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("States", "MonsterHappy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

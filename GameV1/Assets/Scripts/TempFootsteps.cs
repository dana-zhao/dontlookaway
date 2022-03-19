using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFootsteps : MonoBehaviour
{
    public float stepRate = 0.9f;
    public float stepCoolDown;
    public AK.Wwise.Event footstepLoop;


    // Update is called once per frame
    void Update()
    {
        stepCoolDown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCoolDown < 0f)
        {
            footstepLoop.Post(gameObject);
            stepCoolDown = stepRate;
        }
    }
}
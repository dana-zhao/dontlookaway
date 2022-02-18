using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFootsteps : MonoBehaviour
{
    public float stepRate = 0.9f;
    public float stepCoolDown;
    public AudioSource sfx_TempFootStep;


    // Update is called once per frame
    void Update()
    {
        stepCoolDown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCoolDown < 0f)
        {
            sfx_TempFootStep.pitch = 1f + Random.Range(-0.2f, 0.2f);
            sfx_TempFootStep.Play();
            stepCoolDown = stepRate;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot fader;
    [SerializeField]
    private AudioMixer specmusicfade;
    // Start is called before the first frame update
    void Start()
    {
        fader.TransitionTo(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

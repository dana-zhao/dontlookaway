using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrontPage : MonoBehaviour
{
    public Button play;
    public Button cont;
    public Button giftshop;
    public Button library;
    public Button statue;
    public Button back;

    public bool open = false;
    public GameObject main;
    public GameObject level;

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(() => {Load(play);});
        giftshop.onClick.AddListener(() => {Load(giftshop);});
        library.onClick.AddListener(() => {Load(library);});
        statue.onClick.AddListener(() => {Load(statue);});

        cont.onClick.AddListener(levels);
        back.onClick.AddListener(levels);

    }

    void Load(Button button)
    {
        string scene = button.name;
		SceneManager.LoadScene(scene);
        AkSoundEngine.StopAll();
    }

    void levels()
    {
        main.SetActive(open);
        level.SetActive(!open);
        open = !open;
    }
}

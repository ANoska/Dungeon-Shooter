using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button _PlayButton;

    // Start is called before the first frame update
    void Start()
    {
        _PlayButton?.onClick.AddListener(() => SceneManager.LoadScene("_MainScene"));
    }

    // Update is called once per frame
    void Update()
    {   
    }
}

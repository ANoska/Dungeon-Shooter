using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button _PlayButton;

    [Header("UI Elements")]
    public RectTransform _LoadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        _PlayButton?.onClick.AddListener(() =>
        {
            _LoadingPanel.gameObject.SetActive(true);
            SceneManager.LoadScene("_MainScene");
        }
        );
    }

    // Update is called once per frame
    void Update()
    {   
    }
}

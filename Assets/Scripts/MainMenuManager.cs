using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Text m_LevelText;

    [SerializeField]
    private Button m_StartButton;

    [SerializeField]
    private Button m_ExitButton;

    [SerializeField]
    private List<string> m_Levels = new List<string>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        m_StartButton.onClick.AddListener(StartGame);
        m_ExitButton.onClick.AddListener(Exit);

        m_LevelText.text = "Level:" + Static.level.ToString();
    }

    private void StartGame()
    {
        int dr = Mathf.FloorToInt((float)Static.level / m_Levels.Count);
        int dr2 = dr * m_Levels.Count;
        int res = Static.level - dr2;
        int id = Static.level - 1 >= m_Levels.Count ? res : Static.level - 1;
        SceneManager.LoadScene(m_Levels[id]);
    }

    private void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JengaManager : MonoBehaviour
{
    [SerializeField]
    private TableTouching _table;

    [SerializeField]
    private GameObject _jengaPiece;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Vector3 _spawnPoint;

    private float _pieceOffsetZ = 0.25f;
    private float _pieceOffsetY = 0.15f;

    [SerializeField]
    private int _layers = 9;

    private int _currentLayer;

    private float _spawnDelay = 0.1f;

    [SerializeField]
    private string _jengaPieceTag = "JengaPiece";

    [HideInInspector]
    public bool pieceSelected;

    [HideInInspector]
    public bool isPaused;

    [HideInInspector]
    public bool canMove;

    [Header("UI")]

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _winPanel;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private Button _winButton;

    [Header("Camera")]
    [SerializeField]
    private FlyCamera _flyCamera;

    private bool _isWin = false;

    public static JengaManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _exitButton.onClick.AddListener(CloseGame);
        _winButton.onClick.AddListener(CloseGame);

        _gameOverPanel.SetActive(false);
        _winPanel.SetActive(false);

        _currentLayer = 0;
        pieceSelected = false;
        isPaused = false;
    }

    public void SpawnJengaPieces()
    {
        if (_currentLayer < _layers)
        {
            if (_currentLayer % 2 == 0)
            {
                SpawnHorizontalLayer(_currentLayer);
            }
            else
            {
                SpawnVerticalLayer(_currentLayer);
            }
            _currentLayer++;

            Invoke("SpawnJengaPieces", _spawnDelay);
        }
        else
        {
            Vector3 center = new Vector3(_spawnPoint.x, _spawnPoint.y + _pieceOffsetY * 2 * _currentLayer, _spawnPoint.z);

            Quaternion rotation = new Quaternion(0, 180, 0, 0);

            Instantiate(_player, center, rotation);
        }
    }

    private void SpawnHorizontalLayer(int layer)
    {
        Vector3 center = new Vector3(_spawnPoint.x, _spawnPoint.y + _pieceOffsetY * layer, _spawnPoint.z);
        Quaternion rotation = new Quaternion();
        Instantiate(_jengaPiece, center, rotation);
        Instantiate(_jengaPiece, new Vector3(center.x, center.y, center.z + _pieceOffsetZ), rotation);
        Instantiate(_jengaPiece, new Vector3(center.x, center.y, center.z - _pieceOffsetZ), rotation);
    }

    private void SpawnVerticalLayer(int layer)
    {
        Vector3 center = new Vector3(_spawnPoint.x, _spawnPoint.y + _pieceOffsetY * layer, _spawnPoint.z);
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        Instantiate(_jengaPiece, center, rotation);
        Instantiate(_jengaPiece, new Vector3(center.x + _pieceOffsetZ, center.y, center.z), rotation);
        Instantiate(_jengaPiece, new Vector3(center.x - _pieceOffsetZ, center.y, center.z), rotation);
    }

    public void ResetPieces()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ClearPieces()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag(_jengaPieceTag);

        foreach (GameObject piece in pieces)
        {
            Destroy(piece);
        }

        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void GameOver()
    {
        if (_isWin)
            return;

        _gameOverPanel.SetActive(true);

        canMove = false;

        isPaused = true;

        _flyCamera.enabled = false;
    }

    public void CloseGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Win()
    {
        if (_isWin)
            return;

        canMove = false;

        Static.level = Static.level + 1;

        _flyCamera.enabled = false;

        _winPanel.SetActive(true);

        _isWin = true;
    }
}

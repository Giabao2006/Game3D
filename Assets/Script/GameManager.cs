using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;//Singleton
    public static GameManager Instance { get => instance; }
    public GameObject player;
    [Header("Checkpoint")]
    public Vector3 currentCheckPoint;

    [Header("Enemy")]
    public int enemyKilled = 0;

    [Header("Point")]
    public int point = 0;
    public TextMeshProUGUI pointText;

    public GameObject winUI;
    public GameObject loseUI;
    void Awake()
    {
        if (GameManager.instance == null || GameManager.instance != this) GameManager.instance = this;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        UpdatePoint();
    }

    //Point
    public void UpdatePoint()
    {
        pointText.text = "Point: " + point.ToString();
    }
    public void PlusPoint(int p)
    {
        point += p;
        UpdatePoint();
    }
    //CheckPoint
    public void SaveCheckPoint(Vector3 newCheckPoint)
    {
        currentCheckPoint = newCheckPoint;
    }
    public void LoadCheckPoint()
    {
        if (currentCheckPoint == Vector3.zero) return;
        player.transform.position = currentCheckPoint;
    }
    //Enemy
    public void UpdateEnemyKilled(int c)
    {
        enemyKilled += c;
    }
    public void ShowWinUI()
    {
        winUI.SetActive(true);
    }
    public void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }
    
}

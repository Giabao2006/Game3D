using TMPro;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    public TextMeshProUGUI point;
    public TextMeshProUGUI enemyKilled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        point.text = "Point:: "+ GameManager.Instance.point.ToString();
        enemyKilled.text = "Enemy Killed: "+ GameManager.Instance.enemyKilled.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

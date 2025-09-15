
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    private Camera _cam;
    public int maxHP=3 ; // Maximum health points of the enemy
    public int currentHP; // Current health points of the enemy
    private float deltaTime = 0f; // Time since the last frame
    public Canvas playerCanvas;
    public GameManager gameManager;
    public UnityEngine.UI.Image healthBar; // Reference to the health bar UI element
    public TMP_Text healthText;
    void Start()
    {
        _cam = Camera.main;
        gameManager = FindAnyObjectByType<GameManager>();
        currentHP = maxHP;
        CapNhatMau(0);
    }
void Update()
    {
       playerCanvas.transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
public void CapNhatMau(int amount)
{
    currentHP += amount;
    healthBar.fillAmount = (float)currentHP / maxHP;
    healthText.text = currentHP + "/" + maxHP;
    if (currentHP <= 0)
    {
        currentHP = 0;
        gameObject.SetActive(false);
    }
}
}

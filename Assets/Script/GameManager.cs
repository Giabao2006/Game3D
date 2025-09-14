using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int point = 0;
    public TextMeshProUGUI pointText;
    
    void Start()
    {
        UpdatePoint();
    }

    // Update is called once per frame
    public void UpdatePoint()
    {
        pointText.text = "Point: " + point.ToString();
    }
    public void PlusPoint(int p)
    {
        point += p;
        UpdatePoint();
    }
    
}

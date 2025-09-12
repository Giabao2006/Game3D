using UnityEngine;
using System.Collections;
using TMPro;

public class PointCollect : MonoBehaviour
{
    private int point = 0;
    public TextMeshProUGUI pointText;
    void Start()
    {

    }

    // Update is called once per frame
    void UpdatePoint()
    {
        pointText.text = "Point: " + point.ToString();
    }
    void PlusPoint(int p)
    {
        point += p;
        UpdatePoint();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlusPoint(1);
            Destroy(gameObject);
        }
    }
}

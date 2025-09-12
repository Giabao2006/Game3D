using UnityEngine;
using System.Collections;
using TMPro;

public class PointCollect : MonoBehaviour
{
    private int point = 0;
    public TextMeshProUGUI pointText;
    public GameObject fxPrefab; // prefab hiệu ứng FX
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
    void SpawnFX()
    {
        if (fxPrefab != null)
        {
            Instantiate(fxPrefab, transform.position, Quaternion.identity);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlusPoint(1);
            SpawnFX();
            Destroy(gameObject);
        }
    }
}

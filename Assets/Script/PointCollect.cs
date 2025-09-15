using UnityEngine;
using System.Collections;
using TMPro;

public class PointCollect : MonoBehaviour
{
    public GameObject fxPrefab; // prefab hiệu ứng FX
    public void SpawnFX()
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
            GameManager.Instance.PlusPoint(1);
            SpawnFX();
            Destroy(gameObject);
        }
    }
}

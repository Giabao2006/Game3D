using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.currentCheckPoint = transform.position;
        }
    }
}

using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public GameObject fxPrefab;
    public LayerMask enemyLayer;
    //public CameraShake cameraShake;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            fxPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            var fx=Instantiate(fxPrefab, transform.position, Quaternion.identity);
            
            Destroy(fx, 1f);
            //cameraShake.Shake(.25f, .2f);
            Debug.Log("TakeDamage");
            other.GetComponent<EnemyHealth>().CapNhatMau(-25);
            
        }
    }
}

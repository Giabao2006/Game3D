using UnityEngine;

public class healthTim : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("health", playerHealth.currentHP);
    }
}

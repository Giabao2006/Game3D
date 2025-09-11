using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float i = 5;
        float moveVertical = Input.GetAxis("Vertical"); 
        rb.linearVelocity = new Vector3(moveHorizontal * speed, rb.linearVelocity.y, moveVertical * speed);
    }
    
}

using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;

    public float speed = 5f;
    public float jumpForce = 5f;
    private Vector3 moveInput;
    private bool isGrounded = true;
    private PlayerState playerState;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isGrounded) return;
        
            if (moveInput == Vector3.zero)
            {
                ChangeState(PlayerState.Idle);
            }
            else if (moveInput != Vector3.zero)
            {
                ChangeState(PlayerState.Walk);
            }
        

    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput.x * speed, rb.linearVelocity.y, moveInput.z * speed);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput2D = context.ReadValue<Vector2>();
        moveInput = new Vector3(moveInput2D.x, 0, moveInput2D.y);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");
            ChangeState(PlayerState.Jump);
        }
    }

    public void EndAnim()
    {
        isGrounded = true;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(playerState==PlayerState.Jump)ChangeState(PlayerState.JumpLand);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    public void ChangeState(PlayerState newState)
    {
        if (playerState == PlayerState.Idle) anim.SetBool("isIdle", false);
        else if (playerState == PlayerState.Walk) anim.SetBool("isWalk", false);
        else if (playerState == PlayerState.Run) anim.SetBool("isRun", false);
        else if (playerState == PlayerState.Dance) anim.SetBool("isDance", false);
        else if (playerState == PlayerState.Jump) anim.SetBool("isJump", false);
        playerState = newState;
        if (playerState == PlayerState.Idle) anim.SetBool("isIdle", true);
        else if (playerState == PlayerState.Walk) anim.SetBool("isWalk", true);
        else if (playerState == PlayerState.Run) anim.SetBool("isRun", true);
        else if (playerState == PlayerState.Dance) anim.SetBool("isDance", true);
        else if (playerState == PlayerState.Jump)anim.SetBool("isJump", true);
        else if (playerState == PlayerState.JumpLand) anim.SetTrigger("isJumpLand");

    }
    public enum PlayerState
    {
        Idle, Walk, Run, Jump,JumpLand, Dance
    }
    

}

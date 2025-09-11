using System.Linq;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public Joystick joystick;

    public float speed = 1.5f;
    private bool isRunning = false;
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
        
        if (isRunning) speed = 3f;
        else speed = 1.5f;
        if (!isGrounded) return;
        if (moveInput == Vector3.zero)
        {
            ChangeState(PlayerState.Idle);
        }
        else if (moveInput != Vector3.zero && !isRunning)
        {
            ChangeState(PlayerState.Walk);
        }
        else if (isRunning)
        {
            ChangeState(PlayerState.Run);
        }
        

    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput.x * speed, rb.linearVelocity.y, moveInput.z * speed);
        HandleRotation();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput2D = context.ReadValue<Vector2>();
        moveInput = new Vector3(moveInput2D.x, 0, moveInput2D.y);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
        }
        else isRunning = false;
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
    public void HandleRotation()
    {
        if (moveInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 10f);
        }
    }
    public void JumpByButton()
    {
        if (!isGrounded) return;
        isGrounded = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jump");
        ChangeState(PlayerState.Jump);

    }

    public void EndAnim()
    {
        isGrounded = true;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (playerState == PlayerState.Jump && moveInput == Vector3.zero) ChangeState(PlayerState.JumpLand);
            else isGrounded = true;
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

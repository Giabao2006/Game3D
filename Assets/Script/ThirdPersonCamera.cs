
using UnityEngine;


public class ThirdPersonCamera : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Transform orientation;
    public Transform thirdPersonCam;
    public Transform player;
    public Transform playerobj;
    public Rigidbody rb;
    public float rotationSpeed;
    void Start()
    {
        ChangeStateCamera(CameraStyle.ThirdPerson);
    }
    private void FixedUpdate()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
    }
    public void ChangeStateCamera(CameraStyle newStyle)
    {
        if (newStyle == CameraStyle.FreeLook)
        {

            // float horizontalInput = playerMovement.moveInput.x;
            // float verticalInput = playerMovement.moveInput.z;

            // Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            // if (inputDir != Vector3.zero)
            // {
            //     player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            // }
        }
        else if (newStyle == CameraStyle.ThirdPerson)
        {
            Vector3 dirThirdPerson = thirdPersonCam.position - new Vector3(transform.position.x, thirdPersonCam.position.y, transform.position.z);
            orientation.forward = dirThirdPerson.normalized;
            Vector3 inputDir = orientation.forward;
            if (inputDir != Vector3.zero)
            {
                playerobj.forward = Vector3.Slerp(playerobj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (newStyle == CameraStyle.FirstPerson)
        {
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;
            Vector3 inputDir = orientation.forward;
            if (inputDir != Vector3.zero)
            {
                playerobj.forward = Vector3.Slerp(playerobj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
          
    }
    public enum CameraStyle
    {
        FreeLook,
        ThirdPerson,
        FirstPerson
    }
}

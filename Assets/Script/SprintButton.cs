using UnityEngine;
using UnityEngine.EventSystems;


public class SprintButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerMovement playerMovement;
    public void OnPointerDown(PointerEventData eventData)
    {
        playerMovement.isRunning=true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerMovement.isRunning=false;
    }
}

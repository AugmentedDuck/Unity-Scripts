using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public bool isOnConveyor1 = true;
    public bool isOnConveyor2 = true;


    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePositionOffset = Input.mousePosition - GetMousePosition();
        gameObject.layer = 2; //Set object to ignore raycasts
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition - mousePositionOffset); //Make a ray from camera to mouse position
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            transform.SetPositionAndRotation(hit.point, hit.transform.rotation); //Rotate the object to match the hit object, and place it at hitpoint
            
            if(hit.transform.CompareTag("Conveyor"))
            {
                isOnConveyor1 = true;
                isOnConveyor2 = false;
            }
            else if (hit.transform.CompareTag("Conveyor 2"))
            {
                isOnConveyor1 = false;
                isOnConveyor2 = true;
            }
            else
            {
                isOnConveyor1 = false;
                isOnConveyor2 = false;
            }
        }
    }

    private void OnMouseUp()
    {
        gameObject.layer = 0; //Make the object react to raycasts
    }
}

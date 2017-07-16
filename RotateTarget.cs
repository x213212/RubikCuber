using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RotateTarget : MonoBehaviour , IPointerDownHandler
{

    public Transform target;
    public float speed = 10;

   public bool isMouseDown = false;
    private Vector3 lastPosition;
    public float angle = 30f;
    public float distance = 2f;
    public bool click_scor = false;
    Quaternion q = Quaternion.identity;
    void Update () 
    {
        if (Input.GetMouseButtonDown(0) && click_scor == false)
        {
            isMouseDown = true;
            lastPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            click_scor = false;
            GameObject tmp = GameObject.Find("Rotate");

                tmp.GetComponent<RotateTarget>().enabled = true;
        }


        if (isMouseDown && Input.mousePosition != lastPosition && click_scor==false)
        {
            if (target == null)
                return ;
            Vector3 offset = Input.mousePosition - lastPosition;
            target.localRotation = Quaternion.Euler(offset.y * Time.deltaTime * speed, -offset.x * Time.deltaTime * speed, 0) * target.localRotation;
            lastPosition = Input.mousePosition;
        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Scrollbar") {
            click_scor = true;
            GameObject tmp = GameObject.Find("Rotate");
 
                tmp.GetComponent<RotateTarget>().enabled = false;
        }
      
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
 
            Vector2 touchPosition = Input.GetTouch(0).position;


            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {

                GameObject obj = hit.collider.gameObject;


                Plant1 objectInteraction = obj.GetComponent<Plant1>();
                if (objectInteraction != null)
                {
 
                    objectInteraction.OnObjectClicked();
                }
            }
        }
    }
}

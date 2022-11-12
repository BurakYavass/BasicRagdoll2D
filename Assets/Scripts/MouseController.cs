using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private RagdollController rdController;
    private Vector2 offset;
    private Rigidbody2D _hitObject;
    
  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPPosition = Input.mousePosition;
            mouseWorldPPosition.z = 0f;
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit.collider != null)
            {
                offset = (Vector2)hit.collider.transform.position - hit.point ;
                _hitObject = hit.rigidbody;
                
                if (hit.collider.gameObject.CompareTag("Character"))
                {
                    if (!rdController.RagdollActive)
                    {
                        rdController.ActivateRagdoll();
                    }
                    else
                    {
                        rdController.DisableRagdoll();
                    }  
                }
            }
        }

        if (Input.GetMouseButton(0) &&_hitObject)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            _hitObject.MovePosition(mousePosition + offset);
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (_hitObject &&_hitObject.CompareTag("Character"))
            {
                if (!rdController.RagdollActive)
                {
                    rdController.ActivateRagdoll();
                }
                else
                {
                    rdController.DisableRagdoll();
                } 
            }
            _hitObject = null;
        }
    }
}

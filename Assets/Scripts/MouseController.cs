using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private RagdollController rdController;
    private Vector2 offset;
    [SerializeField] private Rigidbody2D _hitObject;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask objectMask;
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPPosition = Input.mousePosition;
            mouseWorldPPosition.z = 0f;
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,objectMask);
            
            if (hit.collider != null)
            {
                offset = (Vector2)hit.collider.transform.position - hit.point ;
                if (hit.collider.gameObject.CompareTag("Character"))
                {
                    _hitObject = hit.rigidbody;
                    
                    if (!rdController.RagdollActive)
                    {
                        rdController.ActivateRagdoll();
                    }
                    else
                    {
                        rdController.DisableRagdoll();
                    }  
                }
                else if (hit.collider.CompareTag("BallCenter"))
                {
                    _hitObject = hit.rigidbody;
                }
            }
        }

        if (Input.GetMouseButton(0) &&_hitObject)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,layerMask );

            if (!hit || hit.collider.CompareTag("Ground"))
            {
                _hitObject = null;
                return;
            }
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

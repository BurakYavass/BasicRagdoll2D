using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D hitObject;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask objectMask;

    private RagdollController _rdController;
    private Camera _cameraMain;
    private ObjectId _objectId;
    
    private Vector2 _offset;

    private void Start()
    {
        _cameraMain = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPPosition = Input.mousePosition;
            mouseWorldPPosition.z = 0f;
            
            RaycastHit2D hit = Physics2D.Raycast(_cameraMain.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,objectMask);
            
            if (hit.collider != null)
            {
                _offset = (Vector2)hit.collider.transform.position - hit.point;
                _objectId = hit.collider.GetComponentInParent<ObjectId>();
                if (_objectId != null)
                {
                    if (_objectId.objId == 10)
                    {
                        hitObject = hit.rigidbody;
                        _rdController = _objectId.ragdollController;
                        if (!_rdController.RagdollActive)
                        {
                            _rdController.ActivateRagdoll();
                        }
                        else
                        {
                            _rdController.DisableRagdoll();
                        }  
                    }
                    else if (_objectId.objId == 20)
                    {
                        hitObject = hit.rigidbody;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) &&hitObject)
        {
            Vector2 mousePosition = _cameraMain.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(_cameraMain.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,layerMask );

            if (!hit || hit.collider.CompareTag("Obstacle"))
            {
                if (_objectId.objId == 10)
                {
                    _rdController.DisableRagdoll();
                }
                else if (_objectId.objId == 20)
                {
                    //Debug.Log("Ball");
                }
                hitObject = null;
                return;
            }
            hitObject.MovePosition(mousePosition + _offset);
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (hitObject && _objectId.objId == 10)
            {
                if (_rdController.RagdollActive)
                {
                    _rdController.DisableRagdoll();
                }
            }
            hitObject = null;
        }
    }
}

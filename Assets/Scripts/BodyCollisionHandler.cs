using System;
using System.Collections.Generic;
using UnityEngine;


public class BodyCollisionHandler : MonoBehaviour
{
    private Rigidbody2D _hipsRb2D;
    private RagdollController _rdController;
    
    private void Start()
    {
        _hipsRb2D = GetComponent<Rigidbody2D>();
        _rdController = GetComponentInParent<RagdollController>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ball")|| col.collider.CompareTag("BallCenter"))
        {
            
            //_hipsRb2D.freezeRotation = false;
            //_rdController.ActivateRagdoll();
            //_rdController.RecordTransform();
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ball")|| other.collider.CompareTag("BallCenter"))
        {
            
            //_hipsRb2D.freezeRotation = true;
            //_rdController.DisableRagdoll();
        }
    }
    
}

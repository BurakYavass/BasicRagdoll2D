using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyCollisionHandler : MonoBehaviour
{
    private RagdollController _rdController;
    private bool _active = false;
    private bool _ragdollDisable = false;
    
    private void Start()
    {
        _rdController = GetComponentInParent<RagdollController>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ball")|| col.collider.CompareTag("BallCenter"))
        {
            if (!_active)
            {
                _rdController.ActivateRagdoll();
                StartCoroutine(Timer());
                _active = true;
            }
        }
    }

    private void Update()
    {
        if (_ragdollDisable)
        {
            _ragdollDisable = false;
            _active = false;
            _rdController.DisableRagdoll();
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        _ragdollDisable = true;
    } 

}

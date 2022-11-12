using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour
{
    //private RectTransform _rectTransform;
    
    [SerializeField] private RagdollController _rdController;

    private bool _isDragging;

    private void Update()
    {
        if (_isDragging)
        {
            //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //transform.Translate(mousePosition);
        }
    }


    public void OnMouseDown()
    {
        _isDragging = true;
        Debug.Log("On Pointer Down");
        if (_rdController != null)
        {
            if (!_rdController.RagdollActive)
            {
                _rdController.ActivateRagdoll();
            }
            else
            {
                _rdController.DisableRagdoll();
            }  
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }
}

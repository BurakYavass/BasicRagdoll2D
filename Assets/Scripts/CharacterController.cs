using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private RagdollController rdController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

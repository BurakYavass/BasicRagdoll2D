using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public bool RagdollActive { get; private set; }

    private HingeJoint2D[] _joints;
    private Rigidbody2D[] _rbs;
    [SerializeField] private Rigidbody2D hipsRigidbody2D;

    private Dictionary<Rigidbody2D, Vector3> _initialPos = new Dictionary<Rigidbody2D, Vector3>();
    private Dictionary<Rigidbody2D, Quaternion> _initialRot = new Dictionary<Rigidbody2D, Quaternion>();

  
    void Awake()
    {
        _joints = GetComponentsInChildren<HingeJoint2D>();
        _rbs = GetComponentsInChildren<Rigidbody2D>();

        foreach (var rb in _rbs)
        {
            if (rb != hipsRigidbody2D)
            {
                _initialPos.Add(rb, rb.transform.localPosition);
            }
            _initialRot.Add(rb, rb.transform.localRotation);
        }

        RecordTransform();
        DisableRagdoll();
    }

    void RecordTransform()
    {
        foreach (var rb in _rbs)
        {
            if (rb != hipsRigidbody2D)
            {
                _initialPos[rb] = rb.transform.localPosition;
            }
            _initialRot[rb] = rb.transform.localRotation;
        }
    }

    public void ActivateRagdoll()
    {
        RagdollActive = true;
     
        foreach (var joint in _joints)
        {
            var jointAngleLimits2D = joint.limits;
            jointAngleLimits2D.min = -30f;
            jointAngleLimits2D.max = 30f;
            joint.limits = jointAngleLimits2D;
        }
        
    }

    public void DisableRagdoll()
    {
        RagdollActive = false;
        
        foreach (var rb in _rbs)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            // if (_initialRot[rb] != rb.transform.localRotation)
            // {
            //     rb.transform.localRotation = Quaternion.Lerp(rb.transform.localRotation,_initialRot[rb], 5.0f);
            // }
            //
            // if (_initialPos.TryGetValue(rb,out var pos))
            // {
            //     rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition,pos, 5.0f);
            // }
        }

        foreach (var joint in _joints)
        {
            var jointAngleLimits2D = joint.limits;
            jointAngleLimits2D.min = 0f;
            jointAngleLimits2D.max = 0f;
            joint.limits = jointAngleLimits2D;
        }
    }
    
    
}

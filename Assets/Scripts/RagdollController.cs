using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] GameObject leftHandIk, rightHandIk, leftFootIk, rightFootIk;

    //[SerializeField] Animator animator;
    public bool RagdollActive { get; private set; }

    private HingeJoint2D[] _joints;
    private Rigidbody2D[] _rbs;

    private Dictionary<Rigidbody2D, Vector3> _initialPos = new Dictionary<Rigidbody2D, Vector3>();
    private Dictionary<Rigidbody2D, Quaternion> _initialRot = new Dictionary<Rigidbody2D, Quaternion>();

  
    void Awake()
    {
        _joints = GetComponentsInChildren<HingeJoint2D>();
        _rbs = GetComponentsInChildren<Rigidbody2D>();

        foreach (var rb in _rbs)
        {
            _initialPos.Add(rb, rb.transform.localPosition);
            _initialRot.Add(rb, rb.transform.localRotation);
        }

        DisableRagdoll();
    }

    void RecordTransform()
    {
        foreach (var rb in _rbs)
        {
            _initialPos[rb] = rb.transform.localPosition;
            _initialRot[rb] = rb.transform.localRotation;
        }
    }

    public void ActivateRagdoll()
    {
        RagdollActive = true;
        RecordTransform(); //record last bones transform, for disabling later
        //animator.enabled = false;
        foreach (var rb in _rbs)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        foreach (var joint in _joints)
        {
            //joint.enabled = true;
            var jointAngleLimits2D = joint.limits;
            jointAngleLimits2D.min = -30f;
            jointAngleLimits2D.max = 30f;
            joint.limits = jointAngleLimits2D;
        }
        
    }

    public void DisableRagdoll()
    {
        RagdollActive = false;
        //animator.enabled = true;
        foreach (var rb in _rbs)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            //rb.bodyType = RigidbodyType2D.Kinematic;

            //rb.transform.localPosition = _initialPos[rb];
            //rb.transform.localRotation = _initialRot[rb];
        }

        foreach (var joint in _joints)
        {
            var jointAngleLimits2D = joint.limits;
            jointAngleLimits2D.min = 0f;
            jointAngleLimits2D.max = 0f;
            joint.limits = jointAngleLimits2D;
            //joint.enabled = false;
        }
    }
}

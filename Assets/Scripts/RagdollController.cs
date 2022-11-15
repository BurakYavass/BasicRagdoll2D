using UnityEngine;

public class RagdollController : MonoBehaviour
{  
    public bool RagdollActive { get; private set; }

    private HingeJoint2D[] _joints;
    private Rigidbody2D[] _rbs;
    private Quaternion _hips;
    

  
    void Awake()
    {
        _joints = GetComponentsInChildren<HingeJoint2D>();
        _rbs = GetComponentsInChildren<Rigidbody2D>();
        
        DisableRagdoll();
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

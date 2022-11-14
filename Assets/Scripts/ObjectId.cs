using System;
using UnityEngine;

public class ObjectId : MonoBehaviour
{
    public int objId;
    [NonSerialized] public RagdollController ragdollController;

    private void Awake()
    {
        if (objId == 10)
        {
            ragdollController = GetComponent<RagdollController>();
        }
    }
}

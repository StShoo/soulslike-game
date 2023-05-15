using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public abstract class BaseEventTrigger : MonoBehaviour
{
    [SerializeField] protected Collider interactableCollider;

    protected virtual void OnTriggerEnter(Collider other)
    {
        StartEvent();
        Destroy(gameObject.GetComponent<SphereCollider>());
    }

    protected abstract void StartEvent();
}

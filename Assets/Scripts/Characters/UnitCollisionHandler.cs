using System;
using UnityEngine;

public class UnitCollisionHandler : MonoBehaviour, ICollision
{
    [SerializeField] private Cloud _cloud;

    public event Action WasCollised;

    public void OnCollisionAction(Vector3 collisionObjectPosition)
    {
        WasCollised?.Invoke();
        _cloud.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollision collision))
        {
            collision.OnCollisionAction(transform.position);
            WasCollised?.Invoke();
        }
    }
}

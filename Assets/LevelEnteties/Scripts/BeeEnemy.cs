using UnityEngine;

public class BeeEnemy : MonoBehaviour, ICollision
{
  [SerializeField] private Bang _bangFX;

  public void OnCollisionAction(Vector3 collisionObjectPosition)
  {
    Instantiate(_bangFX, collisionObjectPosition, Quaternion.identity);
  }
}
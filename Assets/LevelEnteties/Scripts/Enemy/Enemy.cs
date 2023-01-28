using UnityEngine;

public class Enemy : MonoBehaviour, ICollision
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Bang _bangTemplate;

    public void OnCollisionAction(Vector3 collisionObjectPosition)
    {
        Instantiate(_bangTemplate, collisionObjectPosition, Quaternion.identity);
        StopMove();
    }

    public void StartMove() => _enemyMovement.StartMove();

    public void StopMove() => _enemyMovement.StopMove();
}
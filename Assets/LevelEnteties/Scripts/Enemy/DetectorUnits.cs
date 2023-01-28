using UnityEngine;

public class DetectorUnits : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private float _radius = 2.0f;

    private Unit _targetUnit;

    private bool TryDetectUnit()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.TryGetComponent(out Unit unit))
            {
                _targetUnit = unit;
                _enemyMovement.Init(_targetUnit);
                enabled = false;
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        TryDetectUnit();
    }
}

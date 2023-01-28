using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour, ICollision
{
    [SerializeField] private Bang _bangFX;

    private Vector3 _openScale = new Vector3(0.07f, 0, 1);
    private float _duration = .2f;
    private SpriteRenderer _renderer;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color) => _renderer.color = color;

    public void Open()
    {
        _collider.enabled = false;

        transform
          .DOScale(_openScale, _duration)
          .OnComplete(DeactivateModel);
    }

    public void OnCollisionAction(Vector3 collisionObjectPosition)
    {
        Instantiate(_bangFX, collisionObjectPosition, Quaternion.identity);
    }

    private void DeactivateModel() => gameObject.SetActive(false);
}
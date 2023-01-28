using UnityEngine;

public class GuideTrail : MonoBehaviour
{
  private TrailRenderer _trailRenderer;
  private Color _endColor;

  private void Awake() => _trailRenderer = GetComponent<TrailRenderer>();

  public void SetColor(Color color)
  {
    _endColor = color;
    _endColor.a = 0f;
    
    _trailRenderer.startColor = color;
    _trailRenderer.endColor = _endColor;
  }

  public void Restart() => _trailRenderer.Clear();
}
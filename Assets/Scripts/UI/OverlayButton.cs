using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;
using Agava.YandexGames;

public class OverlayButton : MonoBehaviour
{
  private RectTransform _rectTransform;

  [Header("Desktop")] [SerializeField] private Vector3 _desktopPosition;
  [SerializeField] private int _desktopSize;

  [Header("Mobile")] [Space(10)] [SerializeField]
  private Vector3 _mobilePosition;

  [SerializeField] private int _mobileSize;

  private void Awake() => _rectTransform = GetComponent<RectTransform>();

  public void CheckPlatform()
  {
    if (Device.Type == DeviceType.Desktop || Device.Type == DeviceType.TV)
      SetDesktopSize();
    else
      SetMobileSize();
  }


  private void SetDesktopSize()
  {
    _rectTransform.anchoredPosition = _desktopPosition;
    _rectTransform.sizeDelta = new Vector2(_desktopSize, _desktopSize);
  }

  private void SetMobileSize()
  {
    _rectTransform.anchoredPosition = _mobilePosition;
    _rectTransform.sizeDelta = new Vector2(_mobileSize, _mobileSize);
  }
}
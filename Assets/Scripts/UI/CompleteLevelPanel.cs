using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CompleteLevelPanel : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = .2f;

    private CanvasGroup _canvasGroup;

    public event Action OpenedCompletePanel;

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Show();
    }

    private void Show()
    {
        OpenedCompletePanel?.Invoke();
        _canvasGroup.DOFade(1, _fadeDuration);
    }
}
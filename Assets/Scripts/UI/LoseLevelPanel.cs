using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLevelPanel : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = .2f;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, _fadeDuration);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

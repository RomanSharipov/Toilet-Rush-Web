using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseButton : MonoBehaviour
{
    [SerializeField] RectTransform _disableWindow;
    private Button _closeButton;

    private void Awake()
    {
        _closeButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _disableWindow.gameObject.SetActive(false);

    }
}

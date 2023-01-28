using Agava.YandexGames;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FinalGameUI : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private CanvasGroup _canvasGroup;
    private readonly float _duration = .3f;
    private readonly Vector3 _offset = new Vector3(0.1f, 0.1f, 0.1f);

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _closeButton.onClick.AddListener(ClosePanel);
    }

    public void ShowPanel()
    {
        Sequence _sequence = DOTween.Sequence();

        _canvasGroup.interactable = true;

        _sequence
          .Append(transform.DOScale(Vector3.one + _offset, _duration))
          .Append(transform.DOScale(Vector3.one, 0.2f));

        int currentLevelIndex = LevelIndexSaver.LoadLevel() + 1;
        LevelIndexSaver.SaveLevel(currentLevelIndex);

#if UNITY_EDITOR
        return;
#endif
        if (PlayerAccount.IsAuthorized && PlayerAccount.HasPersonalProfileDataPermission)
        {
            LeaderBoardSaver.AddScoreLeaderBoard();
        }
    }

    private void ClosePanel()
    {
        _canvasGroup.interactable = false;
        transform.DOScale(Vector3.zero, _duration);
    }
}
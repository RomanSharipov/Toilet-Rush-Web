using System;
using System.Collections;
using Agava.YandexMetrica;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private CompleteLevelPanel _completePanel;
    [SerializeField] private LoseLevelPanel _loseLevelPanel;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _restartCompleteButton;
    [SerializeField] private Button _restartFailedButton;
    [SerializeField] private Button[] _backwardButtons;
    [SerializeField] private InGameOverlay _gameOverlay;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button[] _skipLevelButtons;
    [SerializeField] private Button _resetLinesButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private OptionsPanel _optionsPanel;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _helpButton;
    [SerializeField] private RewardAdvertising _reward;
    [SerializeField] private FinalGameUI _finalGameUI;
    [SerializeField] private Button _finalPanelButton;

    private Coroutine _coroutine;
    private float _delay = 1.5f;

    public LoseLevelPanel LoseLevelPanel => _loseLevelPanel;

    public event Action BackwardClicked;
    public event Action RestartClicked;
    public event Action NextClicked;

    public void ShowGuidesTest() => _levelHandler.ShowGuides();

    public void SkipLevelTest() => OnNextButtonClick();

    private void OnEnable()
    {
        _helpButton.onClick.AddListener(OnClickHelpButton);
        _optionsButton.onClick.AddListener(OpenOptionsPanel);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _restartCompleteButton.onClick.AddListener(OnRestartButtonClick);
        _restartFailedButton.onClick.AddListener(OnRestartButtonClick);
        _nextButton.onClick.AddListener(OnNextButtonClick);
        _resetLinesButton.onClick.AddListener(OnRestartButtonClick);

        for (int i = 0; i < _skipLevelButtons.Length; i++)
            _skipLevelButtons[i].onClick.AddListener(OnSkipLevelButton);

        for (int i = 0; i < _backwardButtons.Length; i++)
            _backwardButtons[i].onClick.AddListener(ClosePanelAndOpenMainMenu);
    }


    private void OnDisable()
    {
        _helpButton.onClick.RemoveListener(OnClickHelpButton);
        _optionsButton.onClick.RemoveListener(OpenOptionsPanel);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
        _restartFailedButton.onClick.AddListener(OnRestartButtonClick);
        _restartCompleteButton.onClick.RemoveListener(OnRestartButtonClick);
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
        _resetLinesButton.onClick.RemoveListener(OnRestartButtonClick);

        for (int i = 0; i < _skipLevelButtons.Length; i++)
        {
            _skipLevelButtons[i].onClick.RemoveListener(OnSkipLevelButton);
        }

        for (int i = 0; i < _backwardButtons.Length; i++)
        {
            _backwardButtons[i].onClick.RemoveListener(ClosePanelAndOpenMainMenu);
        }
    }

    public void RaiseLevelCounter()
    {
        _gameOverlay.SetLevelText();
        _mainMenu.SetCounter();
    }

    public void ShowFinalPanel() => _finalGameUI.ShowPanel();

    public void ChangeCompletePanelState(bool state) => _coroutine = StartCoroutine(CompletePanelWaiter(state));

    private void OnShopButtonClick() => _shopPanel.ChangeActiveState(true);

    private void OnRestartButtonClick() => RestartClicked?.Invoke();

    private void OnNextButtonClick()
    {
        NextClicked?.Invoke();
        LoseLevelPanel.Close();
    }

    private void OnClickHelpButton() => _reward.TryShowAD(ShowGuides);

    private void ShowGuides()
    {
        _levelHandler.ShowGuides();

#if UNITY_EDITOR
        return;
#endif
        YandexMetrica.Send("Showguides");
    }

    private void OpenOptionsPanel() => _optionsPanel.ShowPanel();

    private void ClosePanelAndOpenMainMenu()
    {
        _completePanel.gameObject.SetActive(false);
        LoseLevelPanel.Close();
        _mainMenu.Show();
        BackwardClicked?.Invoke();
    }

    private IEnumerator CompletePanelWaiter(bool state)
    {
        var delay = new WaitForSeconds(_delay);

        if (state)
            yield return delay;

        _completePanel.gameObject.SetActive(state);
    }

    private void OnSkipLevelButton()
    {
        int currentLevel = LevelIndexSaver.LoadLevel();

        if (_levelHandler.CountLevels == currentLevel + 1)
        {
            LevelIndexSaver.SaveLevel(-1);
        }
        
        _reward.TryShowAD(OnRewardSkipLevel);
#if UNITY_EDITOR
        return;
#endif
        LeaderBoardSaver.AddScoreLeaderBoard();
    }

    private void OnRewardSkipLevel()
    {
        OnNextButtonClick();
#if UNITY_EDITOR
        return;
#endif
        YandexMetrica.Send("SkipLevel");
    }
}
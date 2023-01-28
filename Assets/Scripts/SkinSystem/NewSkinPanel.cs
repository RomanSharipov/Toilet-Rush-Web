using DG.Tweening;
using UnityEngine;

public class NewSkinPanel : MonoBehaviour
{
    [SerializeField] private DataSkins _dataSkins;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private RewardAdvertising _rewardAdvertising;

    private SkinPanel _newSkinPanel;
    private int _indexNewSkin;
    private CanvasGroup _canvasGroup;
    private Vector3 _offset = new Vector3(0.1f, 0.1f, 0.1f);
    private float _duration = .3f;

    private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

    private void OnDisable()
    {
        if (_newSkinPanel == null)
            return;

        _newSkinPanel.ClickedButton -= OnClickButton;
    }

    public void ShowNewSkin()
    {
        SkinPanel newSkinInShop = _dataSkins.GetSkin(_indexNewSkin);
        _newSkinPanel = Instantiate(newSkinInShop, transform);
        _newSkinPanel.Init(_rewardAdvertising);
        _newSkinPanel.SetButtonPosition();
        _newSkinPanel.ClickedButton += OnClickButton;

        _indexNewSkin++;
        ES3.Save(SkinSaver.IndexNewSkin, _indexNewSkin);
    }

    public void OnClickButton()
    {
        _shopPanel.SaveSkin(_newSkinPanel.SkinType);
        _newSkinPanel.CheckPanelState();
        _newSkinPanel.ClickedButton -= OnClickButton;
        Destroy(_newSkinPanel.gameObject);
        Close();
    }

    public void Close()
    {
        _newSkinPanel.CheckPanelState();
        Destroy(_newSkinPanel.gameObject);
        gameObject.SetActive(false);
    }

    public void TryOpen()
    {
        _indexNewSkin = ES3.Load(SkinSaver.IndexNewSkin, 0);

        if (_indexNewSkin >= _dataSkins.CountSkins)
            return;

        Open();
        gameObject.SetActive(true);
        ShowNewSkin();
    }

    private void Open()
    {
        Sequence _sequence = DOTween.Sequence();

        _canvasGroup.interactable = true;

        _sequence
          .Append(transform.DOScale(Vector3.one + _offset, _duration))
          .Append(transform.DOScale(Vector3.one, 0.2f));
    }
}
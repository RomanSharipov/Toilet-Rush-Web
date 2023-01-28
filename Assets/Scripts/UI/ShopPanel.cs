using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    private const string Male = "Male";
    private const string Female = "Female";

    [SerializeField] private Button _exitButton;
    [SerializeField] private List<SkinPanel> _panels;
    [SerializeField] private int _currentMale;
    [SerializeField] private int _currentFemale;
    [SerializeField] private Transform _containerSkins;
    [SerializeField] private RewardAdvertising _rewardAdvertising;
    [SerializeField] private DataSkins _dataSkins;

    private SkinType _male;
    private SkinType _female;
    private int _countNewSkins;

    public void AddSkinsByPlayerPrefs()
    {
        _countNewSkins = ES3.Load(SkinSaver.IndexNewSkin, 0);

        for (int i = 0; i < _countNewSkins; i++)
        {
            SkinPanel newSkinInShop = _dataSkins.GetSkin(i);

            AddSkinToShop(newSkinInShop);
        }
    }

    private void OnEnable()
    {
        foreach (var t in _panels)
        {
            t.SetStatesByPlayerPrefs();
            t.CheckPanelState();
            t.OnClick += SaveSkin;
        }
           

        _exitButton.onClick.AddListener(OnExitButtonClick);

    }

    private void OnDisable()
    {
        foreach (var t in _panels)
            t.OnClick -= SaveSkin;

        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void Start() => LoadShop();

    private void LoadShop()
    {
        _currentFemale = ES3.Load(Female, 1);
        _currentMale = ES3.Load(Male, 0);

        _panels[_currentFemale].Activate();
        _panels[_currentMale].Activate();
    }
    
    public void SaveSkin(SkinType skinType)
    {
        if (skinType.Gender == Gender.Female)
        {
            if (skinType.Index != _currentFemale)
                _panels[_currentFemale].Deactivate();

            _currentFemale = skinType.Index;
            ES3.Save(Female, _currentFemale);
            SkinSaver.SaveFemaleSkinIndex(skinType.Index);
        }
        else
        {
            if (skinType.Index != _currentMale)
                _panels[_currentMale].Deactivate();

            _currentMale = skinType.Index;
            ES3.Save(Male, _currentMale);
            SkinSaver.SaveMaleSkinIndex(skinType.Index);
        }
    }
    
    private void OnExitButtonClick() => ChangeActiveState(false);

    public void ChangeActiveState(bool state) => gameObject.SetActive(state);

    public void AddSkinToShop(SkinPanel skinPanel)
    {
        SkinPanel newSkinPanel = Instantiate(skinPanel);
        _panels.Add(newSkinPanel);
        newSkinPanel.OnClick += SaveSkin;
        newSkinPanel.transform.parent = _containerSkins;
        newSkinPanel.Init(_rewardAdvertising);
    }
}
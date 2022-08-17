using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLevelManager : MonoBehaviour
{
    private GameObject _gameObject;
    private GameObject _itemTemplate;
    [SerializeField] private Transform levelContent;
    [Serializable]
    public class LevelItem
    {
        public String levelText;
        
        
    }
    [SerializeField]
    public List<LevelItem> list;

    private void Start()
    {
        PlayerPrefs.SetInt("Level1",1);
        _itemTemplate = levelContent.GetChild(0).gameObject;
        for (int i = 0; i < list.Count; i++)
        {
            _gameObject = Instantiate(_itemTemplate, levelContent);
            _gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = list[i].levelText;
            var btn = _gameObject.GetComponent<Button>();
            btn.interactable = PlayerPrefs.GetInt("Level"+(i+1))>0;
            btn.AddEventListener(i,OnButtonClicked);
        }
        Destroy(_itemTemplate);
    }

    private void OnButtonClicked(int i)
    {
        SceneManager.LoadScene(i + 1);
    }
}

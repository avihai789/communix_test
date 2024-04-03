using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonsContainer : MonoBehaviour
{
    [SerializeField] private DifficultyButton difficultyButton;
    
    public event Action OnDifficultyChosenEvent;
    
    public void SetData(AllDifficulties allDifficulties)
    {
        SetDifficultyButtons(allDifficulties);
        GetComponent<VerticalLayoutGroup>().enabled = false;
        GetComponent<ContentSizeFitter>().enabled = false;
    }
    private void SetDifficultyButtons(AllDifficulties allDifficulties)
    {
        InstantiateDifficultyButton(allDifficulties.Easy);
        InstantiateDifficultyButton(allDifficulties.Medium);
        InstantiateDifficultyButton(allDifficulties.Hard);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private void InstantiateDifficultyButton(Difficulty difficulty)
    {
        var button = Instantiate(difficultyButton, transform);
        button.SetButtonData(difficulty);
        button.OnClickEvent += OnDifficultyButtonClick;
    }

    private void OnDifficultyButtonClick(Difficulty obj)
    {
        gameObject.SetActive(false);
        OnDifficultyChosenEvent?.Invoke();
    }
}

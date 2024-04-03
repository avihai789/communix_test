using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonsContainer : MonoBehaviour
{
    [SerializeField] private DifficultyButton difficultyButton;
    
    public event Action OnDifficultyChosenEvent;
    
    private List<DifficultyButton> difficultyButtons = new List<DifficultyButton>();
    
    public void SetData(Dictionary<string, Difficulty> allDifficulties)
    {
        SetDifficultyButtons(allDifficulties);
        GetComponent<VerticalLayoutGroup>().enabled = false;
        GetComponent<ContentSizeFitter>().enabled = false;
    }
    private void SetDifficultyButtons(Dictionary<string, Difficulty> allDifficulties)
    {
        foreach (var difficulty in allDifficulties.Values)
        {
            InstantiateDifficultyButton(difficulty);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private void InstantiateDifficultyButton(Difficulty difficulty)
    {
        var button = Instantiate(difficultyButton, transform);
        button.SetButtonData(difficulty);
        difficultyButtons.Add(button);
        button.OnClickEvent += OnDifficultyButtonClick;
    }

    private void OnDifficultyButtonClick(Difficulty obj)
    {
        foreach (var button in difficultyButtons)
        {
            button.OnClickEvent -= OnDifficultyButtonClick;
            Destroy(button.gameObject);
        }
        gameObject.SetActive(false);
        OnDifficultyChosenEvent?.Invoke();
    }
}

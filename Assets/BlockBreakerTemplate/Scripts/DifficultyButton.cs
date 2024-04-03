using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private Text difficultyText;
    
    public event Action<Difficulty> OnClickEvent;
    
    private Difficulty _difficulty;
    
    public void SetButtonData(Difficulty difficulty)
    {
        difficultyText.text = difficulty.ToString();
        _difficulty = difficulty;
    }
    
    public void OnClick()
    {
        OnClickEvent?.Invoke(_difficulty);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class MenuUI : MonoBehaviour 
{
	[SerializeField] private DifficultyButtonsContainer difficultyButtonsContainer;
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private RemoteConfig remoteConfig;

	private void Start()
	{
		difficultyButtonsContainer.OnDifficultyChosenEvent += OnDifficultyChosen;
		remoteConfig.OnRemoteConfigValuesFetched += OnRemoteConfigValuesFetched;
	}

	private void OnRemoteConfigValuesFetched(Dictionary<string, Difficulty> allDifficulties)
	{
		remoteConfig.OnRemoteConfigValuesFetched -= OnRemoteConfigValuesFetched;
		difficultyButtonsContainer.SetData(allDifficulties);
	}

	private void OnDifficultyChosen()
	{
		difficultyButtonsContainer.OnDifficultyChosenEvent -= OnDifficultyChosen;
		menuPanel.SetActive(true);
	}

	//Called when the play button is pressed
	public void PlayButton ()
	{
		Application.LoadLevel(1);	//Loads the 'Game' scene to begin the game
	}

	//Called when the quit button is pressed
	public void QuitButton ()
	{
		Application.Quit();			//Quits the game
	}
}

﻿using Game;
using Handlers;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private Animator mainCameraAnimator;
    [SerializeField] private Animator timeTrialAnimator;
    [SerializeField] private Animator championshipAnimator;
    [SerializeField] private Animator levelEditorAnimator;
    [SerializeField] private Animator kartSelectorAnimator;
    [SerializeField] private CustomCanvas gameModeCanvas;

    public void SelectModeTimeTrial()
    {
        timeTrialAnimator.SetBool("Choosen",true);
        levelEditorAnimator.SetBool("NotSelected",true);
        championshipAnimator.SetBool("NotSelected",true);
        mainCameraAnimator.SetTrigger("move");
        gameModeCanvas.disableUIInteraction();
        LevelManager.instance.gameConfig.mode = GameMode.TimeTrial;
    }
    public void SelectModeLevelEditor()
    {
        timeTrialAnimator.SetBool("NotSelected",true);
        levelEditorAnimator.SetBool("Choosen",true);
        championshipAnimator.SetBool("NotSelected",true);
        mainCameraAnimator.SetTrigger("move");
        gameModeCanvas.disableUIInteraction();
        LevelManager.instance.gameConfig.mode = GameMode.Editor;
    }
    public void SelectModeChampionship()
    {
        timeTrialAnimator.SetBool("NotSelected",true);
        levelEditorAnimator.SetBool("NotSelected",true);
        championshipAnimator.SetBool("Choosen",true);
        mainCameraAnimator.SetTrigger("move");
        gameModeCanvas.disableUIInteraction();
        LevelManager.instance.gameConfig.mode = GameMode.Championship;
    }
    public void ShowNextScreen()
    {
        mainCameraAnimator.SetTrigger("move");
    }
    public void ShowPreviousScreen()
    {
        mainCameraAnimator.SetTrigger("back");
    }
    public void HideKartSelector()
    {
        kartSelectorAnimator.SetBool("isHidden", true);
    }

    public void ShowTransition()
    {
        TransitionController.Instance.FadeIn(StartLevel);//TODO
    }

    private void StartLevel() {
        SceneManager.instance.LoadGameMode(LevelManager.instance.gameConfig.mode);//TODO
    }
    public void QuitGame()
    {
        
#if UNITY_EDITOR
        Debug.Log("Quitting the app !");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ShowCredits()
    {
        SceneManager.instance.LoadCredits();//TODO
    }
}

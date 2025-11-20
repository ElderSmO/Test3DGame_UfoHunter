using Assets.GameControl;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] TMP_Text counterText;
    [SerializeField] TMP_Text loseCounterText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] List<GameObject> listForDisable;
    [SerializeField] int deadCounter;
    [SerializeField] int loseCounter;
    [SerializeField] int secondCounter;

    private void Start()
    {
        UfoGameEvents.EnemyIsDead += EnemyDead;
        UfoGameEvents.EnemyIsLose += EnemyLose;
        UfoGameEvents.UpdateTimeChange += AddSecond;
        UfoGameEvents.LoseGameAction += LoseGame;
    }

    void DisableUI()
    {
        for (int i = 0; i < listForDisable.Count; i++)
        {
            listForDisable[i].SetActive(false);
        }
    }
    void EnableUI()
    {
        for (int i = 0; i < listForDisable.Count; i++)
        {
            listForDisable[i].SetActive(true);
        }
    }



    private void EnemyDead()
    {
        deadCounter += 1;
        UpdateUI();
        Debug.Log("EnemyDead");
    }

    private void EnemyLose()
    {
        loseCounter += 1;
        UpdateUI();
    }

    private void AddSecond()
    {
        secondCounter += 1;
        UpdateUI();
    }

    private void LoseGame()
    {
        loseCounter = 0;
        secondCounter = 0;
        deadCounter = 0;
        UpdateUI();

    }

    void UpdateUI()
    {
        timerText.text = secondCounter.ToString();
        loseCounterText.text = loseCounter.ToString();
        counterText.text = deadCounter.ToString();
    }



}

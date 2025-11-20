using Assets.GameControl;
using UnityEngine;

/// <summary>
/// Контрол для событий
/// </summary>
public class GameControl : MonoBehaviour
{
    public void StartGame()
    {
        UfoGameEvents.OnStartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetActiveTrue(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void SetActiveFalse(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

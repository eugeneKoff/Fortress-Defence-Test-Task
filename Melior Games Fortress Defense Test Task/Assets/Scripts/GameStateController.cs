using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateController : MonoBehaviour
{
    public FortController fortController;
    public EnemySpawner spawner;
    public Canvas defeatUI;
    public Canvas winUI;
    public TextMeshProUGUI enemyCount;

    private void OnEnable()
    {
        Time.timeScale = 1;

        fortController.FortDefeated += OnGameDefeat;

        spawner.AllEnemiesDefeated += OnGameWin;
    }

    private void OnGameDefeat()
    {
        defeatUI.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    private void OnGameWin()
    {
        winUI.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Update()
    {
        enemyCount.text = $"Enemies: { spawner.enemiesKilled} / { spawner.enemiesQuantity}";
    }
}

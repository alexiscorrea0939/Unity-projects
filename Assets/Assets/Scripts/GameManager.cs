using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Ready, Playing, Ended };

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Ready;
    public RawImage background, plataform;
    public float parallaxSpeed = 0.02f;
    public GameObject uiReady, UiScore;

    private bool isRestarting = false; // CAMBIO: Añadida una variable para evitar múltiples reinicios

    void Update()
    {
        bool action = Input.GetKeyDown("space") || Input.GetMouseButtonDown(0);
        HandleJump(action);
        HandleCollisions();
        UpdateParallax();
        UpdateGameState(action);
        HandleExit();
    }

    void HandleJump(bool action)
    {
        if (gameState == GameState.Playing && action)
        {
            PlayerManager.Instance.SetAnimation("PlayerJump");
        }
    }

    void HandleCollisions()
    {
        if (gameState == GameState.Playing && PlayerManager.Instance.enemyCollision)
        {
            gameState = GameState.Ended;
            PlayerManager.Instance.SetAnimation("PlayerDie");
            SpawnManager.Instance.StopSpawn();
            SpeedManager.Instance.ResetSpeed();
        }
    }

    void UpdateGameState(bool action)
    {
        if (gameState == GameState.Ready && action)
        {
            gameState = GameState.Playing;
            uiReady.SetActive(false);
            UiScore.SetActive(true);

            PlayerManager.Instance.SetAnimation("PlayerRun");
            SpawnManager.Instance.StartSpawn();
            SpeedManager.Instance.StartSpeedIncrease();
        }
        else if (gameState == GameState.Ended && action)
        {
            HandleRestart(); // CAMBIO: Se asegura que se invoque el reinicio al detectar acción
        }
    }

    void UpdateParallax()
    {
        if (gameState == GameState.Playing)
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
            plataform.uvRect = new Rect(background.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
        }
    }

    void HandleRestart()
    {
        if (!isRestarting) // CAMBIO: Verificación para evitar múltiples reinicios
        {
            isRestarting = true; // CAMBIO: Se marca que el reinicio está en progreso
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
        }
    }

    void HandleExit()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
}

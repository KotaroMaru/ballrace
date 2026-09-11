using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// レース中のポーズ（一時停止）、操作説明確認、リトライ、タイトルへ戻る処理を管理するスクリプト
/// </summary>
public class PauseAndRuleManager : MonoBehaviour
{
    [Header("--- UI Panels ---")]
    [Tooltip("ポーズメニューパネル")]
    public GameObject pausePanel;

    [Tooltip("操作説明・ルールパネル")]
    public GameObject rulePanel;

    [Header("--- Settings ---")]
    [Tooltip("ポーズキー（デフォルト: Escape）")]
    public KeyCode pauseKey = KeyCode.Escape;

    [Tooltip("タイトルシーンの名前")]
    public string titleSceneName = "TitleScene";

    private bool isPaused = false;

    void Start()
    {
        // 初期状態は非表示
        if (pausePanel != null) pausePanel.SetActive(false);
        if (rulePanel != null) rulePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (rulePanel != null && rulePanel.activeSelf)
            {
                // ルール画面が開いている場合はルールを閉じてポーズメニューに戻る
                BackToPauseFromRule();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// ゲームを一時停止してポーズメニューを開く
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // 時間停止
        if (pausePanel != null) pausePanel.SetActive(true);
        if (rulePanel != null) rulePanel.SetActive(false);
    }

    /// <summary>
    /// ゲームを再開
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // 時間再開
        if (pausePanel != null) pausePanel.SetActive(false);
        if (rulePanel != null) rulePanel.SetActive(false);
    }

    /// <summary>
    /// ポーズ中にルール・操作説明画面を開く
    /// </summary>
    public void OpenRuleFromPause()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (rulePanel != null) rulePanel.SetActive(true);
    }

    /// <summary>
    /// ルール画面からポーズメニューに戻る
    /// </summary>
    public void BackToPauseFromRule()
    {
        if (rulePanel != null) rulePanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    /// <summary>
    /// レースを最初からやり直す（リトライ）
    /// </summary>
    public void RestartRace()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// タイトル画面に戻る
    /// </summary>
    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(titleSceneName))
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}

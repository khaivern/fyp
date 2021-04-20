using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Health
    [SerializeField] int maxHealth = 500;
    public int currentHealth;
    public HealthBar healthbar;


    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoresText;

    // Skill
    public Image image;
    public float cooldown = 5f;

    bool onCooldown;

    private void Awake()
    {
        int numOfSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfSessions > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        // health initialisation
        healthbar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);

        // score initialisation
        scoresText.text = score.ToString();
    }
    private void Update()
    {
        CheckIfSkillUsed();
    }

    public void IncreaseMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        if (currentHealth + 500 > 1000)
        {
            currentHealth = 1000;
        }
        else
        {
            currentHealth += 500;
        }
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);

    }

    public void ReduceHealth(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoresText.text = score.ToString();
    }

    public void ReduceScore(int pointsToReduce)
    {
        score -= pointsToReduce;
        scoresText.text = score.ToString();
    }

    void CheckIfSkillUsed()
    {
        
        if (onCooldown)
        {
            image.fillAmount -= 1 / cooldown * Time.deltaTime;
        }

        if (image.fillAmount <= 0)
        {
            
            onCooldown = false;
        }
    }

    public void SetSkillCooldown(float cooldown)
    {
        image.fillAmount = 1;
        this.cooldown = cooldown;
        onCooldown = true;

    }

    public bool GetCooldown()
    {
        return onCooldown;
    }

    public int GetScore()
    {
        return score;
    }

    public void ProcessPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Destroy(gameObject);
        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        healthbar.gameObject.SetActive(true);
    }

    public void HideHealthBar()
    {
        healthbar.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Time.timeScale = 1f;

        Application.Quit();
    }
}

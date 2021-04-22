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
    public TextMeshProUGUI hitPointsText;

    // Score
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoresText;

    // Skill
    public Image image;
    public float cooldown = 5f;
    bool onCooldown;

    // Damage
    bool hasDD = false;
    [SerializeField] Bullet bullet;
    [SerializeField] TextMeshProUGUI damageText;

    // Skins
    bool isSoldier = false;
    bool isKing = false;

    // Options
    public GameObject options;
    bool pressed = false;

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
        hitPointsText.text = currentHealth.ToString();

        // score initialisation
        scoresText.text = score.ToString();

        // option initialistation
        options.SetActive(false);
    }

    private void Update()
    {
        CheckIfSkillUsed();
        damageText.text = bullet.GetDamage().ToString();
        hitPointsText.text = currentHealth.ToString();
        CheckForOptionClick();
    }


    private void CheckForOptionClick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pressed)
        {
            Time.timeScale = 0f;
            options.SetActive(true);
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pressed)
        {
            Time.timeScale = 1f;
            options.SetActive(false);
            pressed = false;
        }
    }

    public bool GetPressed()
    {
        return pressed;
    }

    public bool GetDD()
    {
        return hasDD;
    }

    public void SetDD(bool data)
    {
        hasDD = data;
    }

    public bool GetKing()
    {
        return isKing;
    }

    public void SetKing(bool data)
    {
        isKing = data;
    }

    public bool GetSoldier()
    {
        return isSoldier;
    }

    public void SetSoldier(bool data)
    {
        isSoldier = data;
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
        hitPointsText.text = currentHealth.ToString();

    }

    public void ReduceHealth(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        hitPointsText.text = currentHealth.ToString();
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
        if (onCooldown)  image.fillAmount -= 1 / cooldown * Time.deltaTime; 

        if (image.fillAmount <= 0) onCooldown = false; 
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
        hitPointsText.text = currentHealth.ToString();
        currentHealth = maxHealth;
        healthbar.gameObject.SetActive(true);
    }

    public void HideHealthBar()
    {
        healthbar.gameObject.SetActive(false);
    }

    
}

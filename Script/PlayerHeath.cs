using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 99;
    public int currentHealth; // Renommé currentHelath en currentHealth
    public float invincibilityDelay = 0.2f; // Renommé invicubillityDelay en invincibilityDelay
    public float delay;
    public float damageDelay;
    public float deathDelay;

    public bool isInvincible = false; // Renommé isInvicible en isInvincible
    public SpriteRenderer graphics;

    public HealthBar healthBar;
    public static PlayerHealth instance;

    public float curentArmoPoint;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(50);
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
            currentHealth += 1;
            return;


        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(DamageSubtract(damage)); // Renommé DamageSubstract en DamageSubtract
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            isInvincible = true;
            StartCoroutine(Invincibility());
            StartCoroutine(InvincibilityDelay()); // Renommé InvicibillityDelay en InvincibilityDelay
        }
    }

    public void HealPlayer(int amount)
    {
        StartCoroutine(AddedLifePoint(amount));
    }

    public IEnumerator Invincibility()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0);
            yield return new WaitForSeconds(invincibilityDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityDelay);
        }
    }

    public IEnumerator InvincibilityDelay()
    {
        yield return new WaitForSeconds(delay);
        isInvincible = false;
    }

    public IEnumerator DamageSubtract(float damage)
    {
        damage = (damage * (1 - (curentArmoPoint / 100)));

        for (int i = 0; i < damage; i++)
        {
            if (currentHealth > 0)
            {
                currentHealth -= 1;
                healthBar.SetHealth(currentHealth);
                yield return new WaitForSeconds(damageDelay);
            }
            else
            {
                currentHealth = 0;
            }
        }

    }
    public IEnumerator AddedLifePoint(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if ((currentHealth + amount) > maxHealth)
            {
                currentHealth += 1;
                healthBar.SetHealth(currentHealth);
                yield return new WaitForSeconds(damageDelay);

                if(currentHealth == maxHealth) 
                {
                    break;
                }
            }
            else 
            {
                currentHealth += 1;
                healthBar.SetHealth(currentHealth);
                yield return new WaitForSeconds(damageDelay);
            }
            
        }
    }

        public IEnumerator Die()
    {
        Debug.Log("Le joueur est éliminé");
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.animator.SetTrigger("Die");
        yield return new WaitForSeconds(deathDelay);
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        PlayerPrefs.SetInt("playerHealth", maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}

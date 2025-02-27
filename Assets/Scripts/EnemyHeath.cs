using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private NavMeshAgent agent;
    private bool speedBoosted = false; // Kiểm tra đã tăng tốc chưa

    public float speedMultiplier = 2f; // Hệ số nhân tốc độ khi máu dưới 50%
    public Renderer enemyRenderer;
    public Color hitColor = Color.red;
    private Color originalColor;
    public float hitEffectDuration = 0.2f;
    public ParticleSystem hitEffect;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(FlashRed());

        // 💥 Hiển thị Particle Effect
        if (hitEffect != null)
        {
            hitEffect.Play();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (!speedBoosted && currentHealth <= maxHealth * 0.5f)
        {
            BoostSpeed();
        }
    }

    void BoostSpeed()
    {
        speedBoosted = true;
        agent.speed *= speedMultiplier;
    }

    private IEnumerator FlashRed()
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = hitColor;
            yield return new WaitForSeconds(hitEffectDuration);
            enemyRenderer.material.color = originalColor;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

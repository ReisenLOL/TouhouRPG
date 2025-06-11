using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public float speed;
    public float combatSpeed;
    public float attackStrength;
    public float health;
    public float maxHealth;
    public string UnitID;
    public Transform healthBar;

    protected virtual void Start()
    {
        if (SceneManager.GetActiveScene().name != "Combat")
        {
            healthBar.parent.gameObject.SetActive(false);
        }
    }
    public virtual void TakeDamage(float Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.localScale = new Vector3(health / maxHealth, 1);
    }
}

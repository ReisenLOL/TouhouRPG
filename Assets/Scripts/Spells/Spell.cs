using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spell : MonoBehaviour
{
    public int MPCost;
    public int damage;
    public Unit target;
    public CombatManager combatManager;

    protected void Start()
    {
        if (SceneManager.GetActiveScene().name == "Combat")
        {
            combatManager = FindFirstObjectByType<CombatManager>();
        }
    }

    public virtual void CastSpell(PlayerController playerCastingSpell)
    {
        playerCastingSpell.SetMP(-MPCost);
        AttackMechanic();
    }

    protected virtual void AfterMechanic()
    {
        
    }
    protected virtual void AttackMechanic()
    {
        
    }
}

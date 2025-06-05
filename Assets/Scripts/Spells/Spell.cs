using UnityEngine;

public class Spell : MonoBehaviour
{
    public int MPCost;
    public int damage;
    public virtual void CastSpell(PlayerController playerCastingSpell)
    {
        playerCastingSpell.SetMP(-MPCost);
        AttackMechanic();
    }

    protected virtual void AttackMechanic()
    {
        
    }
}

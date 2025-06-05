using UnityEngine;

public class EnemyController : Unit
{
    public EnemyController enemiesToAddToCombat;
    public int enemyAmountToAdd;
    void Update()
    {
        //wander i think
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        CombatSwitcher.instance.currentEnemySpawningData = new CombatSwitcher.EnemySpawningData(UnitID, enemyAmountToAdd);
        CombatSwitcher.instance.SwitchToCombatScene();
    }
}

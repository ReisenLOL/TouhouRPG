using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    //huge block of variables that i'll forever be too lazy to sort
    public int currentTurn;
    private Queue<IEnumerator> actionQueue = new(); //figure this shit out later
    public List<EnemyController> allEnemies = new();
    public List<Unit> unitsInBattle = new();
    public EnemyController enemiesToAddToCombat;
    public List<EnemyController> currentEnemies = new(); 
    public PlayerController[] playerUnits;
    public List<PlayerController> playerUnitsInBattle = new(); 
    public Transform[] enemySpawnLocations;
    public Transform[] playerSpawnLocations;
    public bool playerSelectingAttack;
    public PlayerController currentPlayerTurn;
    public EnemyController enemyToAttack;
    public Spell selectedSpell;
    public GameObject targetSelectionText;
    public GameObject combatUI;
    private void Start()
    {
        enemiesToAddToCombat = allEnemies.Find(w => w.UnitID.Trim().ToLower() == CombatSwitcher.instance.currentEnemySpawningData.UnitID.Trim().ToLower());
        if (enemiesToAddToCombat == null)
        {
            Debug.Log("lmao: " + CombatSwitcher.instance.currentEnemySpawningData.UnitID);
        }
        for (int i = 0; i < playerUnits.Length; i++)
        {
            PlayerController newPlayerUnit = Instantiate(playerUnits[i], playerSpawnLocations[i].position, playerUnits[i].transform.rotation);
            unitsInBattle.Add(newPlayerUnit);
            playerUnitsInBattle.Add(newPlayerUnit);
            newPlayerUnit.canMove = false;
        }
        for (int i = 0; i < CombatSwitcher.instance.currentEnemySpawningData.spawningAmount; i++)
        {
            EnemyController newEnemyUnit = Instantiate(enemiesToAddToCombat, enemySpawnLocations[i].position, enemiesToAddToCombat.transform.rotation);
            currentEnemies.Add(newEnemyUnit);
            unitsInBattle.Add(newEnemyUnit);
        }
        unitsInBattle.Sort(SortUnitListBySpeed);
        unitsInBattle.Reverse(); //LAZY
        currentTurn = unitsInBattle.Count;
        //now what?
        // eh i'll figure out tmrw
        SwitchTurn(); //also lazy
        for (int i = 0; i < unitsInBattle.Count; i++)
        {
            //unitsInBattle[i].healthBar.parent.gameObject.SetActive(true);
            //Debug.Log(unitsInBattle[i]);
        }
    }

    private static int SortUnitListBySpeed(Unit unitA, Unit unitB)
    {
        return unitA.combatSpeed.CompareTo(unitB.combatSpeed);
    }
    public void SwitchTurn()
    {
        currentTurn++;
        if (currentTurn >= unitsInBattle.Count)
        {
            currentTurn = 0;
        }
        Debug.Log(unitsInBattle[currentTurn] + "'s turn!");
        if (unitsInBattle[currentTurn].TryGetComponent(out EnemyController isEnemy))
        {
            EnemyTurn(); //haha immediate
        }
        if (unitsInBattle[currentTurn].TryGetComponent(out PlayerController isPlayer))
        {
            currentPlayerTurn = isPlayer; //haha not really immediate
        }
    }

    public void EnemyTurn()
    {
        Attack(playerUnitsInBattle[Random.Range(0, playerUnitsInBattle.Count)], unitsInBattle[currentTurn]);
        SwitchTurn();
        //wow that worked?!
    }
    public void StartAttack()
    {
        playerSelectingAttack = true;
        targetSelectionText.SetActive(true);
        combatUI.SetActive(false);
    }
    public void StartAttack(Spell spellSelected)
    {
        playerSelectingAttack = true;
        targetSelectionText.SetActive(true);
        combatUI.SetActive(false);
        selectedSpell = spellSelected; 
    }


    public void Attack(Unit targetEnemy, Unit attackingPlayer)
    {
        targetEnemy.TakeDamage(attackingPlayer.attackStrength);
        Debug.Log(attackingPlayer + " attacks " + targetEnemy + "!");
        Debug.Log(targetEnemy + "'s Health: " + targetEnemy.health.ToString() + "/" + targetEnemy.maxHealth.ToString());
        bool combatStillActive = true;
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            if (currentEnemies[i].gameObject.activeSelf == true)
            {
                combatStillActive = true;
                break;
            }
            combatStillActive = false;
        }

        if (!combatStillActive)
        {
            EndCombat();
        }
    }
    private void Update() 
    {
        if (playerSelectingAttack && Input.GetMouseButtonDown(0)) //i hate this new ide!!! i wish visual studio was on linux!!!!!! vscode sucks!!!!!!!!!!!!!!!!!
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0);
            if (hit && hit.transform.gameObject.TryGetComponent(out EnemyController isEnemy))
            {
                if (selectedSpell == null)
                {
                    enemyToAttack = isEnemy;
                    Attack(enemyToAttack, unitsInBattle[currentTurn]); //SHUT UP RIDER I DON'T CARE!!!!
                    playerSelectingAttack = false;
                    SwitchTurn();
                    //congrats!
                }
                else
                {
                    selectedSpell.CastSpell(currentPlayerTurn);
                    selectedSpell.target = isEnemy;
                    selectedSpell = null;
                    playerSelectingAttack = false;
                }
                targetSelectionText.SetActive(false);
                combatUI.SetActive(true);
            }
        }
    }

    private void EndCombat()
    {
        SceneManager.LoadScene("Prototype");
    }
}

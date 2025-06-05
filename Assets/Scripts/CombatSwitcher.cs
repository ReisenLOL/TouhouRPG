using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawningData
    {
        public string UnitID;
        public int spawningAmount;

        public EnemySpawningData(string id, int amount)
        {
            UnitID = id;
            spawningAmount = amount;
        }
    }
    public EnemySpawningData currentEnemySpawningData;
    public static CombatSwitcher instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SwitchToCombatScene()
    {
        SceneManager.LoadScene("Combat");
    }
}

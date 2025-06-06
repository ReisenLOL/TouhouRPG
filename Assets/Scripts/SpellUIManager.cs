using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUIManager : MonoBehaviour
{
    private List<Spell> spellListToCreate = new();
    public Transform spellUIFrame;
    public Button templateSpellButton;
    private CombatManager combatManager;

    private void Start()
    {
        combatManager = FindFirstObjectByType<CombatManager>();
    }

    public void RebuildSpellList()
    {
        spellUIFrame.gameObject.SetActive(true);
        List<Spell> spellList = combatManager.currentPlayerTurn.SpellList;
        for (int i = 0; i < spellList.Count; i++)
        {
            Button newButton = Instantiate(templateSpellButton, spellUIFrame);
            newButton.gameObject.SetActive(true);
            newButton.onClick.AddListener(() => spellList[i].CastSpell(combatManager.currentPlayerTurn));
        }
    }
}

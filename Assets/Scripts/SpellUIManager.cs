using System;
using System.Collections.Generic;
using TMPro;
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
        foreach (Transform child in spellUIFrame)
        {
            Destroy(child.gameObject);
        }
        List<Spell> spellList = combatManager.currentPlayerTurn.SpellList;
        foreach (Spell spell in spellList)
        {
            Button newButton = Instantiate(templateSpellButton, spellUIFrame);
            newButton.gameObject.SetActive(true);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = spell.name;
            newButton.onClick.AddListener(() => combatManager.StartAttack(spell)); //hell yeah
            newButton.onClick.AddListener(() => ResetSpellList());
        }
    }

    public void ResetSpellList()
    {
        foreach (Transform child in spellUIFrame)
        {
            Destroy(child.gameObject);
        }
        spellUIFrame.gameObject.SetActive(false);
    }
}

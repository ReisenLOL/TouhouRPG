using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public SpriteRenderer playerSpriteRenderer;
    private bool isFacingRight = false;
    public bool canMove = true;
    public List<Spell> SpellList = new();
    public Transform spellFolderObject;
    public static int MP;
    public int MaxMP;
    public static int level;
    public static int exp;
    public static int expToNextLevel = 20;

    protected override void Start()
    {
        base.Start();
        foreach (Spell spellObject in spellFolderObject.GetComponentsInChildren<Spell>())
        {
            SpellList.Add(spellObject); //still have to make it so that players can choose spells, this is just predetermined for now
        }
    }

    void Update()
    {
        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            if (this.moveInput.x > 0f && this.isFacingRight)
            {
                playerSpriteRenderer.flipX = true;
                this.isFacingRight = !this.isFacingRight;
            }
            else if (this.moveInput.x < 0f && !this.isFacingRight)
            {
                playerSpriteRenderer.flipX = false;
                this.isFacingRight = !this.isFacingRight;
            }
        }

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = moveInput * speed;
        }
    }

    public void AddExp(int amountToAdd)
    {
        exp += amountToAdd;
        if (exp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    public void SetMP(int MPToChangeBy)
    {
        MP += MPToChangeBy;
    }

    public void LevelUp()
    {
        level++;
        expToNextLevel *= 2;
    }
}

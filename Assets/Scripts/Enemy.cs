using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public void ChangeSprite(int i)
    {
        spriteRenderer.sprite = sprites[i];
    }
}

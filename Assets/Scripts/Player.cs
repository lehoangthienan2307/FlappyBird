using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Vector3 direction; 
    private Rigidbody2D birdRigidbody2D; 
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;
    private int spriteIndex;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float strength = 5f;
    private const float JUMP_AMOUNT = 4f;
    private void Awake()
    {
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        float timeRepeat = 0.15f;
        float rateRepeat = 0.15f;
        InvokeRepeating(nameof(AnimateSprite), timeRepeat, rateRepeat);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
            
            
        }
    }
    private void Jump() {
        birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        //SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }
    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}

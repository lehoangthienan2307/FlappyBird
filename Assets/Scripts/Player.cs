using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D birdRigidbody2D; 
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;
    private int spriteIndex;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    private const float JUMP_AMOUNT = 4f;
    private State gameState;
    private void Awake()
    {
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        /*float timeRepeat = 0.15f;
        float rateRepeat = 0.15f;
        InvokeRepeating(nameof(AnimateSprite), timeRepeat, rateRepeat);*/
        ChangeGameState(State.NONE);
    }
    private void Update()
    {
        if (gameState == State.READY)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                ChangeGameState(State.PLAYING);
                Jump();
                
            }
        }
        else if (gameState == State.PLAYING)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                Jump();
            }
            transform.eulerAngles = new Vector3(0, 0, birdRigidbody2D.velocity.y * 5f);
        }
        
    }
    private void Jump() {
        birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        
        soundManager.Play(SoundId.JUMP);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            soundManager.Play(SoundId.LOSE);
            ChangeGameState(State.DEAD);   
        }
        else if (other.gameObject.tag == "Scoring")
        {
            soundManager.Play(SoundId.SCORE);
            gameManager.IncreaseScore();  
        }
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
    /*private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
    }*/
    public void ChangeGameState(State state)
    {
        gameState = state;
        gameManager.ChangeGameState(state);
        switch (state)
        {
            case State.NONE:
                birdRigidbody2D.bodyType = RigidbodyType2D.Static;
                break;

            case State.READY:
                birdRigidbody2D.bodyType = RigidbodyType2D.Static;

                float timeRepeat = 0.15f;
                float rateRepeat = 0.15f;
                InvokeRepeating(nameof(AnimateSprite), timeRepeat, rateRepeat);

                Vector3 position = transform.position;
                position.y = 0f;
                transform.position = position;
                transform.eulerAngles = Vector3.zero;
                
                break;

            case State.PLAYING:
                birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                break;

            case State.DEAD:
                CancelInvoke(nameof(AnimateSprite));
                break;
                
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum State {
    NONE,
    READY,
    PLAYING,
    DEAD
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private Image gameOver;
    [SerializeField] private Image gameReady;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    private int score;
    private void Awake()
    {
        ChangeGameState(State.NONE);
    }
    public void ChangeGameState(State state)
    {
        switch (state)
        {
            case State.NONE:
                playButton.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(false);
                gameReady.gameObject.SetActive(false);

                score = 0;
                scoreText.text = score.ToString();

                Time.timeScale = 0f;
                
                break;

            case State.READY:
                playButton.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(false);
                gameReady.gameObject.SetActive(true);

                score = 0;
                scoreText.text = score.ToString();

                Time.timeScale = 1f;
                spawner.enabled = false;

                Pipe[] pipes = FindObjectsOfType<Pipe>();
                foreach(Pipe pipe in pipes)
                {
                    Destroy(pipe.gameObject);
                }
                
                break;

            case State.PLAYING:
                gameReady.gameObject.SetActive(false);
                spawner.enabled = true;
                
                break;

            case State.DEAD:
                gameOver.gameObject.SetActive(true);
                playButton.gameObject.SetActive(true);

                Time.timeScale = 0f;
                
                break;
                
            
        }
    }
    public void Play()
    {
        player.ChangeGameState(State.READY);
    }
    
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    
}

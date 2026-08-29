using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Animator animator;
    public bool enemyCollision = false;

    void Awake()
    {
               Instance = this;
    }
       

    public void SetAnimation(string name)
    {
        animator.Play(name);

        if (name == "PlayerJump")
        {
            AudioManager.Instance.PlaySound("Jump");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (!enemyCollision)
            {
                enemyCollision = true;
                GetComponent<BoxCollider2D>().enabled = false;
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlaySound("Die");
            }
        }
        else if (collider.CompareTag("Points"))
        {
            ScoreManager.Instance.IncreasePoints();
            AudioManager.Instance.PlaySound("Point");
        }
    }
}

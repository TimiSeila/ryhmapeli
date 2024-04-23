using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;
    private int collectedAmount = 0;

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Animator animator;

    public int victorySceneNumber;
    public int deathSceneNumber;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (playerRB.velocity.y > 0f)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        else if (playerRB.velocity.y < 0f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && playerRB.velocity.y > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(horizontal * speed, playerRB.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.SetTrigger("isDead");
            animator.SetBool("isDeadBool", true);
            playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (collision.gameObject.tag == "Collectible")
        {
            collectedAmount += 1;
        }

        if(collectedAmount == 3)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("previousScene" + victorySceneNumber, currentScene);
            SceneManager.LoadScene(victorySceneNumber);
        }
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("previousScene" + deathSceneNumber, currentScene);
        SceneManager.LoadScene(deathSceneNumber); ;
    }
}

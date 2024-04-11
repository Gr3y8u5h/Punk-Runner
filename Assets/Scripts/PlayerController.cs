using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private float jumpForce = 550;
    private float gravityModifier = 2;
    public bool isOnGround = true;
    public bool gameOver;
    private int count;

    




    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        // Physics.gravity *= gravityModifier;
        Physics.gravity = new Vector3(0, -19.62f, 0);
        count = 0;
        

        SetCountText();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;  //  in the air, is not on the ground
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;

            SetCountText();
        }
    }
    void DelayedSceneChange()
    {
        SceneManager.LoadScene("End Game");
        gameOver = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;  // tells the system that player is back on the ground
            dirtParticle.Play();

        } else if (collision.gameObject.CompareTag("Obstacles"))
        {
            
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Invoke("DelayedSceneChange", 3.0f);

        }

        
        
    }
} // class
















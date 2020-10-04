using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private float xAxis;
    private float yAxis;
    private Vector3 change;
    private Vector3 respawnPosition;
    public float movementForce = 5f;

    private const string PLAYER_IDLE = "PlayerIdle";
    private const string PLAYER_WALK = "PlayerWalk";
    private const string PLAYER_JUMP = "PlayerJump";
    private const string PLAYER_DIE = "PlayerDeath";

    private string currentAnimation = "IdleFront";
    
    public AudioClip shootSound;
    public AudioSource audioPlayer;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public bool canMove = true;

    public GameObject weapon;
    public ParticleSystem dustLandParticle;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        ChangeAnimationState(PLAYER_IDLE);
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            change.x = xAxis;
            change.y = yAxis;    
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ToDo: Interaction is pressed
        }
    }

    private void FixedUpdate()
    {

        if (canMove)
        {
            if (xAxis < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (xAxis > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }

            if (change != Vector3.zero)
            {
                if (currentAnimation != PLAYER_WALK)
                {
                    dustLandParticle.Play();
                }
                ChangeAnimationState(PLAYER_WALK);
                MoveCharacter();
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }    
        }
        
    }

    void MoveCharacter()
    {
        _rigidbody.MovePosition(transform.position + change * movementForce * Time.deltaTime);
    }

    public void TeleportToPosition(Vector3 position)
    {
        respawnPosition = position;
        transform.position = position;
    }

    public void PlayerDie()
    {
        canMove = false;
        weapon.gameObject.SetActive(false);
        ChangeAnimationState(PLAYER_DIE);
    }

    public void Respawn()
    {
        canMove = true;
        weapon.gameObject.SetActive(true);
        transform.position = respawnPosition;
        ChangeAnimationState(PLAYER_IDLE);
    }

    public void ChangeAnimationState(string newStateName)
    {
        if (currentAnimation == newStateName) return;
        _animator.Play(newStateName);
        currentAnimation = newStateName;
    }
    
}
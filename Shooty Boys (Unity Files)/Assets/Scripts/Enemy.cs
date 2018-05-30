using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health = 2;
    public int currentAnimationState;
    private const float movSpeed = 0.05f;
    private float distance;
    private bool attacking;

    GameObject gameManagerObject;
    GameManager gameManager;
    Animator animator;
    GameObject player;
    ParticleSystem[] particles;
    AudioSource impact;
    AudioSource pain;

    private const int STATE_MOVE = 0;
    private const int STATE_ATTACK = 1;


    // Use this for initialization
    void Start()
    {
        attacking = false;
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        particles = GetComponentsInChildren<ParticleSystem>();
        impact = GetComponent<AudioSource>();
        pain = GameObject.FindGameObjectWithTag("Zombie_Pain").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
      //  rotateTowardsPlayer();
       // transform.Translate(movSpeed, 0, 0);
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 2)
        {
            ChangeState(STATE_ATTACK);

            if(attacking == false)
            {
                StartCoroutine(Attack());
            }
            
        }
        else if (distance > 2)
        {
            ChangeState(STATE_MOVE);
            print("zombie moving");
        }
    }

    public IEnumerator Attack()
    {
        attacking = true;

        yield return new WaitForSecondsRealtime(0.5f);
        if (distance < 2)
        {
            player.GetComponent<PlayerController>().TakeDamage();
        }
        yield return new WaitForSecondsRealtime(0.3f);
        attacking = false;
    }

    public void TakeDamage()
    {
        pain.Play();
        impact.Play();
        health--;
        if (health > 0)
        {
            
            ParticleSystem blood = particles[0];
            blood.Play();
        }
        if (health <= 0)
        {
            Death();
        }

    }

    public void Death()
    {
        gameManager.setNumOfZombies(gameManager.getNumOfZombies() - 1);
        gameManager.setScore(gameManager.getScore() + 1);
        impact.Play();
        Renderer render = gameObject.GetComponent<Renderer>();
        render.enabled = false;
        GameObject deathBlood = Instantiate(Resources.Load("Melee Blood", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
        ParticleSystem deathBloodPart = deathBlood.GetComponent<ParticleSystem>();
        deathBloodPart.Play();
        DestroyImmediate(gameObject);
        
    }

    void ChangeState(int state)
    {
        if (currentAnimationState == state)
            return;

        switch (state)
        {

            case STATE_MOVE:
                animator.SetInteger("State", STATE_MOVE);
                break;

            case STATE_ATTACK:
                animator.SetInteger("State", STATE_ATTACK);
                break;
        }
        currentAnimationState = state;
    }

    protected void rotateTowardsPlayer()
    {
        this.transform.Rotate(0, 0, -90);
    }
}

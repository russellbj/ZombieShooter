using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private float walkSpeed;
    public float curSpeed;
    private float maxSpeed;
    public GameObject crosshair;
    public GameObject canvasObject;
    private AmmoControl ammoControl;
    private int health;
    private SpriteRenderer renderer;
    private bool dead = false;
    private GameObject spotlightObject;
    private GameObject pointLightObject;
    private Light spotlight;
    private Light pointLight;
    private GameManager gameManager;
    private AudioSource[] playerSoundArray;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private bool _isGrounded = true; // is player on the ground?

    Animator animator;
    Rigidbody2D body;
    AudioSource pistolShot;

    //animation states - the values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_MOVE = 1;
    const int STATE_SHOOT = 2;
    const int STATE_MELEE = 3;
    const int STATE_RELOAD = 4;

    public int currentAnimationState = STATE_IDLE;

    public int getCurrentAnimState()
    {
        return currentAnimationState;
    }

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        health = 4;
        ammoControl = canvasObject.GetComponent<AmmoControl>();
        pistolShot = GetComponent<AudioSource>();
        maxSpeed = 15;
        walkSpeed = 5.5f;
        curSpeed = 5;
        animator = gameObject.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        spotlightObject = GameObject.FindGameObjectWithTag("Spotlight");
        pointLightObject = GameObject.FindGameObjectWithTag("PointLight");
        spotlight = spotlightObject.GetComponent<Light>();
        pointLight = pointLightObject.GetComponent<Light>();
        playerSoundArray = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health <= 0)
        {
            if (dead == false)
            {
                StartCoroutine(Die());
            }
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Handgun_Reload"))
        {
            if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                
                changeState(STATE_IDLE);
            }
            else
            {
                
                changeState(STATE_MOVE);
            }
        }

        Run();
        body.velocity = moveVelocity;
        print(GetComponent<Rigidbody2D>().velocity);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKey(KeyCode.Space))
        {
            changeState(STATE_MELEE);
        }
    }

    //--------------------------------------
    // Change the players animation state
    //--------------------------------------
    public void changeState(int state)
    {   
        if (currentAnimationState == state)
            return;

        switch (state)
        {

            case STATE_MOVE:
                animator.SetInteger("state", STATE_MOVE);
                break;

            case STATE_SHOOT:
                animator.SetInteger("state", STATE_SHOOT);
                pistolShot.Play();
                break;

            case STATE_MELEE:
                animator.SetInteger("state", STATE_MELEE);
                break;

            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;

            case STATE_RELOAD:
                animator.SetInteger("state", STATE_RELOAD);
                print("reload triggered");
                break;

        }

        currentAnimationState = state;
    }

    public void TakeDamage()
    {
        health--;
        gameManager.ChangeHealth(health);
        playerSoundArray[3].Play();

        if(dead == false)
            playerSoundArray[1].Play();
        
    }

    void Run()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        moveVelocity = moveInput * walkSpeed;
    }

     public void CheckForHit()
     {

         RaycastHit2D objectHit;


         Vector3 crosshairLoc = crosshair.transform.position;

         Vector3 heading = crosshairLoc - transform.position;
         float distance = heading.magnitude;
         Vector3 direction = heading / distance;

         Debug.DrawRay(transform.position, heading* 10, Color.green, 20f);

         objectHit = Physics2D.Raycast(transform.position, heading, 50000);

         if (objectHit.collider)
        {
            if (objectHit.collider.tag == "Enemy")
            {
                Enemy enemy = objectHit.collider.GetComponent<Enemy>();
                enemy.TakeDamage();
            }
            else
            {
                print("Hit");
                playerSoundArray[4].Play();
            }
        }
         
     }

    public IEnumerator Die()
    {
        renderer.enabled = false;
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        playerSoundArray[2].Play();
        spotlightObject.SetActive(false);
        canvasObject.SetActive(false);
        setDead(true);
        yield return new WaitForSecondsRealtime(0.5f);
        while(pointLight.intensity > 0)
        {
            pointLight.intensity = pointLight.intensity - 0.1f;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(2);
    }

    public void setDead(bool dead)
    {
        this.dead = dead;
    }
    public bool getDead()
    {
        return dead;
    }
}

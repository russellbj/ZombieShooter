using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoControl : MonoBehaviour {

    public static int ammo;
    private PlayerController playerScript;
    private Animator animator;
    public GameObject player;
    public GameObject six;
    public GameObject five;
    public GameObject four;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject zero;
    public bool canShoot = true;
    public bool reloading = false;

    //animation states - the values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_MOVE = 1;
    const int STATE_SHOOT = 2;
    const int STATE_MELEE = 3;
    const int STATE_RELOAD = 4;

    // Use this for initialization
    void Start() {
        ammo = 6;
        playerScript = player.GetComponent<PlayerController>();
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Iterator());
    }

    void DisableCurrentAmmo()
    {
        if (ammo == 0)
        {
            zero.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (ammo == 1)
        {
            one.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (ammo == 2)
        {
            two.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (ammo == 3)
        {
            three.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (ammo == 4)
        {
            four.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (ammo == 5)
        {
            five.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (ammo == 6)
        {
            six.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    IEnumerator Iterator()
    {
        print("iterating");
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Handgun_Shoot") && ((canShoot == false && Input.GetMouseButtonDown(0) && reloading == false) || (ammo != 6 && reloading == false && Input.GetKeyDown(KeyCode.R) )))
        {
            reloading = true;
            playerScript.changeState(STATE_RELOAD);
            print("Reloading");
            DisableCurrentAmmo();
            ammo = 6;
            six.GetComponent<TextMeshProUGUI>().enabled = true;
            yield return new WaitForSeconds(2);
            print("Done Reloading");

            
            reloading = false;
            canShoot = true;
        }
        if (canShoot && Input.GetMouseButtonDown(0) && ( animator.GetCurrentAnimatorStateInfo(0).IsName("Handgun_Move") || animator.GetCurrentAnimatorStateInfo(0).IsName("Handgun_Idle") ))
        {
     
            if (ammo == 6)
            {
                print("shoot");
                ammo = 5;
                playerScript.changeState(STATE_SHOOT);
                playerScript.CheckForHit();
                ParticleSystem muzzleFlare = playerScript.gameObject.GetComponentInChildren<ParticleSystem>();
                muzzleFlare.Play();
                six.GetComponent<TextMeshProUGUI>().enabled = false;
                five.GetComponent<TextMeshProUGUI>().enabled = true;
                yield return new WaitForSeconds(1);

            }


            else if (ammo == 5)
            {
         
                ammo = 4;
                playerScript.changeState(STATE_SHOOT);
                playerScript.CheckForHit();
                ParticleSystem muzzleFlare = playerScript.gameObject.GetComponentInChildren<ParticleSystem>();
                muzzleFlare.Play();
                five.GetComponent<TextMeshProUGUI>().enabled = false;
                four.GetComponent<TextMeshProUGUI>().enabled = true;
                yield return new WaitForSeconds(1);
            }
            else if (ammo == 4)
            {
            
                ammo = 3;
                playerScript.changeState(STATE_SHOOT);
                playerScript.CheckForHit();
                ParticleSystem muzzleFlare = playerScript.gameObject.GetComponentInChildren<ParticleSystem>();
                muzzleFlare.Play();
                four.GetComponent<TextMeshProUGUI>().enabled = false;
                three.GetComponent<TextMeshProUGUI>().enabled = true;
                yield return new WaitForSeconds(1);
            }
            else if (ammo == 3)
            {
                
                ammo = 2;
                playerScript.changeState(STATE_SHOOT);
                playerScript.CheckForHit();
                ParticleSystem muzzleFlare = playerScript.gameObject.GetComponentInChildren<ParticleSystem>();
                muzzleFlare.Play();
                three.GetComponent<TextMeshProUGUI>().enabled = false;
                two.GetComponent<TextMeshProUGUI>().enabled = true;
                yield return new WaitForSeconds(1);
            }
            else if (ammo == 2 )
            {
               
                ammo = 1;
                playerScript.changeState(STATE_SHOOT);
                playerScript.CheckForHit();
                ParticleSystem muzzleFlare = playerScript.gameObject.GetComponentInChildren<ParticleSystem>();
                muzzleFlare.Play();
                two.GetComponent<TextMeshProUGUI>().enabled = false;
                one.GetComponent<TextMeshProUGUI>().enabled = true;
                yield return new WaitForSeconds(1);
            }
            else if (ammo == 1)
            {
               
                ammo = 0;
                playerScript.changeState(STATE_SHOOT);
                playerScript.CheckForHit();
                ParticleSystem muzzleFlare = playerScript.gameObject.GetComponentInChildren<ParticleSystem>();
                muzzleFlare.Play();
                canShoot = false;
                one.GetComponent<TextMeshProUGUI>().enabled = false;
                zero.GetComponent<TextMeshProUGUI>().enabled = true;
            }
        }
    }
}

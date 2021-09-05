using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class Movement : MonoBehaviour
{
    Rigidbody2D player;
    BoxCollider2D bxc;
    BoxCollider2D boxCarry;
    JumpCheck jc;
    Animator anim;
    float horizontal;
    public bool onVine = false;
    private float vineSpeed = 4;

    [Header("Movement Parameters")]
    public float jumpHeight = 5.0f;
    public float speed = 10.0f;
    public bool isGrounded = false;
    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor = 0.1f; 
    float lastTimeGrounded;
    public float hitOffset = 1.53f;
    private Vector3 respawnLoc;
    private float backLimit;
    public GameObject block;
    public static bool freezeMovement = false;
    public bool hasFired = false;
    bool teleportInAction = false;

    [Header("Orb Launch")]
    public GameObject orbPrefab;
    public float throwSpeed = 5;
    public float slingshotSpeed = 12;
    public float orbSpeed;
    Vector3 mousePos;
    private bool hasSlingshot = false;
    public bool orbOnScreen = false;
    MagicOrbScript oScript;
    public float teleportOffset = 0.012f;
    public bool orbPicked = false;
    private float originalGrav;
    public float footstepSound = 0.4f;
    bool isResetting = false;
    bool left = false;

    [Header("Objects")]
    private GameObject[] objectsInScene;

    [Header("Box")]
    public float boxthrowSpeed = 3f;
    public float boxPickupDistance = 2.0f;
    private bool boxPicked;
    private GameObject box;

    [Header("Sounds")]
    public AudioClip bgm;
    public AudioClip ambience;
    public AudioClip jgBgm;
    public AudioClip jgAmbience;
    public AudioClip spikeDeath;
    public AudioClip orbThrow;
    public float orbVol = 0.4f;

    public AudioClip teleportSound;
    public float volTeleport = 0.5f;

    public AudioClip[] desertFootsteps;
    public AudioClip[] jungleFootsteps;
    private bool isFootStepPlaying;
    

    private float pplayerY = 0;
    private float pplayerX = 0;
    private bool pleft = false;
    private bool pboxPicked = false;
    private bool pisIdle = true;
    private bool pySpeed = false;
    private bool pOnVine = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        bxc = GetComponent<BoxCollider2D>();
        jc = this.gameObject.transform.GetChild(0).GetComponent<JumpCheck>();
        boxCarry = this.gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>();
        //Will break iwth multiple box colliders


        boxPicked = false;
        if(hitOffset == 0)
            hitOffset = bxc.size.y / 2;

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SoundManager.Instance.PlayMusic(bgm, 0.8f);
            SoundManager.Instance.PlayAmbience(ambience);
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SoundManager.Instance.PlayMusic(jgBgm);
            SoundManager.Instance.PlayAmbience(jgAmbience);
        }

        
        originalGrav = player.gravityScale;
        isFootStepPlaying = false;
    }

    bool GetKeyJump()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
    }


    void Jump()
    {
        if(GetKeyJump() && !onVine && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            player.velocity = new Vector2(player.velocity.x, jumpHeight); 
        } else if(onVine)
        {
            if(GetKeyJump())
            {
                player.velocity = new Vector2(player.velocity.x, vineSpeed);
            } else if (Input.GetKey(KeyCode.S))
            {
                player.velocity = new Vector2(player.velocity.x, -vineSpeed);
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, 0);
            }
        }
    }



    void JumpModification()
    {
        if (player.velocity.y < 0) {
            player.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } else if (player.velocity.y > 0) {
            player.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }  
    }

    public void SetRespawnLocation(Vector3 loc)
    {
        respawnLoc = loc;
    }

    public void Respawn()
    {
        
        if(orbOnScreen)
        {
            oScript = GameObject.FindGameObjectWithTag("orb").GetComponent<MagicOrbScript>();
            oScript.destroy();
            orbOnScreen = false;
        }

        // while(GameObject.FindGameObjectWithTag("orb") != null)
        // {
        //     GameObject.FindGameObjectWithTag("orb").GetComponent<MagicOrbScript>().destroy();
        // }

        reset();
        player.velocity = new Vector2(0, 0);
        transform.position = respawnLoc;
        freezeMovement = false;
    }

    bool allowableJump(string s)
    {
        return s == "ground" || s == "block" || s == "Box" || s == "orb";
    }

    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down, hitOffset);
        
        if(hit.collider != null)
        {
            //Debug.Log(hit);
            // Debug.Log((hit.distance + " " +  hitOffset) + " " + hit.collider.tag);
            if(hit.distance <= hitOffset && allowableJump(hit.collider.tag))
            {
                //Debug.Log("ON GROUND 2");
                isGrounded = true;
                lastTimeGrounded = Time.time;
            }
            else
                isGrounded = false;
        }
        else
            isGrounded = false;

    }
    private void FixedUpdate()
    {
        JumpModification();
    }

    private void Update()
    {
        if(freezeMovement)
            player.velocity = new Vector2(0, player.velocity.y);

        //g(freezeMovement + " " + PauseMenu.GameIsPaused);

        if(!freezeMovement && !PauseMenu.GameIsPaused)
        {
            //CheckIfGrounded();
            if(!teleportInAction)
            {
                isGrounded = jc.GetJump();
                Jump();

                horizontal = Input.GetAxis("Horizontal");
                if(horizontal > 0)
                    left = false;
                else if(horizontal < 0)
                    left = true;
                //Debug.Log("Horizontal " + horizontal);
                // if(horizontal == 0)
                //     player.velocity = new Vector2(0, player.velocity.y);
                // else
                //     player.AddForce(new Vector2(speed * horizontal, 0), ForceMode2D.Impulse);
                //Debug.Log("MOVING" + horizontal);
                player.velocity = new Vector2(speed *  horizontal, player.velocity.y);
                //Debug.Log("VELOCITy " + player.velocity.x + " " + player.velocity.y);
            }
            // Debug.Log(backLimit + " " + transform.position.x);
            // if(player.velocity.x < 0 && transform.position.x <= backLimit)
            // {
            //     player.velocity = new Vector2(0, player.velocity.y);
            // }

            if (Input.GetKeyDown(KeyCode.T) || transform.position.y < -28f)
            {
                //Debug.Log("HI");
                Respawn();
            }

            if(Input.GetMouseButtonDown(1) && orbOnScreen)
            {
                resetOrb();
            }

            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S)) && !boxPicked)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 direction = (Vector2)(mousePos - transform.position);
                direction.Normalize();
                LayerMask lm =  LayerMask.GetMask("Player");
                RaycastHit2D hit = Physics2D.Raycast(transform.position,direction);
                Debug.DrawRay(transform.position, direction * boxPickupDistance, Color.red, 10);
                //Debug.Log(hit);
                if (hit.collider != null)
                {
                    //Debug.Log(hit + " " + hit.distance + "  " + hit.collider);
                    if(hit.collider.tag == "Box" && hit.distance <= boxPickupDistance)
                    {
                        box = hit.transform.gameObject;
                        boxPicked = true;
                        //box.transform.SetParent(transform);
                        boxCarry.enabled = true;
                        box.GetComponent<BoxScript>().GetPickedUp();
                    }
                }
            } else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S)) && boxPicked)
            {
                boxPicked = false;
                box.GetComponent<BoxScript>().GetDropped();
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 direction = (Vector2)(mousePos - transform.position);
                direction.Normalize();
                boxCarry.enabled = false;
                //box.transform.SetParent(null);
                box.GetComponent<Rigidbody2D>().velocity = new Vector2(player.velocity.x + direction.x * boxthrowSpeed, player.velocity.y);
            }
                
            TransferVariables();

            orbSpeed = hasSlingshot ? slingshotSpeed : throwSpeed;
            if (!hasFired && !boxPicked && orbPicked && Input.GetMouseButtonDown(0) && !orbOnScreen/*&& GameObject.FindGameObjectsWithTag("orb").Length == 0*/)
            {
                orbOnScreen = true;
                hasFired = true;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SoundManager.Instance.PlaySound(orbThrow, orbVol);
                Vector2 direction = (Vector2)(mousePos - transform.position);
                direction.Normalize();
                //GetComponent<SpriteRenderer>().enabled = false;
                GameObject orb = Instantiate(orbPrefab, transform.position + (Vector3)(direction * 1f), Quaternion.identity);
                //orb.GetComponent<Rigidbody2D>().velocity = player.velocity;
                //player.AddForce(new Vector2(-direction.x * orbSpeed, -direction.y * orbSpeed), ForceMode2D.Impulse);
                orb.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * orbSpeed, direction.y * orbSpeed), ForceMode2D.Impulse);
            } 
            else if(!teleportInAction && !isResetting && Input.GetMouseButtonDown(0) && orbOnScreen)
            {
                teleportInAction = true;
                TeleportToLocation();
                
            }

            playFootSteps();
            
        }

    }


    public void TransferVariables()
    {
        float yv = player.velocity.y;
        if(Mathf.Abs(yv) <= 0.01f)
            yv = 0;
        //if((pplayerX > 0 && player.velocity.x <= 0) || pplayerX < 0 && player.velocity.x >= 0)
            anim.SetFloat("xVelocity", player.velocity.x);

        //if((pplayerY > 0 && yv <= 0) || pplayerY < 0 && yv >= 0)
            anim.SetFloat("yVelocity", yv);

        if(pleft != left)
            anim.SetBool("left", left);

        if(pboxPicked != boxPicked)
            anim.SetBool("boxCarry", boxPicked);

        if(pisIdle != (player.velocity.x == 0))
            anim.SetBool("isIdle", player.velocity.x == 0);

        if(pySpeed != ( Mathf.Abs(player.velocity.y) >= 0.01f))
            anim.SetBool("ySpeed", Mathf.Abs(player.velocity.y) >= 0.01f);

        if(pOnVine != onVine)
            anim.SetBool("isClimbing", onVine);

        //Debug.Log("XVelocity" + player.velocity.x + " " + pplayerX + "  " + (Mathf.Abs(player.velocity.y) >= 0.01f));

        pplayerX = player.velocity.x;
        pplayerY = yv;
        pleft = left;
        pboxPicked = boxPicked;
        pisIdle = player.velocity.x == 0;
        pySpeed = Mathf.Abs(player.velocity.y) >= 0.01f;
        pOnVine = onVine;
    }

    public void SetBackLimit(float x, float y)
    {
        backLimit = x;
        block.transform.position = new Vector2(x - block.transform.localScale.x, y);
    }

    public void resetOrb()
    {
        if(!teleportInAction && !isResetting && orbOnScreen)
        {
            isResetting = true;
            oScript = GameObject.FindGameObjectWithTag("orb").GetComponent<MagicOrbScript>();
            StartCoroutine(destroyOrb(oScript));
        }
    }

    IEnumerator destroyOrb(MagicOrbScript orb)
    {
        float k = orb.playRespawn();
        yield return new WaitForSeconds(k);
        orbOnScreen = false;
        hasFired = false;
        if(orb != null)
            orb.destroy();
        isResetting = false;
    }

    public void resetHasFired()
    {
        if(!orbOnScreen)
            hasFired = false;
    }

    public void reset()
    {
        //Debug.Log("REEEESSSSEt");
        boxPicked = false;
        for(int i = 0; i < objectsInScene.Length; i++)
        {
            //Debug.Log("RESETTING");
            objectsInScene[i].GetComponent<ResetObjectController>().reset();
        }
    }

    private void playFootSteps()
    {
        if(!isFootStepPlaying && player.velocity.x != 0 && isGrounded)
        {
            if(SceneManager.GetActiveScene().name == "Level1")
            {
                StartCoroutine(FootstepSounds(desertFootsteps[Random.Range(0, desertFootsteps.Length)]));
            } else if(SceneManager.GetActiveScene().name == "Level2")
            {
                StartCoroutine(FootstepSounds(jungleFootsteps[Random.Range(0, jungleFootsteps.Length)]));
            }
            //Debug.Log(Random.Range(0, footsteps.Length - 1));
            //Debug.Log(footsteps.Length - 1);
            
        }
    }

    IEnumerator FootstepSounds(AudioClip cl)
    {
        isFootStepPlaying = true;
        SoundManager.Instance.PlaySound(cl, footstepSound);
        yield return new WaitForSeconds(cl.length + cl.length);
        isFootStepPlaying = false;

    }

    public void pickUpOrb()
    {
        orbPicked = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log("ON GROUND");
        // if(collision.collider.tag == "ground" || collision.collider.tag == "block" || collision.collider.tag == "Box")
        // {
        //     //Debug.Log("ON GROUND 2");
        //     isGrounded = true;
        // }

        if (collision.collider.tag == "spikes")
        {
            
            SoundManager.Instance.PlaySound(spikeDeath);
            player.velocity = new Vector2(0, 0);
            Invoke("Respawn", 0.1f);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // if (collision.collider.tag == "ground" || collision.collider.tag == "block" || collision.collider.tag == "Box")
        // {
        //     isGrounded = false;
        //     lastTimeGrounded = Time.time;
        // }
    }

    public void TeleportToLocation()
    {
        oScript = GameObject.FindGameObjectWithTag("orb").GetComponent<MagicOrbScript>();
        Vector2 prevPos = transform.position;
        transform.position = new Vector3(oScript.dir.x, oScript.dir.y, 0);
        // if(oScript.GetContact())
        // {
        //     Vector2 position = oScript.dir;
        //     Vector2 normal = oScript.GetNormal();
        //     transform.position = new Vector2(position.x, position.y + teleportOffset - 1.5f) + normal * (bxc.size - bxc.offset)/2 - normal * oScript.GetSize()/2;
        // }

        SoundManager.Instance.PlaySound(teleportSound, volTeleport);

        GameObject li = oScript.getLight();
        anim.SetBool("isTeleport", true);
        anim.SetTrigger("teleport");
        if(left)
            anim.Play("Player2TeleportLeftOut", 0, 0.0f);
        else if(!left)
            anim.Play("Player2TeleportRightOut", 0, 0.0f);
        Destroy(li);
        StartCoroutine(teleporting());
        player.velocity = new Vector2(0, 0);
        player.gravityScale = 0;
        oScript.SetPosition(prevPos);
        StartCoroutine(orbTeleporting());
        //GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator orbTeleporting()
    {
        if(left)
            oScript.Play("Player2TeleportLeftIn2");
        else if(!left)
            oScript.Play("Player2TeleportRightIn");
        //StartCoroutine(oScript.reduceLightToZero(0.37f));
        yield return new WaitForSeconds(0.37f);
        teleportInAction = false;
        if(oScript != null)
            oScript.destroy();
        GameObject orbs = GameObject.FindGameObjectWithTag("orb");
        if(orbs!= null)
            Destroy(orbs);
        orbOnScreen = false;
    }

    IEnumerator teleporting()
    {
        //Debug.Log("SECONDS" + anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        yield return new WaitForSeconds(0.37f);
        anim.SetBool("isTeleport", false);
        //Destroy(li);
        player.gravityScale = originalGrav;
    }

    public void TeleportToLocation(Vector2 normal, Vector2 position, float size)
    {
        //Debug.Log(bxc.size);
        //GetComponent<SpriteRenderer>().enabled = true;
        transform.position = new Vector2(position.x, position.y + teleportOffset - 1.5f) + normal * (bxc.size - bxc.offset)/2 - normal * size/2;
        //Debug.DrawRay(position, new Vector2(0, 10), Color.red, 10);
        //Debug.Log(transform.position.y + " " + normal + " " + bxc.size + " " + bxc.offset + " " + position.x + " " + position.y);
        //Debug.Log(normal * (bxc.size - bxc.offset));
        orbOnScreen = false;
    }

    public void SetObjectsInScene(GameObject[] ar)
    {
        objectsInScene = ar;
    }
}
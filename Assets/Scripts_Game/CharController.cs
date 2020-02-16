
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cyclone;


public class CharController : MonoBehaviour
{

    public float xMoveConst = 0.8f ; // 0.8f is good
    public float yMoveConst = 10f; // 10f is good

    private Particle2D particle;
   
   
    // to be able to determina if it is Grounded;
    private bool isGrounded = false;
        
    //double jump
    private bool wallJump = false;

    // for animations
    private Animator anim;

    //flip sprite
    private bool isFacingLeft = true;
    
    //for dash move
    bool canDash = false;
    public GameObject dash;
    IEnumerator dashCoroutine = null;
    bool coroutineActive = false;
    

    //for fly
    GameObject flyObject; 
    bool isFlyEnabled = false;
    const float flyDuration = 3f; 

    //for die animation
    public GameObject dieAnim;
    

    void Start()
    {
        if(gameObject)
        {
            particle = gameObject.GetComponent<Particle2D>();
            anim = gameObject.GetComponent<Animator>();
            flyObject = transform.GetChild(0).gameObject;
        }            
    }

    
    void FixedUpdate()
    {
        if(isFlyEnabled) AdjustFly();
        if(!wallJump) AdjustMovement();
        AdjustAnimation();
        AdjustCharFacing();
    
        AdjustDash();

        isGrounded = false;
        
        if(Input.GetKeyDown(KeyCode.R)) StartCoroutine(Die());
    }

    private void AdjustMovement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        xMove *= xMoveConst;

        particle.Velocity = particle.Velocity + new Vector2D(xMove,0);
           
        if(isGrounded && Input.GetKeyDown(GameManager.inputManager.jump))
        {
            particle.Velocity = particle.Velocity + new Vector2D(0,yMoveConst);
        } 
    }

    private void AdjustAnimation()
    {
        if(!isGrounded) anim.Play("jump");

        else if(particle.Velocity.x >= 1.5f || particle.Velocity.x <= -1.5f)
        {
            anim.Play("run");
            anim.SetBool("isRunning",true); 
        } 
        else
        {
            anim.Play("Idle");
            anim.SetBool("isRunning",false);
        }
          
    }


    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    
    private void AdjustCharFacing()
    {
        float velocityX = particle.Velocity.x;
        if(isFacingLeft && velocityX>0.0f ) Flip();
        if(!isFacingLeft && velocityX<0.0f) Flip();
    }



    void AdjustDash()
    {
        if(isGrounded && !coroutineActive)
        {
            RefreshDash();
        } 
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        
        if(Input.GetKeyDown(GameManager.inputManager.dash)  && canDash)
        {   
            if(!(x==0 && y==0))
            {
                StopCoroutine("Dash1");
                dashCoroutine = Dash1(x,y);
                StartCoroutine(dashCoroutine);
            }
        }
    }

    public void RefreshJump()
    {
        particle.Velocity = particle.Velocity + new Vector2D(0,yMoveConst);
    }

    public bool IsGrounded
    {
        set
        {
            isGrounded = value;
        }
        get
        {
            return isGrounded;
        }
    }

    public bool WallJump
    {
        set
        {
            wallJump = value;
        }
        get
        {
            return wallJump;
        }
    }

    public bool IsFlyEnabled
    {
        set
        {
            isFlyEnabled = value;
        }
        get
        {
            return isFlyEnabled;
        }
    }

    public void RefreshDash()
    {
        canDash = true; 
    }

    public void AdjustFly()
    {         
        if(isGrounded || Input.GetKeyDown(GameManager.inputManager.climb) || Input.GetKeyDown(GameManager.inputManager.hook) || Input.GetKeyDown(GameManager.inputManager.dash))
        {
            StopCoroutine(Fly());
            flyObject.SetActive(false);
            isFlyEnabled = false;
            gameObject.GetComponent<PGravity>().enabled = true;
        }
        
    }

    public void SetUpFly()
    {
        isFlyEnabled = true;
        StartCoroutine(Fly());
    }


    IEnumerator Dash1(float x,float y)
    {
        coroutineActive = true;
        
        Vector2D dashDirection = new Vector2D(x,y) ;
        dashDirection.Normalize();
        dashDirection.x *= 10;
        dashDirection.y *= 10;
        
        if(x==0) dashDirection.y = y*10f;
        if(y==0) dashDirection.x = x*9f;

        particle.Velocity = new Vector2D(dashDirection);
        gameObject.GetComponent<PDrag>().enabled = false;
        gameObject.GetComponent<PGravity>().enabled = false;
        canDash = false;
        GameObject newDash = Instantiate(dash,transform.position,transform.rotation);
        float deg = Mathf.Rad2Deg * Mathf.Atan2(y,x) +180f;
        newDash.transform.eulerAngles = new Vector3(0,0,deg);
        newDash.transform.localScale = new Vector3(1.5262f,1.5262f,1.5262f);

        Vector3 scale = newDash.transform.localScale;
        if(x != 0) scale.y *= -x;
        newDash.transform.localScale = scale;        
        Destroy(newDash,0.5f);

        yield return new WaitForSeconds(0.24f);

        if(!isFlyEnabled) gameObject.GetComponent<PGravity>().enabled = true;
        gameObject.GetComponent<PDrag>().enabled = true;

        yield return new WaitForSeconds(0.24f);

        if(isGrounded) canDash = true;

        coroutineActive = false;

        yield return null;
    }

    IEnumerator Fly()
    {
        flyObject = transform.GetChild(0).gameObject;
        
        flyObject.SetActive(true);

        gameObject.GetComponent<PGravity>().enabled = false;
        particle.Velocity = new Vector2D(particle.Velocity.x,-1f);

        yield return new WaitForSeconds(flyDuration);

        flyObject.SetActive(false);
        isFlyEnabled = false;
        gameObject.GetComponent<PGravity>().enabled = true;

        yield return null;

    }

    public void DieChar()
    {
        StartCoroutine(Die());
    }
    
    IEnumerator Die()
    {
        GameManager.gameStats.deathCount++;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CharController>().enabled = false;
        gameObject.GetComponent<Particle2D>().enabled = false;
        GameObject dieObj = Instantiate(dieAnim,transform.position,transform.rotation);
        yield return new WaitForSeconds(1.1f);
        Destroy(dieObj);
        GameManager.sceneManager.LoadScene("Level" + GameManager.gameStats.currentLevel.ToString());
        yield return null;
    }

    public void StopDash()
    {
        StopCoroutine("Dash1");
    }

    public void StopFly()
    {
        StopCoroutine(Fly());
        flyObject.SetActive(false);
        isFlyEnabled = false;
        gameObject.GetComponent<PGravity>().enabled = true;
    }
   
}



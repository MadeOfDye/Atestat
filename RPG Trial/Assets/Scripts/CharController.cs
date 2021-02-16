using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Transform cam;
    private void Start()
    {
        cam = Camera.main.transform;
        boxCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, height, 0),Color.red,1);
        CheckGround();
        Gravity();
        GetInputs();
        CalculateDirection();
        TehJump();
        if (Mathf.Abs(_forward)<1 && Mathf.Abs(_sideways)<1)
        {
            return;
        }
        Rotation();
        CalculatingForward();
        
        Movement();
        TheCollision();
    }

    //Setting the raw input values into float Variables
   private float _forward;
    private float _sideways;
    void GetInputs()
    {
        _forward = Input.GetAxisRaw("Vertical");
        _sideways = Input.GetAxisRaw("Horizontal");
    }

    //Calculate the orientation of the character(from the above perspective) dependent on the camera angle
    private float _angle;
    void CalculateDirection()
    {
        _angle = Mathf.Atan2(_sideways, _forward);
        _angle = Mathf.Rad2Deg * _angle;
        _angle += cam.eulerAngles.y;
    }
    //Rotate the player dependand on the direction 
    public float smoothRot = 10f;
    private Quaternion playerRot;
    void Rotation()
    {
        playerRot = Quaternion.Euler(0, _angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerRot, smoothRot*Time.deltaTime);
    }
    //Calculating the forward vector for slopes
    private Vector3 forward;
    private RaycastHit hit;
    void CalculatingForward()
    {
        if(!grounded)
        {
            forward = transform.forward;
            return;
        }
        else
        {
            Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
            Physics.SphereCast(ray, 0.3f,out hit, groundPoint, eButPlayer);
            if (Vector3.Cross(hit.normal, -transform.right).y < 0.65f) 
            {
                forward = Vector3.Cross(hit.normal, -transform.right);
            }
            else
            {
               forward = Vector3.zero;
            }
        }
    }
    //this is the movement part, where the actual movement happens
    public float crackSpeed  = 10f;
   private Vector3 movement;
    void Movement()
    {
        movement = forward * crackSpeed * Time.deltaTime;
        transform.position += movement;
    }
    //Setting the boolean and applying gravity
   private bool grounded = false;
    public float gravity = 12.5f;
    void Gravity()
    {
        if(grounded == false)
        {
            transform.position -= new Vector3(0, gravity * Time.deltaTime, 0);
            crackSpeed /= 2.5f;
        }
        else
        {
            crackSpeed = 10f;
            return;
        }
    }
    //checking and confirming solid objects beneath the player
    public float height = 2f;
    public Vector3 liftPoint = new Vector3(0, 1f, 0);  
    public LayerMask eButPlayer;
    public float groundPoint = 5f;
    void CheckGround()
    {
        
        Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
        RaycastHit tempHit = new RaycastHit();
        if (Physics.SphereCast(ray, 0.3f,out tempHit,groundPoint, eButPlayer))
        {
            GroundConfirm(tempHit);
        }
        else
        {
            grounded = false;
        }
    }
    //Confirming that I am standing on the ground not clipping 
    private Vector3 groundCheckPoint = new Vector3(0, -0.57f, 0);
    public float groundCheckRadius = 1f;
    private RaycastHit groundHit;
    public float smoothFall = 10f;
    void GroundConfirm(RaycastHit temphit)
    {
        Collider[] col = new Collider[3];
        int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCheckPoint),groundCheckRadius, col,eButPlayer);
        //grounded = false;
        for(int i=0;i<num;i++)
        {
            if(col[i] == temphit.collider)
            {
                groundHit = temphit;
                grounded = true;
                if (jumped == false)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (groundHit.point.y + height / 2), transform.position.z), smoothFall * Time.deltaTime);
                    Debug.Log("Here");
                }
                break;
            }
        }

    }
    //yes I am programming the collision by hand, no it doesn't work as intended, what of it?
    BoxCollider boxCol;
    void TheCollision()
    {
        Collider[] overlaps = new Collider[4];
        int num = Physics.OverlapBoxNonAlloc(transform.TransformPoint(boxCol.center), boxCol.size / 2, overlaps, transform.rotation, eButPlayer, QueryTriggerInteraction.UseGlobal);
        for(int i=0;i<num;i++)
        {
            //Transform t = overlaps[i].transform;
            Vector3 dir;
            float dist;
            if (Physics.ComputePenetration(boxCol, transform.position, transform.rotation, overlaps[i], overlaps[i].transform.position, overlaps[i].transform.rotation, out dir, out dist))
            {
                Vector3 penetration = dir * dist;
                Vector3 movementProjected = Vector3.Project(movement, -dir);
                transform.position = transform.position + penetration;
                     movement -= movementProjected;
            }
        }
    }
    //Leave the ground and go up in the air hopefully
   private bool jumped = false;
    public float jumpForce = 0.6f;
    public float jumpSpeed = 0.5f;
    private void TehJump()
    {
       bool canJump = false;
        if(grounded)
        {
            jumped = false;
        }
        canJump = !Physics.Raycast(new Ray(transform.position, Vector3.up), height, eButPlayer);
        if(grounded && canJump)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position =Vector3.Lerp(transform.position,transform.position +  (Vector3.up * jumpForce * height),jumpSpeed * Time.deltaTime);
                jumped = true;
        }

    }
}

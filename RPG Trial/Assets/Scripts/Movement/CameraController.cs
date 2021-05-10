using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool changeTransparency = true;
    public MeshRenderer targetRend;
    public float moveSpeed = 5f;
    public float returnSpeed = 9f;
    public float wallPush = 0.7f;
    public float dToP = 2;
    public float smallerDToP = 1;
    public LayerMask notPlayer;
    public bool pitchLock = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
     // targetRend = player.Find("Basemesh").GetComponent<MeshRenderer>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
       _GetInputs();
        Clamp();
       _LookAt();
    }

    public Transform player;
    public float camHeight = 2f;
    public float camBehind = 4f;
    public float smooth = 0.6f;
    public Vector2 pitchMinMax = new Vector2(-40f,75f);
    Quaternion rotation;
    //This is where Movement actually happens
    private void LateUpdate()
    {
        Vector3 offsetPos =  new Vector3(0,camHeight,-camBehind);
        if (!pitchLock)
        {
            rotation = Quaternion.Euler(NewMove.y, NewMove.x, 0f);
        }
        else
        {
            NewMove.y = pitchMinMax.y;
            rotation = Quaternion.Euler(NewMove.y, NewMove.x, 0f);
        }

        //transform.position = player.position + rotation * offsetPos;
        CollisionCheck(player.position + rotation * offsetPos);
        WallCheck();
    }

    private Vector2 mouseMov;
    private float _Xaxis;
    private float _Yaxis;
    public float sensX = 4.0f;
    public float sensY = 4.0f;
    public float Ymin = 330f;
    public float Ymax = 60f;
    Vector3 NewMove;
   //Receiving Inputs adding the sensitivity and interpolating for smootheness
   private void _GetInputs()
    {
       _Xaxis += Input.GetAxis("Mouse X");
        _Yaxis -= Input.GetAxis("Mouse Y");
        mouseMov = new Vector2(_Xaxis, _Yaxis);
        mouseMov = Vector2.Scale(mouseMov, new Vector2(sensX, sensY));
        NewMove.x = Mathf.Lerp(NewMove.x, mouseMov.x, smooth );
        NewMove.y = Mathf.Lerp(NewMove.y, mouseMov.y, smooth );
    }

    //Generic clamp function used for the Yaxis
    private void Clamp()
    {
        if (_Yaxis > Ymax/sensY)
        {
           _Yaxis = Ymax/sensY;
        }
        else if (_Yaxis < Ymin/sensY)
        {
            _Yaxis = Ymin/sensY;
        }
        return;
    }

    Quaternion lVector;
   //Generic vector that rotates the camera towards the player
    private void _LookAt()
    {
        lVector = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = lVector;
    }

    private void WallCheck()
    {
        Ray ray = new Ray(player.position, -player.forward);
        RaycastHit hit;

        if(Physics.SphereCast(ray,0.5f,out hit, 0.7f, notPlayer))
        {
            pitchLock = true;
        }
        else
        {
            pitchLock = false;
        }
    }

    //Checking for objects between the player and the camera in that order
    private void CollisionCheck(Vector3 retPoint)
    {
        RaycastHit hit;
        if (Physics.Linecast(player.position, retPoint, out hit, notPlayer))
        {
            Vector3 normalData = hit.normal * wallPush;
            Vector3 p = hit.point + normalData;
           // TransparencyCheck();
            if(Vector3.Distance(Vector3.Lerp(transform.position,p,moveSpeed*Time.deltaTime),player.position) <= smallerDToP)
            {

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime);
            }
            return;
        }
       // FullTransparency();

        transform.position = Vector3.Lerp(transform.position, retPoint, returnSpeed * Time.deltaTime);
        pitchLock = false;
    }
    public float transSpeed = 0.6f;
   /*private void TransparencyCheck()
    {
        if(changeTransparency)
        {
            if(Vector3.Distance(transform.position,player.position) <= dToP)
            {
                Color temp = targetRend.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 0.2f, transSpeed * Time.deltaTime);

                targetRend.sharedMaterial.color = temp;
            }
            else
            {
                if(targetRend.sharedMaterial.color.a <= 0.99f)
                {
                    Color temp = targetRend.sharedMaterial.color;
                    temp.a = Mathf.Lerp(temp.a,1f, transSpeed * Time.deltaTime);

                    targetRend.sharedMaterial.color = temp;
                }
            }
        }

    }
   // private void FullTransparency()
    {
        if(changeTransparency)
        {

            if (targetRend.sharedMaterial.color.a <= 0.99f)
            {
                Color temp = targetRend.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 1f, transSpeed * Time.deltaTime);

                targetRend.sharedMaterial.color = temp;
            }
        }
    }*/
}

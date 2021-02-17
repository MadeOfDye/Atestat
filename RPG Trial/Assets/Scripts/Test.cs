using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	/*public Transform lookAt;


	Transform camTransform;
	Quaternion looks;
	Vector2 rotator;

	private float currentX = 0.0f;
	private float currentY = 0.0f;

	public float distance = 5.0f;
	public float sensivityX = 4.0f;
	public float sensivityY = 1.0f;
	public const float minClamp = -8.0f;
	public const float maxClamp = 70.0f;
	public float height = 1.75f;
	public float smoothness = 2.0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		camTransform = transform;
	}
	private void Update()
	{
		GetInputs();
		Clamp();
		LookAt();
	}
	private void LateUpdate()
	{
		Vector3 offset = new Vector3(0, height, -distance);
		Quaternion rotation = Quaternion.Euler(rotator.y, rotator.x, 0);
		camTransform.position = lookAt.position + rotation * offset;
		//camTransform.LookAt(lookAt.position);
	}
	private void Clamp()
	{
		if (currentY > maxClamp)
			currentY = maxClamp;
		else if (currentY < minClamp)
			currentY = minClamp;
		return;
	}
	void GetInputs()
	{
		currentX += Input.GetAxis("Mouse X");
		currentY -= Input.GetAxis("Mouse Y");
		rotator = new Vector2(currentX, currentY);
		rotator = Vector2.Scale(rotator, new Vector2(sensivityX, sensivityY));
	}
	private void LookAt()
	{
		looks = Quaternion.LookRotation(lookAt.position - camTransform.position);
		camTransform.rotation = Quaternion.Lerp(camTransform.rotation, looks, smoothness);
	}
	#region Public_Variables_and_References
	public float playerHeight = 2f;
	public float heightPadding = 0.2f;
	public float smoothness = 0.5f;
	public float moveSpeed = 10.0f;
	public float gravity = 2.5f;
	public float groundHitCheck;
	public bool smooth;
	public float smoothSpeed;
	public LayerMask discludePlayer;
	public BoxCollider boxCol;
	Quaternion thisRot;
	//Rigidbody rb;
	Camera cam;
	public float groundCheckRadius;
	#endregion
	#region Main_Methods
	void Start()
	{
		//rb = GetComponent<Rigidbody>();
		cam = Camera.main;
		thisRot = transform.rotation;

	}

	void Update()
	{
		Gravity();
		GetInput();
		CalculateDirection();
		Move();
		GroundChecking();
		CollisionCheck();
		DrawDebugLines();

	}
	#endregion
	#region Movement
	private float forw;
	private float side;
	void GetInput()
	{
		side = Input.GetAxisRaw("Horizontal");
		forw = Input.GetAxisRaw("Vertical");
	}
	private float angle;
	void CalculateDirection()
	{
		angle = Mathf.Atan2(side, forw);
		angle = Mathf.Rad2Deg * angle;
		angle += cam.transform.eulerAngles.y;
	}
	Quaternion playerRotation;
	private Vector3 forward;
	void CalculateForward()
	{
		if (!grounded)
		{
			forward = transform.forward;
			return;
		}
		//universal transform.forward in 3d graphics
		forward = Vector3.Cross(groundHit.normal, -transform.right);
	}
	void Rotate()
	{
		playerRotation = Quaternion.Euler(0, angle, 0);
		transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, smoothness * Time.deltaTime);
	}
	//Vector3 playerPosition;
	Vector3 moveVectr;
	void Move()
	{
		transform.position += new Vector3(0, -currentGravity, 0);
		if (Mathf.Abs(side) < 1 && Mathf.Abs(forw) < 1) return;
		CalculateForward();
		Rotate();
		moveVectr = forward * moveSpeed * Time.deltaTime;
		transform.position += moveVectr;
		//playerPosition =transform.forward * moveSpeed * Time.deltaTime;
		//rb.MovePosition(rb.position + playerPosition);
	}
	#endregion
	#region Gravity
	private bool grounded;
	private float currentGravity = 0f;
	private void Gravity()
	{
		if (grounded == false)
		{
			currentGravity = gravity;
		}
		else
		{
			currentGravity = 0;
		}
	}

	private Vector3 liftPoint = new Vector3(0, 2.2f, 0);
	private RaycastHit groundHit;


	private void GroundChecking()
	{

		Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
		RaycastHit tempHit = new RaycastHit();
		if (Physics.SphereCast(ray, 0.17f, out tempHit, groundHitCheck, discludePlayer))
		{
			GroundConfirm(tempHit);
		}
		else
		{
			grounded = false;
		}
	}
	private Vector3 groundCheckPoint = new Vector3(0, -0.57f, 0);
	private void GroundConfirm(RaycastHit tempHit)
	{
		//	float currentSlope = Vector3.Angle(tempHit.normal, Vector3.up);
		Collider[] col = new Collider[3];
		int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCheckPoint), groundCheckRadius, col, discludePlayer);
		grounded = false;
		for (int i = 0; i < num; i++)
		{
			if (col[i].transform == tempHit.transform)
			{
				groundHit = tempHit;
				grounded = true;
				if (!smooth)
				{
					transform.position = new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z);
				}
				else
				{
					transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z), smoothSpeed * Time.deltaTime);
				}

				break;
			}
		}
		if (num <= 1 && tempHit.distance <= 3.1f)
		{
			if (col[0] != null)
			{
				Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 3.1f, discludePlayer))
				{
					if (hit.transform != col[0].transform)
					{
						grounded = false;
						return;
					}
				}
			}
		}
	}
	#endregion
	#region Collision
	private void CollisionCheck()
	{
		Collider[] overlaps = new Collider[4];
		int num = Physics.OverlapBoxNonAlloc(transform.TransformPoint(boxCol.center), (boxCol.size / 2), overlaps, thisRot, discludePlayer, QueryTriggerInteraction.UseGlobal);
		for (int i = 0; i < num; i++)
		{
			Transform t = overlaps[i].transform;
			Vector3 dir;
			float dist;
			if (Physics.ComputePenetration(boxCol, transform.position, transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist)) ;
			Vector3 penetrateVector = dir * dist;
			Vector3 velocityProjected = Vector3.Project(moveVectr, -dir);
			transform.position = transform.position + penetrateVector;
			moveVectr -= velocityProjected;
		}
	}
	#endregion
	void DrawDebugLines()
	{
		Debug.DrawLine(transform.position, transform.position + forward * playerHeight * 2, Color.blue);
		Debug.DrawLine(transform.position, transform.position - Vector3.up, Color.green);
	}
/*	readonly string axisX = "Mouse X", axisY = "Mouse Y";
	float mouseX, mouseY;
	public float rotationSpeedX, rotationSpeedY;
	[Range(0, 5)]
	public float smoothing;
	public float lowClamp, highClamp;
	Vector2 position, calculate, finale;
	public Transform Player, Target;

	private void Start()
	{

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		CamControl();
	}

	void CamControl()
	{
		mouseX = Input.GetAxisRaw(axisX);
		mouseY = Input.GetAxisRaw(axisY);

		position = new Vector2(mouseX, mouseY);
		position = Vector2.Scale(position, new Vector2(rotationSpeedY + smoothing, rotationSpeedX * smoothing));

		calculate.x = Mathf.LerpAngle(calculate.x, position.x, 1f / smoothing);
		calculate.y = Mathf.LerpAngle(calculate.y, position.y, 1f / smoothing);
		finale += calculate;
		finale.y = Mathf.Clamp(finale.y, lowClamp, highClamp);

		transform.LookAt(Target);

		Target.rotation = Quaternion.Euler(finale.y, finale.x, 0);
		Player.rotation = Quaternion.Euler(0, finale.x, 0);


	}
*/
	#region Variables

	[Header("Player Options")]
	public float playerHeight;

	[Header("Movement Options")]
	public float movementSpeed;
	public bool smooth;
	public float smoothSpeed;

	[Header("Jump Options")]
	public float jumpForce;
	public float jumpSpeed;
	public float jumpDecrease;

	[Header("Gravity")]
	public float gravity = 2.5f;

	[Header("Physics")]
	public LayerMask discludePlayer;

	[Header("References")]
	public SphereCollider sphereCol;


	//Private Variables

	//Movement Vectors
	private Vector3 velocity;
	private Vector3 move;
	private Vector3 vel;

	#endregion

	#region Main Methods

	private void Update()
	{
		Gravity();
		SimpleMove();
		Jump();
		FinalMove();
		GroundChecking();
		CollisionCheck();
	}

	#endregion

	#region Movement Methods

	private void SimpleMove()
	{
		move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		velocity += move;
	}

	private void FinalMove()
	{

		Vector3 vel = new Vector3(velocity.x, velocity.y, velocity.z) * movementSpeed;
		//velocity = (new Vector3 (move.x, -currentGravity, move.z)+vel)*movementSpeed;
		//velocity = transform.TransformDirection (velocity);
		vel = transform.TransformDirection(vel);
		transform.position += vel * Time.deltaTime;

		velocity = Vector3.zero;

	}

	#endregion

	#region Gravity/Grounding
	//Gravity Private Variables
	private bool grounded;
	private float currentGravity = 0;

	//Grounded Private Variables
	private Vector3 liftPoint = new Vector3(0, 1.2f, 0);
	private RaycastHit groundHit;
	private Vector3 groundCheckPoint = new Vector3(0, -0.87f, 0);

	private void Gravity()
	{
		if (grounded == false)
		{
			velocity.y -= gravity;
		}
		else
		{
			currentGravity = 0;
		}
	}

	private void GroundChecking()
	{
		Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
		RaycastHit tempHit = new RaycastHit();

		if (Physics.SphereCast(ray, 0.17f, out tempHit, 20, discludePlayer))
		{
			GroundConfirm(tempHit);
		}
		else
		{
			grounded = false;
		}

	}


	private void GroundConfirm(RaycastHit tempHit)
	{

		Collider[] col = new Collider[3];
		int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCheckPoint), 0.55f, col, discludePlayer);

		grounded = false;

		for (int i = 0; i < num; i++)
		{

			if (col[i].transform == tempHit.transform)
			{
				groundHit = tempHit;
				grounded = true;

				//Snapping 
				if (inputJump == false)
				{
					if (!smooth)
					{
						transform.position = new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z);
					}
					else
					{
						transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z), smoothSpeed * Time.deltaTime);
					}
				}

				break;

			}

		}

		if (num <= 1 && tempHit.distance <= 3.1f && inputJump == false)
		{

			if (col[0] != null)
			{
				Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 3.1f, discludePlayer))
				{
					if (hit.transform != col[0].transform)
					{
						grounded = false;
						return;
					}
				}

			}

		}




	}

	#endregion

	#region Collision

	private void CollisionCheck()
	{
		Collider[] overlaps = new Collider[4];
		int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(sphereCol.center), sphereCol.radius, overlaps, discludePlayer, QueryTriggerInteraction.UseGlobal);

		for (int i = 0; i < num; i++)
		{

			Transform t = overlaps[i].transform;
			Vector3 dir;
			float dist;

			if (Physics.ComputePenetration(sphereCol, transform.position, transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist))
			{
				Vector3 penetrationVector = dir * dist;
				Vector3 velocityProjected = Vector3.Project(velocity, -dir);
				transform.position = transform.position + penetrationVector;
				vel -= velocityProjected;
			}

		}

	}

	#endregion

	#region Jumping

	private float jumpHeight = 0;
	private bool inputJump = false;

	private void Jump()
	{
		bool canJump = false;

		canJump = !Physics.Raycast(new Ray(transform.position, Vector3.up), playerHeight, discludePlayer);

		if (grounded && jumpHeight > 0.2f || jumpHeight <= 0.2f && grounded)
		{
			jumpHeight = 0;
			inputJump = false;
		}

		if (grounded && canJump)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				inputJump = true;
				transform.position += Vector3.up * 0.6f * 2;
				jumpHeight += jumpForce;
			}
		}
		else
		{
			if (!grounded)
			{
				jumpHeight -= (jumpHeight * jumpDecrease * Time.deltaTime);
			}
		}

		velocity.y += jumpHeight;


	}

	#endregion
}




using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace StarterAssets
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerController : MonoBehaviour
	{
		public float horizontal;
		public float vertical;
		[Header("Player")]
		[Tooltip("Velocidade de movimento do jogador em m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Velocidade de corrida do jogador em m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Velocidade de rotação")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Aceleração e desaceleração")]
		public float SpeedChangeRate = 10.0f;
		[Space(10)]
		public float jumpForce = 5f; // Força do salto
		public float fallMultiplier = 2.5f; // Multiplicador de queda
		public float lowJumpMultiplier = 2f; // Multiplicador de salto baixo



		[Header("Player Grounded")]
		public bool Grounded = true;
		public float GroundedOffset = -0.14f;
	
		public float GroundedRadius = 0.5f;
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		public GameObject CinemachineCameraTarget;
		public float TopClamp = 90.0f;
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		//private float _terminalVelocity = 53.0f;
		private Vector3 moveDirection = Vector3.zero;
		private Vector3 smoothMoveVelocity = Vector3.zero;

		public Transform aimTarget;
		public Transform aimBody;

		private PlayerInput _playerInput;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;
        private Vector2 lookOffset;
		private Vector2 finalLook;

        private const float _threshold = 0.01f;

		private Rigidbody rb;

		private AudioSource audioSource;

		private Animator anim;

        private float animTurn;
		private float animTurnWeight;

		bool inAir;

		float speedMultiplier;
        private bool IsCurrentDeviceMouse
		{
			get
			{
				return _playerInput.currentControlScheme == "KeyboardMouse";
				//return false;
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
			anim = GetComponent<Animator>();
		}

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
			rb = GetComponent<Rigidbody>();
			_input = GetComponent<StarterAssetsInputs>();
			_playerInput = GetComponent<PlayerInput>();

        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void FixedUpdate()
        {
			JumpAndGravity();
			GroundedCheck();
			Move();
		}

		private void GroundedCheck()
		{
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers);
			if (Grounded)
			{
				if (inAir)
				{
					inAir = false;
                    anim.ResetTrigger("Jump");
                }
            }
		}
		public void SetLookOffset(Vector2 value) 
		{
			lookOffset = value;
		}
		public Vector2 GetLookOffset()
		{
			return lookOffset;
		}

		private void CameraRotation()
		{
			finalLook = _input.look + lookOffset;
			if (lookOffset.sqrMagnitude >= 0)
			{
				lookOffset = Vector2.Lerp(lookOffset, Vector2.zero, Time.deltaTime * 20f);
			}
			else
			{
				lookOffset = Vector2.zero;
			}
            if (finalLook.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

				_cinemachineTargetPitch += finalLook.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = finalLook.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
            }
            if (horizontal == 0 && vertical == 0 && Grounded)
            {
				if (finalLook.sqrMagnitude >= 0.1)
				{
                    animTurn = Mathf.MoveTowards(animTurn, finalLook.x, 2 * Time.deltaTime);

                }
				else
				{
                    animTurn = Mathf.MoveTowards(animTurn, 0, 2 * Time.deltaTime);
                    //animTurnWeight = Mathf.MoveTowards(animTurnWeight, 0, 10 * Time.deltaTime); ;
                }
                animTurnWeight = Mathf.MoveTowards(animTurnWeight, 1, 10 * Time.deltaTime); ;

                //animTurn = Mathf.Lerp(animTurn, _input.look.x, 5f * Time.deltaTime);
            }
            else
            {
                animTurn = Mathf.MoveTowards(animTurn, 0, 5 * Time.deltaTime);
                animTurnWeight = Mathf.MoveTowards(animTurnWeight, 0, 10 * Time.deltaTime); ;
            }
            anim.SetLayerWeight(1, animTurnWeight);
            anim.SetFloat("Turn", animTurn);

        }
        private Vector3 GetPosFromAngle(Vector3 projectedPoint, float angle, Vector3 axis)
        {
            float dist = (projectedPoint - transform.position).magnitude * Mathf.Tan(angle * Mathf.Deg2Rad);
            return projectedPoint + (dist * axis);
        }

        private void Move()
		{
			float speed = GetPlayerSpeed();

            if (_input.move.x != 0) horizontal = Mathf.Lerp(horizontal, _input.move.x, 5f * Time.deltaTime);
            if (_input.move.y != 0) vertical = Mathf.Lerp(vertical, _input.move.y, 5f * Time.deltaTime);



			if(_input.move.x == 0)
            {
                horizontal = Mathf.Lerp(horizontal, 0, 50f * Time.deltaTime);

                if (horizontal < 0.05 && horizontal > -0.05)
                {
                    horizontal = 0;
                }

            }
            if (_input.move.y == 0)
            {
                vertical = Mathf.Lerp(vertical, 0, 50f * Time.deltaTime);

                if (vertical < 0.05 && vertical > -0.05)
                {
                    vertical = 0;
                }

            }
			if(speed == SprintSpeed)
			{
				if (speedMultiplier < 1.99f)
				{
					speedMultiplier = Mathf.Lerp(speedMultiplier, 2, 10f * Time.deltaTime);
				}
				else
				{
					speedMultiplier = 2;
				}

			}

			else
            {
				if (speedMultiplier > 1.01f) speedMultiplier = Mathf.Lerp(speedMultiplier, 1, 10f * Time.deltaTime);
				else { speedMultiplier = 1; }
            }
            anim.SetFloat("MoveX", horizontal*speedMultiplier);
            anim.SetFloat("MoveY", vertical * speedMultiplier);
            Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
            inputDirection = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * inputDirection;

            Vector3 targetVelocity = inputDirection * speed;
            targetVelocity.y = rb.velocity.y;

            rb.velocity = targetVelocity;
            if (horizontal != 0f || vertical != 0f)
            {

                if (!audioSource.isPlaying && Grounded)
                {
                    //GetComponent<Passos>().TocarSomPasso(audioSource);
                }
            }

        }
        public void SetVelocidade(float vel)
        {
			rb.velocity = new Vector3(vel, rb.velocity.y, vel);

		}
		float GetPlayerSpeed()
		{
			if (_input.sprint)
			{
				return SprintSpeed;
			}
			else
			{
				return MoveSpeed;
			}
		}
	
		
		private void JumpAndGravity()
		{
			if (Grounded)
			{
				if (_input.jump && inAir == false)
				{
					anim.SetTrigger("Jump");
					inAir = true;
					rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
				}

				// Aplica multiplicadores de queda e salto baixo
				if (rb.velocity.y < 0)
				{
					rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
				}
				else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
				{
					rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
				}
			}
			anim.SetBool("Air",!Grounded);
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}
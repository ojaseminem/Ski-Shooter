using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        private void Awake() => instance = this;

        private CharacterController _controller;
        private Vector3 _move;
        public float forwardSpeed;
        public float maxSpeed;

        private int _desiredLane = 1;//0:left, 1:middle, 2:right
        public float laneDistance = 2.5f;//The distance between tow lanes

        public bool isGrounded;
        public LayerMask groundLayer;
        public Transform groundCheck;

        public float gravity = -12f;
        public float jumpHeight = 2;
        private Vector3 _velocity;

        public Animator animator;
        private bool _isSliding = false;

        public float slideDuration = 1.5f;

        private bool _toggle = false;
        
        private static readonly int IsGameStarted = Animator.StringToHash("isGameStarted");
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int IsSliding = Animator.StringToHash("isSliding");
        private static readonly int JumpBlend = Animator.StringToHash("jumpBlend");
        private static readonly int Jump1 = Animator.StringToHash("jump");
        private static readonly int SlideBlend = Animator.StringToHash("slideBlend");

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            Time.timeScale = 1.2f;
        }

        private void FixedUpdate()
        {
            if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
                return;

            //Increase Speed
            if (_toggle)
            {
                _toggle = false;
                if (forwardSpeed < maxSpeed)
                    forwardSpeed += 0.1f * Time.fixedDeltaTime;
            }
            else
            {
                _toggle = true;
                if (Time.timeScale < 2f)
                    Time.timeScale += 0.005f * Time.fixedDeltaTime;
            }
        }

        private void Update()
        {
            if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
                return;

            animator.SetBool(IsGameStarted, true);
            _move.z = forwardSpeed;

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.7f, groundLayer);
            animator.SetBool(IsGrounded, isGrounded);
            if (isGrounded && _velocity.y < 0)
                _velocity.y = -1f;

            if (isGrounded)
            {
                if (SwipeManager.swipeUp)
                    Jump();

                if (SwipeManager.swipeDown && !_isSliding)
                    StartCoroutine(Slide());
            }
            else
            {
                _velocity.y += gravity * Time.deltaTime;
                if (SwipeManager.swipeDown && !_isSliding)
                {
                    StartCoroutine(Slide());
                    _velocity.y = -10;
                }                

            }
            _controller.Move(_velocity * Time.deltaTime);

            //Gather the inputs on which lane we should be
            if (SwipeManager.swipeRight)
            {
                _desiredLane++;
                if (_desiredLane == 3)
                    _desiredLane = 2;
            }
            if (SwipeManager.swipeLeft)
            {
                _desiredLane--;
                if (_desiredLane == -1)
                    _desiredLane = 0;
            }

            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (_desiredLane == 0)
                targetPosition += Vector3.left * laneDistance;
            else if (_desiredLane == 2)
                targetPosition += Vector3.right * laneDistance;

            //transform.position = targetPosition;
            if (transform.position != targetPosition)
            {
                Vector3 diff = targetPosition - transform.position;
                Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
                if (moveDir.sqrMagnitude < diff.magnitude)
                    _controller.Move(moveDir);
                else
                    _controller.Move(diff);
            }

            _controller.Move(_move * Time.deltaTime);
        }

        private void Jump()
        {   
            StopCoroutine(Slide());
            animator.SetBool(IsSliding, false);
            var tempBlend = Random.Range(0, 2);
            animator.SetFloat(JumpBlend, tempBlend);
            animator.SetTrigger(Jump1);
            _controller.center = new Vector3(0, 1, 0);
            _controller.height = 1.95f;
            _isSliding = false;
   
            _velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
        }
        
        private IEnumerator Slide()
        {
            _isSliding = true;
            var tempBlend = Random.Range(0, 2);
            animator.SetFloat(SlideBlend, tempBlend);
            animator.SetBool(IsSliding, true);
            yield return new WaitForSeconds(0.25f/ Time.timeScale);
            _controller.center = new Vector3(0, .5f, 0);
            _controller.height = 1;

            yield return new WaitForSeconds((slideDuration - 0.25f)/Time.timeScale);

            animator.SetBool(IsSliding, false);

            _controller.center = new Vector3(0, 1, 0);
            _controller.height = 2;

            _isSliding = false;
        }
    }
}

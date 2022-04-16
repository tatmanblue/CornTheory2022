using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.Character.CharacterControllers
{
    [Obsolete("this doesnt work yet")]
    public class UmaPlayerCharacterController : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }
        public float ForwardInput { get; set; }
        public float TurnInput { get; set; }
        public bool JumpInput { get; set; }
        
        [Tooltip("Maximum slope the character can jump on")]
        [Range(5f, 60f)]
        public float slopeLimit = 45f;
        [Tooltip("Move speed in meters/second")]
        public float moveSpeed = 2f;
        [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
        public float turnSpeed = 300;
        [Tooltip("Whether the character can jump")]
        public bool allowJump = false;
        [Tooltip("Upward speed to apply when jumping in meters/second")]
        public float jumpSpeed = 4f;
        
        private Rigidbody umaRigidbody;
        private CapsuleCollider capsuleCollider;
        
        private void Awake()
        {
            umaRigidbody = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            ForwardInput = 100;
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            ProcessActions();
        }
        
        private void CheckGrounded()
        {
            IsGrounded = false;
            float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
            Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
            float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;

            Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, radius * 5f))
            {
                float normalAngle = Vector3.Angle(hit.normal, transform.up);
                if (normalAngle < slopeLimit)
                {
                    float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                        IsGrounded = true;
                }
            }
        }
        
        private void ProcessActions()
        {
            // Turning
            if (TurnInput != 0f)
            {
                float angle = Mathf.Clamp(TurnInput, -1f, 1f) * turnSpeed;
                transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
            }

            // Movement
            Vector3 move = transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) *
                           moveSpeed * Time.fixedDeltaTime;
            umaRigidbody.MovePosition(transform.position + move);

            // Jump
            if (JumpInput && allowJump && IsGrounded)
            {
                umaRigidbody.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
            }
        }
    }
}

using System;
using UnityEngine;

namespace TatmanGames.Character.CharacterControllers
{
    public class SimpleThirdPersonCharacterController : MonoBehaviour
    {
        public CharacterController Controller;
        public Transform cameraTransform;
        
        public float Speed = 6f;
        public float TurnSmoothTime = 0.1f;

        private float turnSmoothVelocity = 0f;


        private void FixedUpdate()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float y = (cameraTransform != null ? cameraTransform.eulerAngles.y : 0f);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, 
                    ref turnSmoothVelocity, TurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                Controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
            }
        }
    }
}
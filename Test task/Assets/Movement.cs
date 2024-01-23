using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Spine.Unity.Test
{
    public class Movement : MonoBehaviour
    {
        public BoyAnimations model;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float maxWalkSpeed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float distance;
        [SerializeField] private float slowDistance;
        [SerializeField] private float speedBoost;
        private Vector3 mousePos;
        private Vector2 moveAxis;
        public float speed;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            
        }

        // Update is called once per frame
        void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distance = Vector2.Distance(new Vector2(mousePos.x,0),new Vector2(transform.position.x,0));
            moveAxis = new Vector2(mousePos.x - transform.position.x, 0).normalized;
            speed = rb.velocity.x;
            model.SpeedTraking(rb.velocity.x * moveAxis.x);
            if(Input.GetMouseButtonDown(1))
            {
                model.StartAim();
            }
            if(Input.GetMouseButtonUp(1))
            {
                model.StopAim();
            }
        }
        void FixedUpdate()
        {
            rb.velocity = rb.velocity.magnitude * moveAxis;
            Move(moveAxis);
            SpeedLimiter();
            if(distance < 1.5f)
            {
                rb.velocity = new Vector2(0,0);
            }
        }
        private void Move(Vector2 moveDirection)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity += moveDirection * speedBoost;
                model.TryMove(rb.velocity.x);

            }
        }
        private void SpeedLimiter()
        {
            if(model.style == MovementStyle.Walk)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxWalkSpeed);
                if(distance < slowDistance)
                {
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, distance);
                }

            }
            else if(model.style == MovementStyle.Run)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

            }
            else if(model.style == MovementStyle.Hybrid)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
                if(distance < slowDistance)
                {
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, distance);
                }

            }
        }
    }

}
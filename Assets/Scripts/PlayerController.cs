using UnityEngine;
using System.Collections;
using System;

namespace Unit
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D body;
        private bool keyReleased = true;
        private Vector2 lastMove;

        public void Awake()
        {
            body = gameObject.GetComponent<Rigidbody2D>();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            var otherTag = other.gameObject.tag;

            switch (otherTag)
            {
                case "Wall":
                    body.position -= lastMove;
                    break;
                default:
                    break;
            }
        }

        // Update is called every frame, if the MonoBehaviour is enabled
        public void Update()
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");
            vertical = Mathf.Ceil(Mathf.Abs(vertical)) * Mathf.Sign(vertical);
            horizontal = Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal);

            if (Input.GetButtonUp("Horizontal"))
                MoveHorizontal(horizontal);
            else if (Input.GetButtonUp("Vertical"))
                MoveVertical(vertical);

            //if (vertical != 0)
            //    MoveVertical(vertical);
            //else if (horizontal != 0)
            //    MoveHorizontal(horizontal);
        }

        private void MoveHorizontal(float amount)
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            lastMove = new Vector2(amount, 0);
            body.position += lastMove;
            if (amount > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        private void MoveVertical(float amount)
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            lastMove = new Vector2(0, amount);
            body.position += lastMove;
        }
    }
}

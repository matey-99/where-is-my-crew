using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private LayerMask groundLayer = default;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float toGround = 0.5f;

    [Header("Stats")]
    [SerializeField] private float moveAmount = 0f;
    [SerializeField] private bool isGrounded = false;

    private Rigidbody rb = default;
    private Animator anim = default;

    public void Init(GameObject activeModel)
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 4f;
        rb.angularDrag = 999f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        anim = activeModel.GetComponent<Animator>();
    }

    public void Move(float vertical, float horizontal)
    {
        Vector3 verticalVector = vertical * Vector3.forward;
        Vector3 horizontalVector = horizontal * Vector3.right;

        moveDirection = (verticalVector + horizontalVector).normalized;

        float move = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
        moveAmount = Mathf.Clamp01(move);

        //Vector3 moveVector = moveDirection * moveAmount * moveSpeed;
        //rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        isGrounded = IsGrounded();
        rb.drag = (moveAmount > 0 || !isGrounded) ? 0f : 4f;

        rb.velocity = moveDirection * moveAmount * moveSpeed;
        if (!isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + Physics.gravity.y, rb.velocity.z);
        }

        Vector3 targetDirection = moveDirection;
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveAmount * rotateSpeed);

        HandleMovementAnimations();
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * toGround;
        RaycastHit hit;
        float distance = toGround + 0.3f;
        Debug.DrawRay(origin, Vector3.down * distance, Color.red);
        if (Physics.Raycast(origin, Vector3.down, out hit, distance, groundLayer))
        {
            Vector3 targetPosition = hit.point;
            transform.position = targetPosition;
            return true;
        }

        return false;
    }

    private void HandleMovementAnimations()
    {
        anim.SetFloat("Vertical", moveAmount, 0.4f, Time.deltaTime);
    }
}

using UnityEngine;

public class RoombaMove : MonoBehaviour
{
    public float v = 5f; // Forward steps
    public float h = 3f; // Sideways steps
    public float moveSpeed = 2f; // Units per second
    public float turnSpeed = 90f; // Degrees per second

    private enum State { MoveForward, Turn1, MoveSideways, Turn2 }
    private State currentState = State.MoveForward;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isTurning = false;

    void Start()
    {
        startPosition = transform.position;
        SetTargetPosition(v);
    }

    void Update()
    {
        switch (currentState)
        {
            case State.MoveForward:
                MoveToTarget(() =>
                {
                    currentState = State.Turn1;
                    SetTargetRotation(90);
                });
                break;

            case State.Turn1:
                RotateToTarget(() =>
                {
                    currentState = State.MoveSideways;
                    SetTargetPosition(h);
                });
                break;

            case State.MoveSideways:
                MoveToTarget(() =>
                {
                    currentState = State.Turn2;
                    SetTargetRotation(90);
                });
                break;

            case State.Turn2:
                RotateToTarget(() =>
                {
                    currentState = State.MoveForward;
                    SetTargetPosition(v);
                });
                break;
        }
    }

    void SetTargetPosition(float distance)
    {
        startPosition = transform.position;
        targetPosition = startPosition - transform.forward * distance;
    }

    void SetTargetRotation(float angle)
    {
        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
        isTurning = true;
    }

    void MoveToTarget(System.Action onReached)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            onReached?.Invoke();
        }
    }

    void RotateToTarget(System.Action onRotated)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            transform.rotation = targetRotation;
            isTurning = false;
            onRotated?.Invoke();
        }
    }
}

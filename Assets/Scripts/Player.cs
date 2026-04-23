using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private float playerRadius = 0.65f;
    [SerializeField] private float playerHeight = 2f;

    private bool isWalking = false;
    private BaseCounter selectedCounter;
    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnOperateAction += GameInput_OnOperateAction;
    }


    private void Update()
    {
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    public bool IsWalking
    {
        get
        {
            return isWalking;
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    private void GameInput_OnOperateAction(object sender, System.EventArgs e)
    {
        selectedCounter?.InteractOperate(this);
    }

    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();

        float moveDistance = Time.fixedDeltaTime * moveSpeed;

        Vector3 capsuleBottom = transform.position + Vector3.up * playerRadius;
        Vector3 capsuleTop = transform.position + Vector3.up * (playerHeight - playerRadius);

        bool canMove = direction != Vector3.zero && !Physics.CapsuleCast(
            capsuleBottom,
            capsuleTop,
            playerRadius,
            direction,
            moveDistance,
            counterLayerMask,
            QueryTriggerInteraction.Ignore
        );

        if (!canMove && direction != Vector3.zero)
        {
            Vector3 directionX = new Vector3(direction.x, 0, 0).normalized;
            canMove = directionX != Vector3.zero && !Physics.CapsuleCast(
                capsuleBottom,
                capsuleTop,
                playerRadius,
                directionX,
                moveDistance,
                counterLayerMask,
                QueryTriggerInteraction.Ignore
            );

            if (canMove)
            {
                direction = directionX;
            }
            else
            {
                Vector3 directionZ = new Vector3(0, 0, direction.z).normalized;
                canMove = directionZ != Vector3.zero && !Physics.CapsuleCast(
                    capsuleBottom,
                    capsuleTop,
                    playerRadius,
                    directionZ,
                    moveDistance,
                    counterLayerMask,
                    QueryTriggerInteraction.Ignore
                );

                if (canMove)
                {
                    direction = directionZ;
                }
            }
        }

        if (canMove)
        {
            rb.MovePosition(rb.position + direction * moveDistance);
        }

        isWalking = canMove && direction != Vector3.zero;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.fixedDeltaTime * rotateSpeed);
        }
    }

    private void HandleInteraction()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 2f,counterLayerMask))
        {
            if(hitinfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
            {
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    public void SetSelectedCounter(BaseCounter counter)
    {
        if (counter != selectedCounter)
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();

            this.selectedCounter = counter;
        }
    }
}
using UnityEngine;

public class playerMovement: MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    public float walkSwayAmount = 0.1f; // 걷는 동안의 흔들림 양
    public float walkSwayFrequency = 3.0f; // 걷는 동안의 흔들림 주파수

    private float originalYPosition; // 원래 카메라의 Y 위치
    private bool isWalking; // 걷는지 여부를 나타내는 플래그

    private Vector3 originalCameraLocalPosition; // 원래 카메라 위치를 저장하는 변수
    private Vector3 targetCameraLocalPosition; // 목표 카메라 위치를 저장하는 변수
    private float swaySmoothSpeed = 6.0f; // 부드러운 움직임을 위한 보간 속도

    private float verticalRotation = 0;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        originalYPosition = Camera.main.transform.localPosition.y; // 초기 카메라의 Y 위치 설정

        originalCameraLocalPosition = Camera.main.transform.localPosition; // 초기 카메라 위치를 저장합니다.
        targetCameraLocalPosition = originalCameraLocalPosition; // 초기 목표 카메라 위치를 저장합니다.
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse rotation
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Character movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        moveDirection = new Vector3(sideSpeed, moveDirection.y, forwardSpeed);
        moveDirection = transform.TransformDirection(moveDirection);

        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            SwayCamera();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
    void SwayCamera()
    {
        float currentSwayAmount = isWalking ? walkSwayAmount : 0f;
        float currentSwayFrequency = isWalking ? walkSwayFrequency : 0f;

        if (isWalking)
        {
            float swayOffset = Mathf.Sin(Time.time * currentSwayFrequency * Mathf.PI * 2) * currentSwayAmount;
            Vector3 swayOffsetVector = new Vector3(0, swayOffset, 0);
            targetCameraLocalPosition = originalCameraLocalPosition + swayOffsetVector; // 목표 카메라 위치를 갱신합니다.
        }
        else
        {
            // 움직이지 않을 때 목표 카메라 위치를 원래 위치로 설정합니다.
            targetCameraLocalPosition = originalCameraLocalPosition;
        }

        // 카메라 위치를 보간하여 부드럽게 이동하도록 합니다.
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetCameraLocalPosition, Time.deltaTime * swaySmoothSpeed);
    }
}

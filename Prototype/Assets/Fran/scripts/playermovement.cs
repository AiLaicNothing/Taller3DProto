using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class playermovement : MonoBehaviour
// Asegura que este objeto tenga un componente CharacterController, podria agregarlo manualmente, pero cuando vi que se podia anclar al código consideré que era más facil que solo con jalar el script spawneara ya con todo puesto

{
    //
    public Camera playerCamera;
    [SerializeField] private float walkSpeed = 6f; 
    [SerializeField] private float runSpeed = 12f; 
    [SerializeField] private float jumpPower = 7f; 
    [SerializeField] private float gravity = 10f; 
    [SerializeField] private float lookSpeed = 2f; 
    [SerializeField] private float lookXLimit = 45f; 
    [SerializeField] private float defaultHeight = 2f; 
    [SerializeField] private float crouchHeight = 1f; 
    [SerializeField] private float crouchSpeed = 3f; // Se muestran en el inspector pero son privadas, serialize field sirve para que salga en el mismo unity y se pueda cambiar desde ahí

    //---------------------------------------------------------------------------------------------------------------------
    // esta parte se usa para definir el movimiento 
    private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;
        private CharacterController characterController;

        private bool canMove = true;
    //---------------------------------------------------------------------------------------------------------------------
    void Start()// Se ejecuta al iniciar el juego, donde se configuran el cursor y se obtiene el componente CharacterController.
    {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;// bloquea el cursor para poder hacer click más facil, o esa es la idea
            Cursor.visible = false;
        }
    //---------------------------------------------------------------------------------------------------------------------
    void Update()
        {
            HandleMovement(); // Manejo del movimiento del jugador
            HandleMouseClick(); // Manejo del clic del mouse
        }
    //---------------------------------------------------------------------------------------------------------------------
    private void HandleMovement()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)//verifica si el player puede saltar comprobando que el botón de salto está presionado, el jugador está en el suelo y si puede moverse.
        {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.R) && canMove)
            {
                characterController.height = crouchHeight;
                walkSpeed = crouchSpeed;
                runSpeed = crouchSpeed;
            }
            else
            {
                characterController.height = defaultHeight;
                walkSpeed = 6f;
                runSpeed = 12f;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    //---------------------------------------------------------------------------------------------------------------------
    private void HandleMouseClick()// mouse (necesario para el script de bugfix
        {
            if (Input.GetMouseButtonDown(0)) // Verificar si se hizo clic izquierdo
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Realizar el raycast a una distancia de 5 unidades
                if (Physics.Raycast(ray, out hit, 5f))
                {
                    Debug.Log("clikeaste " + hit.collider.name + " en: " + hit.point);
                    // Aquí puedes agregar la lógica adicional que necesites al hacer clic en el objeto
                }
            }
        }
    }
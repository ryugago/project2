using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    public AudioClip clip;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private GameObject[] Keys;
    public bool[] hasKeys;

    [SerializeField]
    private GameObject[] Letter;
    public bool[] hasLetter;

    //[SerializeField]
    //private GameObject Phone;
    public bool hasPhone;
    //static public bool movecamera = true;

    private float applySpeed;
    private float fireDelay;

    [SerializeField]
    private float jumpForce;

    private bool isRun;
    private bool isFireReady;
    public bool isCrouch = false;
    private bool isGround = true;
    private bool iDown;
    private bool fDown;

    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    //카메라민감도
    [SerializeField]
    private float lookSensitivity;
    //카메라 움직임 한계
    [SerializeField]
    private float cameraRotationLimit;
    public float currentCameraRotationX = 0;

    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;

    public Interation_ViewPoint ViewPoint;

    bool isBorder;

    [SerializeField]
    private GameObject[] mess;

    public int Messagenum = 1;
    
    public int notenum = 0;

    private Vector3 moveDirection;

    public Collider Cplayer;
    private bool isFlyMode = false;
    private float flySpeed = 5f;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;

        foreach(GameObject message in mess)
        {
            message.SetActive(false);
        }
    }
    
    void Update()
    {
        for (int i = 0; i < Messagenum; i++)
        {
            mess[i].SetActive(true);
        }

        IsGround();
        if (GameManager.canPlayerMove)
        {
            if (GameManager.canPlayerMove2)
            {
                CameraRotation();
                CharacterRotation();
                if (GameManager.onlycamera)
                {
                    TryJump();
                    TryRun();
                    TryCrouch();

                    if (!isFlyMode)
                    {
                        Move();
                    }
                    else
                    {
                        FlyMove();
                    }

                    ViewPoint.Interation();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (isFlyMode)
            {
                FlyModeOff();
            }
            else
            {
                FlyModeOn();
            }
        }
    }

    private void FlyModeOn()
    {
        isFlyMode = true;
        Cplayer.enabled = false;
        myRigid.useGravity = false;
    }

    private void FlyModeOff()
    {
        isFlyMode = false;
        Cplayer.enabled = true;
        myRigid.useGravity = true;
    }

    private void FlyMove()
    {
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.Space))
        {
            verticalInput = 1;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            verticalInput = -1;
        }

        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _moveUpDown = transform.up * verticalInput;

        moveDirection = (_moveHorizontal + _moveVertical + _moveUpDown).normalized;
        Vector3 _velocity = moveDirection * flySpeed * Time.deltaTime;
        myRigid.MovePosition(transform.position + _velocity);
    }

    /*private void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(Input.GetMouseButtonDown(0)&&isFireReady)
        {
            equipWeapon.Use();
            fireDelay = 0;
        }
    }*/




    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
            StartCoroutine(CrouchCoroutine(2f, 1.2f));
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
            StartCoroutine(CrouchCoroutine(1.2f, 2f));
        }

        
        //theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, applyCrouchPosY,theCamera.transform.localPosition.z);
    }


    IEnumerator CrouchCoroutine(float startHeight, float endHeight)
    {
        float _height = capsuleCollider.height;
        //float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (_height != endHeight)
        {
            count++;
            _height = Mathf.Lerp(_height, endHeight, 0.3f);
            capsuleCollider.height = _height;
            //theCamera.transform.localPosition = new Vector3(0, Mathf.Lerp(_posY, applyCrouchPosY, 0.3f), 0);

            if (count > 15)
                break;

            yield return null;
        }

        capsuleCollider.height = endHeight;
        //theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }



    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
       if (isCrouch)
            Crouch();

        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isCrouch)
            Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;

    }

    private void Move()
    {
        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            SoundManager.instance.SFXPlay("Walk", clip, true);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            SoundManager.instance.SFXStop("Walk");
        }
        */
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirz = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirz;

        moveDirection = (_moveHorizontal + _moveVertical).normalized;
        if (!isBorder)
        {
            Vector3 _velocity = moveDirection * applySpeed * Time.deltaTime;
            myRigid.MovePosition(transform.position + _velocity);
        }

    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, moveDirection * 0.5f, Color.green);
        isBorder = Physics.Raycast(transform.position, moveDirection, 0.5f, LayerMask.GetMask("Wall"));
    }

    void FreezeRotation()
    {
        myRigid.angularVelocity = Vector3.zero;
    }

    public void SetCameraRotationX(float value)
    {
        currentCameraRotationX = value;
    }

    private void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [Header("Move Info")]
    [SerializeField] private float timeToMove =.4f;
    [SerializeField] private float moveUnit = 2f;

    
    [Header("Collision Info")] 
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    
    
    
    private Vector3 originalPos,targetPos,direction;
    private bool isMoving;

    private float gem_count;
    //Components
    private Animator anim;
    private GemCollector _gemCollector;
    
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        _gemCollector = GetComponent<GemCollector>();
    }

    private void Start()
    {
        direction = Vector3.forward;
    }

    void Update()
    {
        InputManager();
        gem_count = _gemCollector.GetGemCount();
    }

    private void InputManager()
    {
        if ((Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))&&!isMoving)
        {
            RotatePlayer(0);
            direction = new Vector3(0, 0, 1);
            if(!WallCheck())
                StartCoroutine(MovePlayer(new Vector3(0,0,moveUnit)));
        }
        else if ((Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))&&!isMoving)
        {
            RotatePlayer(180);
            direction = new Vector3(0, 0, -1);
            if(!WallCheck())
                StartCoroutine(MovePlayer(new Vector3(0,0,-moveUnit)));
        }
        else if ((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))&&!isMoving)
        {
            RotatePlayer(90);
            direction = new Vector3(1, 0, 0);
            if(!WallCheck())
                StartCoroutine(MovePlayer(new Vector3(moveUnit,0,0)));
        }
        else if ((Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))&&!isMoving)
        {
            RotatePlayer(-90);
            direction = new Vector3(-1, 0, 0);
            if(!WallCheck())
                StartCoroutine(MovePlayer(new Vector3(-moveUnit,0,0)));
        }
        
    }

    private IEnumerator MovePlayer(Vector3 dir)
    {
        isMoving = true;
        anim.SetTrigger("jump");
        float elapsedTime = 0;
        originalPos = transform.position;
        targetPos = originalPos + dir;
        float additional_time = gem_count * 0.02f;
        while (elapsedTime <timeToMove + additional_time)
        {
            transform.position = Vector3.Lerp(originalPos, targetPos, (elapsedTime / (timeToMove + additional_time)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = targetPos;
        isMoving = false;
    }

    private void RotatePlayer(float degree)
    {
        transform.eulerAngles = new Vector3(0,degree,0);
    }

    private bool WallCheck()
    {
        if (Physics.Raycast(wallCheck.position, wallCheck.TransformDirection(Vector3.forward), out RaycastHit hitInfo,
                wallCheckDistance))
        {
            if (hitInfo.transform.gameObject.CompareTag("Obstacle") || hitInfo.transform.gameObject.CompareTag("Fence"))
                return true;
        }

        return false;
    }

    public Vector3 GetPlayerDirection()
    {
        return direction;
    }
}

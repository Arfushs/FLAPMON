using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    [Header("Life Info")]
    [SerializeField] private int life;
    private FlipAndDestroy _flipAndDestroy;
    
    [Header("Movement Info")]
    [SerializeField] private float timeToMove =.4f;
    [SerializeField] private float moveUnit = 2f;
    private Vector3 direction,back_dir,orig_pos,target_pos;
    private bool isMoving = false;
    
    
    private void Awake()
    {
        _flipAndDestroy = GetComponent<FlipAndDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isMoving&&life>0)
        {
            ChooseDirection();
            isMoving = true;
            StartCoroutine("Navigator");
        }
        
        
    }

    private IEnumerator Navigator()
    {
        isMoving = true;
        float elapsed_time = 0;
        orig_pos = transform.position;
        target_pos = orig_pos + direction*moveUnit;

        while (elapsed_time<timeToMove)
        {
            transform.position = Vector3.Lerp(orig_pos, target_pos, (elapsed_time / timeToMove));
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        transform.position = target_pos;
        isMoving = false;
    }

    private void ChooseDirection()
    {
        List<Vector3> list = CreateVectorArray();
        int number = UnityEngine.Random.Range(0, list.Count);
        if (list[number].Equals(back_dir)&& !ControlOnlyBack())
        {
            ChooseDirection();
            return;
        }
        direction = list[number];
        RotateEnemy();
    }

    private bool ControlOnlyBack()
    {
        if (back_dir.Equals(Vector3.forward))
        {
            return CheckLeftVector() && CheckRightVector() && CheckBackVector();
        }
        else if (back_dir.Equals(Vector3.back))
        {
            return CheckLeftVector() && CheckRightVector() && CheckForwardVector();
        }
        else if (back_dir.Equals(Vector3.left))
        {
            return CheckForwardVector() && CheckRightVector() && CheckBackVector();
        }
        else
        {
            return CheckLeftVector() && CheckForwardVector() && CheckBackVector();
        }
    }
    
    private void RotateEnemy()
    {
        float xRot = transform.rotation.eulerAngles.x;
        if (direction.Equals(Vector3.forward))
        {
            transform.eulerAngles = new Vector3(xRot,0,0);
            back_dir = new Vector3(0, 0, -1);
        }

        if (direction.Equals(Vector3.back))
        {
            transform.eulerAngles = new Vector3(xRot,180,0);
            back_dir = new Vector3(0, 0, 1);
        }

        if (direction.Equals(Vector3.left))
        {
            transform.eulerAngles = new Vector3(xRot,-90,0);
            back_dir = new Vector3(1, 0, 0);
        }
        if (direction.Equals(Vector3.right))
        {
            transform.eulerAngles = new Vector3(xRot,90,0);
            back_dir = new Vector3(-1, 0, 0);
        }
            
    }


    private List<Vector3> CreateVectorArray()
    {
        List<Vector3> list = new List<Vector3>();
        if(!CheckForwardVector())
            list.Add(Vector3.forward);
        if(!CheckBackVector())
            list.Add(Vector3.back);
        if(!CheckLeftVector())
            list.Add(Vector3.left);
        if(!CheckRightVector())
            list.Add(Vector3.right);
        return list;
    }

    private bool CheckForwardVector() => WallCheck(Vector3.forward);
    private bool CheckBackVector() => WallCheck(Vector3.back);
    
    private bool CheckRightVector() => WallCheck(Vector3.right);
    private bool CheckLeftVector() => WallCheck(Vector3.left);
    
    private bool WallCheck(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, dir, out RaycastHit hitInfo,
                1.9f))
        {
            if (hitInfo.transform.gameObject.CompareTag("Obstacle") || hitInfo.transform.gameObject.CompareTag("Fence"))
                return true;
        }

        return false;
    }
    public void TakeDamage()
    {
        life -= 1;
        if (life<=0)
        {
            StopAllCoroutines();
            GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
            _flipAndDestroy.DestroyObject();
            
        }
    }

    public int GetLifeCount() => life;

}

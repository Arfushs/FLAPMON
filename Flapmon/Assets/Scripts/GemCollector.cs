using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    [Header("Collecting Gems")]
    [SerializeField] private GameObject _gemPref;
    [SerializeField] private Transform _gemContainer;

    [Header("Giving Gems")] 
    [SerializeField] private Transform _gem_cube_check;
    [SerializeField] private float gem_time=1f;
    [SerializeField] private GameManager _gameManager;
    
    private bool isGemGiving = false;
    private bool coroutineOneShot = true;
    
    private int gem_count;
    private float container_offset =0.64f;

    

    private void Update()
    {
        CheckGemCube();
        if (coroutineOneShot)
        {
            coroutineOneShot = false;
            StartCoroutine("GiveGems");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            CollectGem();
            Destroy(other.gameObject);
        }
    }

    private void CollectGem()
    {
        GameObject gem = Instantiate(_gemPref,transform.position,quaternion.identity);
        gem.GetComponent<Gem>().CloseCollider();
        gem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        gem.transform.parent = _gemContainer;
        gem.GetComponent<Gem>().CloseRotation();
        Vector3 position = _gemContainer.transform.position + new Vector3(0, container_offset * gem_count, 0);
        gem.transform.position = position;
        gem.transform.eulerAngles = new Vector3(0, 0, 90);
        gem_count += 1;
    }

    private void CheckGemCube()
    {
        if (Physics.Raycast(_gem_cube_check.position, _gem_cube_check.TransformDirection(Vector3.down),
                out RaycastHit hitInfo, 2))
        {
            if (hitInfo.transform.gameObject.CompareTag("GemCube"))
            {
                isGemGiving = true;
                
            }
            else
            {
                isGemGiving = false;
                StopAllCoroutines();
                coroutineOneShot = true;
            }
        }
        
    }

    private IEnumerator GiveGems()
    {
        yield return new WaitForSeconds(gem_time);
        while (isGemGiving)
        {
            Gem[] gems = _gemContainer.GetComponentsInChildren<Gem>();
            if (gems.Length > 0)
            {
                gems[gem_count - 1].transform.parent = null;
                gems[gem_count - 1].Rise();
                _gameManager.DecreaseGemCount();
                gem_count -= 1;
            }

            yield return new WaitForSeconds(gem_time);
        }

        coroutineOneShot = true;
    }

    public float GetGemCount() => gem_count;
}

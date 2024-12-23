using System;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private List<Transform> ApplePoints;

    [SerializeField] private GameObject ApplePrefab;
    private List<GameObject> AppleGOs = new List<GameObject>();
    
    [SerializeField] private float SpawnRate = 5f;
    
    [SerializeField] private GameObject QuestIcon;
    
    private float _elapsedTime = 0f;

    private int _currentAppleIndex = 0;

    private bool _isPlayerClose = false;
    
    private Quest _quest = new Quest("Соберите яблоки");
    private bool _questWasBegan = false;
    
    private void Update()
    {
        if (_isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            CollectApples();
        }
        
        if (_currentAppleIndex >= ApplePoints.Count)
        {
            if (!_questWasBegan)
            {
                QuestManager.Instance.AddQuest(_quest);
                _questWasBegan = true;
                
                QuestIcon.SetActive(true);
            }

            return;
        }
        
        _elapsedTime += Time.deltaTime;
        
        if (_elapsedTime >= SpawnRate)
        {
            var apple = Instantiate(ApplePrefab, ApplePoints[_currentAppleIndex].position, Quaternion.identity);
            AppleGOs.Add(apple);
            
            _currentAppleIndex++;
            _elapsedTime = 0f;
        }
    }
    
    private void CollectApples()
    {
        if (_questWasBegan)
        {
            QuestManager.Instance.CompleteQuest(_quest);
            QuestIcon.SetActive(false);
        }
        
        foreach (var appleGo in AppleGOs)
        {
            Destroy(appleGo);
        }
        AppleGOs.Clear();
        
        _currentAppleIndex = 0;
        
        Debug.Log("Apples were collected");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _isPlayerClose = false;
        }
    }
}

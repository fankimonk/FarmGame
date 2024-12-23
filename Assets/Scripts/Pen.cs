using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Pen : MonoBehaviour
{
    [SerializeField] private List<Animal> Animals;

    [SerializeField] protected float HungerCapacity = 100f;
    [SerializeField] private float HungerRate = 1.5f;
    protected float _hungerValue;
    
    [SerializeField] private Transform CornerPoint1Transform;
    [SerializeField] private Transform CornerPoint2Transform;

    [SerializeField] private LayerMask AnimalLayer;

    [SerializeField] private GameObject QuestIcon;
    
    private Vector3 _cornerPoint1 => CornerPoint1Transform.position;
    private Vector3 _cornerPoint2 => CornerPoint2Transform.position;
    
    private float _elapsedTime = 0f;

    protected Player _player = null;

    protected UnityEvent OnQuestStarted = new UnityEvent();
    protected UnityEvent OnQuestCompleted = new UnityEvent();
    private bool _questWasBegan = false;
    
    private void Awake()
    {
        StartCoroutine(MoveAnimals());
        
        _hungerValue = HungerCapacity;

        OnQuestStarted.AddListener(() =>
        {
            _questWasBegan = true;
            QuestIcon.SetActive(true);
        });
        OnQuestCompleted.AddListener(() =>
        {
            _questWasBegan = false;
            QuestIcon.SetActive(false);
        });
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= 1)
        {
            _hungerValue -= HungerRate;
            _hungerValue = Mathf.Clamp(_hungerValue, 0f, HungerCapacity);
            _elapsedTime = 0;
        }

        if (_hungerValue == 0 && !_questWasBegan)
        {
            OnQuestStarted.Invoke();
            
            Debug.Log(gameObject.name + " needs to be fed");
        }
    }

    public void Feed(float amount)
    {
        if (_questWasBegan)
            OnQuestCompleted.Invoke();
        
        _hungerValue += amount;
        _hungerValue = Mathf.Clamp(_hungerValue, 0f, HungerCapacity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _player))
        {
            Debug.Log($"Player is close to {gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out _player))
        {
            _player = null;
            Debug.Log($"Player is not close to {gameObject.name}");
        }
    }

    protected IEnumerator MoveAnimals()
    {
        while (true)
        {
            foreach (var animal in Animals)
            {
                var nextPoint = GetRandomPoint(animal);
                yield return StartCoroutine(animal.Move(nextPoint));
            }

            yield return null;
        }
    }
    
    private Vector3 GetRandomPoint(Animal animal)
    {
        var minX = Mathf.Min(_cornerPoint1.x, _cornerPoint2.x);
        var minZ = Mathf.Min(_cornerPoint1.z, _cornerPoint2.z);
        
        var maxX = Mathf.Max(_cornerPoint1.x, _cornerPoint2.x);
        var maxZ = Mathf.Max(_cornerPoint1.z, _cornerPoint2.z);

        Vector3 randomPoint;
        while (true)
        {
            randomPoint = new Vector3(Random.Range(minX, maxX), animal.transform.position.y,
                Random.Range(minZ, maxZ));

            var raycastDir = randomPoint - animal.transform.position;
            if (!Physics.Raycast(animal.transform.position, raycastDir, raycastDir.magnitude, AnimalLayer))
                break;
        }

        return randomPoint;
    }
}

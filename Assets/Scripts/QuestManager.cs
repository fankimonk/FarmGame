using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance = null;

    [SerializeField] private RectTransform UIRect;
    
    [SerializeField] private GameObject QuestTextPrefab;
    private Vector3 _startPosition = new Vector3(-29.5f, 597, 0);
    private float _yOffset = -30.0f;

    private GameObject _questMessages = null;
    
    private List<Quest> _quests = new List<Quest>();
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void DrawQuestTexts()
    {
        if (_questMessages != null) Destroy(_questMessages);
        
        _questMessages = new GameObject("QuestTexts");
        _questMessages.transform.SetParent(UIRect);
        _questMessages.transform.localPosition = Vector3.zero;

        var currentPosition = _startPosition;
        foreach (var quest in _quests)
        {
            var questMessage = Instantiate(QuestTextPrefab, currentPosition, Quaternion.identity, _questMessages.transform);
            questMessage.GetComponentInChildren<TMP_Text>().text = quest.Text;

            currentPosition.y += _yOffset;
        }
    }
    
    public void AddQuest(Quest newQuest)
    {
        _quests.Add(newQuest);
        DrawQuestTexts();
    }

    public void CompleteQuest(Quest quest)
    {
        _quests.Remove(quest);
        DrawQuestTexts();
    }
}

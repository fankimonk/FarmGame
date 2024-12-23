using System;
using System.Collections.Generic;
using UnityEngine;

public class CowPen : Pen
{
    [SerializeField] private List<GameObject> Bales = new List<GameObject>(2);

    private Quest _quest = new Quest("Накормите коров");

    private void Start()
    {
        OnQuestStarted.AddListener(() => QuestManager.Instance.AddQuest(_quest));
        OnQuestCompleted.AddListener(() => QuestManager.Instance.CompleteQuest(_quest));
    }

    private void LateUpdate()
    {
        if (_player != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"{gameObject.name} was fed");

            if (_player.Supply.PenTags.Contains(gameObject.tag))
            {
                Feed(_player.Supply.HungerValue);
                _player.DropBaleOfHay();
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Quest> quests;
    public int activeQuest;
    public List<int> completedQuests;
    public PlayerControl playerControl;
    public IngameUI igui;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        igui = GameObject.Find("UI (1)").GetComponent<IngameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestCheck()) CompleteQuest();
    }

    bool QuestCheck() //Check if the active quest is completed;
    {
        quests[activeQuest].QuestCheck();
        foreach (SubQuest sq in quests[activeQuest].subQuests)
        {
            if (!sq.completed) return false;
        }
        return true;
    }

    public void ChangeQuest(int i)
    {
        if(i < 0)
        {
            return;
        }

        activeQuest = i;
    }

    public void CompleteQuest()
    {
        activeQuest = -1;
    }
}

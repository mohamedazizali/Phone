using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Create New Quest")]

public class Quest : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;
}

using System.Collections.Generic;
using UnityEngine;

public class ScenarioScript : ScriptableObject
{
    [System.Serializable]
    public class CommandAction
    {
        public string command;
        public List<string> args = new List<string>();
    }
    
    public List<CommandAction>	commands	= new List<CommandAction>();
}
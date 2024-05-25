using System.Collections.Generic;
using UnityEngine;
using System;

public class DescriptionCommand : MonoBehaviour
{
    //[SerializeField] private List<Command> _commands = new();
    // public static DescriptionCommand instance;
    [SerializeField] private List<Command> _commands = new();
    private void OnValidate()
    {
        Commands = _commands;
    }

    public static List<Command> Commands = new();

    public static int PlayerCommand = 1;
}
[Serializable]
public class Command
{
    public int LayerNumber;
    public string Name;
    public int Number;
    public Color ColorOfCommand;
    public string Description;
    public Material Material;
    public Material MaterialOfPoint;
}

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SpawnerEnemu : MonoBehaviour
{
    [SerializeField] private List<SpawnStage> _spawnStages;
    [SerializeField] private float _stageTime;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private Transform SpawmPosition;

    private GameObject _enemuGroup;
    private int _layer;
    private void Start()
    {
        foreach (Command command in DescriptionCommand.Commands)
        {
            if (command.Number == EnemuGroupManager.instance.NumberCommand)
            {
                _layer = command.LayerNumber;
            }
        }
        NewStage();
    }
    private void PlaySpawnStage(SpawnStage spawnStage)
    {
        _timeBetweenSpawn -= Time.deltaTime;
        _stageTime-= Time.deltaTime;
        if(_timeBetweenSpawn <= 0)
        {
            _enemuGroup = Instantiate(spawnStage.EnemuGroupPrefab, SpawmPosition.position, SpawmPosition.rotation);
            _enemuGroup.layer = _layer;
            _timeBetweenSpawn = spawnStage.TimeBetweenSpawn;
        }
        if(_stageTime <= 0)
        {
            _spawnStages.RemoveAt(0);
            NewStage();
        }
    }
    private void Update()
    {
        if (_spawnStages.Count > 0)
        {
            PlaySpawnStage(_spawnStages[0]);
        }
    }
    private void NewStage()
    {
        _timeBetweenSpawn = _spawnStages[0].TimeBetweenSpawn;
        _stageTime = _spawnStages[0].StageTime;
    }

}

[Serializable]
public class SpawnStage
{
    public GameObject EnemuGroupPrefab;
    public float StageTime;
    public float TimeBetweenSpawn;
}

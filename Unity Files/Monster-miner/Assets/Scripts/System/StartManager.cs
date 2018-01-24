﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartManager : MonoBehaviour {
    public TerrainSpawner terrainSpawner;
    string path = "./";
    // Use this for initialization
    void Start(){
        /*This will delete the save*/
        //File.Delete(path + "SaveData.dat");

        TimeManager.Instance.BeginTime();
        MonsterSpawner.Instance.SpawnMonsterLists();
        ColonistSpawner.Instance.SpawnColonistLists();
        if (FileExist())
        {
            Debug.Log("Found Save");
            LoadWorld();
        }

        else
        {
            Debug.Log("No Save Found");
            GenerateWorld();
        }

        StartCoroutine(BehaviourTreeManager.Instance.BehaviourTrees());
        ColonistSpawner.Instance.SpawnColonist(new Vector3(0,0,0),ColonistJobType.Crafter);
    }

    void GenerateWorld() {
        StartCoroutine(terrainSpawner.SpawnNewWorld());
        MonsterSpawner.Instance.NewWorldSpawnMonsters();
    }

    void LoadWorld() {
        StartCoroutine(terrainSpawner.LoadWorld());
    }

	public bool FileExist()
    {
        return true ;
       // return (File.Exists(path + "SaveData.dat")) ;
    }
}

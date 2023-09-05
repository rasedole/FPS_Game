using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JSONExample : MonoBehaviour
{
    [Serializable]
    public class Monster
    {
        public string name;
        public int hp;
        public int attackPower;
        public int speed;
    }

    [Serializable]
    public class MyClass
    {
        public int level;
        public float timeElapsed;
        public string playerName;
    }

    [Serializable]
    public class StageData
    {
        public int stageNumber;
        public List<Monster> monsters;
    }

    [Serializable]
    public class TotalStageData
    {
        public List<StageData> stages;
    }


    private void Start()
    {
        Monster monster1 = new Monster();
        monster1.name = "Hulk";
        monster1.hp = 100;
        monster1.attackPower = 1000;
        monster1.speed = 50;

        string json = JsonUtility.ToJson(monster1);
        string newtonJson = JsonConvert.SerializeObject(monster1);

        JObject keyValuePairs = JObject.Parse(newtonJson);

        JObject stagehead = (JObject)keyValuePairs["stages"];
        if (stagehead.ContainsKey("monsters"))
        {
            JObject monsters = (JObject)stagehead["monsters"];

            if (monsters["name"].ToString() == "Goblin")
            {
                monsters["hp"] = 10;
            }
        }

        //print(json);

        //WriteJson();

        ReadJson(StageDataReader());
        foreach (var stage in totalStageData.stages)
        {
            Debug.Log($"Stage number: {stage.stageNumber}");
            foreach (var monster in stage.monsters)
            {
                Debug.Log($"Monster name : {monster.name},\nhp : {monster.hp},\nattackPower : {monster.attackPower},\nspeed : {monster.speed}\n");
            }
        }
    }

    public TotalStageData totalStageData;

    void WriteJson()
    {
        string json =
            "{\r\n \"level\" : 11, \r\n \"timeElapsed\" : 30.55, \r\n \"playerName\" : \"New York\"\r\n}";

        MyClass myClass = JsonUtility.FromJson<MyClass>(json);
        print(myClass.level);
    }

    public string StageDataReader()
    {
        string path = Application.dataPath + "/stages.json";
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        string json = streamReader.ReadToEnd();
        Debug.Log(json);
        streamReader.Close();
        fileStream.Close();

        return json;
    }

    void ReadJson(string json)
    {
        totalStageData = JsonUtility.FromJson<TotalStageData>(json);
    }
}

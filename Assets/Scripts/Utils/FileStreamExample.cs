using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileStreamExample : MonoBehaviour
{
    FileStream fileStream;

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/fileStream.txt";
        fileStream = new FileStream(path, FileMode.OpenOrCreate);

        //for(int i = 65; i < 90; i++)
        //{
        //    fileStream.WriteByte((Byte)i);

        //}

        WriteStream();

        //ReadStream();
        
        fileStream.Close();

        ParseCSV();
    }

    void ReadStream()
    {
        StreamReader streamReader = new StreamReader(fileStream);
        string line = streamReader.ReadLine();

        Debug.Log(line);
        streamReader.Close();
    }

    void WriteStream()
    {
        StreamWriter streamWriter = new StreamWriter(fileStream);

        streamWriter.WriteLine("Hello Everyone");
        
        streamWriter.Close();
    }

    void WriteCSVFormat()
    {
        string mosterName = "Hulk";
        int hp = 10;
        int speed = 50;
        string csvFormat = string.Format("{0}, {1}, {2}", mosterName, hp, speed);
    }

    void ParseCSV()
    {
        string csvFormat = "Hulk, 10, 50";
        char[] sep = {','};
        string[] strings = csvFormat.Split(sep);

        foreach (string s in strings)
        {
            print(s);
        }
    }


}

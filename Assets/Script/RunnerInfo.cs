using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;

public class RunnerInfo {

    public struct scorePlayer
    {

        public float time;
        public string name;

        public scorePlayer(float t, string n)
        {
            time = t;
            name = n;
        }

    }

    public static int levelNum = 0;
    public static int lengthNum = 0;

    public static bool easy = true;
    public static float density = 0.25f; //0.4f изменить плотность в начальной цене
    public static int lengthWay = 7;
    public static string name = "";
    //public static List<scorePlayer>[] highscore;

    public static List<scorePlayer> highscore;

    //public static void initHS()
    //{
    //    highscore = new List<scorePlayer>[9];
    //    for(int i = 0; i < 9; i++)
    //    {
    //        highscore[i] = new List<scorePlayer>();
    //    }
    //    string path = Application.persistentDataPath + "/highscore.xml";
    //    try
    //    {
    //        if (File.Exists(path))
    //        {
    //            XElement root = XDocument.Parse(File.ReadAllText(path)).Element("root");
    //            IEnumerable<XNode> hsArray = root.Nodes();
    //            int index = 0;
    //            foreach (XElement hs in root.Nodes())
    //            {
    //                foreach (XElement s in hs.Nodes())
    //                {
    //                    string name = s.Attribute("name").Value;
    //                    float time = float.Parse(s.Attribute("time").Value);
    //                    highscore[index].Add(new scorePlayer(time, name));
    //                }
    //                index++;                   
    //            }
    //        }
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.Log(e.StackTrace);
    //    }
    //    
    //}

    public static void initHS()
    {
        highscore = new List<scorePlayer>();
        string path = Application.persistentDataPath + "/highscore.xml";
        try
        {
            if (File.Exists(path))
            {
                XElement root = XDocument.Parse(File.ReadAllText(path)).Element("root");

                int index = 0;
                XElement hs = root.Element("highscore");
                foreach (XElement s in hs.Nodes())
                {
                    string name = s.Attribute("name").Value;
                    float time = float.Parse(s.Attribute("time").Value);
                    highscore.Add(new scorePlayer(time, name));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        
    }

    //public static int addScore(string name, float time, int indexLevel)
    //{
    //    scorePlayer sc = new scorePlayer(time, name);
    //    int index = 0;
    //    for(;index < highscore[indexLevel].Count; index++)
    //    {
    //        if(highscore[indexLevel][index].time > sc.time)
    //        {
    //            break;
    //        }
    //    }
    //    highscore[indexLevel].Insert(index, sc);
    //    return index + 1;
    //}

    public static int addScore(string name, float time)
    {
        scorePlayer sc = new scorePlayer(time, name);
        int index = 0;
        for(;index < highscore.Count; index++)
        {
            if(highscore[index].time > sc.time)
            {
                break;
            }
        }
        highscore.Insert(index, sc);
        return index + 1;
    }

    //public static void saveHS()
    //{
    //    string path = Application.persistentDataPath + "/highscore.xml";
    //    XElement root = new XElement("root");
    //    foreach(List<scorePlayer> hs in highscore)
    //    {
    //        XElement highscore = new XElement("highscore");
    //        foreach (scorePlayer sc in hs)
    //        {
    //            XElement score = new XElement("score");
    //            score.SetAttributeValue("name", sc.name);
    //            score.SetAttributeValue("time", sc.time);
    //            highscore.Add(score);
    //        }
    //        root.Add(highscore);
    //    }
    //    XDocument saveDoc = new XDocument(root);
    //    File.WriteAllText(path, saveDoc.ToString());
    //
    //}

    public static void saveHS()
    {
        string path = Application.persistentDataPath + "/highscore.xml";
        XElement root = new XElement("root");

        XElement hs = new XElement("highscore");
        foreach (scorePlayer sc in highscore)
        {
            XElement score = new XElement("score");
            score.SetAttributeValue("name", sc.name);
            score.SetAttributeValue("time", sc.time);
            hs.Add(score);
        }
        root.Add(hs);
            
        XDocument saveDoc = new XDocument(root);
        File.WriteAllText(path, saveDoc.ToString());

    }
}

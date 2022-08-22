using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tables
{
    public class StringUI
    {
        public int _ID;
        public string _Code;
        public string _Korean;

        //static List<StringUI> _data;
        static Dictionary<int, StringUI> _map;

        static void Add(StringUI Sender)
        {
            //_data.Add(Sender);
            _map.Add(Sender._ID, Sender);
        }

        public static StringUI Get(int key)
        {
            StringUI ret;
            _map.TryGetValue(key, out ret);
            return ret;
        }

        public static void StringUILoad()
        {
            TextAsset dataset = Resources.Load<TextAsset>("CSVs/StringUI");
            string[] dataLines = dataset.text.Split('\n');

            for (int i = 0; i < dataLines.Length; i++)
            {
                var data = dataLines[i].Split(',');

                StringUI Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Code = data[idx++];
                Tmp._Korean = data[idx++];

                Add(Tmp);
            }
        }
    }






    public class TablesLoader
    {
        public static void Load()
        {
            // 새로운 Class가 생기면 추가해줘야 함
            StringUI.StringUILoad();
        }
    }
}



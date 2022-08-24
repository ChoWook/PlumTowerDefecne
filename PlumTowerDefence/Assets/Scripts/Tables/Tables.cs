using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptables/Tables")]
public class Tables : ScriptableObject
{
    static bool IsLoaded = false;

    public static void Load()
    {
        if (!IsLoaded)
        {
            // 새로운 Class가 생기면 추가해줘야 함
            StringUI.Load();
            GroundPattern.Load();
            MapPattern.Load();

            IsLoaded = true;
            Debug.Log("Load End");
        }
    }

    public class CSVFile<_T>
    {
        public int _ID;

        static Dictionary<int, _T> _map = new();

        protected void Add(_T Sender)
        {
            //_data.Add(Sender);
            _map.Add(_ID, Sender);
        }

        public static _T Get(int key)
        {
            _T ret;
            _map.TryGetValue(key, out ret);
            return ret;
        }
    }

    public class StringUI : CSVFile<StringUI>
    {
        public string _Code;
        public string _Korean;

        static Dictionary<string, StringUI> _StringMap = new();

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/StringUI");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log(dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                StringUI Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Code = data[idx++];
                Tmp._Korean = data[idx++];

                Tmp.Add(Tmp);
                _StringMap.Add(Tmp._Code, Tmp);
            }
        }

        public StringUI Get(string code)
        {
            return _StringMap[code];
        }
    }

    public class GroundPattern : CSVFile<GroundPattern>
    {
        public EGroundType _Type;
        public List<ETileType> _Tiles;

        public static Dictionary<EGroundType, List<GroundPattern>> _PatternWithType = new(); 

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/GroundPattern");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log(dataLines.Length);

            // 타입별로 분류하기 위해 딕셔너리에 분배
            for (EGroundType j = EGroundType.TR; j <= EGroundType.URD; j++)
            {
                _PatternWithType.Add(j, new List<GroundPattern>());
            }

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                GroundPattern Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Type = Enum.Parse<EGroundType>(data[idx++]);

                Tmp._Tiles = new();
                
                for(; idx < data.Length; idx++)
                {
                    Tmp._Tiles.Add((ETileType)int.Parse(data[idx]));
                }

                Tmp.Add(Tmp);


                _PatternWithType[Tmp._Type].Add(Tmp);
            }
        }

        public static List<GroundPattern> Get(EGroundType type)
        {
            return _PatternWithType[type];
        }
    }

    public class MapPattern : CSVFile<MapPattern>
    {
        public struct GroundInfo
        {
            public int _PosX;
            public int _PosY;
            public EGroundType _Type;
        }

        public int _LevelNum;
        public List<GroundInfo> _Grounds;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MapPattern");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log(dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                dataLines[i] = dataLines[i].Replace("\"", string.Empty);
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MapPattern Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._LevelNum = int.Parse(data[idx++]);

                Tmp._Grounds = new();
                for (; idx < data.Length;)
                {
                    if (string.IsNullOrEmpty(data[idx]))
                    {
                        break;
                    }

                    GroundInfo groundInfo = new GroundInfo();

                    groundInfo._PosX = int.Parse(data[idx++]);
                    groundInfo._PosY = int.Parse(data[idx++]);
                    groundInfo._Type = Enum.Parse<EGroundType>(data[idx++]);

                    Tmp._Grounds.Add(groundInfo);
                }

                Tmp.Add(Tmp);
            }
        }
    }
}
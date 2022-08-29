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
            MapGimmick.Load();
            MapGimmickObstacle.Load();
            MapGimmickResource.Load();
            GlobalSystem.Load();

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

        public static int GetSzie()
        {
            return _map.Count;
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

            Debug.Log("StringUI : " + dataLines.Length);

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

        public static StringUI Get(string code)
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

            Debug.Log("GroundPattern : " + dataLines.Length);

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

            Debug.Log("MapPattern : " + dataLines.Length);

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

    public class MapGimmick : CSVFile<MapGimmick>
    {
        public EMapGimmickType _Type;
        public string _Korean;
        public int _Priority;
        public float _Probability;
        public bool _ExistedMap;

        static Dictionary<EMapGimmickType, MapGimmick> _MapWithType;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MapGimmick");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MapGimmick : " + dataLines.Length);

            _MapWithType = new Dictionary<EMapGimmickType, MapGimmick>();

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MapGimmick Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Type = Enum.Parse<EMapGimmickType>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._Priority = int.Parse(data[idx++]);
                Tmp._Probability = float.Parse(data[idx++]);
                Tmp._ExistedMap = bool.Parse(data[idx++]);

                Tmp.Add(Tmp);
                _MapWithType.Add(Tmp._Type, Tmp);

            }
        }

        public static MapGimmick Get(EMapGimmickType type)
        {
            return _MapWithType[type];
        }
    }

    public class MapGimmickObstacle : CSVFile<MapGimmickObstacle>
    {
        public int _Removal;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MapGimmickObstacle");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MapGimmickObstacle : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MapGimmickObstacle Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Removal = int.Parse(data[idx++]);

                Tmp.Add(Tmp);

            }
        }
    }

    public class MapGimmickResource : CSVFile<MapGimmickResource>
    {
        public EResourceType _Type;
        public string _Korean;
        public float _Probability;
        public int _MiningMoney;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MapGimmickResource");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MapGimmickResource : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MapGimmickResource Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Type = Enum.Parse<EResourceType>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._Probability = float.Parse(data[idx++]);
                Tmp._MiningMoney = int.Parse(data[idx++]);

                Tmp.Add(Tmp);

            }
        }
    }

    public class GlobalSystem : CSVFile<GlobalSystem>
    {
        public string _Code;
        public int _Value;
        public string _Description;

        static Dictionary<string, GlobalSystem> _StringMap = new();

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/GlobalSystem");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("GlobalSystem : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                GlobalSystem Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Code = data[idx++];
                Tmp._Value = int.Parse(data[idx++]);
                Tmp._Description = data[idx++];

                Tmp.Add(Tmp);
                _StringMap.Add(Tmp._Code, Tmp);
            }
        }

        public static GlobalSystem Get(string code)
        {
            return _StringMap[code];
        }
    }

    public class Monster : CSVFile<Monster>
    {
        public EMonsterType _Type;
        public string _Korean;
        public int _Hp;
        public int _Sheild;
        public int _Armor;
        public int _Speed;
        public int _None;
        public int _Water;
        public int _Ground;
        public int _Fire;
        public int _Electric;
        public int _Proper1_1;
        public int _Proper2_1;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/Monster");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("Monster : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                Monster Tmp = new();

                int idx = 0;

                Tmp._Type = Enum.Parse<EMonsterType>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._Hp = int.Parse(data[idx++]);
                Tmp._Sheild = int.Parse(data[idx++]);
                Tmp._Armor = int.Parse(data[idx++]);
                Tmp._Speed = int.Parse(data[idx++]);
                Tmp._None = int.Parse(data[idx++]);
                Tmp._Water = int.Parse(data[idx++]);
                Tmp._Ground = int.Parse(data[idx++]);
                Tmp._Fire = int.Parse(data[idx++]);
                Tmp._Electric = int.Parse(data[idx++]);
                Tmp._Proper1_1 = int.Parse(data[idx++]);
                Tmp._Proper2_1 = int.Parse(data[idx++]);

                Tmp.Add(Tmp);
            }
        }
    }
}
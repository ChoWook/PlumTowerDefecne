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
            Monster.Load();
            MonsterClass.Load();
            MonsterLevel.Load();
            //MonsterProperty.Load();
            MonsterSpeciality.Load();
            Tower.Load();

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
            if (_map.ContainsKey(_ID))
            {
                Debug.LogWarning($"Key {_ID} is already contained");
            }
            else
            {
                _map.Add(_ID, Sender);
            }
        }

        public static _T Get(int key)
        {
            _T ret;
            _map.TryGetValue(key, out ret);

            if(ret == null)
            {
                Debug.Log($"Key {key} is not Contained.");
                Debug.Log($"Map.Count = {_map.Count}");
            }
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

                for (; idx < data.Length; idx++)
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
                dataLines[i] = dataLines[i].Replace("\"", string.Empty).Replace("\r", string.Empty);
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
        public float _None;
        public float _Water;
        public float _Ground;
        public float _Fire;
        public float _Electric;
        public int _Speciality_1;
        public int _Speciality_2;

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

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Type = Enum.Parse<EMonsterType>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._Hp = int.Parse(data[idx++]);
                Tmp._Sheild = int.Parse(data[idx++]);
                Tmp._Armor = int.Parse(data[idx++]);
                Tmp._Speed = int.Parse(data[idx++]);
                Tmp._None = float.Parse(data[idx++]);
                Tmp._Water = float.Parse(data[idx++]);
                Tmp._Ground = float.Parse(data[idx++]);
                Tmp._Fire = float.Parse(data[idx++]);
                Tmp._Electric = float.Parse(data[idx++]);
                Tmp._Speciality_1 = int.Parse(data[idx++]);
                Tmp._Speciality_2 = int.Parse(data[idx++]);

                //Debug.Log($"End ADD Monster {Tmp._ID}");

                Tmp.Add(Tmp);
            }
        }
    }

    public class MonsterClass : CSVFile<MonsterClass>
    {
        public EMonsterClass _Type;
        public string _Korean;
        public float _Hp;
        public float _Sheild;
        public float _Size;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MonsterClass");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MonsterClass : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MonsterClass Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Type = Enum.Parse<EMonsterClass>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._Hp = float.Parse(data[idx++]);
                Tmp._Sheild = float.Parse(data[idx++]);
                Tmp._Size = float.Parse(data[idx++]);

                Tmp.Add(Tmp);
            }
        }
    }

    public class MonsterLevel : CSVFile<MonsterLevel>
    {
        public float _Hp;
        public float _Sheild;
        public float _Armor;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MonsterLevel");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MonsterLevel : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MonsterLevel Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Hp = float.Parse(data[idx++]);
                Tmp._Sheild = float.Parse(data[idx++]);
                Tmp._Armor = float.Parse(data[idx++]);

                Tmp.Add(Tmp);
            }
        }
    }

    public class MonsterProperty : CSVFile<MonsterProperty>
    {
        public EPropertyType _PropertyType;
        public string _Korean;
        public int _Class;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MonsterProperty");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MonsterProperty : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MonsterProperty Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._PropertyType = Enum.Parse<EPropertyType>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._Class = int.Parse(data[idx++]);

                Tmp.Add(Tmp);
            }
        }
    }

    public class MonsterSpeciality : CSVFile<MonsterSpeciality>
    {
        public ESpecialityType _SpecialityType;
        public string _Korean;
        public EStat _ChangeStat;
        public float _Amount;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/MonsterSpeciality");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("MonsterSpeciality : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                MonsterSpeciality Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._SpecialityType = Enum.Parse<ESpecialityType>(data[idx++]);
                Tmp._Korean = data[idx++];
                Tmp._ChangeStat = Enum.Parse<EStat>(data[idx++]);
                Tmp._Amount = float.Parse(data[idx++]);

                Tmp.Add(Tmp);
            }
        }
    }

    public class Tower : CSVFile<Tower>
    {
        public string _Name;
        public EAttackSepcialization _AttackSepcialization;
        public ETowerType _Type;
        public int _Size;
        public float _Attack;
        public float _Speed;
        public float _ProjectileSpeed;
        public float _Ability;
        public EUpgradeStat _UpgradeStat;
        public float _UpgradeAmount;
        public int _UpgradePrice;
        public float _Range;
        public int _Price;

        public static void Load()
        {
            TextAsset dataset = Resources.Load<TextAsset>(@"CSVs/Tower");
            string[] dataLines = dataset.text.Split("\n");

            Debug.Log("Tower : " + dataLines.Length);

            for (int i = 2; i < dataLines.Length; i++)
            {
                dataLines[i].Trim();
                var data = dataLines[i].Split(',');

                if (string.IsNullOrEmpty(data[0]))
                {
                    break;
                }

                Tower Tmp = new();

                int idx = 0;

                Tmp._ID = int.Parse(data[idx++]);
                Tmp._Name = data[idx++];
                Tmp._AttackSepcialization = Enum.Parse<EAttackSepcialization>(data[idx++]);
                Tmp._Type = Enum.Parse<ETowerType>(data[idx++]);
                Tmp._Size = int.Parse(data[idx++]);
                Tmp._Attack = float.Parse(data[idx++]);
                Tmp._Speed = float.Parse(data[idx++]);
                Tmp._ProjectileSpeed = float.Parse(data[idx++]);
                Tmp._Ability = float.Parse(data[idx++]);
                Tmp._UpgradeStat = Enum.Parse<EUpgradeStat>(data[idx++]);
                Tmp._UpgradeAmount = float.Parse(data[idx++]);
                Tmp._UpgradePrice = int.Parse(data[idx++]);
                Tmp._Range = float.Parse(data[idx++]);
                Tmp._Price = int.Parse(data[idx++]);

                Tmp.Add(Tmp);
            }
        }
    }
}
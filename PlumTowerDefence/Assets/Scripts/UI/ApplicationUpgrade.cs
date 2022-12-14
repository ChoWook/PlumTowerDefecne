using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class ApplicationUpgrade : MonoBehaviour
{
    public static ApplicationUpgrade instance = null;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ApplicationPassiveUpgrade(int id)
    {
        if (id / 100 == 301)    //HP증가
        {
            GameManager.instance.MaxHp += (int)Tables.UpgradePassiveStat.Get(30101)._Increase_Passive_Value;
        }

        if (id / 100 == 302)    //증강체 갯수 증가
        {
            GameManager.instance.NumberOfUpgrade += (int)Tables.UpgradePassiveStat.Get(30201)._Increase_Passive_Value;
        }

        if (id / 100 == 303)    //장애물제거 비용 감소
        {
            GameManager.instance.DiscountObstacle += Tables.UpgradePassiveStat.Get(30301)._Increase_Passive_Value;
        }

        if (id / 100 == 304)    //타워 출현 확률 증가
        {
            GameManager.instance.IncreaseTowerCoupon += Tables.UpgradePassiveStat.Get(30401)._Increase_Passive_Value;
        }
    }

    public void ApplicationInGameUpgrade(int id)
    {
        switch (id)
        {
            #region ArrowTower

            case 10102:     //독화살 I
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonAttackMultiModifier.Add(0.25f);
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonDurationPlusModifier.Add(5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10106))
                    JsonManager.instance.usingList.Add(10106);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10107))
                    JsonManager.instance.usingList.Add(10107);
                break;
            
            case 10103:     //예리한 화살 I
                TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier.Add(5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10108))
                    JsonManager.instance.usingList.Add(10108);
                break;
            
            case 10104:     //날랜 화살 I
                TowerUpgradeAmount.instance._ArrowTowerStat.SpeedPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10109))
                    JsonManager.instance.usingList.Add(10109);
                break;
            
            case 10105:     //불화살 I
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnAttackMultiModifier.Add(0.25f);
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnDurationPlusModifier.Add(5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10110))
                    JsonManager.instance.usingList.Add(10110);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10111))
                    JsonManager.instance.usingList.Add(10111);
                break;
            
            case 10106:     //맹독 화살 I
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonDurationPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10112))
                    JsonManager.instance.usingList.Add(10112);
                break;
            
            case 10107:     //독화살 II
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonAttackMultiModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10113))
                    JsonManager.instance.usingList.Add(10113);
                break;
            
            case 10108:     //예리한 화살 II
                TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier.Add(5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10114))
                    JsonManager.instance.usingList.Add(10114);
                break;
            
            case 10109:     //날랜 화살 II
                TowerUpgradeAmount.instance._ArrowTowerStat.SpeedPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10115))
                    JsonManager.instance.usingList.Add(10115);
                break;
            
            case 10110:     //불화살 II
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnAttackMultiModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10116))
                    JsonManager.instance.usingList.Add(10116);
                break;
            
            case 10111:     //맹렬한 화살 I
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnDurationPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10117))
                    JsonManager.instance.usingList.Add(10117);
                break;
            
            case 10112:     //맹독 화살 II
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonDurationPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10118))
                    JsonManager.instance.usingList.Add(10118);
                break;
            
            case 10113:     //독화살 III
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonAttackMultiModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10119))
                    JsonManager.instance.usingList.Add(10119);
                break;
            
            case 10114:     //예리한 화살 III
                TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier.Add(10f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10120))
                    JsonManager.instance.usingList.Add(10120);
                break;
            
            case 10115:     //날랜 화살 III
                TowerUpgradeAmount.instance._ArrowTowerStat.SpeedPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10121))
                    JsonManager.instance.usingList.Add(10121);
                break;
            
            case 10116:     //불화살 III
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnAttackMultiModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10122))
                    JsonManager.instance.usingList.Add(10122);
                break;
            
            case 10117:     //맹렬한 화살 II
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnDurationPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10123))
                    JsonManager.instance.usingList.Add(10123);
                break;
            
            case 10118:     //맹독 화살 III
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonDurationPlusModifier.Add(3f);
                break;
            
            case 10119:     //독화살 IV
                TowerUpgradeAmount.instance._ArrowTowerStat.PoisonAttackMultiModifier.Add(1.25f);
                break;
            
            case 10120:     //예리한 화살 IV
                TowerUpgradeAmount.instance._ArrowTowerStat.AttackPlusModifier.Add(80);
                break;
            
            case 10121:     //날랜 화살 IV
                TowerUpgradeAmount.instance._ArrowTowerStat.SpeedPlusModifier.Add(7f);
                break;
            
            case 10122:     //불화살 IV
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnAttackMultiModifier.Add(1.25f);
                break;
            
            case 10123:     //맹렬한 화살 III
                TowerUpgradeAmount.instance._ArrowTowerStat.BurnDurationPlusModifier.Add(3f);
                break;

            #endregion

            #region HourglassTower

            case 10202:     //감속의 모래 I
                TowerUpgradeAmount.instance._HourglassTowerStat.AbilityMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10203))
                    JsonManager.instance.usingList.Add(10203);
                break;
            
            case 10203:     //감속의 모래 II
                TowerUpgradeAmount.instance._HourglassTowerStat.AbilityMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10204))
                    JsonManager.instance.usingList.Add(10204);
                break;
            
            case 10204:     //감속의 모래 III
                TowerUpgradeAmount.instance._HourglassTowerStat.AbilityMultiModifier.Add(0.1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10205))
                    JsonManager.instance.usingList.Add(10205);
                break;
            
            case 10205:     //감속의 모래 IV
                TowerUpgradeAmount.instance._HourglassTowerStat.AbilityMultiModifier.Add(0.3f);
                break;

            #endregion

            #region PoisonTower

            case 10302:     //유독성 구름 I
                TowerUpgradeAmount.instance._PoisonTowerStat.RangePlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10305))
                    JsonManager.instance.usingList.Add(10305);
                break;
            
            case 10303:     //맹독 폭발 I
                TowerUpgradeAmount.instance._PoisonTowerStat.AbilityPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10306))
                    JsonManager.instance.usingList.Add(10306);
                break;
            
            case 10304:     //포이즌 브레스 I
                TowerUpgradeAmount.instance._PoisonTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10307))
                    JsonManager.instance.usingList.Add(10307);
                break;
            
            case 10305:     //유독성 구름 II
                TowerUpgradeAmount.instance._PoisonTowerStat.RangePlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10308))
                    JsonManager.instance.usingList.Add(10308);
                break;
            
            case 10306:     //맹독 폭발 II
                TowerUpgradeAmount.instance._PoisonTowerStat.AbilityPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10309))
                    JsonManager.instance.usingList.Add(10309);
                break;
            
            case 10307:     //포이즌 브레스 II
                TowerUpgradeAmount.instance._PoisonTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10310))
                    JsonManager.instance.usingList.Add(10310);
                break;
            
            case 10308:     //유독성 구름 III
                TowerUpgradeAmount.instance._PoisonTowerStat.RangePlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10311))
                    JsonManager.instance.usingList.Add(10311);
                break;
            
            case 10309:     //맹독 폭발 III
                TowerUpgradeAmount.instance._PoisonTowerStat.AbilityPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10312))
                    JsonManager.instance.usingList.Add(10312);
                break;
            
            case 10310:     //포이즌 브레스 III
                TowerUpgradeAmount.instance._PoisonTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10313))
                    JsonManager.instance.usingList.Add(10313);
                break;
            
            case 10311:     //유독성 구름 IV
                TowerUpgradeAmount.instance._PoisonTowerStat.RangePlusModifier.Add(6f);
                break;
            
            case 10312:     //맹독 폭발 IV
                TowerUpgradeAmount.instance._PoisonTowerStat.AbilityPlusModifier.Add(7f);
                break;
            
            case 10313:     //포이즌 브레스 IV
                TowerUpgradeAmount.instance._PoisonTowerStat.SlowMultiModifier.Add(0.15f);
                TowerUpgradeAmount.instance._PoisonTowerStat.AbilityPlusModifier.Add(5f);
                break;

            #endregion

            #region FlameTower

            case 10402:     //뜨거운 열기 I
                TowerUpgradeAmount.instance._FlameTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10405))
                    JsonManager.instance.usingList.Add(10405);
                break;
            
            case 10403:     //소각 I
                TowerUpgradeAmount.instance._FlameTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10406))
                    JsonManager.instance.usingList.Add(10406);
                break;
            
            case 10404:     //발화 I
                TowerUpgradeAmount.instance._FlameTowerStat.RangePlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10407))
                    JsonManager.instance.usingList.Add(10407);
                break;
            
            case 10405:     //뜨거운 열기 II
                TowerUpgradeAmount.instance._FlameTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10408))
                    JsonManager.instance.usingList.Add(10408);
                break;
            
            case 10406:     //소각 II
                TowerUpgradeAmount.instance._FlameTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10409))
                    JsonManager.instance.usingList.Add(10409);
                break;
            
            case 10407:     //발화 II
                TowerUpgradeAmount.instance._FlameTowerStat.RangePlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10410))
                    JsonManager.instance.usingList.Add(10410);
                break;
            
            case 10408:     //뜨거운 열기 III
                TowerUpgradeAmount.instance._FlameTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10411))
                    JsonManager.instance.usingList.Add(10411);
                break;
            
            case 10409:     //소각 III
                TowerUpgradeAmount.instance._FlameTowerStat.AttackPlusModifier.Add(75f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10412))
                    JsonManager.instance.usingList.Add(10412);
                break;
            
            case 10410:     //발화 III
                TowerUpgradeAmount.instance._FlameTowerStat.RangePlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10413))
                    JsonManager.instance.usingList.Add(10413);
                break;
            
            case 10411:     //플레임 헤이즈
                TowerUpgradeAmount.instance._FlameTowerStat.SlowMultiModifier.Add(0.15f);
                TowerUpgradeAmount.instance._FlameTowerStat.SpeedPlusModifier.Add(2f);
                break;
            
            case 10412:     //붕괴
                TowerUpgradeAmount.instance._FlameTowerStat.AngleplusModifier.Add(60f);
                TowerUpgradeAmount.instance._FlameTowerStat.AttackPlusModifier.Add(75f);
                break;
            
            case 10413:     //이퀄라이저
                TowerUpgradeAmount.instance._FlameTowerStat.RangePlusModifier.Add(1f);
                TowerUpgradeAmount.instance._FlameTowerStat.SpeedPlusModifier.Add(2f);
                break;

            #endregion

            #region AttackBuffTower

            case 10502:     //공격력버프타워의 결의 I
                TowerUpgradeAmount.instance._AttackBuffTowerStat.RangePlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10504)) 
                    JsonManager.instance.usingList.Add(10504);
                break;
            
            case 10503:     //공격력버프타워의 열정 I
                TowerUpgradeAmount.instance._AttackBuffTowerStat.AbilityPlusModifier.Add(10f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10505))
                    JsonManager.instance.usingList.Add(10505);
                break;
            
            case 10504:     //공격력버프타워의 결의 II
                TowerUpgradeAmount.instance._AttackBuffTowerStat.RangePlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10506))
                    JsonManager.instance.usingList.Add(10506);
                break;
            
            case 10505:     //공격력버프타워의 열정 II
                TowerUpgradeAmount.instance._AttackBuffTowerStat.AbilityPlusModifier.Add(10f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10507))
                    JsonManager.instance.usingList.Add(10507);
                break;
            
            case 10506:     //공격력버프타워의 결의 III
                TowerUpgradeAmount.instance._AttackBuffTowerStat.RangePlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10508))
                    JsonManager.instance.usingList.Add(10508);
                break;
            
            case 10507:     //공격력버프타워의 열정 III
                TowerUpgradeAmount.instance._AttackBuffTowerStat.AbilityPlusModifier.Add(10f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10509))
                    JsonManager.instance.usingList.Add(10509);
                break;
            
            case 10508:     //공격력버프타워의 결의 IV
                TowerUpgradeAmount.instance._AttackBuffTowerStat.RangePlusModifier.Add(3f);
                TowerUpgradeAmount.instance._AttackBuffTowerStat.AbilityPlusModifier.Add(30f);
                break;
            
            case 10509:     //공격력버프타워의 열정 IV
                TowerUpgradeAmount.instance._AttackBuffTowerStat.AbilityPlusModifier.Add(20f);
                TowerUpgradeAmount.instance._AttackBuffTowerStat.RangePlusModifier.Add(2f);
                break;

            #endregion

            #region SpeedBuffTower

            case 10602:     //공격속도버프타워의 결의 I
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10604))
                    JsonManager.instance.usingList.Add(10604);
                break;
            
            case 10603:     //공격속도버프타워의 열정 I
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.AbilityPlusModifier.Add(0.1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10605))
                    JsonManager.instance.usingList.Add(10605);
                break;
            
            case 10604:     //공격속도버프타워의 결의 II
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10606))
                    JsonManager.instance.usingList.Add(10606);
                break;
            
            case 10605:     //공격속도버프타워의 열정 II
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.AbilityPlusModifier.Add(0.1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10607))
                    JsonManager.instance.usingList.Add(10607);
                break;
            
            case 10606:     //공격속도버프타워의 결의 III
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10608))
                    JsonManager.instance.usingList.Add(10608);
                break;
            
            case 10607:     //공격속도버프타워의 열정 III
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.AbilityPlusModifier.Add(0.3f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10609))
                    JsonManager.instance.usingList.Add(10609);
                break;
            
            case 10608:     //공격속도버프타워의 결의 IV
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier.Add(3f);
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.AbilityPlusModifier.Add(0.5f);
                break;
            
            case 10609:     //공격속도버프타워의 열정 IV
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.AbilityPlusModifier.Add(0.5f);
                TowerUpgradeAmount.instance._SpeedBuffTowerStat.RangePlusModifier.Add(2f);
                break;

            #endregion

            #region LaserTower

            case 10702:     //빠른 광선 I
                TowerUpgradeAmount.instance._LaserTowerStat.SpeedPlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10704))
                    JsonManager.instance.usingList.Add(10704);
                break;
            
            case 10703:     //죽음의 광선 I
                TowerUpgradeAmount.instance._LaserTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10705))
                    JsonManager.instance.usingList.Add(10705);
                break;
            
            case 10704:     //빠른 광선 II
                TowerUpgradeAmount.instance._LaserTowerStat.SpeedPlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10706))
                    JsonManager.instance.usingList.Add(10706);
                break;
            
            case 10705:     //죽음의 광선 II
                TowerUpgradeAmount.instance._LaserTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10707))
                    JsonManager.instance.usingList.Add(10707);
                break;
            
            case 10706:     //빠른 광선 III
                TowerUpgradeAmount.instance._LaserTowerStat.SpeedPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10708))
                    JsonManager.instance.usingList.Add(10708);
                break;
            
            case 10707:     //죽음의 광선 III
                TowerUpgradeAmount.instance._LaserTowerStat.AttackPlusModifier.Add(50f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10709))
                    JsonManager.instance.usingList.Add(10709);
                break;
            
            case 10708:     //빠른 광선 IV
                TowerUpgradeAmount.instance._LaserTowerStat.SpeedPlusModifier.Add(2f);
                break;
            
            case 10709:     //죽음의 광선 IV
                TowerUpgradeAmount.instance._LaserTowerStat.AttackPlusModifier.Add(100f);
                break;

            #endregion

            #region MissileTower

            case 10802:     //양자 폭탄 I
                TowerUpgradeAmount.instance._MissileTowerStat.AttackPlusModifier.Add(100f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10804))
                    JsonManager.instance.usingList.Add(10804);
                break;
            
            case 10803:     //미사일 개조 I
                TowerUpgradeAmount.instance._MissileTowerStat.SpeedPlusModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10805))
                    JsonManager.instance.usingList.Add(10805);
                break;
            
            case 10804:     //양자 폭탄 II
                TowerUpgradeAmount.instance._MissileTowerStat.AttackPlusModifier.Add(100f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10806))
                    JsonManager.instance.usingList.Add(10806);
                break;
            
            case 10805:     //미사일 개조 II
                TowerUpgradeAmount.instance._MissileTowerStat.SpeedPlusModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10807))
                    JsonManager.instance.usingList.Add(10807);
                break;
            
            case 10806:     //양자 폭탄 III
                TowerUpgradeAmount.instance._MissileTowerStat.AttackPlusModifier.Add(300f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10808))
                    JsonManager.instance.usingList.Add(10808);
                break;
            
            case 10807:     //미사일 개조 III
                TowerUpgradeAmount.instance._MissileTowerStat.SpeedPlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10809))
                    JsonManager.instance.usingList.Add(10809);
                break;
            
            case 10808:     //플라즈마 폭탄
                TowerUpgradeAmount.instance._MissileTowerStat.AttackPlusModifier.Add(1500f);
                TowerUpgradeAmount.instance._MissileTowerStat.SpeedPlusModifier.Add(1f);
                break;
            
            case 10809:     //미사일 폭격
                TowerUpgradeAmount.instance._MissileTowerStat.SpeedPlusModifier.Add(2f);
                TowerUpgradeAmount.instance._MissileTowerStat.AttackPlusModifier.Add(500f);
                break;

            #endregion

            #region ElectricTower

            case 10902:     //전기장 I
                TowerUpgradeAmount.instance._ElectricTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10905))
                    JsonManager.instance.usingList.Add(10905);
                break;
            
            case 10903:     //에너지 볼트 I
                TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10906))
                    JsonManager.instance.usingList.Add(10906);
                break;
            
            case 10904:     //전기의 흐름 I
                TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10907))
                    JsonManager.instance.usingList.Add(10907);
                break;
            
            case 10905:     //전기장 II
                TowerUpgradeAmount.instance._ElectricTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10908))
                    JsonManager.instance.usingList.Add(10908);
                break;
                
            case 10906:     //에너지 볼트 II
                TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10909))
                    JsonManager.instance.usingList.Add(10909);
                break;
                
            case 10907:     //전기의 흐름 II
                TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10910))
                    JsonManager.instance.usingList.Add(10910);
                break;
                
            case 10908:     //전기장 III
                TowerUpgradeAmount.instance._ElectricTowerStat.SlowMultiModifier.Add(0.05f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10911))
                    JsonManager.instance.usingList.Add(10911);
                break;
                
            case 10909:     //에너지 볼트 III
                TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier.Add(25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10912))
                    JsonManager.instance.usingList.Add(10912);
                break;
                
            case 10910:     //전기의 흐름 III
                TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier.Add(2f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(10913))
                    JsonManager.instance.usingList.Add(10913);
                break;
                
            case 10911:     //마법 전기장
                TowerUpgradeAmount.instance._ElectricTowerStat.SlowMultiModifier.Add(0.15f);
                TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier.Add(75f);
                break;
                
            case 10912:     //체인 라이트닝
                TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier.Add(125f);
                TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier.Add(4f);
                break;
                
            case 10913:     //라이트닝 필드
                TowerUpgradeAmount.instance._ElectricTowerStat.AbilityPlusModifier.Add(4f);
                TowerUpgradeAmount.instance._ElectricTowerStat.AttackPlusModifier.Add(75f);
                break;

            #endregion

            #region Wall

            case 11002:     //단단한 벽 I
                TowerUpgradeAmount.instance._WallStat.HpPlusModifier.Add(200f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11003))
                    JsonManager.instance.usingList.Add(11003);
                break;
            
            case 11003:     //단단한 벽 II
                TowerUpgradeAmount.instance._WallStat.HpPlusModifier.Add(300f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11004))
                    JsonManager.instance.usingList.Add(11004);
                break;
            
            case 11004:     //단단한 벽 III
                TowerUpgradeAmount.instance._WallStat.HpPlusModifier.Add(1500f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11005))
                    JsonManager.instance.usingList.Add(11005);
                break;
            
            case 11005:     //단단한 벽 IV
                TowerUpgradeAmount.instance._WallStat.HpPlusModifier.Add(8000f);
                break;

            #endregion

            #region Bomb

            case 11102:     //폭탄 성능 향상 I
                TowerUpgradeAmount.instance._BombStat.AttackPlusModifier.Add(300f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11103))
                    JsonManager.instance.usingList.Add(11103);
                break;
            
            case 11103:     //폭탄 성능 향상 II
                TowerUpgradeAmount.instance._BombStat.AttackPlusModifier.Add(300f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11104))
                    JsonManager.instance.usingList.Add(11104);
                break;
            
            case 11104:     //폭탄 성능 향상 III
                TowerUpgradeAmount.instance._BombStat.AttackPlusModifier.Add(1400f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11105))
                    JsonManager.instance.usingList.Add(11105);
                break;
            
            case 11105:     //폭탄 성능 향상 IV
                TowerUpgradeAmount.instance._BombStat.AttackPlusModifier.Add(8000f);
                TowerUpgradeAmount.instance._BombStat.RangePlusModifier.Add(2f);
                break;

            #endregion

            #region GatlingTower

            case 11202:     //M-137 개틀링건 I
                TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier.Add(5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11204))
                    JsonManager.instance.usingList.Add(11204);
                break;
            
            case 11203:     //개틀링건 과부하 I
                TowerUpgradeAmount.instance._GatlingTowerStat.SpeedPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11205))
                    JsonManager.instance.usingList.Add(11205);
                break;
            
            case 11204:     //M-137 개틀링건 II
                TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier.Add(5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11206))
                    JsonManager.instance.usingList.Add(11206);
                break;
            
            case 11205:     //개틀링건 과부하 II
                TowerUpgradeAmount.instance._GatlingTowerStat.SpeedPlusModifier.Add(1f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11207))
                    JsonManager.instance.usingList.Add(11207);
                break;
            
            case 11206:     //M-137 개틀링건 III
                TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier.Add(10f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11208))
                    JsonManager.instance.usingList.Add(11208);
                break;
            
            case 11207:     //개틀링건 과부하 III
                TowerUpgradeAmount.instance._GatlingTowerStat.SpeedPlusModifier.Add(2f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11209))
                    JsonManager.instance.usingList.Add(11209);
                break;
            
            case 11208:     //MLDRS-95
                TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier.Add(30f);
                TowerUpgradeAmount.instance._GatlingTowerStat.SpeedPlusModifier.Add(4f);
                break;
                
            case 11209:     //개틀링건 과부하 IV
                TowerUpgradeAmount.instance._GatlingTowerStat.SpeedPlusModifier.Add(6f);
                TowerUpgradeAmount.instance._GatlingTowerStat.AttackPlusModifier.Add(20f);
                break;

            #endregion

            #region CannonTower

            case 11302:     //대포 성능 향상 I
                TowerUpgradeAmount.instance._CannonTowerStat.AttackPlusModifier.Add(50f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11304))
                    JsonManager.instance.usingList.Add(11304);
                break;
            
            case 11303:     //숙련된 대포 사용 I
                TowerUpgradeAmount.instance._CannonTowerStat.SpeedPlusModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11305))
                    JsonManager.instance.usingList.Add(11305);
                break;
            
            case 11304:     //대포 성능 향상 II
                TowerUpgradeAmount.instance._CannonTowerStat.AttackPlusModifier.Add(50f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11306))
                    JsonManager.instance.usingList.Add(11306);
                break;
            
            case 11305:     //숙련된 대포 사용 II
                TowerUpgradeAmount.instance._CannonTowerStat.SpeedPlusModifier.Add(0.25f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11307))
                    JsonManager.instance.usingList.Add(11307);
                break;
            
            case 11306:     //대포 성능 향상 III
                TowerUpgradeAmount.instance._CannonTowerStat.AttackPlusModifier.Add(100f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11308))
                    JsonManager.instance.usingList.Add(11308);
                break;
            
            case 11307:     //숙련된 대포 사용 III
                TowerUpgradeAmount.instance._CannonTowerStat.SpeedPlusModifier.Add(0.5f);
                if(JsonManager.instance.SaveData.upgradedCard.Contains(11309))
                    JsonManager.instance.usingList.Add(11309);
                break;
            
            case 11308:     //대포 성능 향상 IV
                TowerUpgradeAmount.instance._CannonTowerStat.AttackPlusModifier.Add(800f);
                TowerUpgradeAmount.instance._CannonTowerStat.SpeedPlusModifier.Add(1f);
                break;
            
            case 11309:     //숙련된 대포 사용 IV
                TowerUpgradeAmount.instance._CannonTowerStat.SpeedPlusModifier.Add(2f);
                TowerUpgradeAmount.instance._CannonTowerStat.AttackPlusModifier.Add(200f);
                break;

            #endregion

            #region Magnetite

            case 20101:     //자철석 채광 효율 I
                TowerUpgradeAmount.instance._MagnetiteStat.moneyRate = 0.2f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20102))
                    JsonManager.instance.usingList.Add(20102);
                break;
            
            case 20102:     //자철석 채광 효율 II
                TowerUpgradeAmount.instance._MagnetiteStat.moneyRate = 0.4f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20103))
                    JsonManager.instance.usingList.Add(20103);
                break;
            
            case 20103:     //자철석 채광 효율 III
                TowerUpgradeAmount.instance._MagnetiteStat.moneyRate = 0.6f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20104))
                    JsonManager.instance.usingList.Add(20104);
                break;
            
            case 20104:     //자철석 채광 효율 IV
                TowerUpgradeAmount.instance._MagnetiteStat.moneyRate = 0.8f;
                break;

            #endregion

            #region Crystal

            case 20201:     //수정 채광 효율 I
                TowerUpgradeAmount.instance._CrystalStat.moneyRate = 0.2f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20202))
                    JsonManager.instance.usingList.Add(20202);
                break;
            
            case 20202:     //수정 채광 효율 II
                TowerUpgradeAmount.instance._CrystalStat.moneyRate = 0.4f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20203))
                    JsonManager.instance.usingList.Add(20203);
                break;
            
            case 20203:     //수정 채광 효율 III
                TowerUpgradeAmount.instance._CrystalStat.moneyRate = 0.6f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20204))
                    JsonManager.instance.usingList.Add(20204);
                break;
            
            case 20204:
                TowerUpgradeAmount.instance._CrystalStat.moneyRate = 0.8f;
                break;

            #endregion

            #region Gold

            case 20301:
                TowerUpgradeAmount.instance._GoldStat.moneyRate = 0.2f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20302))
                    JsonManager.instance.usingList.Add(20302);
                break;
            
            case 20302:
                TowerUpgradeAmount.instance._GoldStat.moneyRate = 0.4f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20303))
                    JsonManager.instance.usingList.Add(20303);
                break;
            
            case 20303:
                TowerUpgradeAmount.instance._GoldStat.moneyRate = 0.6f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20304))
                    JsonManager.instance.usingList.Add(20304);
                break;
            
            case 20304:
                TowerUpgradeAmount.instance._GoldStat.moneyRate = 0.8f;
                break;
            
            #endregion

            #region Diamond

            case 20401:
                TowerUpgradeAmount.instance._DiamondStat.moneyRate = 0.2f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20402))
                    JsonManager.instance.usingList.Add(20402);
                break;
            
            case 20402:
                TowerUpgradeAmount.instance._DiamondStat.moneyRate = 0.4f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20403))
                    JsonManager.instance.usingList.Add(20403);
                break;
            
            case 20403:
                TowerUpgradeAmount.instance._DiamondStat.moneyRate = 0.6f;
                if(JsonManager.instance.SaveData.upgradedCard.Contains(20404))
                    JsonManager.instance.usingList.Add(20404);
                break;
            
            case 20404:
                TowerUpgradeAmount.instance._DiamondStat.moneyRate = 0.8f;
                break;

            #endregion
        }
    }
    public void ResetInGameUpgrade()
    {
        TowerUpgradeAmount.instance._ArrowTowerStat.ResetList();

        TowerUpgradeAmount.instance._HourglassTowerStat.ResetList();

        TowerUpgradeAmount.instance._PoisonTowerStat.ResetList();

        TowerUpgradeAmount.instance._FlameTowerStat.ResetList();

        TowerUpgradeAmount.instance._AttackBuffTowerStat.ResetList();

        TowerUpgradeAmount.instance._SpeedBuffTowerStat.ResetList();

        TowerUpgradeAmount.instance._LaserTowerStat.ResetList();

        TowerUpgradeAmount.instance._MissileTowerStat.ResetList();

        TowerUpgradeAmount.instance._ElectricTowerStat.ResetList();

        TowerUpgradeAmount.instance._WallStat.ResetList();

        TowerUpgradeAmount.instance._BombStat.ResetList();

        TowerUpgradeAmount.instance._GatlingTowerStat.ResetList();

        TowerUpgradeAmount.instance._CannonTowerStat.ResetList();

        TowerUpgradeAmount.instance._MagnetiteStat.moneyRate = 0f;
        
        TowerUpgradeAmount.instance._CrystalStat.moneyRate = 0f;

        TowerUpgradeAmount.instance._GoldStat.moneyRate = 0f;

        TowerUpgradeAmount.instance._DiamondStat.moneyRate = 0f;
    }
}

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
            GameManager.instance.maxHp += (int)Tables.UpgradePassiveStat.Get(30101)._Increase_Passive_Value;
        }

        if (id / 100 == 302)    //증강체 갯수 증가
        {
            GameManager.instance.number_Of_Upgrade += (int)Tables.UpgradePassiveStat.Get(30201)._Increase_Passive_Value;
        }

        if (id / 100 == 303)    //장애물제거 비용 감소
        {
            GameManager.instance.discountObstacle += Tables.UpgradePassiveStat.Get(30301)._Increase_Passive_Value;
        }

        if (id / 100 == 304)    //타워 출현 확률 증가
        {
            GameManager.instance.increaseTowerCoupon += Tables.UpgradePassiveStat.Get(30401)._Increase_Passive_Value;
        }
    }

    public void ApplicationInGameUpgrade(int id)
    {
        switch (id)
        {
            #region ArrowTower

            case 10102:     //독화살 I
                TowerUpgradeAmount.ArrowTowerStat.isPoison = true;
                TowerUpgradeAmount.ArrowTowerStat.poisonDamageRate = 0.25f;
                TowerUpgradeAmount.ArrowTowerStat.poisonDamageRate = 5f;
                JsonManager.instance.usingList.Add(10106);
                JsonManager.instance.usingList.Add(10107);
                break;
            
            case 10103:     //예리한 화살 I
                TowerUpgradeAmount.ArrowTowerStat.attackDamage = 5;
                JsonManager.instance.usingList.Add(10108);
                break;
            
            case 10104:     //날랜 화살 I
                TowerUpgradeAmount.ArrowTowerStat.attackSpeed = 1;
                JsonManager.instance.usingList.Add(10109);
                break;
            
            case 10105:     //불화살 I
                TowerUpgradeAmount.ArrowTowerStat.isBurn = true;
                TowerUpgradeAmount.ArrowTowerStat.burnDamageRate = 0.25f;
                TowerUpgradeAmount.ArrowTowerStat.burnDuration = 5f;
                JsonManager.instance.usingList.Add(10110);
                JsonManager.instance.usingList.Add(10111);
                break;
            
            case 10106:     //맹독 화살 I
                TowerUpgradeAmount.ArrowTowerStat.poisonDuration = 6f;
                JsonManager.instance.usingList.Add(10112);
                break;
            
            case 10107:     //독화살 II
                TowerUpgradeAmount.ArrowTowerStat.poisonDamageRate = 0.5f;
                JsonManager.instance.usingList.Add(10113);
                break;
            
            case 10108:     //예리한 화살 II
                TowerUpgradeAmount.ArrowTowerStat.attackDamage = 10;
                JsonManager.instance.usingList.Add(10114);
                break;
            
            case 10109:     //날랜 화살 II
                TowerUpgradeAmount.ArrowTowerStat.attackSpeed = 2;
                JsonManager.instance.usingList.Add(10115);
                break;
            
            case 10110:     //불화살 II
                TowerUpgradeAmount.ArrowTowerStat.burnDamageRate = 0.5f;
                JsonManager.instance.usingList.Add(10116);
                break;
            
            case 10111:     //맹렬한 화살 I
                TowerUpgradeAmount.ArrowTowerStat.burnDuration = 6;
                JsonManager.instance.usingList.Add(10117);
                break;
            
            case 10112:     //맹독 화살 II
                TowerUpgradeAmount.ArrowTowerStat.poisonDuration = 7;
                JsonManager.instance.usingList.Add(10118);
                break;
            
            case 10113:     //독화살 III
                TowerUpgradeAmount.ArrowTowerStat.poisonDamageRate = 0.75f;
                JsonManager.instance.usingList.Add(10119);
                break;
            
            case 10114:     //예리한 화살 III
                TowerUpgradeAmount.ArrowTowerStat.attackDamage = 20;
                JsonManager.instance.usingList.Add(10120);
                break;
            
            case 10115:     //날랜 화살 III
                TowerUpgradeAmount.ArrowTowerStat.attackSpeed = 3;
                JsonManager.instance.usingList.Add(10121);
                break;
            
            case 10116:     //불화살 III
                TowerUpgradeAmount.ArrowTowerStat.burnDamageRate = 0.75f;
                JsonManager.instance.usingList.Add(10122);
                break;
            
            case 10117:     //맹렬한 화살 II
                TowerUpgradeAmount.ArrowTowerStat.burnDuration = 7;
                JsonManager.instance.usingList.Add(10123);
                break;
            
            case 10118:     //맹독 화살 III
                TowerUpgradeAmount.ArrowTowerStat.poisonDuration = 10;
                break;
            
            case 10119:     //독화살 IV
                TowerUpgradeAmount.ArrowTowerStat.poisonDamageRate = 2;
                break;
            
            case 10120:     //예리한 화살 IV
                TowerUpgradeAmount.ArrowTowerStat.attackDamage = 100;
                break;
            
            case 10121:     //날랜 화살 IV
                TowerUpgradeAmount.ArrowTowerStat.attackSpeed = 10;
                break;
            
            case 10122:     //불화살 IV
                TowerUpgradeAmount.ArrowTowerStat.burnDamageRate = 2;
                break;
            
            case 10123:     //맹렬한 화살 III
                TowerUpgradeAmount.ArrowTowerStat.burnDuration = 10;
                break;

            #endregion

            #region HourglassTower

            case 10202:     //감속의 모래 I
                TowerUpgradeAmount.HourglassTowerStat.slowRate = 0.05f;
                JsonManager.instance.usingList.Add(10203);
                break;
            
            case 10203:     //감속의 모래 II
                TowerUpgradeAmount.HourglassTowerStat.slowRate = 0.1f;
                JsonManager.instance.usingList.Add(10204);
                break;
            
            case 10204:     //감속의 모래 III
                TowerUpgradeAmount.HourglassTowerStat.slowRate = 0.2f;
                JsonManager.instance.usingList.Add(10205);
                break;
            
            case 10205:     //감속의 모래 IV
                TowerUpgradeAmount.HourglassTowerStat.slowRate = 0.5f;
                break;

            #endregion

            #region PoisonTower

            case 10302:     //유독성 구름 I
                TowerUpgradeAmount.PoisonTowerStat.attackRange = 1f;
                JsonManager.instance.usingList.Add(10305);
                break;
            
            case 10303:     //맹독 폭발 I
                TowerUpgradeAmount.PoisonTowerStat.abilityDamage = 1f;
                JsonManager.instance.usingList.Add(10306);
                break;
            
            case 10304:     //포이즌 브레스 I
                TowerUpgradeAmount.PoisonTowerStat.slowRate = 0.05f;
                JsonManager.instance.usingList.Add(10307);
                break;
            
            case 10305:     //유독성 구름 II
                TowerUpgradeAmount.PoisonTowerStat.attackRange = 2f;
                JsonManager.instance.usingList.Add(10308);
                break;
            
            case 10306:     //맹독 폭발 II
                TowerUpgradeAmount.PoisonTowerStat.abilityDamage = 2f;
                JsonManager.instance.usingList.Add(10309);
                break;
            
            case 10307:     //포이즌 브레스 II
                TowerUpgradeAmount.PoisonTowerStat.slowRate = 0.1f;
                JsonManager.instance.usingList.Add(10310);
                break;
            
            case 10308:     //유독성 구름 III
                TowerUpgradeAmount.PoisonTowerStat.attackRange = 3f;
                JsonManager.instance.usingList.Add(10311);
                break;
            
            case 10309:     //맹독 폭발 III
                TowerUpgradeAmount.PoisonTowerStat.abilityDamage = 3f;
                JsonManager.instance.usingList.Add(10312);
                break;
            
            case 10310:     //포이즌 브레스 III
                TowerUpgradeAmount.PoisonTowerStat.slowRate = 0.15f;
                JsonManager.instance.usingList.Add(10313);
                break;
            
            case 10311:     //유독성 구름 IV
                TowerUpgradeAmount.PoisonTowerStat.attackRange = 10f;
                break;
            
            case 10312:     //맹독 폭발 IV
                TowerUpgradeAmount.PoisonTowerStat.abilityDamage = 10f;
                break;
            
            case 10313:     //포이즌 브레스 IV
                TowerUpgradeAmount.PoisonTowerStat.slowRate = 0.3f;
                TowerUpgradeAmount.PoisonTowerStat.additionalAbilityDamage += 5f;
                break;

            #endregion

            #region FlameTower

            case 10402:     //뜨거운 열기 I
                TowerUpgradeAmount.FlameTowerStat.slowRate = 0.05f;
                JsonManager.instance.usingList.Add(10405);
                break;
            
            case 10403:     //소각 I
                TowerUpgradeAmount.FlameTowerStat.attackDamage = 25f;
                JsonManager.instance.usingList.Add(10406);
                break;
            
            case 10404:     //발화 I
                TowerUpgradeAmount.FlameTowerStat.attackRange = 0.5f;
                JsonManager.instance.usingList.Add(10407);
                break;
            
            case 10405:     //뜨거운 열기 II
                TowerUpgradeAmount.FlameTowerStat.slowRate = 0.1f;
                JsonManager.instance.usingList.Add(10408);
                break;
            
            case 10406:     //소각 II
                TowerUpgradeAmount.FlameTowerStat.attackDamage = 50f;
                JsonManager.instance.usingList.Add(10409);
                break;
            
            case 10407:     //발화 II
                TowerUpgradeAmount.FlameTowerStat.attackRange = 1f;
                JsonManager.instance.usingList.Add(10410);
                break;
            
            case 10408:     //뜨거운 열기 III
                TowerUpgradeAmount.FlameTowerStat.slowRate = 0.15f;
                JsonManager.instance.usingList.Add(10411);
                break;
            
            case 10409:     //소각 III
                TowerUpgradeAmount.FlameTowerStat.attackDamage = 75f;
                JsonManager.instance.usingList.Add(10412);
                break;
            
            case 10410:     //발화 III
                TowerUpgradeAmount.FlameTowerStat.attackRange = 2f;
                JsonManager.instance.usingList.Add(10413);
                break;
            
            case 10411:     //플레임 헤이즈
                TowerUpgradeAmount.FlameTowerStat.slowRate = 0.3f;
                TowerUpgradeAmount.FlameTowerStat.additionalAttackSpeed += 2f;
                break;
            
            case 10412:     //붕괴
                TowerUpgradeAmount.FlameTowerStat.centerAngle = 120f;
                TowerUpgradeAmount.FlameTowerStat.attackDamage = 150f;
                break;
            
            case 10413:     //이퀄라이저
                TowerUpgradeAmount.FlameTowerStat.attackRange = 3f;
                TowerUpgradeAmount.FlameTowerStat.additionalAttackSpeed += 2f;
                break;

            #endregion

            #region AttackBuffTower

            case 10502:     //공격력버프타워의 결의 I
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 0.5f;
                JsonManager.instance.usingList.Add(10504);
                break;
            
            case 10503:     //공격력버프타워의 열정 I
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 10f;
                JsonManager.instance.usingList.Add(10505);
                break;
            
            case 10504:     //공격력버프타워의 결의 II
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 1f;
                JsonManager.instance.usingList.Add(10506);
                break;
            
            case 10505:     //공격력버프타워의 열정 II
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 20f;
                JsonManager.instance.usingList.Add(10507);
                break;
            
            case 10506:     //공격력버프타워의 결의 III
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 2f;
                JsonManager.instance.usingList.Add(10508);
                break;
            
            case 10507:     //공격력버프타워의 열정 III
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 30f;
                JsonManager.instance.usingList.Add(10509);
                break;
            
            case 10508:     //공격력버프타워의 결의 IV
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 5f;
                TowerUpgradeAmount.AttackBuffTowerStat.additionalAbilityDamage += 30f;
                break;
            
            case 10509:     //공격력버프타워의 열정 IV
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 50f;
                TowerUpgradeAmount.AttackBuffTowerStat.additionalAttackRange += 2f;
                break;

            #endregion

            #region SpeedBuffTower

            case 10602:     //공격속도버프타워의 결의 I
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 0.5f;
                JsonManager.instance.usingList.Add(10604);
                break;
            
            case 10603:     //공격속도버프타워의 열정 I
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 0.1f;
                JsonManager.instance.usingList.Add(10605);
                break;
            
            case 10604:     //공격속도버프타워의 결의 II
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 1f;
                JsonManager.instance.usingList.Add(10606);
                break;
            
            case 10605:     //공격속도버프타워의 열정 II
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 0.2f;
                JsonManager.instance.usingList.Add(10607);
                break;
            
            case 10606:     //공격속도버프타워의 결의 III
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 2f;
                JsonManager.instance.usingList.Add(10608);
                break;
            
            case 10607:     //공격속도버프타워의 열정 III
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 0.5f;
                JsonManager.instance.usingList.Add(10609);
                break;
            
            case 10608:     //공격속도버프타워의 결의 IV
                TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 5f;
                TowerUpgradeAmount.AttackBuffTowerStat.additionalAbilityDamage += 0.5f;
                break;
            
            case 10609:     //공격속도버프타워의 열정 IV
                TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 1f;
                TowerUpgradeAmount.AttackBuffTowerStat.additionalAttackRange += 2f;
                break;

            #endregion

            #region LaserTower

            case 10702:     //빠른 광선 I
                TowerUpgradeAmount.LaserTowerStat.attackSpeed = 0.5f;
                JsonManager.instance.usingList.Add(10704);
                break;
            
            case 10703:     //죽음의 광선 I
                TowerUpgradeAmount.LaserTowerStat.attackDamage = 25f;
                JsonManager.instance.usingList.Add(10705);
                break;
            
            case 10704:     //빠른 광선 II
                TowerUpgradeAmount.LaserTowerStat.attackSpeed = 1f;
                JsonManager.instance.usingList.Add(10706);
                break;
            
            case 10705:     //죽음의 광선 II
                TowerUpgradeAmount.LaserTowerStat.attackDamage = 50f;
                JsonManager.instance.usingList.Add(10707);
                break;
            
            case 10706:     //빠른 광선 III
                TowerUpgradeAmount.LaserTowerStat.attackSpeed = 2f;
                JsonManager.instance.usingList.Add(10708);
                break;
            
            case 10707:     //죽음의 광선 III
                TowerUpgradeAmount.LaserTowerStat.attackDamage = 100f;
                JsonManager.instance.usingList.Add(10709);
                break;
            
            case 10708:     //빠른 광선 IV
                TowerUpgradeAmount.LaserTowerStat.attackSpeed = 4f;
                break;
            
            case 10709:     //죽음의 광선 IV
                TowerUpgradeAmount.LaserTowerStat.attackDamage = 200f;
                break;

            #endregion

            #region MissileTower

            case 10802:     //양자 폭탄 I
                TowerUpgradeAmount.MissileTowerStat.attackDamage = 100f;
                JsonManager.instance.usingList.Add(10804);
                break;
            
            case 10803:     //미사일 개조 I
                TowerUpgradeAmount.MissileTowerStat.attackSpeed = 0.25f;
                JsonManager.instance.usingList.Add(10805);
                break;
            
            case 10804:     //양자 폭탄 II
                TowerUpgradeAmount.MissileTowerStat.attackDamage = 200f;
                JsonManager.instance.usingList.Add(10806);
                break;
            
            case 10805:     //미사일 개조 II
                TowerUpgradeAmount.MissileTowerStat.attackSpeed = 0.5f;
                JsonManager.instance.usingList.Add(10807);
                break;
            
            case 10806:     //양자 폭탄 III
                TowerUpgradeAmount.MissileTowerStat.attackDamage = 500f;
                JsonManager.instance.usingList.Add(10808);
                break;
            
            case 10807:     //미사일 개조 III
                TowerUpgradeAmount.MissileTowerStat.attackSpeed = 1f;
                JsonManager.instance.usingList.Add(10809);
                break;
            
            case 10808:     //플라즈마 폭탄
                TowerUpgradeAmount.MissileTowerStat.attackDamage = 2000f;
                TowerUpgradeAmount.MissileTowerStat.additionalAttackSpeed = 1f;
                break;
            
            case 10809:     //미사일 폭격
                TowerUpgradeAmount.MissileTowerStat.attackSpeed = 3f;
                TowerUpgradeAmount.MissileTowerStat.additionalAttackDamage = 500f;
                break;

            #endregion

            #region ElectricTower

            case 10902:     //전기장 I
                TowerUpgradeAmount.ElectricTowerStat.slowRate = 0.05f;
                JsonManager.instance.usingList.Add(10905);
                break;
            
            case 10903:     //에너지 볼트 I
                TowerUpgradeAmount.ElectricTowerStat.attackDamage = 25f;
                JsonManager.instance.usingList.Add(10906);
                break;
            
            case 10904:     //전기의 흐름 I
                TowerUpgradeAmount.ElectricTowerStat.abilityDamage = 1f;
                JsonManager.instance.usingList.Add(10907);
                break;
            
            case 10905:     //전기장 II
                TowerUpgradeAmount.ElectricTowerStat.slowRate = 0.1f;
                JsonManager.instance.usingList.Add(10908);
                break;
                
            case 10906:     //에너지 볼트 II
                TowerUpgradeAmount.ElectricTowerStat.attackDamage = 50f;
                JsonManager.instance.usingList.Add(10909);
                break;
                
            case 10907:     //전기의 흐름 II
                TowerUpgradeAmount.ElectricTowerStat.abilityDamage = 2f;
                JsonManager.instance.usingList.Add(10910);
                break;
                
            case 10908:     //전기장 III
                TowerUpgradeAmount.ElectricTowerStat.slowRate = 0.15f;
                JsonManager.instance.usingList.Add(10911);
                break;
                
            case 10909:     //에너지 볼트 III
                TowerUpgradeAmount.ElectricTowerStat.attackDamage = 75f;
                JsonManager.instance.usingList.Add(10912);
                break;
                
            case 10910:     //전기의 흐름 III
                TowerUpgradeAmount.ElectricTowerStat.abilityDamage = 4f;
                JsonManager.instance.usingList.Add(10913);
                break;
                
            case 10911:     //마법 전기장
                TowerUpgradeAmount.ElectricTowerStat.slowRate = 0.3f;
                TowerUpgradeAmount.ElectricTowerStat.additionalAttackDmage += 75f;
                break;
                
            case 10912:     //체인 라이트닝
                TowerUpgradeAmount.ElectricTowerStat.attackDamage = 200f;
                TowerUpgradeAmount.ElectricTowerStat.additionalAbilityDamage += 4f;
                break;
                
            case 10913:     //라이트닝 필드
                TowerUpgradeAmount.ElectricTowerStat.abilityDamage = 8f;
                TowerUpgradeAmount.ElectricTowerStat.additionalAttackDmage += 75;
                break;

            #endregion

            #region Wall

            case 11002:     //단단한 벽 I
                TowerUpgradeAmount.WallStat.hp = 200f;
                JsonManager.instance.usingList.Add(11003);
                break;
            
            case 11003:     //단단한 벽 II
                TowerUpgradeAmount.WallStat.hp = 500f;
                JsonManager.instance.usingList.Add(11004);
                break;
            
            case 11004:     //단단한 벽 III
                TowerUpgradeAmount.WallStat.hp = 2000f;
                JsonManager.instance.usingList.Add(11005);
                break;
            
            case 11005:     //단단한 벽 IV
                TowerUpgradeAmount.WallStat.hp = 10000f;
                break;

            #endregion

            #region Bomb

            case 11102:     //폭탄 성능 향상 I
                TowerUpgradeAmount.BombStat.attackDamage = 300f;
                JsonManager.instance.usingList.Add(11103);
                break;
            
            case 11103:     //폭탄 성능 향상 II
                TowerUpgradeAmount.BombStat.attackDamage = 600f;
                JsonManager.instance.usingList.Add(11104);
                break;
            
            case 11104:     //폭탄 성능 향상 III
                TowerUpgradeAmount.BombStat.attackDamage = 2000f;
                JsonManager.instance.usingList.Add(11105);
                break;
            
            case 11105:     //폭탄 성능 향상 IV
                TowerUpgradeAmount.BombStat.attackDamage = 10000f;
                TowerUpgradeAmount.BombStat.range = 5;
                break;

            #endregion

            #region GatlingTower

            case 11202:     //M-137 개틀링건 I
                TowerUpgradeAmount.GatlingTowerStat.attackDamage = 5f;
                JsonManager.instance.usingList.Add(11204);
                break;
            
            case 11203:     //개틀링건 과부하 I
                TowerUpgradeAmount.GatlingTowerStat.attackSpeed = 1f;
                JsonManager.instance.usingList.Add(11205);
                break;
            
            case 11204:     //M-137 개틀링건 II
                TowerUpgradeAmount.GatlingTowerStat.attackDamage = 10f;
                JsonManager.instance.usingList.Add(11206);
                break;
            
            case 11205:     //개틀링건 과부하 II
                TowerUpgradeAmount.GatlingTowerStat.attackSpeed = 2f;
                JsonManager.instance.usingList.Add(11207);
                break;
            
            case 11206:     //M-137 개틀링건 III
                TowerUpgradeAmount.GatlingTowerStat.attackDamage = 20f;
                JsonManager.instance.usingList.Add(11208);
                break;
            
            case 11207:     //개틀링건 과부하 III
                TowerUpgradeAmount.GatlingTowerStat.attackSpeed = 4f;
                JsonManager.instance.usingList.Add(11209);
                break;
            
            case 11208:     //MLDRS-95
                TowerUpgradeAmount.GatlingTowerStat.attackDamage = 50f;
                TowerUpgradeAmount.GatlingTowerStat.additionalAttackSpeed = 4f;
                break;
                
            case 11209:     //개틀링건 과부하 IV
                TowerUpgradeAmount.GatlingTowerStat.attackSpeed = 10f;
                TowerUpgradeAmount.GatlingTowerStat.additionalAttackDamage = 20f;
                break;

            #endregion

            #region CannonTower

            case 11302:     //대포 성능 향상 I
                TowerUpgradeAmount.CannonTowerStat.attackDamage = 50f;
                JsonManager.instance.usingList.Add(11304);
                break;
            
            case 11303:     //숙련된 대포 사용 I
                TowerUpgradeAmount.CannonTowerStat.attackSpeed = 0.25f;
                JsonManager.instance.usingList.Add(11305);
                break;
            
            case 11304:     //대포 성능 향상 II
                TowerUpgradeAmount.CannonTowerStat.attackDamage = 100f;
                JsonManager.instance.usingList.Add(11306);
                break;
            
            case 11305:     //숙련된 대포 사용 II
                TowerUpgradeAmount.CannonTowerStat.attackSpeed = 0.5f;
                JsonManager.instance.usingList.Add(11307);
                break;
            
            case 11306:     //대포 성능 향상 III
                TowerUpgradeAmount.CannonTowerStat.attackDamage = 200f;
                JsonManager.instance.usingList.Add(11308);
                break;
            
            case 11307:     //숙련된 대포 사용 III
                TowerUpgradeAmount.CannonTowerStat.attackSpeed = 1f;
                JsonManager.instance.usingList.Add(11309);
                break;
            
            case 11308:     //대포 성능 향상 IV
                TowerUpgradeAmount.CannonTowerStat.attackDamage = 1000f;
                TowerUpgradeAmount.CannonTowerStat.additionalAttackSpeed = 1f;
                break;
            
            case 11309:     //숙련된 대포 사용 IV
                TowerUpgradeAmount.CannonTowerStat.attackSpeed = 3f;
                TowerUpgradeAmount.CannonTowerStat.additionalAttackDamage = 200f;
                break;

            #endregion

            #region Magnetite

            case 20101:     //자철석 채광 효율 I
                TowerUpgradeAmount.MagnetiteStat.moneyRate = 0.2f;
                JsonManager.instance.usingList.Add(20102);
                break;
            
            case 20102:     //자철석 채광 효율 II
                TowerUpgradeAmount.MagnetiteStat.moneyRate = 0.4f;
                JsonManager.instance.usingList.Add(20103);
                break;
            
            case 20103:     //자철석 채광 효율 III
                TowerUpgradeAmount.MagnetiteStat.moneyRate = 0.6f;
                JsonManager.instance.usingList.Add(20104);
                break;
            
            case 20104:     //자철석 채광 효율 IV
                TowerUpgradeAmount.MagnetiteStat.moneyRate = 0.8f;
                break;

            #endregion

            #region Crystal

            case 20201:     //수정 채광 효율 I
                TowerUpgradeAmount.CrystalStat.moneyRate = 0.2f;
                JsonManager.instance.usingList.Add(20202);
                break;
            
            case 20202:     //수정 채광 효율 II
                TowerUpgradeAmount.CrystalStat.moneyRate = 0.4f;
                JsonManager.instance.usingList.Add(20203);
                break;
            
            case 20203:     //수정 채광 효율 III
                TowerUpgradeAmount.CrystalStat.moneyRate = 0.6f;
                JsonManager.instance.usingList.Add(20204);
                break;
            
            case 20204:
                TowerUpgradeAmount.CrystalStat.moneyRate = 0.8f;
                break;

            #endregion

            #region Gold

            case 20301:
                TowerUpgradeAmount.GoldStat.moneyRate = 0.2f;
                JsonManager.instance.usingList.Add(20302);
                break;
            
            case 20302:
                TowerUpgradeAmount.GoldStat.moneyRate = 0.4f;
                JsonManager.instance.usingList.Add(20303);
                break;
            
            case 20303:
                TowerUpgradeAmount.GoldStat.moneyRate = 0.6f;
                JsonManager.instance.usingList.Add(20304);
                break;
            
            case 20304:
                TowerUpgradeAmount.GoldStat.moneyRate = 0.8f;
                break;
            
            #endregion

            #region Diamond

            case 20401:
                TowerUpgradeAmount.DiamondStat.moneyRate = 0.2f;
                JsonManager.instance.usingList.Add(20402);
                break;
            
            case 20402:
                TowerUpgradeAmount.DiamondStat.moneyRate = 0.4f;
                JsonManager.instance.usingList.Add(20403);
                break;
            
            case 20403:
                TowerUpgradeAmount.DiamondStat.moneyRate = 0.6f;
                JsonManager.instance.usingList.Add(20404);
                break;
            
            case 20404:
                TowerUpgradeAmount.DiamondStat.moneyRate = 0.8f;
                break;

            #endregion
        }
    }

    public void ResetInGameUpgrade()
    {
        TowerUpgradeAmount.ArrowTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.ArrowTowerStat.attackSpeed = 0f;
        TowerUpgradeAmount.ArrowTowerStat.isPoison = false;
        TowerUpgradeAmount.ArrowTowerStat.poisonDamageRate = 0f;
        TowerUpgradeAmount.ArrowTowerStat.poisonDuration = 0f;
        TowerUpgradeAmount.ArrowTowerStat.isBurn = false;
        TowerUpgradeAmount.ArrowTowerStat.burnDamageRate = 0f;
        TowerUpgradeAmount.ArrowTowerStat.burnDuration = 0f;

        TowerUpgradeAmount.HourglassTowerStat.slowRate = 0f;

        TowerUpgradeAmount.PoisonTowerStat.attackRange = 0f;
        TowerUpgradeAmount.PoisonTowerStat.abilityDamage = 0f;
        TowerUpgradeAmount.PoisonTowerStat.slowRate = 0f;
        TowerUpgradeAmount.PoisonTowerStat.additionalAbilityDamage = 0f;

        TowerUpgradeAmount.FlameTowerStat.slowRate = 0f;
        TowerUpgradeAmount.FlameTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.FlameTowerStat.attackRange = 0f;
        TowerUpgradeAmount.FlameTowerStat.centerAngle = 60f;
        TowerUpgradeAmount.FlameTowerStat.additionalAttackSpeed = 0f;

        TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 0f;
        TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 0f;
        TowerUpgradeAmount.AttackBuffTowerStat.additionalAbilityDamage = 0f;
        TowerUpgradeAmount.AttackBuffTowerStat.additionalAttackRange = 0f;

        TowerUpgradeAmount.AttackBuffTowerStat.abilityDamage = 0f;
        TowerUpgradeAmount.AttackBuffTowerStat.attackRange = 0f;
        TowerUpgradeAmount.AttackBuffTowerStat.additionalAbilityDamage = 0f;
        TowerUpgradeAmount.AttackBuffTowerStat.additionalAttackRange = 0f;

        TowerUpgradeAmount.LaserTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.LaserTowerStat.attackSpeed = 0f;

        TowerUpgradeAmount.MissileTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.MissileTowerStat.attackSpeed = 0f;
        TowerUpgradeAmount.MissileTowerStat.additionalAttackDamage = 0f;
        TowerUpgradeAmount.MissileTowerStat.additionalAttackSpeed = 0f;

        TowerUpgradeAmount.ElectricTowerStat.abilityDamage = 0f;
        TowerUpgradeAmount.ElectricTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.ElectricTowerStat.slowRate = 0f;
        TowerUpgradeAmount.ElectricTowerStat.additionalAbilityDamage = 0f;
        TowerUpgradeAmount.ElectricTowerStat.additionalAttackDmage = 0f;

        TowerUpgradeAmount.WallStat.hp = 0f;

        TowerUpgradeAmount.BombStat.attackDamage = 0f;
        TowerUpgradeAmount.BombStat.range = 3;

        TowerUpgradeAmount.GatlingTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.GatlingTowerStat.attackSpeed = 0f;
        TowerUpgradeAmount.GatlingTowerStat.additionalAttackDamage = 0f;
        TowerUpgradeAmount.GatlingTowerStat.additionalAttackSpeed = 0f;

        TowerUpgradeAmount.CannonTowerStat.attackDamage = 0f;
        TowerUpgradeAmount.CannonTowerStat.attackSpeed = 0f;
        TowerUpgradeAmount.CannonTowerStat.additionalAttackDamage = 0f;
        TowerUpgradeAmount.CannonTowerStat.additionalAttackSpeed = 0f;

        TowerUpgradeAmount.MagnetiteStat.moneyRate = 0f;
        
        TowerUpgradeAmount.CrystalStat.moneyRate = 0f;

        TowerUpgradeAmount.GoldStat.moneyRate = 0f;

        TowerUpgradeAmount.DiamondStat.moneyRate = 0f;
    }
}

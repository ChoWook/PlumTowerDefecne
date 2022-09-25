using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffTower : Tower
{
    public const string TowerTag = "Tower";

    public List<GameObject> TowerList = new List<GameObject>();
    
    private void Awake()
    {
        Setstat(ETowerName.AttackBuff);
    }

    
    public override float AbilityStat
    {
        get
        {
            List<float> list = TowerUpgradeAmount.instance._AttackBuffTowerStat.AbilityPlusModifier;

            float sum = 0f;

            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            float multi = 1f;

            for (int i = 0; i < AbilityMultiModifier.Count; i++)
            {
                multi *= AbilityMultiModifier[i];
            }


            return (BaseAbilityStat + sum) * multi;
        }
    }
    

    // attackrange, additionaldamage 





    protected override void UpdateTarget()
    {
        TowerList.Clear();


        GameObject[] Towers = GameObject.FindGameObjectsWithTag(TowerTag); // Collider로 바꾸면 정말 좋을텐데ㅜㅜ

        foreach(GameObject tower in Towers)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, tower.transform.position);

            if (distanceToEnemy <= RealRange)
            {
                TowerList.Add(tower);
            }
        }

    }

    protected override void Update()
    {
        if (TowerList.Count == 0)
        {
            return;
        }
           
        /*
        for(int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if (!(t.CheckAttackBuffTowers.ContainsKey(this))) // 추후 변경
            {
                
                t.AttackBuffTowers.TryAdd(this, AbilityStat);
                t.CheckAttackBuffTowers.TryAdd(this, true);

            }
        }
        */
    }


    public override void Shoot()
    {
        
    }

    public override void UpgradeTower()
    {
        base.UpgradeTower();
        
        /*
        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if ((t.CheckAttackBuffTowers.ContainsKey(this))) // 추후 변경
            {

                t.AttackBuffTowers[this] = AbilityStat;

            }
        }
        */
    }


    public override void SellTower()
    {
        base.SellTower();

        // 공격, 공속 버프 타워일 때  버프 삭제 효과 넣어주기
        /*
        for (int i = 0; i < TowerList.Count; i++)
        {

            Tower t = TowerList[i].GetComponent<Tower>();

            if ((t.CheckAttackBuffTowers.ContainsKey(this))) // 추후 변경
            {

                t.AttackBuffTowers[this] = AbilityStat;
                t.CheckAttackBuffTowers[this] = false;

            }
        }
        */
    }

    public override void MoveTower(Tile tile)
    {
        base.MoveTower(tile);

        // 새로 체크해서 바꿔줘야 하나?
    }



}

public class AttackBuffTowerComparer : IEqualityComparer<AttackBuffTower>
{
    public bool Equals(AttackBuffTower x, AttackBuffTower y)
    {
        return x.GetHashCode() == y.GetHashCode();
    }

    public int GetHashCode(AttackBuffTower pos)
    {
        return pos.GetHashCode() ^ pos.GetHashCode() << 2;
    }

}

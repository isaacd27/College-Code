using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTarget
{
    public enum TargetType
    {
        First,
        Close,
        Last
    }
    public static EnemyController GetTarget(TowerBehavior CurrenTower, TargetType targetMethod)
    {
        Collider[] EnemiesInRange = Physics.OverlapSphere(CurrenTower.transform.position, CurrenTower.Range, CurrenTower.EnemiesMask);

        //NativeArray<EnemyData> EnemiesToCalculate = new NativeArray<EnemyData>(EnemiesInRange.Length,Allocator.tempjob);

        for (int i = 0; i < EnemiesInRange.Length; i++)
        {
            EnemyController CurrentEnemy = EnemiesInRange[i].GetComponent<EnemyController>();
            //EnemiesToCalculate[i] = new EnemyData(CurrentEnemy.transform.position,CurrentEnemy.NodeIndex,CurrentEnemy.Health)


        }
        return null;
    }

    struct EnemyData
    {

        public EnemyData(Vector3 pos, int node, float hp)
        {
            EnemyPosition = pos;
            NodeIndex = node;
            Health = hp;

        }
        public Vector3 EnemyPosition;
        public int NodeIndex;
        public float Health;
    }

   /* struct SearchForEnemy: IJobFor
    {
        //NativeArray<>
        public void Execute(int index)
        {

        }
    }*/

    // Start is called before the first frame update

}

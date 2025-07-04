using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnSystem
{
    private const int kMaxEnemy = 10;

    private int mTotalEnemy = 0;
    private GameObject mEnemyTemplate = null;
    private Vector2 mSpawnRegionMin, mSpawnRegionMax;

    private int mEnemyDestroyed = 0;
    private bool isRandom =false;//abcdef or random destination
    public EnemySpawnSystem(Vector2 min, Vector2 max)
    {
        // Make sure all enemy sees the same EnemySystem and WayPointSystem
        EnemyBehavior.InitializeEnemySystem(this);

        mEnemyTemplate = Resources.Load<GameObject>("Prefabs/Enemy");
        mSpawnRegionMin = min * 0.9f;
        mSpawnRegionMax = max * 0.9f;
        GenerateEnemy();
    }
    public void SetisRandom(bool f)
    {
        isRandom = f;
        Debug.Log("Set isRandom = " + isRandom);
    }
    public bool GetisRandom()
    {
        return isRandom;
    }
    private void GenerateEnemy()
    {
        for (int i = mTotalEnemy; i < kMaxEnemy; i++)
        {
            GameObject p = GameObject.Instantiate(mEnemyTemplate) as GameObject;
            float x = Random.Range(mSpawnRegionMin.x, mSpawnRegionMax.x);
            float y = Random.Range(mSpawnRegionMin.y, mSpawnRegionMax.y);
            p.transform.position = new Vector3(x, y, 0f);
            mTotalEnemy++;
        }
    }

    public void OneEnemyDestroyed() { mEnemyDestroyed++;  ReplaceOneEnemy(); }
    public void ReplaceOneEnemy() { mTotalEnemy--; GenerateEnemy(); }
    public string GetWayPointState() {
        if (isRandom)
            return "WAYPOINTS:(Random)   ";
        else
            return "WAYPOINTS:(Sequence)   ";
    }
    public string GetEnemyState() { return "  ENEMY: Count(" + mTotalEnemy + ") Destroyed(" + mEnemyDestroyed + ")"; }
}
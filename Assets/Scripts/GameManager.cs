using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null;

    public TMP_Text mGameStateEcho = null;  // Defined in UnityEngine.UI
    public HeroBehavior mHero = null;
    private EnemySpawnSystem mEnemySystem = null;

    private CameraSupport mMainCamera;

    private void Start()
    {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        Debug.Assert(mHero != null);

        mMainCamera = Camera.main.GetComponent<CameraSupport>();
        Debug.Assert(mMainCamera != null);

        Bounds b = mMainCamera.GetWorldBound();
        mEnemySystem = new EnemySpawnSystem(b.min, b.max);
    }

    void Update()
    {
        EchoGameState(); // always do this

        if (Input.GetKeyDown(KeyCode.J))
        {
            bool isRandom = mEnemySystem.GetisRandom();
            mEnemySystem.SetisRandom(!isRandom);
        }

        if (Input.GetKey(KeyCode.Q))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.J))
        {
            mEnemySystem.SetisRandom(!mEnemySystem.GetisRandom());
            Debug.Log("Set isRandom in Manager");
        }
    }


    #region Bound Support
    public CameraSupport.WorldBoundStatus CollideWorldBound(Bounds b) { return mMainCamera.CollideWorldBound(b); }
    #endregion 

    private void EchoGameState()
    {
        mGameStateEcho.text = mEnemySystem.GetWayPointState() + mHero.GetHeroState(); // + "  " + mEnemySystem.GetEnemyState();
    }
}
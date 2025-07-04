using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {

    // All instances of Enemy shares this one WayPoint and EnemySystem
    static private EnemySpawnSystem sEnemySystem = null;
    static private bool isRandom=false;
    static public void InitializeEnemySystem(EnemySpawnSystem s,bool f) { sEnemySystem = s; isRandom = f; }
    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kEnemyEnergyLost = 0.8f;
    private APointBehaviour aPointBehaviour=null;
    private BPointBehaviour bPointBehaviour=null;
    private CPointBehaviour cPointBehaviour=null;
    private DPointBehaviour dPointBehaviour=null;
    private EPointBehaviour ePointBehaviour=null;
    private FPointBehaviour fPointBehaviour=null;
    private Vector3[] waypoint=new Vector3[6];
    private int nowDestintion = 0;
    private Vector3 velocity;
    const float v = 20f;
    public void Start()
    {
        aPointBehaviour = GameObject.Find("AWayPoint").GetComponent<APointBehaviour>();
        bPointBehaviour = GameObject.Find("BWayPoint").GetComponent<BPointBehaviour>();
        cPointBehaviour = GameObject.Find("CWayPoint").GetComponent<CPointBehaviour>();
        dPointBehaviour = GameObject.Find("DWayPoint").GetComponent<DPointBehaviour>();
        ePointBehaviour = GameObject.Find("EWayPoint").GetComponent<EPointBehaviour>();
        fPointBehaviour = GameObject.Find("FWayPoint").GetComponent<FPointBehaviour>();
        nowDestintion = isRandom ? Random.Range(0, 6) : 0;
        velocity = (waypoint[nowDestintion] - transform.position).normalized * v;
    }
    public void Update()
    {
        waypoint[0] = aPointBehaviour.getPosition();
        waypoint[1] = bPointBehaviour.getPosition();
        waypoint[2] = cPointBehaviour.getPosition();
        waypoint[3] = dPointBehaviour.getPosition();
        waypoint[4] = ePointBehaviour.getPosition();
        waypoint[5] = fPointBehaviour.getPosition();
        isRandom = sEnemySystem.GetisRandom();
        // isRandom = t;
        UpdateMotion();
        UpdateDestination();
    }
    private void UpdateMotion() {
        Vector3 currentVelocity = velocity;
        Vector3 targetDirection = (waypoint[nowDestintion] - transform.position).normalized;

        float speed = currentVelocity.magnitude;
        float maxRadiansPerFrame = 0.5f / 60f;

        Vector3 newDirection = Vector3.RotateTowards(
        currentVelocity.normalized,
        targetDirection,
        maxRadiansPerFrame,
        0f
        );
        velocity = newDirection * speed;
        transform.position += velocity * Time.smoothDeltaTime;
        transform.up = velocity.normalized;
    }
    private void UpdateDestination()
    {
        if ((transform.position - waypoint[nowDestintion]).magnitude < 25f)
        {
            int nextDestination;
            if (isRandom)
            {
                nextDestination = Random.Range(0, 5);
                if (nextDestination >= nowDestintion) nextDestination++;
            }
            else nextDestination = (nowDestintion + 1) % 6;
            nowDestintion = nextDestination;
        }
    }
    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Hero")
        {
            ThisEnemyIsHit();

        } else if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                c.a = c.a * kEnemyEnergyLost;
                GetComponent<Renderer>().material.color = c;
            } else
            {
                ThisEnemyIsHit();
            }
        }
    }

    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        Destroy(gameObject);
    }
    #endregion
}

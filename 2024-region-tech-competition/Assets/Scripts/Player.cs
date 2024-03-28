using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : BaseFlighter
{
    private static Player instance;
    public static Player Instance => instance ??= FindAnyObjectByType<Player>();

    private DataManager dataManager => DataManager.Instance;
    private GameManager gameManager => GameManager.Instance;

    public Transform orientation;
    public LayerMask modelAlignLayer;
    public float acc;
    public float maxSpeed;
    public float steerSpeed;
    public float driftSteerSpeed;

    [NonSerialized] public int money;
    [NonSerialized] public float curMaxSpeed;
    [NonSerialized] public float curSteerSpeed;
    [NonSerialized] public new Rigidbody rigidbody;
    [NonSerialized] public PlayerModel playerModel;

    public float DisplaySpeed => rigidbody.velocity.magnitude * 5;
    public float DisplayMaxSpeed => maxSpeed * 5;
    public float DisplaySteerSpeed => steerSpeed * 5;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        playerModel = model.GetComponent<PlayerModel>();
        curMaxSpeed = maxSpeed;
    }

    private void Update()
    {
        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 3);

        if (!GameManager.Instance.isGameRunning) return;
        if (!canMove) return;

        playerModel.isDrift = Input.GetKey(KeyCode.LeftShift);
        curSteerSpeed = Mathf.Lerp(curSteerSpeed, Input.GetKey(KeyCode.LeftShift) ? driftSteerSpeed : steerSpeed, Time.deltaTime * 2f);

        orientation.Rotate(0, Input.GetAxis("Horizontal") * curSteerSpeed * Time.deltaTime, 0);

        var velY = rigidbody.velocity.y;
        float mul = dataManager.engine switch
        {
            EngineType.Engine6 => 1.1f,
            EngineType.Engine8 => 1.25f,
            _ => 1
        };
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), curMaxSpeed * mul * slow);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameRunning) return;

        var displayWheel = gameManager.stage switch
        {
            1 => dataManager.desertWing,
            2 => dataManager.mountainWing,
            3 => dataManager.cityWing,
            _ => false
        };
        rigidbody.AddForce(Input.GetAxisRaw("Vertical") * model.forward * acc * (displayWheel ? 1.25f : 1), ForceMode.Acceleration);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Item") && !ItemManager.Instance.IsPickingItem)
        {
            Destroy(other.gameObject);
            ItemManager.Instance.GetRandomItem(item =>
            {
                switch (item)
                {
                    case ItemType.Money10: money += 10; break;
                    case ItemType.Money50: money += 50; break;
                    case ItemType.Money100: money += 100; break;
                    case ItemType.SpeedFast: StartCoroutine(SpeedFastRoutine()); break;
                    case ItemType.SpeedSuperFast: StartCoroutine(SpeedSuperFastRoutine()); break;
                    //case ItemType.JoinShop: UpgradeManager.Instance.Display(); break;
                    case ItemType.EndEnum: break;
                }
            });
        }
    }

    private IEnumerator SpeedFastRoutine()
    {
        playerModel.isSpeed = true;
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed * 1.5f, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed, 0.5f, x => curMaxSpeed = x);
        playerModel.isSpeed = false;
    }

    private IEnumerator SpeedSuperFastRoutine()
    {
        playerModel.isSpeed = true;
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed * 2, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed, 0.5f, x => curMaxSpeed = x);
        playerModel.isSpeed = false;
    }
}
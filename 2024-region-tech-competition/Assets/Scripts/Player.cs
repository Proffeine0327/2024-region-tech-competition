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
    [NonSerialized] public List<TrailRenderer> driftTrails;
    [NonSerialized] public GameObject speedParticle;
    [NonSerialized] public List<MeshRenderer> improveWheelMeshs;
    [NonSerialized] public List<MeshRenderer> engineUpgradeMeshs;

    public float DisplaySpeed => rigidbody.velocity.magnitude * 5;
    public float DisplayMaxSpeed => maxSpeed * 5;
    public float DisplaySteerSpeed => steerSpeed * 5;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        driftTrails = this.GetComponentsInChildren<TrailRenderer>("drift-trail");
        speedParticle = GetComponentsInChildren<Transform>().Where(x => x.name == "speed-particle").First().gameObject;
        improveWheelMeshs = this.GetComponentsInChildren<MeshRenderer>("improve-wheel");
        engineUpgradeMeshs = this.GetComponentsInChildren<MeshRenderer>("engine-upgrade");
        speedParticle.SetActive(false);
        curMaxSpeed = maxSpeed;
    }

    private void Update()
    {
        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 3);

        if (!GameManager.Instance.IsGameRunning) return;
        if (!canMove) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            driftTrails.ForEach(t => t.gameObject.SetActive(true));
            curSteerSpeed = Mathf.Lerp(curSteerSpeed, driftSteerSpeed, Time.deltaTime * 2f);
        }
        else
        {
            driftTrails.ForEach(t => t.gameObject.SetActive(false));
            curSteerSpeed = Mathf.Lerp(curSteerSpeed, steerSpeed, Time.deltaTime * 2f);
        }
        orientation.Rotate(0, Input.GetAxis("Horizontal") * curSteerSpeed * Time.deltaTime, 0);

        var velY = rigidbody.velocity.y;
        float mul = 1;
        if (dataManager.engine == EngineType.Engine6) mul = 1.1f;
        if (dataManager.engine == EngineType.Engine8) mul = 1.25f;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), curMaxSpeed * mul * slow);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z);

        //improveWheelMeshs.ForEach(x => x.gameObject.SetActive((dataManager.wingType & WingType.Desert) != 0));
        //engineUpgradeMeshs.For((x, index) => x.gameObject.SetActive((dataManager.engineType & (EngineType)(index - 1)) != 0));
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameRunning) return;
        //rigidbody.AddForce(Input.GetAxisRaw("Vertical") * model.forward * acc * (dataManager. == 1 ? 1.25f : 1), ForceMode.Acceleration);
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
        speedParticle.SetActive(true);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed * 1.5f, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed, 0.5f, x => curMaxSpeed = x);
        speedParticle.SetActive(false);
    }

    private IEnumerator SpeedSuperFastRoutine()
    {
        speedParticle.SetActive(true);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed * 2, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed, 0.5f, x => curMaxSpeed = x);
        speedParticle.SetActive(false);
    }
}
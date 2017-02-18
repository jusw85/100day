using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MoverController))]
[RequireComponent(typeof(AnimationController))]
public class PlayerController : MonoBehaviour, IDamageable {

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    public int maxHp = 6;
    private int currentHp;

    public GameObject projectile;
    public float shotDelay = 0.05f;
    public float shotSpeed = 20f;
    public float shotSpread = 7.5f;
    public float controllerAimLength = 5f;

    public AudioClip shootSound;
    public AudioClip swordSound;
    public AudioClip[] owSounds;
    public AudioClip[] deathSounds;

    [System.NonSerialized]
    public bool isPaused = false;

    private MoverController moverController;
    private AnimationController animationController;
    private LineRenderer lineRenderer;
    private GameObject playerFace;

    private Vector2 moveInput;
    private Vector3 lookPosition = Vector3.zero;
    private Vector3 lookVector;
    private bool mouseActive = true;
    private bool gamepadActive = false;
    private bool canShoot = true;

    public GameObject heartsPanel;
    public delegate void HpChangeEvent(int hp);
    public static event HpChangeEvent HpChangedEvent;

    private bool tookDamageThisFrame = false;
    private float flashSpeed = 1.5f;
    private Image damageFlashImage;
    private Color damageFlashColour = new Color(1f, 1f, 1f, 0.6f);

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        moverController = GetComponent<MoverController>();
        animationController = GetComponent<AnimationController>();
        lineRenderer = GetComponent<LineRenderer>();
        playerFace = transform.Find("PlayerFace").gameObject;

        currentHp = maxHp;
    }

    private void Start() {
        GameObject damageFlash = GameObject.Find("DamageFlash");
        damageFlashImage = damageFlash.GetComponent<Image>();

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.target = gameObject;

        PoolManager.instance.CreatePool(projectile, 50);

        CreateHeartPanel();
    }

    private void Update() {
        if (isPaused)
            return;
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moverController.MoveDirection = moveInput;

        var mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var gamepadLookInput = new Vector2(Input.GetAxisRaw("PadRHorizontal"), Input.GetAxisRaw("PadRVertical"));
        var gamePadMoved = false;
        if (mouseInput.magnitude > 0.01f) {
            mouseActive = true;
            gamepadActive = false;
        } else if (gamepadLookInput.magnitude > 0.01f) {
            mouseActive = false;
            gamepadActive = true;
            gamePadMoved = true;
        }

        if (mouseActive) {
            lookVector = (lookPosition - transform.position).normalized;
        }
        if (gamepadActive) {
            if (gamePadMoved) {
                lookVector = gamepadLookInput.normalized * controllerAimLength;
            }
            lookPosition = transform.position + lookVector;
        }


        // update face direction only if not attacking
        if (animationController.isIdle()) {
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, lookPosition - transform.position);
            playerFace.transform.rotation = rot;
        }

        bool isSwordAttack = Input.GetButton("Fire1") || (Input.GetAxisRaw("PadLTrigger") > 0.1f);
        bool isShootAttack = Input.GetButton("Fire2") || (Input.GetAxisRaw("PadRTrigger") > 0.1f);

        animationController.doAttack(isSwordAttack);
        if (animationController.isIdle() && isSwordAttack) {
            AudioManager.Instance.PlaySfx(swordSound);
        }

        if (canShoot && isShootAttack) {
            StartCoroutine(Shoot());
        }

        if (tookDamageThisFrame) {
            damageFlashImage.color = damageFlashColour;
        } else {
            damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        tookDamageThisFrame = false;
    }

    private void UpdateLookPosition() {
        var rawMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookPosition = new Vector3(rawMousePosition.x, rawMousePosition.y, 0);
    }


    public IEnumerator Shoot() {
        Vector3 dir = playerFace.transform.up.normalized;
        Vector3 spawnLoc = transform.position + (dir * 1.5f);
        Quaternion originalRot = playerFace.transform.rotation;
        Vector3 originalAngles = originalRot.eulerAngles;
        Vector3 newAngles = new Vector3(originalAngles.x, originalAngles.y, originalAngles.z + Random.Range(-shotSpread, shotSpread));
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = newAngles;

        GameObject newprojectile = PoolManager.instance.ReuseObject(projectile, spawnLoc, newRot);
        newprojectile.GetComponent<ProjectileController>().movementSpeed = shotSpeed;

        AudioManager.Instance.PlaySfx(shootSound);

        canShoot = false;
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    private void LateUpdate() {
        UpdateLookPosition();

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, lookPosition);
        Debug.DrawLine(transform.position, lookPosition, Color.red);

        animationController.SetIsFacingRight(lookPosition.x > transform.position.x);
    }

    public void Damage() {
        currentHp--;
        tookDamageThisFrame = true;
        if (HpChangedEvent != null) {
            HpChangedEvent(currentHp);
        }

        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        cameraShake.shakeIntensity = 0.1f;
        cameraShake.duration = 0.25f;

        AudioManager.Instance.PlaySfx(owSounds[Random.Range(0, owSounds.Length)]);
        if (currentHp <= 0) {
            AudioManager.Instance.PlaySfx(deathSounds[Random.Range(0, deathSounds.Length)]);
        }
    }

    private void CreateHeartPanel() {
        GameObject hud = GameObject.Find("HUDCanvas");
        GameObject heartPanel = Instantiate(heartsPanel);
        heartPanel.transform.SetParent(hud.transform);
        heartPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

}

using DG.Tweening;
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
    private Light muzzleFlash;

    private Vector2 moveInput;
    private Vector2 lastMoveInput;
    private Vector3 lookPosition = Vector3.zero;
    private Vector3 lookVector;
    private bool mouseActive = true;
    private bool gamepadActive = false;
    private bool canShoot = true;

    public GameObject heartsPanel;

    private float flashSpeed = 1.5f;
    private Image damageFlashImage;
    private Color damageFlashColour = new Color(1f, 1f, 1f, 0.6f);
    private Tween screenFlashTween;

    private Slider chargeSlider;
    private bool isLAxisHeld = false;
    //private bool isRAxisHeld = false;
    private bool isAttackCharging = false;
    private float chargeRate = 0.5f;
    private float initialChargeValue = -0.5f;
    private float chargeValue;

    private GameObject dialogCanvas;

    private GunBeam gunBeam;

    private PoolManager poolManager;
    private EventManager eventManager;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        moverController = GetComponent<MoverController>();
        animationController = GetComponent<AnimationController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = -2;
        playerFace = transform.Find("PlayerFace").gameObject;
        muzzleFlash = transform.Find("PlayerFace/MuzzleFlash").GetComponent<Light>();
        gunBeam = transform.Find("GunBeam").GetComponent<GunBeam>();

        currentHp = maxHp;
        chargeValue = initialChargeValue;
    }

    private void Start() {
        var obj = GameObject.Find("ScreenFlash");
        damageFlashImage = obj.GetComponent<Image>();

        var obj2 = GameObject.Find("ChargeSlider");
        chargeSlider = obj2.GetComponent<Slider>();

        dialogCanvas = GameObject.Find("DialogCanvas");
        dialogCanvas.SetActive(false);

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.target = gameObject;

        poolManager = Toolbox.RegisterComponent<PoolManager>();
        poolManager.CreatePool(projectile, 50);

        eventManager = Toolbox.RegisterComponent<EventManager>();
        CreateHeartPanel();
    }

    public void Move(Vector2 moveInput) {
        this.moveInput = moveInput;
        moverController.MoveDirection = moveInput;
        
        // set player state i.e. walking
        // fsm e.g. idle -> walking?

        // anim update based on fsm in one class
        animationController.SetIsMoving(moveInput.magnitude > 0);
        animationController.SetMoveVector(moveInput);
        animationController.SetLastMoveVector(lastMoveInput);

        animationController.SetIsFacingRight(true);
        if (moveInput.x < 0 ||
            (moveInput.magnitude == 0f && lastMoveInput.x < 0)) {
            animationController.SetIsFacingRight(false);
        }

        if (moveInput.magnitude > 0)
            lastMoveInput = moveInput;
    }

    private void Update() {
        if (isPaused)
            return;
        //moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Debug.Log(moveInput.sqrMagnitude + " " + moveInput.ToString("f4") + " " + moveInput.normalized);
        //moveInput = moveInput.normalized;
        

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

        if (gamepadActive) {
            if (gamePadMoved) {
                lookVector = gamepadLookInput.normalized;
            }
            lookPosition = transform.position + (lookVector * controllerAimLength);
        }

        // update face direction only if not attacking
        if (animationController.IsIdle()) {
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, lookPosition - transform.position);
            playerFace.transform.rotation = rot;
        }

        bool isPrimaryDown = Input.GetButtonDown("Fire1");
        bool isPrimaryHold = Input.GetButton("Fire1");
        bool isPrimaryUp = Input.GetButtonUp("Fire1");
        //Debug.Log(isPrimaryDown + " " + isPrimaryHold + " " + isPrimaryUp);
        if (Input.GetAxisRaw("PadLTrigger") > 0) {
            if (!isLAxisHeld) {
                isLAxisHeld = true;
                isPrimaryDown = true;
            } else {
                isPrimaryHold = true;
            }
        } else if (isLAxisHeld) {
            isLAxisHeld = false;
            isPrimaryUp = true;
        }

        animationController.DoAttack(false);
        if (!isAttackCharging && animationController.IsIdle()) {
            chargeValue = initialChargeValue;
            chargeSlider.value = 0f;
        }
        if (isPrimaryDown) {
            animationController.DoAttack(true);
            AudioManager.Instance.PlaySfx(swordSound);
        }
        if ((isPrimaryHold || isPrimaryDown) && animationController.IsIdle()) {
            isAttackCharging = true;
        }
        if (isAttackCharging && isPrimaryHold) {
            //chargeSlider.value += chargeRate * Time.deltaTime;
            chargeValue += chargeRate * Time.deltaTime;
            if (chargeValue >= 0f) {
                chargeSlider.value = chargeValue;
            }
        }
        if (isPrimaryUp) {
            isAttackCharging = false;
        }
        if (isPrimaryUp && chargeSlider.value == 1.0f) {
            animationController.DoAttack(true);
            AudioManager.Instance.PlaySfx(swordSound);
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            dialogCanvas.SetActive(!dialogCanvas.activeSelf);
        }


        Animator a = GetComponent<Animator>();
        //a.SetBool("isAttacking2", false);
        if (Input.GetKeyDown(KeyCode.M)) {
            a.SetBool("isAttacking2", true);
        }
    }

    private void UpdateLookPosition() {
        var rawMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookPosition = new Vector3(rawMousePosition.x, rawMousePosition.y, 0);
        lookVector = (lookPosition - transform.position).normalized;
    }

    public void Reset() {
        muzzleFlash.enabled = false;
        gunBeam.TurnOn(false);
    }

    public void ShootFn() {
        //bool isShootAttack = Input.GetButton("Fire2") || (Input.GetAxisRaw("PadRTrigger") > 0);
        if (canShoot) {
            StartCoroutine(Shoot());
        }
        gunBeam.TurnOn(true);
        //gunBeam.TurnOn(isShootAttack);
    }

    public IEnumerator Shoot() {
        muzzleFlash.enabled = true;
        Vector3 dir = playerFace.transform.up.normalized;
        Vector3 spawnLoc = transform.position + (dir * 1.5f);
        Quaternion originalRot = playerFace.transform.rotation;
        Vector3 originalAngles = originalRot.eulerAngles;
        Vector3 newAngles = new Vector3(originalAngles.x, originalAngles.y, originalAngles.z + Random.Range(-shotSpread, shotSpread));
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = newAngles;

        GameObject newprojectile = poolManager.ReuseObject(projectile, spawnLoc, newRot);
        newprojectile.GetComponent<ProjectileController>().movementSpeed = shotSpeed;

        AudioManager.Instance.PlaySfx(shootSound);

        canShoot = false;
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    private void LateUpdate() {
        if (mouseActive) {
            UpdateLookPosition();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookVector, 100f, Constants.LINECAST_LAYERS);
        float distance = 100f;
        if (hit.collider != null) {
            distance = hit.distance;
        }

        Vector3 lineStart = transform.position;
        Vector3 lineEnd = transform.position + (lookVector * distance);
        lineRenderer.SetPosition(0, lineStart);
        lineRenderer.SetPosition(1, lineEnd);

        if (gunBeam != null) {
            gunBeam.SetPosition(lineStart, lineEnd);
        }
        //Debug.DrawLine(transform.position, lookPosition, Color.red);

        //animationController.SetIsMoving(moveInput.magnitude > 0);
        //animationController.SetMoveVector(moveInput);
        //animationController.SetLastMoveVector(lastMoveInput);

        //animationController.SetIsFacingRight(true);
        //if (moveInput.x < 0 ||
        //    (moveInput.magnitude == 0f && lastMoveInput.x < 0)) {
        //    animationController.SetIsFacingRight(false);
        //}

        //if (moveInput.magnitude > 0)
        //    lastMoveInput = moveInput;
    }

    public void Damage(GameObject damager) {
        int prevHp = currentHp--;
        DamageScreenFlash();

        HpChangeEvent ev = new HpChangeEvent(prevHp, currentHp);
        eventManager.Publish(Events.HPCHANGE_ID, ev);

        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        cameraShake.shakeIntensity = 0.1f;
        cameraShake.duration = 0.25f;

        AudioManager.Instance.PlaySfx(owSounds[Random.Range(0, owSounds.Length)]);
        if (currentHp <= 0) {
            MenuManager.Instance.GameOver();
            AudioManager.Instance.PlaySfx(deathSounds[Random.Range(0, deathSounds.Length)]);
        }
    }

    public void DamageScreenFlash() {
        damageFlashImage.color = damageFlashColour;
        if (screenFlashTween != null) {
            screenFlashTween.Restart();
        } else {
            screenFlashTween = DOTween
                .To(() => damageFlashImage.color, x => damageFlashImage.color = x, Color.clear, flashSpeed)
                .SetAutoKill(false);
            screenFlashTween.Play();
        }
    }
    
    private void CreateHeartPanel() {
        GameObject hud = GameObject.Find("HUDCanvas");
        GameObject heartPanel = Instantiate(heartsPanel);
        heartPanel.transform.SetParent(hud.transform);
        heartPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

}

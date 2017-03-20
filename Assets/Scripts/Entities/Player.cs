using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MoverController))]
[RequireComponent(typeof(AnimationController))]
public class Player : MonoBehaviour, IDamageable {

    private static Player instance;
    public static Player Instance { get { return instance; } }

    public int maxHp = 6;
    private int currentHp;

    public AudioClip swordSound;
    public AudioClip[] owSounds;
    public AudioClip[] deathSounds;

    [System.NonSerialized]
    public bool isPaused = false;

    private MoverController moverController;
    private AnimationController animationController;

    public GameObject heartsPanel;

    //private float flashSpeed = 1.5f;
    //private Image damageFlashImage;
    //private Color damageFlashColour = new Color(1f, 1f, 1f, 0.6f);
    //private Tween screenFlashTween;

    //private Slider chargeSlider;
    private bool isLAxisHeld = false;
    //private bool isRAxisHeld = false;
    private bool isAttackCharging = false;
    private float chargeRate = 0.5f;
    private float initialChargeValue = -0.5f;
    private float chargeValue;

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

        currentHp = maxHp;
        chargeValue = initialChargeValue;
    }

    private void Start() {
        //var obj = GameObject.Find("ScreenFlash");
        //damageFlashImage = obj.GetComponent<Image>();

        //var obj2 = GameObject.Find("ChargeSlider");
        //chargeSlider = obj2.GetComponent<Slider>();

        poolManager = Toolbox.RegisterComponent<PoolManager>();
        eventManager = Toolbox.RegisterComponent<EventManager>();
        //CreateHeartPanel();
    }

    [System.NonSerialized]
    public Vector2 faceDir;

    public void Move(Vector2 moveInput) {
        moverController.MoveDirection = moveInput;
        faceDir = moveInput.normalized;
    }

    private void Update() {
        //if (isPaused)
        //    return;

        //moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Debug.Log(moveInput.sqrMagnitude + " " + moveInput.ToString("f4") + " " + moveInput.normalized);
        //moveInput = moveInput.normalized;

        //bool isPrimaryDown = Input.GetButtonDown("Fire1");
        //bool isPrimaryHold = Input.GetButton("Fire1");
        //bool isPrimaryUp = Input.GetButtonUp("Fire1");
        ////Debug.Log(isPrimaryDown + " " + isPrimaryHold + " " + isPrimaryUp);
        //if (Input.GetAxisRaw("PadLTrigger") > 0) {
        //    if (!isLAxisHeld) {
        //        isLAxisHeld = true;
        //        isPrimaryDown = true;
        //    } else {
        //        isPrimaryHold = true;
        //    }
        //} else if (isLAxisHeld) {
        //    isLAxisHeld = false;
        //    isPrimaryUp = true;
        //}

        //animationController.DoAttack(false);
        //if (!isAttackCharging && animationController.IsIdle()) {
        //    chargeValue = initialChargeValue;
        //    chargeSlider.value = 0f;
        //}
        //if (isPrimaryDown) {
        //    animationController.DoAttack(true);
        //    AudioManager.Instance.PlaySfx(swordSound);
        //}
        //if ((isPrimaryHold || isPrimaryDown) && animationController.IsIdle()) {
        //    isAttackCharging = true;
        //}
        //if (isAttackCharging && isPrimaryHold) {
        //    //chargeSlider.value += chargeRate * Time.deltaTime;
        //    chargeValue += chargeRate * Time.deltaTime;
        //    if (chargeValue >= 0f) {
        //        chargeSlider.value = chargeValue;
        //    }
        //}
        //if (isPrimaryUp) {
        //    isAttackCharging = false;
        //}
        //if (isPrimaryUp && chargeSlider.value == 1.0f) {
        //    animationController.DoAttack(true);
        //    AudioManager.Instance.PlaySfx(swordSound);
        //}

        //Animator a = GetComponent<Animator>();
        //a.SetBool("isAttacking2", false);
        //if (Input.GetKeyDown(KeyCode.M)) {
        //    a.SetBool("isAttacking2", true);
        //}
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
        //damageFlashImage.color = damageFlashColour;
        //if (screenFlashTween != null) {
        //    screenFlashTween.Restart();
        //} else {
        //    screenFlashTween = DOTween
        //        .To(() => damageFlashImage.color, x => damageFlashImage.color = x, Color.clear, flashSpeed)
        //        .SetAutoKill(false);
        //    screenFlashTween.Play();
        //}
    }

    //private void CreateHeartPanel() {
    //    GameObject hud = GameObject.Find("HUDCanvas");
    //    GameObject heartPanel = Instantiate(heartsPanel);
    //    heartPanel.transform.SetParent(hud.transform);
    //    heartPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    //}

}

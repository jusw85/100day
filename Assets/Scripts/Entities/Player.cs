using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MoverController))]
[RequireComponent(typeof(AnimationController))]
public class Player : MonoBehaviour, IDamageable {

    private static Player instance;
    public static Player Instance { get { return instance; } }

    public NamedHpChangeEvent hpChangeEvent;

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

    //private PoolManager poolManager;
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
        CanRoll = true;
    }

    private void Start() {
        //var obj = GameObject.Find("ScreenFlash");
        //damageFlashImage = obj.GetComponent<Image>();

        //var obj2 = GameObject.Find("ChargeSlider");
        //chargeSlider = obj2.GetComponent<Slider>();

        //poolManager = Toolbox.RegisterComponent<PoolManager>();
        eventManager = Toolbox.RegisterComponent<EventManager>();
        //CreateHeartPanel();
    }

    private static readonly Vector2 FACE_DOWN = new Vector2(0f, -1f);
    private static readonly Vector2 FACE_RIGHT = new Vector2(1f, 0f);
    private static readonly Vector2 FACE_UP = new Vector2(0f, 1f);
    private static readonly Vector2 FACE_LEFT = new Vector2(-1f, 0f);

    [System.NonSerialized]
    public Vector2 faceDir = FACE_DOWN;
    public float moveSpeed = 12f;

    public void Move(Vector2 moveInput) {
        moverController.MoveSpeed = moveSpeed;
        moverController.MoveDirection = moveInput;
        Face(moveInput);
    }

    public float rollSpeed = 36f;
    public float rollCooldown = 0.0f;
    public bool CanRoll { get; set; }

    public void StartRoll(Vector2 moveInput) {
        moverController.MoveSpeed = rollSpeed;
        moverController.MoveDirection = moveInput;
        moverController.resetVelocity = false;
        Face(moveInput);
    }

    public void StopRoll() {
        moverController.MoveDirection = Vector2.zero;
        moverController.resetVelocity = true;
        StartCoroutine(RollCooldown());
    }

    private IEnumerator RollCooldown() {
        CanRoll = false;
        yield return new WaitForSeconds(rollCooldown);
        CanRoll = true;
    }

    public void Face(Vector2 moveDir) {
        Vector2 v = moveDir.normalized;

        if (Mathf.Abs(v.x) >= Mathf.Abs(v.y)) {
            if (v.x < 0) {
                faceDir = FACE_LEFT;
            } else {
                faceDir = FACE_RIGHT;
            }
        } else {
            if (v.y < 0) {
                faceDir = FACE_DOWN;
            } else {
                faceDir = FACE_UP;
            }
        }
    }

    private float chargeCurrentTime = 0;
    private float chargeThresholdTime = 1.0f; // threshold before considered 'charging'
    private float chargeFullTime = 2.0f; // time to fully charge
    public void AddCharge(float time) {
        bool a = IsCharging;
        bool b = IsFullyCharged;
        chargeCurrentTime = Mathf.Clamp(chargeCurrentTime + time, 0, chargeFullTime);
        frameInfo.isCharging = !a && IsCharging;
        frameInfo.isFullyCharged = !b && IsFullyCharged;
    }

    public void ResetCharge() {
        chargeCurrentTime = 0;
        frameInfo.hasStoppedCharging = true;
    }

    public float ChargeNormalizedValue {
        get { return Mathf.Clamp((chargeCurrentTime - chargeThresholdTime) / chargeFullTime, 0, 1); }
    }

    public bool IsCharging {
        get { return chargeCurrentTime > chargeThresholdTime; }
    }

    public bool IsFullyCharged {
        get { return chargeCurrentTime >= chargeFullTime; }
    }

    private PlayerFrameInfo frameInfo;
    public void DoUpdate(FsmFrameInfo state, ControlManager c, ref PlayerFrameInfo frameInfo) {
        this.frameInfo = frameInfo;
        if (state.hasChanged) {
            if (state.prev == AnimStates.ROLL) {
                StopRoll();
            }

            if (state.curr == AnimStates.ATTACK1) {
                ResetCharge();
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.ATTACK2) {
                ResetCharge();
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.ATTACK3) {
                ResetCharge();
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.CHARGEATTACK) {
                ResetCharge();
                if (c.isMoved) {
                    Face(c.move);
                }
            } else if (state.curr == AnimStates.ROLL) {
                if (c.isMoved) {
                    StartRoll(c.move);
                } else {
                    StartRoll(faceDir);
                }
            }
        }

        if (state.curr == AnimStates.IDLE) {
            if (c.isAttackHeld) {
                AddCharge(Time.deltaTime);
            }
            if (c.isMoved) {
                Move(c.move);
            }
            if (c.isAttackReleased) {
                if (IsFullyCharged) {
                    frameInfo.isChargeAttacking = true;
                } else if (IsCharging) {
                    frameInfo.isAttacking = true;
                }
                ResetCharge();
            }

        } else if (state.curr == AnimStates.WALK) {
            if (c.isAttackHeld) {
                AddCharge(Time.deltaTime);
            }
            if (c.isMoved) {
                Move(c.move);
            }
            if (c.isAttackReleased) {
                if (IsFullyCharged) {
                    frameInfo.isChargeAttacking = true;
                } else if (IsCharging) {
                    frameInfo.isAttacking = true;
                }
                ResetCharge();
            }

        }

    }

    public void Damage(GameObject damager) {
        int prevHp = currentHp--;
        DamageScreenFlash();

        PlayerHpChangeEvent ev = new PlayerHpChangeEvent(prevHp, currentHp);
        eventManager.Publish(Events.PLAYER_HPCHANGE, ev);

        //CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        //cameraShake.shakeIntensity = 0.1f;
        //cameraShake.duration = 0.25f;

        //AudioManager.Instance.PlaySfx(owSounds[Random.Range(0, owSounds.Length)]);
        //if (currentHp <= 0) {
        //    MenuManager.Instance.GameOver();
        //    AudioManager.Instance.PlaySfx(deathSounds[Random.Range(0, deathSounds.Length)]);
        //}
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

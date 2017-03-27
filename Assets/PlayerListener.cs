using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour {

    private EventManager eventManager;
    private void Awake() {
    }

    private void Start() {
        eventManager = Toolbox.RegisterComponent<EventManager>();
    }

    private void PlayerAttackHandler(IGameEvent e) {
        PlayerAttackEvent ev = (PlayerAttackEvent)e;
        int attackNumber = ev.attackNumber;
        // logic to select audio based on attack number and weapon type


    }

    private void OnEnable() {
        eventManager.AddSubscriber(Events.PLAYER_ATTACK, PlayerAttackHandler);
    }

    private void OnDisable() {
        eventManager.RemoveSubscriber(Events.PLAYER_ATTACK, PlayerAttackHandler);
    }

}

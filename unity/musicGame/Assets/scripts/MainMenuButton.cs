using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour {

    [SerializeField]
    private GameObject _Panel;

    [SerializeField]
    private GameObject _MenuButton;

    private void Start() {
        MenuButtonActive(true);
    }

    public void MenuButton() {
        MenuButtonActive(false);
        GameController.Instance.StopPlay(false);
    }

    private void MenuButtonActive(bool act) {
        _MenuButton.SetActive(act);
        _Panel.SetActive(!act);
    }

    public void ToSelect() {
        MenuButtonActive(true);
        SoundController.Instance.MusicReset();
        SceneMoveScript.Instance.ChangeScene("Select");
    }

    public void BackPlay() {
        MenuButtonActive(true);
        GameController.Instance.StopPlay(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private InputActionReference _pauseKey;
    // Start is called before the first frame update
    void Start()
    {
        if (_pauseKey) _pauseKey.action.performed += QuitToMenu;
    }

    public void QuitToMenu(InputAction.CallbackContext cbc)
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void OnDestroy()
    {
        if(_pauseKey) _pauseKey.action.performed -= QuitToMenu;
    }
}

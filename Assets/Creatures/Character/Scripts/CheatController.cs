using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{

    private string _currentInput;
    [SerializeField] private float _inputTimeLive;
    [SerializeField] private CheatItem[] _cheatKode;
    private float _inputTime;
    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char inputChar)
    {
        _currentInput += inputChar;
        _inputTime = _inputTimeLive;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheatItem in _cheatKode)
        {
            if (_currentInput.Contains(cheatItem.Name))
            {
                cheatItem.Action.Invoke();
                _currentInput = string.Empty;
            }
                
        }
    }

    private void Update()
    {
        if (_inputTime < 0)
            _currentInput = string.Empty;
        else
            _inputTime = Time.deltaTime;
    }
}

[Serializable]
public class CheatItem
{
    public string Name;
    public UnityEvent Action;
}

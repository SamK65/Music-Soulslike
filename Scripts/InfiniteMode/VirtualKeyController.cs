
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualKeyController : MonoBehaviour
{
    [Serializable]
    public class VirtualKeyState
    {
        public int degree;
        public bool pressed;
        public bool highlighted;
    }

    public VirtualKeyState[] keys = new VirtualKeyState[7];

    public event Action<int> OnNotePressed;

    private readonly Key[] keyboardKeys =
    {
        Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4,
        Key.Digit5, Key.Digit6, Key.Digit7
    };

    private readonly Key[] numpadKeys =
    {
        Key.Numpad1, Key.Numpad2, Key.Numpad3, Key.Numpad4,
        Key.Numpad5, Key.Numpad6, Key.Numpad7
    };

    private void Awake()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = new VirtualKeyState
            {
                degree = i + 1,
                pressed = false,
                highlighted = false
            };
        }
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        for (int i = 0; i < 7; i++)
        {
            bool down =
                keyboard[keyboardKeys[i]].wasPressedThisFrame ||
                keyboard[numpadKeys[i]].wasPressedThisFrame;

            bool up =
                keyboard[keyboardKeys[i]].wasReleasedThisFrame ||
                keyboard[numpadKeys[i]].wasReleasedThisFrame;

            if (down)
            {
                keys[i].pressed = true;
                OnNotePressed?.Invoke(i + 1);
            }

            if (up)
                keys[i].pressed = false;
        }
    }

    public void SetHighlight(int degree, bool highlighted)
    {
        if (degree < 1 || degree > 7)
            return;

        keys[degree - 1].highlighted = highlighted;
    }

    public void ClearHighlights()
    {
        foreach (VirtualKeyState key in keys)
            key.highlighted = false;
    }

    public VirtualKeyState GetKeyState(int degree)
    {
        if (degree < 1 || degree > 7)
            return null;

        return keys[degree - 1];
    }
}

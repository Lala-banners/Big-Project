//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Prototypes/LaraTutorials/Lara_Input/Lara_Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Lara_Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Lara_Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Lara_Controls"",
    ""maps"": [
        {
            ""name"": ""Lara_Player"",
            ""id"": ""3eba328f-ff35-4fbb-9be8-7675be357a9a"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""5686da05-27ec-4397-9138-7840d00af072"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eaf6350a-ac57-4496-9092-bba5ec55b652"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Test Keyboard & Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Test Keyboard & Mouse"",
            ""bindingGroup"": ""Test Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Lara_Player
        m_Lara_Player = asset.FindActionMap("Lara_Player", throwIfNotFound: true);
        m_Lara_Player_Look = m_Lara_Player.FindAction("Look", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Lara_Player
    private readonly InputActionMap m_Lara_Player;
    private ILara_PlayerActions m_Lara_PlayerActionsCallbackInterface;
    private readonly InputAction m_Lara_Player_Look;
    public struct Lara_PlayerActions
    {
        private @Lara_Controls m_Wrapper;
        public Lara_PlayerActions(@Lara_Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Lara_Player_Look;
        public InputActionMap Get() { return m_Wrapper.m_Lara_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Lara_PlayerActions set) { return set.Get(); }
        public void SetCallbacks(ILara_PlayerActions instance)
        {
            if (m_Wrapper.m_Lara_PlayerActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_Lara_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_Lara_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_Lara_PlayerActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_Lara_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public Lara_PlayerActions @Lara_Player => new Lara_PlayerActions(this);
    private int m_TestKeyboardMouseSchemeIndex = -1;
    public InputControlScheme TestKeyboardMouseScheme
    {
        get
        {
            if (m_TestKeyboardMouseSchemeIndex == -1) m_TestKeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Test Keyboard & Mouse");
            return asset.controlSchemes[m_TestKeyboardMouseSchemeIndex];
        }
    }
    public interface ILara_PlayerActions
    {
        void OnLook(InputAction.CallbackContext context);
    }
}

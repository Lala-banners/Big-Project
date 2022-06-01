//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Main Game/Inputs/Basic_Controls.inputactions
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

namespace MainGame.Input.BasicControls
{
    public partial class @Basic_Controls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Basic_Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Basic_Controls"",
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
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8ef76d09-300e-4615-8a45-bcf3b55fa6d1"",
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
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e5ff87a8-26b0-49e9-9a33-ae9a7c3e241a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8f093c8e-0b49-416f-9815-601bddbb2925"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Test Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""36f570a0-25e1-4574-899f-72f3491d2873"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Test Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c8f0da65-d751-4cbf-ab40-1db7e0ffb4f2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Test Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""498e392b-c406-4ef7-8e0b-257adedf6938"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Test Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Dumpling"",
            ""id"": ""38953f05-c965-429d-ba44-9cb130a753b0"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""21c1ddb8-282b-4966-b571-ca689e419e05"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""df3b987e-a854-47b3-9e53-bd6f67ed7dc8"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
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
            m_Lara_Player_Move = m_Lara_Player.FindAction("Move", throwIfNotFound: true);
            // Dumpling
            m_Dumpling = asset.FindActionMap("Dumpling", throwIfNotFound: true);
            m_Dumpling_Newaction = m_Dumpling.FindAction("New action", throwIfNotFound: true);
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
        private readonly InputAction m_Lara_Player_Move;
        public struct Lara_PlayerActions
        {
            private @Basic_Controls m_Wrapper;
            public Lara_PlayerActions(@Basic_Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Look => m_Wrapper.m_Lara_Player_Look;
            public InputAction @Move => m_Wrapper.m_Lara_Player_Move;
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
                    @Move.started -= m_Wrapper.m_Lara_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_Lara_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_Lara_PlayerActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_Lara_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public Lara_PlayerActions @Lara_Player => new Lara_PlayerActions(this);

        // Dumpling
        private readonly InputActionMap m_Dumpling;
        private IDumplingActions m_DumplingActionsCallbackInterface;
        private readonly InputAction m_Dumpling_Newaction;
        public struct DumplingActions
        {
            private @Basic_Controls m_Wrapper;
            public DumplingActions(@Basic_Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Newaction => m_Wrapper.m_Dumpling_Newaction;
            public InputActionMap Get() { return m_Wrapper.m_Dumpling; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DumplingActions set) { return set.Get(); }
            public void SetCallbacks(IDumplingActions instance)
            {
                if (m_Wrapper.m_DumplingActionsCallbackInterface != null)
                {
                    @Newaction.started -= m_Wrapper.m_DumplingActionsCallbackInterface.OnNewaction;
                    @Newaction.performed -= m_Wrapper.m_DumplingActionsCallbackInterface.OnNewaction;
                    @Newaction.canceled -= m_Wrapper.m_DumplingActionsCallbackInterface.OnNewaction;
                }
                m_Wrapper.m_DumplingActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Newaction.started += instance.OnNewaction;
                    @Newaction.performed += instance.OnNewaction;
                    @Newaction.canceled += instance.OnNewaction;
                }
            }
        }
        public DumplingActions @Dumpling => new DumplingActions(this);
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
            void OnMove(InputAction.CallbackContext context);
        }
        public interface IDumplingActions
        {
            void OnNewaction(InputAction.CallbackContext context);
        }
    }
}
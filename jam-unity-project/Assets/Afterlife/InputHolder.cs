using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.DualShock;

namespace Assets.Afterlife
{
    public class InputHolder : MonoBehaviour
    {
        public static InputHolder Instance;
        public InputMaster Controls;

        private void Awake()
        {
            Instance = this;

            Controls.Player.MovementLeft.performed += MovementLeft_performed;
        }

        private void MovementLeft_performed(UnityEngine.Experimental.Input.InputAction.CallbackContext obj)
        {
            //var pos = obj.ReadValue<Vector2>();
            //Debug.Log($"X:{pos.x}, {pos.y}");


        }

        private void OnEnable()
        {
            Controls.Enable();
            var inputs = Gamepad.all;
            var newall = DualShockGamepad.all;
            var joySticks = Joystick.all;
        }

        private void Update()
        {
            
            //var gamepad = InputSystem.GetDevice<Gamepad>();
            //var pos = gamepad.leftStick.ReadValue();
            //Debug.Log($"X:{pos.x}, {pos.y}");

        }

        private void InputSystem_onDeviceChange(InputDevice arg1, InputDeviceChange arg2)
        {
            if (arg2 == InputDeviceChange.StateChanged)
                return;


            Debug.Log(arg1.name);
        }

        private void OnDisable()
        {
            Controls.Disable();
        }


    }
}

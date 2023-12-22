using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace JC_Click_V1
{
    // N: This class is responsible for handling low-level keyboard hooks.
    public class KeyboardHook
    {
        // N: Constants for low-level keyboard hook and key-down event.
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        // N: Delegate for the low-level keyboard procedure.
        private readonly LowLevelKeyboardProc _proc;
        // N: Hook ID for the keyboard hook.
        private static IntPtr _hookID = IntPtr.Zero;

        // N: Delegate definition for the low-level keyboard procedure.
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        // N: Event triggered when a key is pressed.
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        // N: Constructor initializes the keyboard hook by setting the hook procedure.
        public KeyboardHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        // N: Unhooks the keyboard hook when it's no longer needed.
        public void Unhook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        // N: Sets the low-level keyboard hook.
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        // N: Callback method for the low-level keyboard hook.
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                // N: Read the virtual key code.
                int vkCode = Marshal.ReadInt32(lParam);

                // N: Raise the KeyPressed event.
                KeyPressed?.Invoke(this, new KeyPressedEventArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #region Win32 API

        // N: External method declarations for Windows API functions related to keyboard hooks.

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }

    // N: Event arguments class for the KeyPressed event.
    public class KeyPressedEventArgs : EventArgs
    {
        // N: The Key that was pressed.
        public Key KeyPressed { get; private set; }

        // N: Constructor to initialize KeyPressedEventArgs with the pressed key.
        public KeyPressedEventArgs(Key key)
        {
            KeyPressed = key;
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace JC_Click_V1
{
    public class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private readonly LowLevelKeyboardProc _proc;
        private static IntPtr _hookID = IntPtr.Zero;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public KeyboardHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                // Raise the KeyPressed event
                KeyPressed?.Invoke(this, new KeyPressedEventArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #region Win32 API

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

    public class KeyPressedEventArgs : EventArgs
    {
        public Key KeyPressed { get; private set; }

        public KeyPressedEventArgs(Key key)
        {
            KeyPressed = key;
        }
    }
}

using System;
using System.Runtime.InteropServices;
using PInvoke;
using static PInvoke.User32;

namespace CSharpWin32HelloWorld
{
    unsafe class Program
    {
        static int Main(string[] args)
        {
            const string ClassName = "CSharpWin32HelloWorld";
            var hinstance = Marshal.GetHINSTANCE(typeof(Program).Module);
            ushort atom;

            fixed (char* className = ClassName)
            {
                var wc = new WNDCLASS
                {
                    lpfnWndProc = WindowProc,
                    hInstance = hinstance,
                    lpszClassName = className,
                };

                atom = RegisterClass(ref wc);

                if (atom == 0)
                    throw new Win32Exception();
            }

            var hwnd = CreateWindowEx(
                0,
                ClassName,
                "Hello World!",
                WindowStyles.WS_OVERLAPPEDWINDOW,
                CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
                IntPtr.Zero,
                IntPtr.Zero,
                hinstance,
                IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
                throw new Win32Exception();

            ShowWindow(hwnd, WindowShowStyle.SW_SHOWDEFAULT);

            var msg = new MSG();
            while (GetMessage(&msg, IntPtr.Zero, 0, 0) != 0)
            {
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }

            return 0;
        }

        private static IntPtr WindowProc(IntPtr hwnd, WindowMessage msg, void* wParam, void* lParam)
        {
            switch (msg)
            {
                case WindowMessage.WM_DESTROY:
                    PostQuitMessage(0);
                    return IntPtr.Zero;
            }

            return DefWindowProc(hwnd, msg, new IntPtr(wParam), new IntPtr(lParam));
        }
    }
}

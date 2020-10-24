using System;
using System.Runtime.InteropServices;
using System.Windows;

#if WPF
using System.Windows.Automation.Provider;
#else
using Avalonia.Win32.Interop.Automation;
#endif

namespace CSharpWin32HelloWorld
{
    internal class WindowProvider : MarshalByRefObject, IRawElementProviderFragmentRoot, IRawElementProviderFragment, IRawElementProviderSimple
    {
        private readonly IntPtr _hwnd;

        public WindowProvider(IntPtr hwnd)
        {
            _hwnd = hwnd;
            System.Diagnostics.Debug.WriteLine("Created WindowProvider");
        }

        public Rect BoundingRectangle => default;
        public IRawElementProviderFragmentRoot FragmentRoot => this;
        public ProviderOptions ProviderOptions => ProviderOptions.ServerSideProvider;

        [return: MarshalAs(UnmanagedType.IUnknown)]
        public virtual object GetPatternProvider(int patternId) => null;

        public virtual object GetPropertyValue(int propertyId)
        {
            if (propertyId == 30005)
            {
                System.Diagnostics.Debug.WriteLine("Get Name property");
                return "Mic check 12";
            }

            return null;
        }
         
        public IRawElementProviderSimple[] GetEmbeddedFragmentRoots() => null;

        public int[] GetRuntimeId()
        {
            throw new NotImplementedException();
        }

        public virtual IRawElementProviderFragment Navigate(NavigateDirection direction)
        {
            throw new NotImplementedException();
        }

        public void SetFocus()
        {
            throw new NotImplementedException();
        }

        public IRawElementProviderFragment ElementProviderFromPoint(double x, double y) => null;
        public IRawElementProviderFragment GetFocus() => null;

        public IRawElementProviderSimple HostRawElementProvider
        {
            get
            {
                var hr = UiaHostProviderFromHwnd(_hwnd, out var result);
                Marshal.ThrowExceptionForHR(hr);
                return result;
            }
        }

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaReturnRawElementProvider", CharSet = CharSet.Unicode)]
        public static extern IntPtr UiaReturnRawElementProvider(IntPtr hwnd, IntPtr wParam, IntPtr lParam, IRawElementProviderSimple el);

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaHostProviderFromHwnd", CharSet = CharSet.Unicode)]
        public static extern int UiaHostProviderFromHwnd(IntPtr hwnd, [MarshalAs(UnmanagedType.Interface)] out IRawElementProviderSimple provider);
    }
}

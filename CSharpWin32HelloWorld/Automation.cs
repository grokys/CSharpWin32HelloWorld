using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Win32.Interop.Automation;

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

        public ProviderOptions ProviderOptions => ProviderOptions.ServerSideProvider;

        [return: MarshalAs(UnmanagedType.IUnknown)]
        public virtual object GetPatternProvider(UiaPatternId patternId) => null;

        public virtual object GetPropertyValue(UiaPropertyId propertyId)
        {
            if (propertyId == UiaPropertyId.Name)
            {
                System.Diagnostics.Debug.WriteLine("Get Name property");
                return "Mic check 12";
            }

            return null;
        }
         
        public Rect BoundingRectangle => default;


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

        public IRawElementProviderFragmentRoot FragmentRoot => this;
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

        [DllImport("UIAutomationCore.dll", EntryPoint = "UiaHostProviderFromHwnd", CharSet = CharSet.Unicode)]
        public static extern int UiaHostProviderFromHwnd(IntPtr hwnd, [MarshalAs(UnmanagedType.Interface)] out IRawElementProviderSimple provider);
    }
}

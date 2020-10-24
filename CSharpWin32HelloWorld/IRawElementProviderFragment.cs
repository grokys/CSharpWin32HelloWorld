using System;
using System.Runtime.InteropServices;

namespace Avalonia.Win32.Interop.Automation
{
    [ComVisible(true)]
    [Guid("670c3006-bf4c-428b-8534-e1848f645122")]
    internal enum NavigateDirection
    {
        Parent,
        NextSibling,
        PreviousSibling,
        FirstChild,
        LastChild,
    }

    struct Rect
    {
        public double x;
        public double y;
        public double w;
        public double h;
    }

    [ComVisible(true)]
    [Guid("f7063da8-8359-439c-9297-bbc5299a7d87")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IRawElementProviderFragment
    {
        IRawElementProviderFragment Navigate(NavigateDirection direction);
        int[] GetRuntimeId();
        Rect BoundingRectangle { get; }
        IRawElementProviderSimple[] GetEmbeddedFragmentRoots();
        void SetFocus();
        IRawElementProviderFragmentRoot FragmentRoot { get; }
    }
}

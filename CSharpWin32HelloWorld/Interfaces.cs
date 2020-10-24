using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Avalonia.Win32.Interop.Automation
{
    [Flags]
    internal enum ProviderOptions
    {
        ClientSideProvider = 0x0001,
        ServerSideProvider = 0x0002,
        NonClientAreaProvider = 0x0004,
        OverrideProvider = 0x0008,
        ProviderOwnsSetFocus = 0x0010,
        UseComThreading = 0x0020
    }

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

    [ComVisible(true)]
    [Guid("d6dd68d1-86fd-4332-8666-9abedea2d24c")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IRawElementProviderSimple
    {
        ProviderOptions ProviderOptions { get; }
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetPatternProvider(int patternId);
        object GetPropertyValue(int propertyId);
        IRawElementProviderSimple HostRawElementProvider { get; }
    }

    [ComVisible(true)]
    [Guid("f7063da8-8359-439c-9297-bbc5299a7d87")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IRawElementProviderFragment : IRawElementProviderSimple
    {
        IRawElementProviderFragment Navigate(NavigateDirection direction);
        int[] GetRuntimeId();
        Rect BoundingRectangle { get; }
        IRawElementProviderSimple[] GetEmbeddedFragmentRoots();
        void SetFocus();
        IRawElementProviderFragmentRoot FragmentRoot { get; }
    }

    [ComVisible(true)]
    [Guid("620ce2a5-ab8f-40a9-86cb-de3c75599b58")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    internal interface IRawElementProviderFragmentRoot : IRawElementProviderFragment
    {
        IRawElementProviderFragment ElementProviderFromPoint(double x, double y);
        IRawElementProviderFragment GetFocus();
    }
}

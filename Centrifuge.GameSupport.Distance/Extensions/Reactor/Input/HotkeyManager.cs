using Reactor.API.Input;
using Reactor.Input;
using System;

public static class HotkeyManagerExtensions
{
    public static Hotkey BindHotkey(this HotkeyManager m, string hotkeyString, Action action, bool isOneTime = false)
    {
        Hotkey hotkey = new Hotkey(hotkeyString, isOneTime);

        m.Bind(hotkey, action);

        return hotkey;
    }

    public static void UnbindHotkey(this HotkeyManager m, Hotkey hotkey)
    {
        if (m.ActionHotkeys.ContainsKey(hotkey))
        {
            m.ActionHotkeys.Remove(hotkey);
        }
    }
}
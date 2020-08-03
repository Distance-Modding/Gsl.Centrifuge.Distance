using Reactor.API.Input;
using Reactor.API.Interfaces.Systems;
using Reactor.Input;
using System;

public static class HotkeyManagerExtensions
{
    public static Hotkey BindHotkey(this IHotkeyManager m, string hotkeyString, Action action, bool isOneTime = false)
    {
        Hotkey hotkey = new Hotkey(hotkeyString, isOneTime);

        m.Bind(hotkey, action);

        return hotkey;
    }

    public static void UnbindHotkey(this IHotkeyManager m, Hotkey hotkey)
    {
        if (m is HotkeyManager manager && manager.ActionHotkeys.ContainsKey(hotkey))
        {
            manager.ActionHotkeys.Remove(hotkey);
        }
    }
}
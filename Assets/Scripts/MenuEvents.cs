using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MenuEvents
{
    internal static event Action OnMainMenuOpened;
    internal static event Action OnSettingsMenuOpened;
    internal static event Action OnCreditsMenuOpened;

    internal static void RaiseMainMenuOpened()
    {
        OnMainMenuOpened?.Invoke();
    }

    internal static void RaiseSettingsMenuOpened()
    {
        OnSettingsMenuOpened?.Invoke();
    }

    internal static void RaiseCreditsOpened()
    {
        OnCreditsMenuOpened?.Invoke();
    }
}


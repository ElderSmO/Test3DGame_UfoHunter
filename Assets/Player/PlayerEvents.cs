
using System;

/// <summary>
/// События игрока
/// </summary>
public static class PlayerEvents
{
    public static event Action PlayerShoting;

    /// <summary>
    /// Игрок стреляет
    /// </summary>
    public static void PlayerShoted()
    {
        PlayerShoting?.Invoke();
    }
}

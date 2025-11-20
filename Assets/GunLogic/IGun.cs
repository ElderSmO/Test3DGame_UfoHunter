

using UnityEngine;

namespace Assets.GunLogic
{
    /// <summary>
    /// Интерфейс описывающий логику оружия
    /// </summary>
    public interface IGun
    {
        AudioClip ShootSound { get; set; }
        int Gun_id { get; set; }
        string name { get; set; }
        void Shoot();

    }
}

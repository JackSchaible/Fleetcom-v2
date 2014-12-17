using System.Collections.Generic;
using Fleetcom.Library.Graphics.Sprites;

namespace Fleetcom.Library.GameObjects.Ships
{
    internal interface IProjectile : IGameObject
    {
        Sprite Sprite { get; set; }
        bool ShouldRemove { get; set; }
    }
}

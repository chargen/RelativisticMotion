using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RelativisticMotion
{
    public interface IRelativisticObservable
    {
        /// <summary>
        /// Given the location of an observer in spacetime, locates all observable locations of this item in spacetime
        /// </summary>
        /// <param name="observer">The location of the observer in spacetime</param>
        /// <returns></returns>
        IEnumerable<Vector4> Observe(Vector4 observer);

        /// <summary>
        /// Indicates the location of this at a certain time
        /// </summary>
        /// <param name="location">A spacetime coordinate this occupies</param>
        void Move(Vector4 location);
    }
}

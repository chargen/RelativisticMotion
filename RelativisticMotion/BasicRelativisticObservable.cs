using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RelativisticMotion
{
    public class BasicRelativisticObservable
        :IRelativisticObservable
    {
        private readonly List<Vector4> _locations = new List<Vector4>();

        private readonly float _speedOfLight;

        public BasicRelativisticObservable(Vector4 spawnLocation, float speedOfLight)
        {
            Move(spawnLocation);
            _speedOfLight = speedOfLight;
        }

        /// <summary>
        /// Returns all the points within the light cone of this and the observer
        /// </summary>
        /// <param name="observer"></param>
        /// <returns>An enumeration of points obserable from the given spacetime location</returns>
        public IEnumerable<Vector4> Observe(Vector4 observer)
        {
            for (int i = _locations.Count - 1; i >= 0; i--)
            {
                if (!RelativisticHelpers.IsFasterThanLight(observer, _locations[i], _speedOfLight))
                    yield return _locations[i];
            }
        }

        /// <summary>
        /// Indicates a location in spacetime this occupies
        /// </summary>
        /// <param name="location"></param>
        public void Move(Vector4 location)
        {
            //Insert new location at appropriate index (ordered by time)
            bool inserted = false;
            for (int index = 0; index < _locations.Count; index++)
            {
                if (_locations[index].W > location.W)
                {
                    _locations.Insert(index, location);
                    AssertCausality(index);
                    inserted = true;
                }
            }

            if (!inserted)
            {
                _locations.Add(location);
                AssertCausality(_locations.Count - 1);
            }
        }

        private void AssertCausality(int index)
        {
            var location = _locations[index];

            if (index != 0 && RelativisticHelpers.IsFasterThanLight(_locations[index - 1], location, _speedOfLight))
                throw new CausalityViolationException("object moved faster than light");
            if (index != _locations.Count - 1 && RelativisticHelpers.IsFasterThanLight(_locations[index + 1], location, _speedOfLight))
                throw new CausalityViolationException("object moved faster than light");
        }
    }
}

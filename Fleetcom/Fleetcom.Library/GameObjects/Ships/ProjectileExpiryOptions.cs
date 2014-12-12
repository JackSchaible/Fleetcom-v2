using System;

namespace Fleetcom.Library.GameObjects.Ships
{
    public static class ProjectileExpiryOptions
    {
        public enum ProjectileExpiryTypes
        {
            Distance,
            Time,
            Collision,
            Offscreen,
            TimeAfterCollision
        }

        public abstract class ProjectileExpiryOption
        {
            protected virtual ProjectileExpiryTypes Type { get; set; }
            public virtual bool ShouldRemove { get; set; }
        }

        public class Distance : ProjectileExpiryOption
        {
            public float DistanceToRemoveAt { get; set; }
            public float CurrentDistance { get; set; }

            public override bool ShouldRemove
            {
                get { return CurrentDistance >= DistanceToRemoveAt; }
            }

            public Distance(float distanceToRemoveAt)
            {
                Type = ProjectileExpiryTypes.Distance;
                DistanceToRemoveAt = distanceToRemoveAt;
                CurrentDistance = 0;
            }
        }
        public class Time : ProjectileExpiryOption
        {
            public TimeSpan TimeToRemoveAt { get; set; }
            public TimeSpan CurrentTime { get; set; }
            public override bool ShouldRemove
            {
                get
                {
                    return CurrentTime >= TimeToRemoveAt;
                }
            }

            public Time(TimeSpan timeToRemoveAt)
            {
                Type = ProjectileExpiryTypes.Time;
                TimeToRemoveAt = timeToRemoveAt;
            }
        }
        public class Collision : ProjectileExpiryOption
        {

            public Collision(object collisionManager)
            {
                Type = ProjectileExpiryTypes.Collision;
                //Add to collision manager list
            }
        }
        public class Offscreen : ProjectileExpiryOption
        {
            public Offscreen()
            {
                Type = ProjectileExpiryTypes.Offscreen;

                //Get screen bounds
            }
        }
        public class TimeAfterCollision : ProjectileExpiryOption
        {
            public TimeSpan TimeAfterCollisionToRemoveAt { get; set; }

            public TimeAfterCollision(object collisionManager, TimeSpan timeAfterCollision)
            {
                Type = ProjectileExpiryTypes.TimeAfterCollision;
                TimeAfterCollisionToRemoveAt = timeAfterCollision;
            }
        }
    }
}

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
            public bool ShouldRemove { get; protected set; }

            protected virtual ProjectileExpiryTypes Type { get; set; }
        }

        public class Distance : ProjectileExpiryOption
        {
            public float DistanceToRemoveAt { get; set; }
            public float CurrentDistance { get; set; }

            public Distance(float distanceToRemoveAt)
            {
                Type = ProjectileExpiryTypes.Distance;
                DistanceToRemoveAt = distanceToRemoveAt;
                CurrentDistance = 0;
                ShouldRemove = false;
            }
 
            public void Update(float distance)
            {
                CurrentDistance += distance;

                if (CurrentDistance >= DistanceToRemoveAt)
                    ShouldRemove = true;
            }           
        }
        public class Time : ProjectileExpiryOption
        {
            public TimeSpan TimeToRemoveAt { get; set; }
            public TimeSpan CurrentTime { get; set; }

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

namespace Fleetcom.Library.GameComponents
{
    public static class Events
    {
        public delegate void StateChanged<in TEnum>(TEnum newState, TEnum oldState);
    }
}

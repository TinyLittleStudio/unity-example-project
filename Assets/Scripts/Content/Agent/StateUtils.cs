namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class StateUtils
    {
        public static bool IsPursuit(this State state)
        {
            return state == State.PURSUE || state == State.PURSUE_ATTACK;
        }
    }
}

namespace Quiz_Application
{
    public sealed class QuizTimer
    {
        public int TotalTime { get; set; }            // Total quiz time
        public int TimePerQuestion { get; set; }       // Time per question (depends on difficulty)
        public DateTime StartTime { get; private set; }

        public void StartTimer()
        {
            StartTime = DateTime.Now;
        }

        public int GetTimeElapsed()
        {
            return (int)(DateTime.Now - StartTime).TotalSeconds;
        }
    }
}

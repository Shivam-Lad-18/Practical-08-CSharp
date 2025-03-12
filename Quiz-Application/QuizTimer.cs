namespace Quiz_Application
{
    public sealed class QuizTimer
    {
        public int TotalTime { get; set; }            // Total quiz time (based on difficulty and number of questions)
        public int TimePerQuestion { get; set; }       // Time allowed for each question (varies by difficulty)
        public DateTime StartTime { get; private set; } // The time when the timer starts

        // Starts the quiz timer by recording the current time
        public void StartTimer()
        {
            StartTime = DateTime.Now;  // Set the start time to now
        }

        // Returns the total time elapsed since the timer started
        public int GetTimeElapsed()
        {
            // Calculate and return elapsed time in seconds
            return (int)(DateTime.Now - StartTime).TotalSeconds;
        }
    }
}

namespace Quiz_Application
{
    // Abstract base class for a question, defines common properties and methods for all questions
    public abstract class BaseQuestion
    {
        public string Text { get; set; }        // The question text
        public string[] Options { get; set; }   // Array of options (e.g., A, B, C, D)
        public string CorrectAnswer { get; set; } // The correct answer to the question

        // Abstract method to display the question, must be implemented by derived classes
        public abstract void DisplayQuestion();
    }
}

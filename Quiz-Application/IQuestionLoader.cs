namespace Quiz_Application
{
    // Interface to load questions based on count and difficulty level
    public interface IQuestionLoader
    {
        // Method to load a list of questions based on the specified count and difficulty
        List<BaseQuestion> LoadQuestions(int count, string difficulty);
    }
}

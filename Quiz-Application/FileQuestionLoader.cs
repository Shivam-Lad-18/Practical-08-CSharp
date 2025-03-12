namespace Quiz_Application
{
    // Class to load questions from a file (QuestionBank.txt) based on difficulty
    public class FileQuestionLoader : IQuestionLoader
    {
        public List<BaseQuestion> LoadQuestions(int count, string difficulty)
        {
            string path = "QuestionBank.txt"; // Path to the question bank file

            // Check if the question bank file exists
            if (!File.Exists(path))
                throw new FileNotFoundException("Question bank file not found!");

            // Read all lines from the file
            var allLines = File.ReadAllLines(path);
            var questions = new List<BaseQuestion>();

            // Iterate through each line in the question bank
            foreach (var line in allLines)
            {
                var parts = line.Split('|'); // Split the line into question components

                // If the line matches the selected difficulty, create a new question object
                if (parts[0].Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                {
                    questions.Add(new MCQQuestion
                    {
                        Text = parts[1], // Question text
                        Options = new[] { parts[2], parts[3], parts[4], parts[5] }, // Answer options
                        CorrectAnswer = parts[6] // Correct answer
                    });

                    // Stop adding questions if the required count is reached
                    if (questions.Count == count)
                        break;
                }
            }

            // If not enough questions were found, throw an exception
            if (questions.Count < count)
                throw new Exception("Not enough questions available for this difficulty.");

            return questions; // Return the list of questions
        }
    }
}

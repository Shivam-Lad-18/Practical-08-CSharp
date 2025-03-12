namespace Quiz_Application
{
    public class QuizManager
    {
        private readonly IQuestionLoader questionLoader;
        private List<BaseQuestion> questions;
        private QuizTimer timer;

        public QuizManager(IQuestionLoader loader)
        {
            this.questionLoader = loader;
            questions = new List<BaseQuestion>();
            timer = new QuizTimer();
        }

        public void StartQuiz(string difficulty, int questionCount)
        {
            try
            {
                // Load questions based on difficulty and count
                questions = questionLoader.LoadQuestions(questionCount, difficulty);

                // Set the timer according to difficulty
                SetTimerBasedOnDifficulty(difficulty, questionCount);

                // Start the timer
                timer.StartTimer();

                int correct = 0;
                int wrong = 0;

                for (int i = 0; i < questions.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"Question {i + 1}/{questions.Count}:");
                    Console.WriteLine($"\nTime per question: {timer.TimePerQuestion} seconds ({difficulty})\n");

                    var question = questions[i];
                    question.DisplayQuestion(); // Display the question and options

                    Console.Write("Your Answer: ");
                    string answer = ReadAnswerWithTimeout(timer.TimePerQuestion);

                    if (string.IsNullOrEmpty(answer))
                    {
                        Console.WriteLine("Time's up! No answer selected.");
                        wrong++;
                    }
                    else if (answer.ToUpper() == question.CorrectAnswer.ToUpper())
                    {
                        correct++; // Correct answer
                    }
                    else
                    {
                        wrong++; // Wrong answer
                    }
                }

                // Calculate total time taken
                int timeTaken = timer.GetTimeElapsed();

                // Display final results
                ShowResults(correct, wrong, timeTaken, questionCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void SetTimerBasedOnDifficulty(string difficulty, int questionCount)
        {
            // Set time per question based on difficulty level
            switch (difficulty.ToLower())
            {
                case "easy":
                    timer.TimePerQuestion = 10;  // Quick recall-type questions
                    break;
                case "medium":
                    timer.TimePerQuestion = 15;  // Slightly tricky questions
                    break;
                case "hard":
                    timer.TimePerQuestion = 20;  // Logical or multi-step questions
                    break;
                default:
                    throw new Exception("Invalid difficulty level!");
            }

            // Calculate total quiz time
            timer.TotalTime = timer.TimePerQuestion * questionCount;
        }

        private string ReadAnswerWithTimeout(int seconds)
        {
            string answer = string.Empty;
            DateTime deadline = DateTime.Now.AddSeconds(seconds);

            // Wait for user input within the given time
            while (DateTime.Now < deadline)
            {
                if (Console.KeyAvailable)
                {
                EnterOption:
                    answer = Console.ReadLine().Trim();

                    // Ensure valid input (A, B, C, or D)
                    if (answer.ToUpper() == "A" || answer.ToUpper() == "B" || answer.ToUpper() == "C" || answer.ToUpper() == "D")
                    {
                        return answer; // Valid answer within time
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter A, B, C, or D.");
                        goto EnterOption; // Ask again if input is invalid
                    }
                }
            }
            return ""; // Return empty if time runs out
        }

        public void ShowResults(int correct, int wrong, int timeTaken, int totalQuestions)
        {
            Console.Clear();
            Console.WriteLine("Quiz Complete!");
            Console.WriteLine($"Correct: {correct}");
            Console.WriteLine($"Wrong: {wrong}");
            Console.WriteLine($"Time Taken: {timeTaken} seconds");

            // Calculate and display final score
            double score = ((double)correct / totalQuestions) * 100;
            Console.WriteLine($"Final Score: {score:F2}% - {(score > 70 ? "Great Job!" : "Keep Practicing")}");
        }
    }
}

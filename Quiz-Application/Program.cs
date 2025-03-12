using Quiz_Application;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\nWelcome to QuizApp!\n");

        while (true)
        {
        getdifficuilty: // Label for retrying if input is invalid
            Console.WriteLine("Choose Difficulty Level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");
            Console.Write("Choice: ");

            string difficulty = Console.ReadLine();

            // Check if input is empty, ask again
            if (difficulty.Length == 0)
            {
                Console.WriteLine("Enter a valid difficulty.");
                goto getdifficuilty; // Go back to difficulty selection
            }

            difficulty = GetDifficulty(difficulty); // Convert numeric input to text difficulty

            Console.Write("How many questions do you want to attempt? (Max 20): ");

            // Convert input to integer (assumes user enters a valid number)
            int questionCount = int.Parse(Console.ReadLine());

            // Ensure question count is within valid limits
            if (questionCount > 20)
            {
                Console.WriteLine("You can attempt a maximum of 20 questions.");
                questionCount = 20; // Set max limit
            }
            else if (questionCount < 1)
            {
                Console.WriteLine("You have to attempt at least 1 question.");
                questionCount = 1; // Set minimum limit
            }

            // Load questions from a file and start the quiz
            var loader = new FileQuestionLoader();
            var quizManager = new QuizManager(loader);
            quizManager.StartQuiz(difficulty, questionCount);

            // Ask if the user wants to play again
            Console.WriteLine("Choose:");
            Console.WriteLine("1. Play Again");
            Console.WriteLine("2. Exit");

            string choice = Console.ReadLine();
            if (choice != "1") break; // Exit loop if choice is not 1
        }
    }

    // Converts user input (1, 2, 3) to difficulty level (Easy, Medium, Hard)
    static string GetDifficulty(string input)
    {
        return input switch
        {
            "1" => "Easy",
            "2" => "Medium",
            "3" => "Hard",
            _ => throw new ArgumentException("Invalid choice.") // Throw an error if input is incorrect
        };
    }
}

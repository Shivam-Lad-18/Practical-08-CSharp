using Quiz_Application;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to QuizApp!");

        while (true)
        {
            Console.WriteLine("Choose Difficulty Level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");
            Console.Write("Choice: ");
            string difficulty = GetDifficulty(Console.ReadLine());

            Console.Write("How many questions do you want to attempt? (Min 5 - Max 20): ");
            int questionCount = int.Parse(Console.ReadLine());

            var loader = new FileQuestionLoader();
            var quizManager = new QuizManager(loader);
            quizManager.StartQuiz(difficulty, questionCount);

            Console.WriteLine("Choose:");
            Console.WriteLine("1. Play Again");
            Console.WriteLine("2. Exit");

            string choice = Console.ReadLine();
            if (choice != "1") break;
        }
    }

    static string GetDifficulty(string input)
    {
        return input switch
        {
            "1" => "Easy",
            "2" => "Medium",
            "3" => "Hard",
            _ => throw new ArgumentException("Invalid choice.")
        };
    }
}

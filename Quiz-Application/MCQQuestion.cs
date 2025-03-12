namespace Quiz_Application
{
    public class MCQQuestion : BaseQuestion
    {
        // Override the base class method to display the multiple-choice question
        public override void DisplayQuestion()
        {
            // Display the question text
            Console.WriteLine(Text);

            // Display each option labeled A, B, C, D
            Console.WriteLine($"A. {Options[0]}");
            Console.WriteLine($"B. {Options[1]}");
            Console.WriteLine($"C. {Options[2]}");
            Console.WriteLine($"D. {Options[3]}");
        }
    }
}

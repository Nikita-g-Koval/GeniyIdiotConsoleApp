using System.Diagnostics;
using System.Xml.Linq;

namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        // получить вопросы
        static Question[] GetQuestions(int countQuestions)
        {
            Question[] questions = new Question[countQuestions];
            questions[0] = new Question("Сколько будет два плюс два умноженное на два?", 6);
            questions[1] = new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9);
            questions[2] = new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25);
            questions[3] = new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60);
            questions[4] = new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2);
            return questions;
        }

        // получить диагнозы
        static string[] GetDiagnoses()
        {
            string[] diagnoses = new string[6];
            diagnoses[0] = "кретин";
            diagnoses[1] = "идиот";
            diagnoses[2] = "дурак";
            diagnoses[3] = "нормальный";
            diagnoses[4] = "талант";
            diagnoses[5] = "гений";
            return diagnoses;
        }

        // перемешать массив
        static void Shuffle(Question[] questions)
        {
            Random random = new Random();
            for (int i = questions.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (questions[i], questions[j]) = (questions[j], questions[i]);
            }
        }

        // получить целочисленный ответ от пользователя с проверкой данных
        static int GetIntAnswer()
        {
            Console.WriteLine("Введите ответ(любое целочисленное число)");
            string inputAnswer = Console.ReadLine();

            int answer;

            while (true)
            {
                if (!int.TryParse(inputAnswer, out answer))
                {
                    Console.WriteLine("Вы ввели некорректные данные! Попробуйте снова! Примеры корректных данных(любое целочисленное число): \"3\", \"120\", \"51\".");
                }
                else
                    break;

                inputAnswer = Console.ReadLine();
            }

            return answer;
        }

        // задать вопросы
        static int RunTest(Question[] questions)
        {
            int countRightAnswers = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine("Вопрос №" + (i + 1));
                Console.WriteLine(questions[i].text);

                int userAnswer = GetIntAnswer();

                int rightAnswer = questions[i].answer;

                if (userAnswer == rightAnswer)
                {
                    countRightAnswers++;
                }
            }
            return countRightAnswers;
        }

        // повторить тест или нет
        static bool RepeatOrNot()
        {
            bool result = true;
            Console.WriteLine("Вы хотите повторить тест? (Введите \"Да\" или \"Нет\")");

            while (true)
            { 
                string userAnswer = Console.ReadLine();
                if (userAnswer == "Нет")
                {
                    result = false;
                    break;
                }
                else if (userAnswer == "Да")
                    break;

                Console.WriteLine($"Вы ввели некорректные данные! (Введите \"Да\" или \"Нет\")!");
            }

            return result;
        }

        // спросить у пользователя имя
        static string AskName()
        {
            string name;

            Console.WriteLine("Здравствуйте! Напишите ваше имя.");
            while (true)
            {
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    break;
                Console.WriteLine("Имя не должно быть пустым!");
            }

            return name;
        }

        static void Main(string[] args)
        {
            
            string name = AskName();

            int countQuestions = 5;
            Question[] questions = GetQuestions(countQuestions);

            bool repeat = true;

            while (repeat)
            {
                Shuffle(questions);

                int countRightAnswers = RunTest(questions);

                Console.WriteLine("Количество правильных ответов: " + countRightAnswers);

                string[] diagnoses = GetDiagnoses();

                Console.WriteLine($"{name}, ваш диагноз: {diagnoses[countRightAnswers]}");

                repeat = RepeatOrNot();
            }

        }
    }
}
﻿namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте, введите ваше имя! Длина имени не меньше двух символов. Можно использовать только латинский алфавит и цифры. Имя должно содержать хотя бы одну букву.");
            string userName = Console.ReadLine();
            while (!ValidateUserName(userName))
            {
                Console.WriteLine("Имя не соответствует требованиям! Попробуйте ввести другое имя.");
                userName = Console.ReadLine();
            }

            int countQuestions = 5;
            Question[] questions = GetQuestions(countQuestions);
            string[] diagnoses = GetDiagnoses();

            bool start = false;
            Console.WriteLine($"{userName}, хотите начать тест? Введите \"Да\" или \"Нет\".");
            int userAnswer = ValidateUserAnswer(Console.ReadLine());

            while (userAnswer == 0)
            {
                Console.WriteLine($"{userName}, вы ввели некорректные данные! Введите \"Да\" или \"Нет\".");
                userAnswer = ValidateUserAnswer(Console.ReadLine());
            }

            while (userAnswer == 1)
            {
                Console.WriteLine("Тест запущен. На каждый вопрос вводите целочисленный ответ. Примеры: \"2\", \"15\", \"-7\".");
                int rightAnswersCount = 0;
                Shuffle(questions);
                
                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine("Вопрос №" + (i + 1));
                    Console.WriteLine(questions[i].Text + "\n" + "Введите ответ.");

                    string input = Console.ReadLine();
                    while (!IsInteger(input))
                    {
                        Console.WriteLine($"{userName}, вы ввели некорректные данные! Введите целочисленное число! Примеры: \"2\", \"15\", \"-7\".");
                        input = Console.ReadLine();
                    }
                    userAnswer = Convert.ToInt32(input);
                    Console.WriteLine("Ответ принят.");

                    if (userAnswer == questions[i].Answer)
                        rightAnswersCount++;

                }

                // вывод количества правильных ответов и диагноза
                Console.WriteLine("Количество правильных ответов: " + rightAnswersCount);
                
                int diagnoseID = Diagnose(countQuestions, rightAnswersCount);
                Console.WriteLine($"{userName}, ваш диагноз: {diagnoses[diagnoseID]}");

                // запрос у пользователя на сохранение результатов теста
                Console.WriteLine("Хотите сохранить результаты теста? Введите \"Да\" или \"Нет\".");
                userAnswer = ValidateUserAnswer(Console.ReadLine());
                while (userAnswer == 0)
                {
                    Console.WriteLine($"{userName}, вы ввели некорректные данные! Введите \"Да\" или \"Нет\".");
                    userAnswer = ValidateUserAnswer(Console.ReadLine());
                }
                if (userAnswer == 1)
                {
                    SaveTestResult(userName, rightAnswersCount, diagnoses[diagnoseID]);
                    Console.WriteLine("Результаты теста успешно сохранены.");
                }

                // запрос у пользователя на просмотр результатов предыдущих тестов
                if (File.Exists(testResultPath))
                {
                    Console.WriteLine("Хотите посмотреть сохранённые результаты тестов? Введите \"Да\" или \"Нет\".");
                    userAnswer = ValidateUserAnswer(Console.ReadLine());
                    while (userAnswer == 0)
                    {
                        Console.WriteLine($"{userName}, вы ввели некорректные данные! Введите \"Да\" или \"Нет\".");
                        userAnswer = ValidateUserAnswer(Console.ReadLine());
                    }
                    if (userAnswer == 1)
                    {
                        string[] testResults = GetTestResults();
                        Console.WriteLine($"|| {"ФИО", -10} || {"кол-во правильных ответов", -25} || {"Диагноз", -10} ||");
                        for (int j = 0; j < testResults.Length; j++)
                        {
                            string[] temp = testResults[j].Split(", ");
                            Console.WriteLine($"|| {temp[0], -10} || {temp[1], -25} || {temp[2], -10} ||");
                        }
                    }
                }

                // запрос у пользователя на повтор теста
                Console.WriteLine($"{userName}, хотите повторить тест? Введите \"Да\" или \"Нет\".");
                userAnswer = ValidateUserAnswer(Console.ReadLine());
                while (userAnswer == 0)
                {
                    Console.WriteLine($"{userName}, вы ввели некорректные данные! Введите \"Да\" или \"Нет\".");
                    userAnswer = ValidateUserAnswer(Console.ReadLine());
                }
            }
        }

        // путь к файлу с результатами теста
        static string testResultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testResult.txt");

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

        // вычислить диагноз
        static int Diagnose(int countQuestions, int countRightAnswers)
        {
            int rightAnswersProcent = countRightAnswers * 100 / countQuestions;
            int result = 0;

            switch (rightAnswersProcent)
            {
                case >= 47 and < 50:
                    result = 1;
                    break;
                case >= 50 and < 65:
                    result = 2;
                    break;
                case >= 65 and < 75:
                    result = 3;
                    break;
                case >= 75 and < 85:
                    result = 4;
                    break;
                case >=85:
                    result = 5;
                    break;
            }

            return result;
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

        // проверка строки на целочисленное число
        static bool IsInteger(string input)
        {
            int temp;

            return int.TryParse(input, out temp);
        }

        // возвращает 1, если ответ "Да"; -1, если ответ "Нет"; 0, если ни "Да", ни "Нет"
        static int ValidateUserAnswer(string userAnswer)
        {
            int result = 0;

            if (userAnswer.ToLower() == "да")
                result = 1;
            else if (userAnswer.ToLower() == "нет")
                result = -1;

            return result;
        }

        // проверка имени пользователя
        static bool ValidateUserName(string userName)
        {
            if (userName.Any(x => !char.IsLetterOrDigit(x)) || !userName.Any(char.IsLetter) || userName.Length < 2)
                return false;
            return true;
        }
        
        // сохранение результатов текста в файл
        static void SaveTestResult(string userName, int rightAnswersCount, string diagnose)
        {
            FileInfo fileInfo = new FileInfo(testResultPath);
            string testResult = $"{userName}, {rightAnswersCount}, {diagnose}";

            if (!fileInfo.Exists)
            {
                using (StreamWriter sw = fileInfo.CreateText())
                {
                    sw.WriteLine(testResult);
                }
            }
            else
            {
                using (StreamWriter sw = fileInfo.AppendText())
                {
                    sw.WriteLine(testResult);
                }
            }
        }

        // возвращает содержимое файла с результатами теста, если файла не существует возвращает null
        static string[] GetTestResults()
        {
            if (File.Exists(testResultPath))
            {
                return File.ReadAllLines(testResultPath);
            }
            return null;
        }
    }
}
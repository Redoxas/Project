using System;

class Triangle
{
    private double sideA, sideB, sideC;

    // Конструктор
    public Triangle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Стороны треугольника должны быть положительными числами.");

        sideA = a;
        sideB = b;
        sideC = c;
    }

    // Деструктор
    ~Triangle()
    {
        Console.WriteLine("Объект треугольника удален.");
    }

    // Метод для вычисления площади треугольника
    public double Area()
    {
        double s = (sideA + sideB + sideC) / 2;
        return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
    }

    // Перегрузка оператора преобразования в вещественное число
    public static implicit operator double(Triangle triangle)
    {
        return triangle.Area();
    }

    // Метод для проверки включения точки в треугольник
    public bool ContainsPoint(double x, double y)
    {
        double denominator = ((sideB - sideC) * (sideA - sideC) + sideC * sideC - sideB * sideB);
        double a = ((sideB - sideC) * (x - sideC) + (sideC * sideC - sideB * sideB) * (y - sideC)) / denominator;
        double b = ((sideC - sideA) * (x - sideC) + (sideA * sideA - sideC * sideC) * (y - sideC)) / denominator;
        double c = 1 - a - b;

        return a >= 0 && a <= 1 && b >= 0 && b <= 1 && c >= 0 && c <= 1;
    }

    // Перегрузка метода ToString для преобразования в символьную строку
    public override string ToString()
    {
        return $"Треугольник со сторонами: {sideA}, {sideB}, {sideC}";
    }

    // Метод для получения объекта-треугольника из строки
    public static Triangle Parse(string input)
    {
        string[] values = input.Split(',');
        if (values.Length != 3)
            throw new FormatException("Строка должна содержать три значения для сторон треугольника.");

        if (!double.TryParse(values[0], out double a) || !double.TryParse(values[1], out double b) || !double.TryParse(values[2], out double c))
            throw new FormatException("Неверный формат чисел.");

        return new Triangle(a, b, c);
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                // Ввод данных для первого треугольника
                Console.WriteLine("Введите стороны первого треугольника через запятую:");
                string input = Console.ReadLine();
                Triangle triangle1 = Triangle.Parse(input);
                Console.WriteLine($"Площадь первого треугольника: {triangle1.Area():F2}");

                // Ввод данных для второго треугольника
                Console.WriteLine("Введите стороны второго треугольника через запятую:");
                input = Console.ReadLine();
                Triangle triangle2 = Triangle.Parse(input);
                Console.WriteLine($"Площадь второго треугольника: {triangle2.Area():F2}");

                // Вывод данных о треугольниках
                Console.WriteLine($"Первый треугольник: {triangle1}");
                Console.WriteLine($"Второй треугольник: {triangle2}");

                // Сравнение треугольников по площади
                if (triangle1 > triangle2)
                    Console.WriteLine("Первый треугольник больше по площади.");
                else if (triangle1 < triangle2)
                    Console.WriteLine("Второй треугольник больше по площади.");
                else
                    Console.WriteLine("Треугольники равны по площади.");

                // Проверка включения точки в треугольники
                Console.WriteLine("Введите координаты точки для проверки включения в треугольники (через запятую):");
                input = Console.ReadLine();
                string[] pointCoords = input.Split(',');
                if (pointCoords.Length != 2)
                {
                    Console.WriteLine("Неверный формат ввода координат точки.");
                    continue;
                }
                if (!double.TryParse(pointCoords[0], out double x) || !double.TryParse(pointCoords[1], out double y))
                {
                    Console.WriteLine("Неверный формат чисел для координат точки.");
                    continue;
                }

                if (triangle1.ContainsPoint(x, y))
                    Console.WriteLine($"Точка ({x}, {y}) включена в первый треугольник.");
                else
                    Console.WriteLine($"Точка ({x}, {y}) не включена в первый треугольник.");

                if (triangle2.ContainsPoint(x, y))
                    Console.WriteLine($"Точка ({x}, {y}) включена во второй треугольник.");
                else
                    Console.WriteLine($"Точка ({x}, {y}) не включена во второй треугольник.");

                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine("Попробуйте еще раз.");
            }
        }
    }
}

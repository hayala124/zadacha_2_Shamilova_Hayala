using System.Collections.ObjectModel;

namespace zadacha_2_Shamilova_Hayala
{
    class Program
    {
        static void Main(string[] args)
        {
            var valuesOfRowOnChessboard = new Dictionary<char, int>()
            {
                {'a', 1},
                {'b', 2},
                {'c', 3},
                {'d', 4},
                {'e', 5},
                {'f', 6},
                {'g', 7},
                {'h', 8},
            };

            var readOnlyValuesOfRowOnChessboard = new ReadOnlyDictionary<char, int>(valuesOfRowOnChessboard);
            Console.WriteLine("Правило: Необходимо определить бьет ли слон фигуру, стоящую на другой указанной клетке за один ход!");

            var input = " ";
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            bool rightFormatCoordinates = true;
            bool rightFormatCoordinateX = true;
            bool rightFormatCoordinateY = true;
            char[] symbols = new char[5];

            do
            {
                Console.WriteLine("\nВведите координаты ладьи x1y1 и координаты фигуры x2y2");
                input = Console.ReadLine();
                symbols = input.ToCharArray();
                rightFormatCoordinates = GetResultOfCheckForInputCoordinateFormat(symbols, rightFormatCoordinates);

                try
                {
                    var keyX1 = CheckTypesCoordinatesX(symbols[0], readOnlyValuesOfRowOnChessboard);
                    // Извлечение из строки, введенной пользователем точки x1.

                    if (keyX1.HasValue)
                    {
                        x1 = keyX1.Value.value;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка! Введите корректную координату x1!");
                        rightFormatCoordinateX = true;
                        continue;
                    }

                    var keyX2 = CheckTypesCoordinatesX(symbols[3], readOnlyValuesOfRowOnChessboard);
                    // Извлечение из строки, введенной пользователем точки x2.
                    if (keyX2.HasValue)
                    {
                        x2 = keyX2.Value.value;
                        rightFormatCoordinateX = false;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка! Введите корректную координату x2!");
                        rightFormatCoordinateX = true;
                        continue;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    continue;
                }
                rightFormatCoordinateY = CheckTypesCoordinatesY(symbols, rightFormatCoordinateY);
                if (rightFormatCoordinateY != true)
                {
                    y1 = int.Parse(symbols[1].ToString());
                    y2 = int.Parse(symbols[4].ToString());
                }
                else
                    continue;

            }
            while (rightFormatCoordinates || rightFormatCoordinateX || rightFormatCoordinateY);

            GetResultOfGame(x1, x2, y1, y2);
        }

        static public void GetResultOfGame(int x1, int x2, int y1, int y2)
        {
            if (Math.Abs(x1-x2) == Math.Abs(y1-y2))
            {
                Console.WriteLine("Слон сможет побить фигуру");
            }
            else
            {
                Console.WriteLine("Слон не сможет побить фигуру");
            }
        }
        // Проверка на правильный ввод пользователем формата для координаты слона и фигуры.
        static public bool GetResultOfCheckForInputCoordinateFormat(char[] symbols, bool rightCoordinates)
        {
            try
            {
                if (symbols[2] != ' ')
                {
                    Console.WriteLine("Ошибка! Введите координаты через пробел!");
                }
                else if (symbols.Length != 5)
                {
                    Console.WriteLine("Ошибка! Введено неправильное количество символов!");
                }
                else
                {
                    rightCoordinates = false;
                }
            }
            catch
            {
                rightCoordinates = true;
            }
            return rightCoordinates;
        }

        // Проверка на правильный ввод координаты х пользователем.
        public static (char xKey, int value)? CheckTypesCoordinatesX(char symbols, ReadOnlyDictionary<char, int> dictionary)
        {
            if (dictionary.TryGetValue(symbols, out int value))
            {
                return (symbols, value);
            }
            return null;
        }

        // Проверка на правильный ввод пользователем типа даных координаты y и её границы.
        static public bool CheckTypesCoordinatesY(char[] symbols, bool rightCoordinatesY)
        {
            try
            {
                int y1 = int.Parse(symbols[1].ToString());
                int y2 = int.Parse(symbols[4].ToString());

                if ((y1 is int) && (y2 is int))
                {
                    if (!((y1 > 0) && (y1 <= 8) && (y2 > 0) && (y2 <= 8)))
                    {
                        Console.WriteLine("Ошибка! Введите координату y от 1 до 8!");
                    }
                    else
                    {
                        rightCoordinatesY = false;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Ошибка! Введите корректные координаты y!");
                rightCoordinatesY = true;
            }
            return rightCoordinatesY;
        }
    }
}
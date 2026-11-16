using System.Text;
using System.Text.Json;

namespace Task6;

internal class Program
{
    private static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

        try
        {
            GameSettings settings;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                GameSettings? loaded = JsonSerializer.Deserialize<GameSettings>(json);

                if (loaded == null || string.IsNullOrWhiteSpace(loaded.PlayerName)
                    || loaded.Difficulty < 1 || loaded.Difficulty > 3
                    || loaded.MusicVolume < 0 || loaded.MusicVolume > 100)
                {
                    Console.WriteLine("В файле сохранены некорректные настройки.");
                    return;
                }

                settings = loaded;
            }
            else
            {
                Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");

                settings = new GameSettings
                {
                    PlayerName = ReadName(),
                    Difficulty = ReadNumber("Сложность (1 — легко, 2 — нормально, 3 — сложно): ", 1, 3),
                    MusicVolume = ReadNumber("Громкость музыки (0–100): ", 0, 100)
                };

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(path, json);
            }

            Console.WriteLine("=== Текущие настройки ===");
            Console.WriteLine($"Имя игрока: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");
        }
        catch (JsonException)
        {
            Console.WriteLine("Не удалось прочитать настройки: файл содержит некорректный JSON.");
        }
        catch (IOException exception)
        {
            Console.WriteLine($"Ошибка работы с файлом: {exception.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Нет доступа к файлу настроек.");
        }
    }

    private static string ReadName()
    {
        while (true)
        {
            Console.Write("Имя игрока: ");
            string name = ReadLine().Trim();

            if (name.Length > 0)
            {
                return name;
            }

            Console.WriteLine("Имя не должно быть пустым.");
        }
    }

    private static int ReadNumber(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = ReadLine();

            if (int.TryParse(input, out int number) && number >= min && number <= max)
            {
                return number;
            }

            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }

    private static string ReadLine()
    {
        return Console.ReadLine() ?? throw new IOException("Ввод завершён до заполнения настроек.");
    }
}

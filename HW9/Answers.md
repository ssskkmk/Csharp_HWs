# Домашнее задание 9

## Задача 1. Анализ JSON-сериализации

### а)

Результат сериализации с настройками по умолчанию:

```json
{"Name":"Alex","Score":100}
```

- `Name` попадёт в JSON: это публичное свойство с публичным геттером.
- `Score` попадёт по той же причине.
- `secretCode` не попадёт: это закрытое поле, которое не включено в сериализацию явно.
- `IsAlive` не попадёт: это поле, а не свойство. Даже публичные поля по умолчанию не сериализуются.

### б)

В .NET 8 и новее можно пометить закрытое поле атрибутом `[JsonInclude]`. При обычном вызове `JsonSerializer.Serialize` используется сериализация через отражение, и поле будет включено в JSON, сохраняя модификатор `private`.

```csharp
using System.Text.Json.Serialization;

public class Player
{
    public string Name { get; set; } = string.Empty;
    public int Score { get; set; }

    [JsonInclude]
    private int secretCode = 42;

    public bool IsAlive;
}
```

Для более старых версий можно оставить поле закрытым и добавить публичное свойство для чтения:

```csharp
private int secretCode = 42;

[JsonPropertyName("secretCode")]
public int SecretCode => secretCode;
```

Тогда сериализатор читает свойство, а поле остаётся `private`. Атрибут задаёт имя ключа `secretCode`.

### в)

Два способа включить публичное поле `IsAlive`:

1. Включить сериализацию публичных полей в настройках:

```csharp
JsonSerializerOptions options = new JsonSerializerOptions
{
    IncludeFields = true
};

string json = JsonSerializer.Serialize(p, options);
```

2. Пометить конкретное поле атрибутом:

```csharp
[JsonInclude]
public bool IsAlive;
```

В первом случае настройка применяется ко всем публичным полям, а во втором явно выбирается одно поле. Ещё можно заменить поле на свойство `public bool IsAlive { get; set; }`.

Справка: [сериализация полей](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/fields), [поддержка непубличных членов в .NET 8](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/immutability).

## Задача 2. Восстановление объекта из JSON

### а)

```csharp
public class Game
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; } = Array.Empty<string>();
    public bool IsMultiplayer { get; set; }
}
```

`Title` хранит строку, `Year` - целое число, `Rating` - дробное, `Genres` - массив строк, а `IsMultiplayer` - логическое значение. Все свойства публичные и имеют сеттеры, поэтому сериализатор может записать в них значения.

```csharp
using System.Text.Json;

Game? game = JsonSerializer.Deserialize<Game>(json);
```

### б)

Обычный вызов `JsonSerializer.Deserialize` по умолчанию сопоставляет имена с учётом регистра, поэтому `title` не совпадает с `Title`. Чтобы сопоставление не зависело от регистра:

```csharp
JsonSerializerOptions options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

Game? game = JsonSerializer.Deserialize<Game>(json, options);
```

В классе из пункта а) при несовпадении имени останется начальное значение `string.Empty`. Если свойство не инициализировать, как в условии, останется `null`.

### в)

```json
[1, "hello", true, null]
```

Это валидный JSON: элементы массива могут иметь разные типы. Его можно десериализовать в `JsonElement[]`:

```csharp
JsonElement[]? items = JsonSerializer.Deserialize<JsonElement[]>(json);

if (items != null)
{
    int number = items[0].GetInt32();
    string? text = items[1].GetString();
    bool flag = items[2].GetBoolean();
    bool isNull = items[3].ValueKind == JsonValueKind.Null;
}
```

Тип каждого элемента определяется через `ValueKind`. Также подойдёт `object?[]`: при стандартных настройках числа, строки и логические значения будут представлены объектами `JsonElement`, а JSON `null` станет C# `null`.

## Задача 3. File vs FileStream

1. **Небольшой конфигурационный файл:** `File.ReadAllText` сразу прочитает весь текст. Для десяти строк это удобно и не требует ручного открытия и закрытия потока.
2. **Автосохранение прогресса:** `File.WriteAllText` подходит для записи небольшого JSON-сохранения целиком при каждом автосохранении. Если сохранения станут очень большими или понадобится управление записью по частям, лучше использовать `FileStream`.
3. **Серверный лог:** `FileStream` в режиме `FileMode.Append` вместе со `StreamWriter`. Поток можно держать открытым и дописывать строки, не загружая многогигабайтный файл в память и не открывая его заново для каждой записи; нужно своевременно сбрасывать буфер и закрыть поток при завершении.
4. **Проверка и чтение сохранения:** `File.Exists`, затем `File.ReadAllText`. Эти методы прямо соответствуют задаче; при этом между проверкой и чтением файл может исчезнуть, поэтому в реальном приложении также нужна обработка ошибок чтения.
5. **Замена десяти байт внутри большого файла:** `FileStream` с `FileMode.Open` и `FileAccess.Write`. Через `Seek` можно перейти к нужному смещению и записать десять байт, не читая и не перезаписывая весь файл.

## Задачи 4-6

Решения находятся в проектах `Task4`, `Task5` и `Task6`. Команды запуска приведены в [README.md](README.md).

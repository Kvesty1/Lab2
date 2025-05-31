using System;
using System.Collections.Generic;

// Класс для электронного документа
public abstract class ElectronicDocument
{
    public string Name { get; set; }
    public string Author { get; set; }
    public List<string> Keywords { get; set; }
    public string Theme { get; set; }
    public string FilePath { get; set; }

    public ElectronicDocument()
    {
        Keywords = new List<string>();
    }

    public virtual string GetDocumentInfo()
    {
        return $"Имя: {Name}\nАвтор: {Author}\nКлючевые слова: {string.Join(", ", Keywords)}\nТематика: {Theme}\nПуть к файлу: {FilePath}";
    }
}

// Класс для документов MS Word
public class WordDocument : ElectronicDocument
{
    public int PageCount { get; set; }

    public override string GetDocumentInfo()
    {
        return base.GetDocumentInfo() + $"\nКоличество страниц: {PageCount}\nТип: MS Word";
    }
}

// Класс для PDF документов
public class PdfDocument : ElectronicDocument
{
    public bool IsProtected { get; set; }

    public override string GetDocumentInfo()
    {
        return base.GetDocumentInfo() + $"\nЗащищен: {IsProtected}\nТип: PDF";
    }
}

// Класс для документов MS Excel
public class ExcelDocument : ElectronicDocument
{
    public int SheetCount { get; set; }

    public override string GetDocumentInfo()
    {
        return base.GetDocumentInfo() + $"\nКоличество листов: {SheetCount}\nТип: MS Excel";
    }
}

// Класс для TXT документов
public class TxtDocument : ElectronicDocument
{
    public string Encoding { get; set; }

    public override string GetDocumentInfo()
    {
        return base.GetDocumentInfo() + $"\nКодировка: {Encoding}\nТип: TXT";
    }
}

// Класс для HTML документов
public class HtmlDocument : ElectronicDocument
{
    public string Version { get; set; }

    public override string GetDocumentInfo()
    {
        return base.GetDocumentInfo() + $"\nВерсия HTML: {Version}\nТип: HTML";
    }
}

// Singleton класс для работы с документами
public sealed class DocumentManager
{
    // Приватное статическое поле для хранения единственного экземпляра класса
    private static DocumentManager _instance;

    // Список всех добавленных электронных документов
    private List<ElectronicDocument> _documents;

    // Приватный конструктор запрещает создание экземпляров извне
    private DocumentManager()
    {
        _documents = new List<ElectronicDocument>();
    }

    // Публичное статическое свойство для получения единственного экземпляра класса (Singleton)
    public static DocumentManager Instance
    {
        get
        {
            // Если экземпляр не создан, то его создаём
            if (_instance == null)
            {
                _instance = new DocumentManager();
            }
            return _instance;
        }
    }

    // Метод добавления нового документа в список
    public void AddDocument(ElectronicDocument document)
    {
        _documents.Add(document);
    }

    // Метод отображения всех документов в списке
    public void DisplayAllDocuments()
    {
        Console.WriteLine("\nВсе документы:");
        for (int i = 0; i < _documents.Count; i++)
        {
            Console.WriteLine($"\nДокумент #{i + 1}");
            Console.WriteLine(_documents[i].GetDocumentInfo());
        }
    }

    // Метод отображения информации о документе по индексу
    public void DisplayDocumentInfo(int index)
    {
        // Проверка индекса
        if (index >= 0 && index < _documents.Count)
        {
            Console.WriteLine($"\nИнформация о документе #{index + 1}");
            Console.WriteLine(_documents[index].GetDocumentInfo());
        }
        else
        {
            Console.WriteLine("Неверный номер документа! Введите еще раз");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Получаем экземпляр DocumentManager
        DocumentManager docManager = DocumentManager.Instance;

        // Добавляем документы всех типов
        docManager.AddDocument(new WordDocument
        {
            Name = "Отчет.docx",
            Author = "Иванов И.И.",
            Keywords = new List<string> { "отчет", "квартальный", "финансы" },
            Theme = "Финансы",
            FilePath = @"C:\Documents\Отчет.docx",
            PageCount = 7
        });

        docManager.AddDocument(new PdfDocument
        {
            Name = "Руководство.pdf",
            Author = "Компания ООО",
            Keywords = new List<string> { "руководство", "инструкция", "помощь" },
            Theme = "Документация",
            FilePath = @"C:\Documents\Руководство.pdf",
            IsProtected = true
        });

        docManager.AddDocument(new ExcelDocument
        {
            Name = "Данные.xlsx",
            Author = "Петрова А.С.",
            Keywords = new List<string> { "данные", "анализ", "2023" },
            Theme = "Статистика",
            FilePath = @"C:\Documents\Данные.xlsx",
            SheetCount = 3
        });

        docManager.AddDocument(new TxtDocument
        {
            Name = "Заметки.txt",
            Author = "Сидоров В.В.",
            Keywords = new List<string> { "заметки", "идеи", "разработка" },
            Theme = "Личное",
            FilePath = @"C:\Documents\Заметки.txt",
            Encoding = "UTF-8"
        });

        docManager.AddDocument(new HtmlDocument
        {
            Name = "Сайт.html",
            Author = "WebStudio",
            Keywords = new List<string> { "html", "веб", "страница" },
            Theme = "Разработка",
            FilePath = @"C:\Documents\Сайт.html",
            Version = "HTML5"
        });

        // Меню взаимодействия
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nМенеджер документов:");
            Console.WriteLine("1. Показать все документы");
            Console.WriteLine("2. Показать информацию о конкретном документе");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    docManager.DisplayAllDocuments();
                    break;
                case "2":
                    Console.Write("Введите номер документа: ");
                    if (int.TryParse(Console.ReadLine(), out int docNumber))
                    {
                        docManager.DisplayDocumentInfo(docNumber - 1);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода! Введите корректный номер.");
                    }
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор! Попробуйте снова.");
                    break;
            }
        }
    }
}
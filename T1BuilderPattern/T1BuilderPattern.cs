using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace T1BuilderPattern
{
    public class Document //общедоступный класс, ориг файл статьи
    {
        public string Heading { get; set; }
        public List<string> Authors { get; set; }
        public string Text { get; set; }
        public string Hash { get; set; }
        public bool flagvalidsha256 { get; set; }
    }

    public class DocumentBuilder //общедоступный класс, строитель
    {
        private Document Heading { get; set; }

        public DocumentBuilder()
        {
            Heading = new Document();
        }

        public DocumentBuilder SetHeading(string title)
        {
            Heading.Heading = title.Trim();
            return this;
        }

        public DocumentBuilder SetAuthors(string authorsLine)
        {
            var authors = new List<string>();
            foreach (var author in authorsLine.Split(';'))
            {
                authors.Add(author.Trim());
            }
            Heading.Authors = authors;
            return this;
        }

        public DocumentBuilder SetBody(string text)
        {
            Heading.Text = text.Trim();
            return this;
        }

        public DocumentBuilder SetHash(string hash)
        {
            Heading.Hash = hash.Trim();
            return this;
        }

        public Document Build()
        {
            return Heading;
        }
    }

    public class DocumentProcessor //общедоступный класс, распорядитель
    {
        public Document ParseText(string filePath)
        {
            string[] filelines = File.ReadAllLines(filePath, Encoding.UTF8);
            DocumentBuilder builder = new DocumentBuilder();

            int line_number = 0;

            string text = string.Empty;

            foreach (string line in filelines)
            {
                line_number++;
                if (line_number == 1)
                {
                    builder.SetHeading(line);
                }
                else if (line_number == 2)
                {
                    builder.SetAuthors(line);
                }
                else if (line_number == filelines.Length)
                {
                    builder.SetHash(line);
                    builder.SetBody(text);
                }
                else
                {
                    text = text + line + "\n";
                }
            }

            return builder.Build();
        }
    }

    public static class HashValidator //валидатор хэша, sha256
    {
        public static bool Verify(Document article)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] documentBytes = Encoding.UTF8.GetBytes(article.Text);
                byte[] hashBytes = sha256.ComputeHash(documentBytes);
                string calculatedHash = BitConverter.ToString(hashBytes)
                            .Replace("-", "")
                            .ToLower();
                return calculatedHash == article.Hash;
            }
        }
    }

    internal class DocumentConverterJSON
    {
        static void Main(string[] args)
        {
            string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            string dataDir = Path.Combine(solutionDir, "InOut");

            string inFile = Path.Combine(dataDir, "article_Gukov.txt");
            string outFile = Path.Combine(dataDir, "article_Gukov.json");

            var parser = new DocumentProcessor();
            var document = parser.ParseText(inFile);
            document.flagvalidsha256 = HashValidator.Verify(document);

            var settings = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(document, settings);

            File.WriteAllText(outFile, json, Encoding.UTF8);

            Console.WriteLine("Исходный файл сохранён в формате json-а стейтема");
        }
    }
}

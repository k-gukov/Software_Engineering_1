using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2AbstractFactoryPattern
{
    internal class FilmDistr
    {
        abstract class AudioContent //абстрактный класс аудиодорожки
        {
            public abstract string GetAudioDescr();
        }
        abstract class Subtitles //абстрактный класс субтитров
        {
            public abstract string GetSubtitleDescr();
        }
        class RussianAudio : AudioContent //класс русской дорожки
        {
            public override string GetAudioDescr() => "Аудиодорожка: Русский";
        }
        class RussianSubtitles : Subtitles //класс русских субтитров
        {
            public override string GetSubtitleDescr() => "Субтитры: Русский";
        }
        class EnglishAudio : AudioContent //класс английской дорожки
        {
            public override string GetAudioDescr() => "Audio Track: English";
        }
        class EnglishSubtitles : Subtitles //класс английских субтитров
        {
            public override string GetSubtitleDescr() => "Subtitles: English";
        }

        class GermanyAudio : AudioContent //класс немецкой дорожки
        {
            public override string GetAudioDescr() => "Tonspur: Deutsch";
        }
        class GermanySubtitles : Subtitles //класс немецких субтитров
        {
            public override string GetSubtitleDescr() => "Untertitel: Deutsch";
        }
        abstract class ContentFactory //абстрактный класс фабрики
        {
            public abstract AudioContent CreateAudioContent();
            public abstract Subtitles CreateSubtitles();
        }
        class RussianContentFactory : ContentFactory //фабрика создания фильма с русской аудиодорожкой и русскими субтитрами
        {
            public override AudioContent CreateAudioContent() => new RussianAudio();
            public override Subtitles CreateSubtitles() => new RussianSubtitles();
        }
        class EnglishContentFactory : ContentFactory //фабрика создания фильма с английской аудиодорожкой и английскими субтитрами
        {
            public override AudioContent CreateAudioContent() => new EnglishAudio();
            public override Subtitles CreateSubtitles() => new EnglishSubtitles();
        }

        class GermanyContentFactory : ContentFactory //фабрика создания фильма с немецкой аудиодорожкой и немецкими субтитрами
        {
            public override AudioContent CreateAudioContent() => new GermanyAudio();
            public override Subtitles CreateSubtitles() => new GermanySubtitles();
        }
        class Content
        {
            private AudioContent audiocontent;
            private Subtitles subtitles;
            public Content(ContentFactory factory)
            {
                audiocontent = factory.CreateAudioContent();
                subtitles = factory.CreateSubtitles();
            }
            public void ShowDescr()
            {
                Console.WriteLine(audiocontent.GetAudioDescr());
                Console.WriteLine(subtitles.GetSubtitleDescr());
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в систему 'Кинопрокат'!");
            Console.WriteLine("Выберите язык: 1 - Русский, 2 - Английский, 3 - Немецкий");

            string choice = Console.ReadLine();
            ContentFactory factory;

            if (choice == "1")
                factory = new RussianContentFactory();
            else if (choice == "2")
                factory = new EnglishContentFactory();
            else if (choice == "3")
                factory = new GermanyContentFactory();
            else
            {
                Console.WriteLine("Неверный ввод символа!");
                return;
            }

            Content movie = new Content(factory);

            Console.WriteLine("\nВаш фильм готов, приятного просмотра!");
            movie.ShowDescr();
        }
    }
}
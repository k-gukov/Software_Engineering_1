using System;
using System.Collections.Generic;

namespace T4SingletonPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var building = Building.GetBuilding(); //единственный экземпляр здания

            building.CallElevator(5);
            building.CallElevator(2);
            building.CallElevator(15); //вызовы лифта на 5 и 2 этажи + несуществующий 15
        }
    }

    public class Building
    {
        private static Building _building; //только один экземпляр здания

        private Elevator _elevator;
        private List<Floor> _floors;

        public static Building GetBuilding()
        {
            if (_building == null)
            {
                _building = new Building();
            }
            return _building;
        }
        private Building()
        {
            Console.WriteLine("Создано новое здание");
            _elevator = Elevator.GetElevator(); //экземпляр лифта (синглтон)
            _floors = new List<Floor>(); //пустой список этажей
            SetupFloors();
        }


        private void SetupFloors()
        {
            for (int i = 1; i <= 10; i++)
            {
                _floors.Add(new Floor(i));
            }
        }

        public void CallElevator(int floorNumber)
        {
            if (floorNumber < 1 || floorNumber > _floors.Count) //проверка существования этажа
            {
                Console.WriteLine($"Этаж {floorNumber} отсутствует в здании");
                return;
            }

            _elevator.GoToFloor(floorNumber, _floors[floorNumber - 1]);
        }
    }

    public struct Floor
    {
        public int Number { get; }
        public string RoomA { get; }
        public string RoomB { get; }

        public Floor(int number)
        {
            Number = number;
            RoomA = $"Комната {number}A";
            RoomB = $"Комната {number}B";
        }

        public void ShowRooms()
        {
            Console.WriteLine($"На этаже {Number}: {RoomA}, {RoomB}");
        }
    }
    public class Elevator
    {
        private static Elevator _elevator; 
        private int _currentFloor;

        public static Elevator GetElevator() //получение экземпляра лифта
        {
            if (_elevator == null)
            {
                _elevator = new Elevator();
            }
            return _elevator;
        }

        private Elevator()
        {
            _currentFloor = 1;
            Console.WriteLine("Лифт установлен на 1 этаже");
        }

        public void GoToFloor(int targetFloor, Floor floor)
        {
            if (targetFloor == _currentFloor)
            {
                Console.WriteLine($"Лифт уже на {_currentFloor} этаже");
                floor.ShowRooms();
                return;
            }

            Console.WriteLine($"Лифт отправляется с {_currentFloor} на {targetFloor} этаж");

            if (targetFloor > _currentFloor)
            {
                Console.WriteLine("Движение вверх...");
            }
            else
            {
                Console.WriteLine("Движение вниз...");
            }

            _currentFloor = targetFloor; //обновление теущего этажа
            Console.WriteLine($"Лифт прибыл на {_currentFloor} этаж");
            floor.ShowRooms();
            Console.WriteLine();
        }
    }
}


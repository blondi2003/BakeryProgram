using System;
using System.IO;
using System.Collections.Generic;
using BakeryLibrary;
using System.Threading;

namespace BakeryProgram
{
    class MyEventArgs : EventArgs
    {
        public char ch;
    }

    class KeyEvent
    {
        // Создадим событие, используя обобщенный делегат.
        public event EventHandler<MyEventArgs> KeyDown;

        public void OnKeyDown(char ch)
        {
            MyEventArgs c = new MyEventArgs();

            if (KeyDown != null)
            {
                c.ch = ch;
                KeyDown(this, c);
            }
        }
    }

    internal class Program
    {
        private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e) //Метод для закрытия программы.
        {
            Console.WriteLine("Программа отменена, все данные сохранены.");
        }
        static void Main(string[] args)
        {

            string path = @"..\Cafe.csv";  //Заносим Баланс и ингридиенты на складе из файла.
            var cafe = new Cafe();
            var lines = File.ReadAllLines(path);
            var splits = lines[1].Split(';');
            cafe.Balance = Convert.ToInt32(splits[0]);
            cafe.IceCream = Convert.ToInt32(splits[1]);
            cafe.Cream = Convert.ToInt32(splits[2]);
            cafe.Salt = Convert.ToInt32(splits[3]);
            cafe.Vanilla = Convert.ToInt32(splits[4]);
            cafe.Arabica = Convert.ToInt32(splits[5]);
            cafe.Robusta = Convert.ToInt32(splits[6]);
            cafe.OrangeJuice = Convert.ToInt32(splits[7]);
            cafe.FreezeRaspberry = Convert.ToInt32(splits[8]);
            cafe.Chocolate = Convert.ToInt32(splits[9]);
            cafe.Cinnamon = Convert.ToInt32(splits[10]);
            cafe.AlmondM = Convert.ToInt32(splits[11]);
            cafe.CoconutM = Convert.ToInt32(splits[12]);
            cafe.RiceM = Convert.ToInt32(splits[13]);
            cafe.MapleS = Convert.ToInt32(splits[14]);
            cafe.RaspberryS = Convert.ToInt32(splits[15]);
            cafe.PistachioS = Convert.ToInt32(splits[16]);

            path = @"..\AllUsers.csv";  //Заносим список пользователей.
            var users = new AllUsers();
            users.LoadAllUsers(path);

            path = @"..\Bakery.csv";  //Заносим список булочек.
            var bakery = new Bakery<string>();
            bakery.LoadBakery(path);

            path = @"..\Coffee.csv";  //Заносим список кофе.
            var coffee = new Coffee<string>();
            coffee.LoadCoffee(path);

            Console.CancelKeyPress += Console_CancelKeyPress;            //Методы для сохранении при закрытии программы.
            Console.CancelKeyPress += users.Console_CancelKeyPress;
            Console.CancelKeyPress += bakery.Console_CancelKeyPress;
            Console.CancelKeyPress += users.Console_CancelKeyPress;

            var sellbakery = new Bakery<string>[bakery.Count()];        //Переменные для меню.
            var sellcoffee = new Coffee<string>[coffee.Count()];
            int numcoffee = 0;
            int numbakery = 0;

            //Menu 
            Console.WriteLine("Приветствуем Вас в нашей кофейне, выберите товар:");               
            Console.WriteLine("1) Кофе\n" + "2) Булочка\n" + "3) Корзина\n" + "e) Выход");
                KeyEvent @event = new KeyEvent();
                @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
                {
                    switch (e.ch)
                    {
                        case '1':                   //Вкладка с кофе.
                            {
                                Console.WriteLine("\n");
                                coffee.Iterator();
                                KeyEvent @event = new KeyEvent();
                                @event.KeyDown += (sender, e) =>
                                {
                                    switch (e.ch)
                                    {
                                        case '1':     //Добавляем в корзину один Эспрессо, весь остальной кофе аналогично.
                                            {
                                                if (cafe.Espresso() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(0);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nEspresso добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '2':
                                            {
                                                if (cafe.Cappuccino() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(1);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nCappuccino добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '3':
                                            {
                                                if (cafe.Americano() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(2);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nAmericano добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '4':
                                            {
                                                if (cafe.Latte() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(3);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nLatte добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '5':
                                            {
                                                if (cafe.Frappe() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(4);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nFrappe добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '6':
                                            {
                                                if (cafe.WithMilk() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(5);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nWithMilk добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '7':
                                            {
                                                if (cafe.Glisse() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(6);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nGlisse добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '8':
                                            {
                                                if (cafe.ConPanna() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(7);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nConPanna добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '9':
                                            {
                                                if (cafe.LatteMacchiato() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(8);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    } 
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nLatteMacchiato добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case '0':
                                            {
                                                if (cafe.Raf() == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = coffee.Return(9);
                                                    for (int i = 0; i < sellcoffee.Length; i++)
                                                    {
                                                        if (sellcoffee[i] != null)
                                                        {
                                                            if (sellcoffee[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellcoffee[index].Quantity = sellcoffee[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellcoffee[numcoffee] = coff;
                                                        sellcoffee[numcoffee].Quantity = 1;
                                                        numcoffee++;
                                                    }
                                                    Console.WriteLine("\nRaf добавлен");
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    coffee.Iterator();
                                                }
                                                break;
                                            }
                                        case 'e':     //Кнопка назад.
                                            {
                                                Console.Clear();
                                                Console.WriteLine("1) Кофе\n" + "2) Булочка\n" + "3) Корзина\n" + "e) Выход");
                                                break;
                                            }
                                    }
                                };
                                char ch;
                                do
                                {
                                    Console.Write("Выберите нужную позицию из меню: ");
                                    ConsoleKeyInfo key;
                                    key = Console.ReadKey();
                                    ch = key.KeyChar;
                                    @event.OnKeyDown(key.KeyChar);
                                }
                                while (ch != 'e');
                                break;
                            }
                        case '2':         //Открываем вкладку с Булочками.
                            {
                                Console.WriteLine("\n");
                                bakery.Iterator();
                                KeyEvent @event = new KeyEvent();
                                @event.KeyDown += (sender, e) =>
                                {
                                    switch (e.ch)
                                    {
                                        case '1':       //Добавляем в корзину первую булочку, все остальные булочки аналогично.
                                            {
                                                if (bakery.Amount(0) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(0);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '2':
                                            {
                                                if (bakery.Amount(1) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(1);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '3':
                                            {
                                                if (bakery.Amount(2) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(2);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '4':
                                            {
                                                if (bakery.Amount(3) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(3);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '5':
                                            {
                                                if (bakery.Amount(4) == true)
                                                {

                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(4);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '6':
                                            {
                                                if (bakery.Amount(5) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(5);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '7':
                                            {
                                                if (bakery.Amount(6) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(6);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '8':
                                            {
                                                if (bakery.Amount(7) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(7);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '9':
                                            {
                                                if (bakery.Amount(8) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(8);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case '0':
                                            {
                                                if (bakery.Amount(9) == true)
                                                {
                                                    int povtor = 0;
                                                    int index = 0;
                                                    var coff = bakery.Return(9);
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        if (sellbakery[i] != null)
                                                        {
                                                            if (sellbakery[i].Name == coff.Name)
                                                            {
                                                                povtor = 1;
                                                                index = i;
                                                            }
                                                        }
                                                    }
                                                    if (povtor == 1)
                                                    {
                                                        sellbakery[index].Quantity = sellbakery[index].Quantity + 1;
                                                    }
                                                    else
                                                    {
                                                        sellbakery[numbakery] = coff;
                                                        sellbakery[numbakery].Quantity = 1;
                                                        numbakery++;
                                                    }
                                                    Thread.Sleep(1000);
                                                    Console.Clear();
                                                    bakery.Iterator();
                                                }
                                                break;
                                            }
                                        case 'e':        //Кнопка назад.
                                            {
                                                Console.Clear();
                                                Console.WriteLine("1) Кофе\n" + "2) Булочка\n" + "3) Коpзина\n" + "e) Выход");
                                                break;
                                            }
                                    }
                                };
                                char ch;
                                do
                                {
                                    Console.Write("Выберите нужную позицию из меню: ");
                                    ConsoleKeyInfo key;
                                    key = Console.ReadKey();
                                    ch = key.KeyChar;
                                    @event.OnKeyDown(key.KeyChar);
                                }
                                while (ch != 'e');
                                break;
                            }
                        case '3':       //Вкладка корзины.
                            {
                                Console.WriteLine("\n");            //Вывод корзины.
                                for (int i = 0; i < sellcoffee.Length; i++)
                                {
                                    if (sellcoffee[i] != null)
                                    {
                                        Console.WriteLine(sellcoffee[i].ToString() + " " + sellcoffee[i].Quantity + " = " + (sellcoffee[i].Price * sellcoffee[i].Quantity));
                                    }
                                }
                                for (int i = 0; i < sellbakery.Length; i++)
                                {
                                    if (sellbakery[i] != null)
                                    {
                                        Console.WriteLine(sellbakery[i].ToString());
                                    }
                                }

                                int fullsum = 0;                          //Подсчёт полной суммы.
                                for (int i = 0; i < sellbakery.Length; i++)
                                {
                                    if (sellbakery[i] != null)
                                    {
                                        fullsum += sellbakery[i].Price * sellbakery[i].Quantity;
                                    }
                                    if (sellcoffee[i] != null)
                                    {
                                        fullsum += sellcoffee[i].Price * sellcoffee[i].Quantity;
                                    }
                                }
                                Console.WriteLine("Полная стоимость вашего заказа:" + fullsum);
                                Console.WriteLine("1) Купить\n" + "2) Написать отзыв о нашей пекарне\n" + "e) Назад");
                                KeyEvent @event = new KeyEvent();
                                @event.KeyDown += (sender, e) =>
                                {
                                    switch (e.ch)
                                    {
                                        case '1':         //Покупка товара.
                                            {
                                                var customer = new AllUsers();
                                                int chek = 0;
                                                while (chek == 0)
                                                {
                                                    Console.Write("Введите Номер карты или е для выхода:");
                                                    string card = Console.ReadLine();
                                                    if (card == "e")
                                                    {
                                                        Console.WriteLine("Полная стоимость вашего заказа:" + fullsum);
                                                        Console.WriteLine("1) Купить\n" + "2) Написать отзыв о нашей пекарне\n" + "e) Назад");
                                                        break;
                                                    }
                                                    Console.Write("Введите CVC код или е для выхода:");
                                                    string cvc = Console.ReadLine();
                                                    if (cvc == "e")
                                                    {
                                                        Console.WriteLine("Полная стоимость вашего заказа:" + fullsum);
                                                        Console.WriteLine("1) Купить\n" + "2) Написать отзыв о нашей пекарне\n" + "e) Назад");
                                                        break;
                                                    }
                                                    customer = users.TryLogin(card, cvc);
                                                    if (customer == null)
                                                    {
                                                        Console.WriteLine("Карта не найдена");
                                                        Thread.Sleep(1000);
                                                        Console.Clear();
                                                    }
                                                    else
                                                    {
                                                        chek++;
                                                    }
                                                }
                                                if (fullsum > customer.Balance)
                                                {
                                                    Console.WriteLine("У вас недостаточно средств");
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        sellbakery[i] = null;
                                                        sellcoffee[i] = null;
                                                    }
                                                }
                                                else if (fullsum <= customer.Balance)
                                                {
                                                    Console.WriteLine("Спасибо за покупку, приходите ещё!");
                                                    customer.Balance -= fullsum;
                                                    cafe.Balance += fullsum;
                                                    for (int i = 0; i < sellbakery.Length; i++)
                                                    {
                                                        sellbakery[i] = null;
                                                        sellcoffee[i] = null;
                                                    }
                                                }
                                                break;
                                            }
                                        case '2':       //Отзыв.
                                            {
                                                bakery.Feedback();
                                                Console.WriteLine("Спасибо за отзыв!");
                                                Console.Clear();
                                                Console.WriteLine("\n");
                                                for (int i = 0; i < sellcoffee.Length; i++)
                                                {
                                                    if (sellcoffee[i] != null)
                                                    {
                                                        Console.WriteLine(sellcoffee[i].ToString() + " " + sellcoffee[i].Quantity + " = " + (sellcoffee[i].Price * sellcoffee[i].Quantity));
                                                    }
                                                }
                                                for (int i = 0; i < sellbakery.Length; i++)
                                                {
                                                    if (sellbakery[i] != null)
                                                    {
                                                        Console.WriteLine(sellbakery[i].ToString());
                                                    }
                                                }
                                                Console.WriteLine("Полная стоимость вашего заказа:" + fullsum);
                                                Console.WriteLine("1) Купить\n" + "2) Написать отзыв о нашей пекарне\n" + "e) Назад");
                                                break;
                                            }
                                        case 'e':         //Кнопка назад.
                                            {
                                                Console.Clear();
                                                Console.WriteLine("1) Кофе\n" + "2) Булочка\n" + "3) Корзина\n" + "e) Выход");
                                                break;
                                            }
                                    }
                                }; char ch;
                                do
                                {
                                    Console.Write("Выберите нужную позицию из меню: ");
                                    ConsoleKeyInfo key;
                                    key = Console.ReadKey();
                                    ch = key.KeyChar;
                                    @event.OnKeyDown(key.KeyChar);
                                }
                                while (ch != 'e');
                                break;
                            }
                        case 'e':       //Выход из программы.
                            {
                                cafe.SaveCafe();
                                users.SaveUsers();
                                bakery.SaveBread();
                                break;
                            }
                                default:
                            {
                                Console.WriteLine("\nТакая команда не найдена!");
                                break;
                            }
                    }
                };
                char ch;
                do
                {
                    Console.Write("Выберите тип товара: ");
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    ch = key.KeyChar;
                    @event.OnKeyDown(key.KeyChar);
                }
                while (ch != 'e');
        }
    }
}

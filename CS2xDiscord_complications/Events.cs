using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;

namespace CS2xDiscord_complications;

public static class Events
{
    private static InputSimulator sim = new();
    private static readonly int secsForAllEvents = 15;
    public static List<Event> GetEvents()
    {
        List<Event> main = new List<Event>();

        main.Add(new Event
        {
            Name = "👀", id = "paranoya", Description = "Жертва оборачиваеться обратно", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(sigmaevent))
        });
        main.Add(new Event
        {
            Name = "🔀", id = "izmenchivost", Description = "Жертва не может нормально выбрать оружие, поэтому постоянно меняет его", EmojiId = 0, lvlOfRarity = 1,
            WorkingThread = new Thread(new ThreadStart(izmenchivost))
        });
        main.Add(new Event
        {
            Name = "🐇", id = "bunny_hopper", Description = "Жертва выдаёт СМАЧНОГО БАННИ ХОПА", EmojiId = 0, lvlOfRarity = 1,
            WorkingThread = new Thread(new ThreadStart(bunnyhop))
        });
        main.Add(new Event
        {
            Name = "🧑‍💻", id = "stop_playing_cs", Description = "Жертва забивает на КС и идёт на рандомную страницу", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(stop_playing_cs))
        });
        main.Add(new Event
        {
            Name = "🧠", id = "zatymanenyi_razym", Description = "Разум жертвы затуманен, и теперь жеврта постоянно на что-то отвлекается", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(zatymanenyi_razym))
        });
        main.Add(new Event
        {
            Name = "😥", id = "panica", Description = "У жертвы начинаеться тревожный приступ, из-за чего прицеливаться становится очень тяжёло", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(panic))
        });
        main.Add(new Event
        {
            Name = "🚶", id = "na_trenirovochnyx_dvijeniyax", Description = "Жертва резко вспомнила своё лучшее время, и начингает проблежку, просто зажимая W",
            EmojiId = 0, lvlOfRarity = 0, WorkingThread = new Thread(new ThreadStart(trenirovochnye_dvijeniya))
        });
        main.Add(new Event
        {
            Name = "💥", id = "kamikadze", Description = "Жертва вспонминает япоский вайб, и отбиваеться только гранатами", EmojiId = 0, lvlOfRarity = 3,
            WorkingThread = new Thread(new ThreadStart(kamikadze))
        });
        /*
         * main.Add(new Event
        {
            Name = "🖼️", id = "gelenjik_2016", Description = "Открывает одну из заготовленых картинок", EmojiId = 0, lvlOfRarity = 2, 
            WorkingThread = new Thread(new ThreadStart(gelenjik))
        });
        */
        main.Add(new Event
        {
            Name = "🔼", id = "moreholdsize", Description = "Увеличевает размер холдера", EmojiId = 0, lvlOfRarity = 3, 
            WorkingThread = new Thread(() => { })
        });
        main.Add(new Event
        {
            Name = "⌛", id = "srochnyi_zakaz", Description = "Ускоряет дроп на 5 секунд", EmojiId = 0, lvlOfRarity = 3,
            WorkingThread = new Thread(() => { })
        });
        main.Add(new Event
        {
            Name = "💣", id = "pomechat_v_pobede", Description = "Не даёт противнику заплентить/задефузить", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(pomechat_v_pobede))
        });
        main.Add(new Event
        {
            Name = "🐔", id = "otdyx", Description = "Даёт противнику передохнуть", EmojiId = 0, lvlOfRarity = 0, WorkingThread = new Thread(
                () => { })
        });
        main.Add(new Event
        {
            Name = "🗑️", id = "trash_zakup", Description = "Портит весь закуп жертве, автоматически закупая все гранаты и пистолеты", EmojiId = 0, lvlOfRarity = 3,
            WorkingThread = new Thread(new ThreadStart(trash))
        });
        main.Add(new Event
        {
            Name = "⛓️", id = "bez_zakupa", Description = "Жертва теряет возможность закупиться на время ивента", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(bez_zakupa))
        });
        main.Add(new Event
        {
            Name = "☠️", id = "awp_monstr", Description = "Жертва верит в себя, да и настолько, что начинает дико чувствовать", lvlOfRarity = 3, EmojiId = 0,
            WorkingThread = new Thread(new ThreadStart(awp_monstr))
        });
        main.Add(new Event
        {
            Name = "🔫", id = "nerwny_kurok", Description = "Жертва иногда стреляет", lvlOfRarity = 0, EmojiId = 0,
            WorkingThread = new Thread(new ThreadStart(nervny_sriv))
        });
        main.Add(new Event
        {
            Name = "🧪", id = "pod_solyami", Description = "Жертва принимает питерский продукт, тем самым становиться очень неуправляемым", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(pod_solyami))
        });
        main.Add(new Event
        {
            Name = "❄️", id = "zamorozka", Description = "Жертва просто останавливаеться", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(zamorozka))
        });
        main.Add(new Event
        {
            Name = "🏪", id = "magazinnye_dvijeniya", Description = "Жертва слишком часто перезаряжаеться", EmojiId = 0, lvlOfRarity = 1,
            WorkingThread = new Thread(new ThreadStart(magazinye_dvijenya))
        });
        main.Add(new Event
        {
            Name = "💦", id = "volnenie", Description = "Жертва так беспокойтся об игре, что теперь его руки трясутся", EmojiId = 0, lvlOfRarity = 1,
            WorkingThread = new Thread(new ThreadStart(silnaya_otdacha))
        });
        main.Add(new Event
        {
            Name = "🔪", id = "ekvando", Description = "Теперь вам доступен лишь нож", EmojiId = 0, lvlOfRarity = 3,
            WorkingThread = new Thread(new ThreadStart(tested))
        });
        main.Add(new Event
        {
            Name = "🎈", id = "skolzkie_ruki", Description = "Жертва постоянно роняет оружие, как так то?", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(skolzlie_ruki))
        });
        main.Add(new Event
        {
            Name = "💪", id = "prisedanya", Description = "Жевтра постоянно приседает", EmojiId = 0, lvlOfRarity = 2,
            WorkingThread = new Thread(new ThreadStart(prisedanya))
        });
        
        return main;
    }

    private static void kamikadze()
    {
        int temp = secsForAllEvents * 10;
        while (temp > 0)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_4);
            Thread.Sleep(100);
            temp--;
        }
    }

    private static void gelenjik()
    {
        //TODO: Картинки не запускаються
        int temp = secsForAllEvents;
        while (temp > 0)
        {
            switch (new Random().Next(1, 5))
            {
                case 1:
                    Process.Start(@"zagotovki\1.jpg");
                    break;
                case 2:
                    Process.Start(@"zagotovki\2.jpg");
                    break;
                case 3:
                    Process.Start(@"zagotovki\3.jpg");
                    break;
                case 4:
                    Process.Start(@"zagotovki\4.jpg");
                    break;
                case 5:
                    Process.Start(@"zagotovki\5.jpg");
                    break;
            }

            int i = new Random().Next(5, 15);
            Thread.Sleep(i*1000);
            temp -= i;
        }
    }

    private static void bez_zakupa()
    {
        int temp = secsForAllEvents * 10;
        while (temp > 0)
        {
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_B);
            Thread.Sleep(100);
            temp--;
        }
    }

    private static void trash()
    {
        sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_5);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_4);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_5);
        
        sim.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        
        sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
    }

    private static void pomechat_v_pobede()
    {
        //TODO: плохо роботает
        int temp = secsForAllEvents * 10;
        while (temp > 0)
        {
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_E);
            sim.Mouse.LeftButtonUp();

            if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_E))
            {
                sim.Mouse.MoveMouseBy(2000, 0);
            }
            
            Thread.Sleep(100);
            temp--;
        }
    }

    private static void stop_playing_cs()
    {
        int temp = secsForAllEvents;
        while (temp > 0)
        {
            switch (new Random().Next(1, 5))
            {
                case 1:
                    Process.Start(new ProcessStartInfo("https://youtu.be/-tssPew4et4") { UseShellExecute = true });
                    break;
                case 2:
                    Process.Start(new ProcessStartInfo("https://www.youtube.com/results?search_query=ВСЕ+СЕРИИ+СКИБИДИ+ТУАЛЕТОВ+ОНДАЙН+ПРЯМО+СЕЙЧАС!!!!!!!!!!!!!!!") { UseShellExecute = true });
                    break;
                case 3:
                    Process.Start(new ProcessStartInfo("https://www.youtube.com/watch?v=I9FMm4vlAOY") { UseShellExecute = true });
                    break;
                case 4:
                    Process.Start(new ProcessStartInfo("https://yandex.ru/search/?text=скачать+вирусы+с+регистрацией+без+манкрафта&clid=2270455&banerid=6302000000%3A6651de628f049799fbbe2c13&win=647&lr=67") { UseShellExecute = true });
                    break;
                case 5:
                    Process.Start(new ProcessStartInfo("https://youtu.be/fbl7EJ99FvA?si=EkkSFc7ZwkGfNutw&t=195") { UseShellExecute = true });
                    break;
            }

            int i = new Random().Next(5, 15);
            Thread.Sleep(i*1000);
            temp -= i;
        }
    }

    private static void pod_solyami()
    {
        for (int z = 0; z < 3; z++)
        {
            int i = new Random().Next(0, 3);
            switch (i)
            {
                case 0:
                    sim.Keyboard.KeyDown(VirtualKeyCode.VK_W);
                    for (int l = 0; l < 5; l++)
                    {
                        sim.Mouse.MoveMouseBy(new Random().Next(-500, 500), new Random().Next(-500, 500));
                        Thread.Sleep(1000);
                    }
                    sim.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                    break;
                case 1:
                    sim.Keyboard.KeyDown(VirtualKeyCode.VK_A);
                    for (int l = 0; l < 5; l++)
                    {
                        sim.Mouse.MoveMouseBy(new Random().Next(-500, 500), new Random().Next(-500, 500));
                        Thread.Sleep(1000);
                    }
                    sim.Keyboard.KeyUp(VirtualKeyCode.VK_A);
                    break;
                case 2:
                    sim.Keyboard.KeyDown(VirtualKeyCode.VK_S);
                    for (int l = 0; l < 5; l++)
                    {
                        sim.Mouse.MoveMouseBy(new Random().Next(-500, 500), new Random().Next(-500, 500));
                        Thread.Sleep(1000);
                    }
                    sim.Keyboard.KeyUp(VirtualKeyCode.VK_S);
                    break;
                case 3:
                    sim.Keyboard.KeyDown(VirtualKeyCode.VK_D);
                    for (int l = 0; l < 5; l++)
                    {
                        sim.Mouse.MoveMouseBy(new Random().Next(-500, 500), new Random().Next(-500, 500));
                        Thread.Sleep(1000);
                    }
                    sim.Keyboard.KeyUp(VirtualKeyCode.VK_D);
                    break;
            }
        }
    }

    private static void nervny_sriv()
    {
        int temp = secsForAllEvents;
        while (temp > 0)
        {
            if (new Random().Next(0, 100) > 60)
            {
                sim.Mouse.LeftButtonClick();
            }
            Thread.Sleep(1000);
            temp--;
        }
    }

    private static void panic()
    {
        int temp = secsForAllEvents * 10;
        while (temp > 0)
        {
            sim.Mouse.MoveMouseBy(new Random().Next(-50, 50), new Random().Next(-50, 50));
            Thread.Sleep(100);
            temp--;
        }
    }

    private static void bunnyhop()
    {
        int temp = secsForAllEvents * 2;
        while (temp > 0)
        {
            if (new Random().Next(0, 100) > 20)
            {
                sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            }
            Thread.Sleep(500);
            temp--;
        }
    }

    private static void izmenchivost()
    {
        int temp = secsForAllEvents;
        while (temp > 0)
        {
            int i = new Random().Next(0, 3);
            switch (i)
            {
                case 0:
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
                    break;
                case 1:
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
                    break;
                case 2:
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
                    break;
                case 3:
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_4);
                    break;
            }
            Thread.Sleep(1000);
            temp--;
        }
    }

    private static void awp_monstr()
    {
        int temp = secsForAllEvents;
        while (temp > 0)
        {
            sim.Mouse.MoveMouseBy(new Random().Next(-2000, 2000), new Random().Next(-2000, 2000));
            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            Thread.Sleep(1000);
            temp--;
        }
    }

    private static void zatymanenyi_razym()
    {
        int temp = secsForAllEvents;
        while (temp>0)
        {
            if (new Random().Next(0, 100) > 25)
            {
                sim.Mouse.MoveMouseBy(new Random().Next(-500, 500), new Random().Next(-500, 500));
            }
            Thread.Sleep(1000);
            temp--;
        }
    }

    private static void zamorozka()
    {
        int temp = secsForAllEvents * 10;
        while (temp > 0)
        {
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_W);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_A);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_S);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_D);
            Thread.Sleep(100);
            temp--;
        }
    }

    private static void magazinye_dvijenya()
    {
        int temp = secsForAllEvents;
        while (temp > 0)
        {
            if (new Random().Next(0, 100) > 30)
            {
                sim.Mouse.LeftButtonUp();
                sim.Keyboard.KeyPress(VirtualKeyCode.VK_R);
            }
            Thread.Sleep(1000);
            temp--;
        }
    }

    private static void trenirovochnye_dvijeniya()
    {
        int temp = secsForAllEvents * 10;

        while (temp > 0)
        {
            sim.Keyboard.KeyDown(VirtualKeyCode.VK_W);
            Thread.Sleep(100);
            temp--;
        }
        sim.Keyboard.KeyUp(VirtualKeyCode.VK_W);
    }

    private static void silnaya_otdacha()
    {
        int temp = secsForAllEvents;
        while (temp>0)
        {
            sim.Mouse.MoveMouseBy(new Random().Next(-50, 50), new Random().Next(-50, 50));
            temp--;
            Thread.Sleep(1000);
        }
    }

    private static void prisedanya()
    {
        int temp = secsForAllEvents;
        while (temp>0)
        {
            sim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            Thread.Sleep(1000);
            sim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            Thread.Sleep(1000);
            temp -= 2;
        }
    }

    private static void skolzlie_ruki()
    {
        int temp = secsForAllEvents;
        while (temp!=0)
        {
            if (new Random().Next(0, 100) > 50)
            {
                sim.Keyboard.KeyPress(VirtualKeyCode.VK_G);
            }
            Thread.Sleep(1000);
            temp--;
        }
    }


    private static void tested()
    {
        int temp = secsForAllEvents;
        while (temp != 0)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
            Thread.Sleep(100);
            temp--;
        }
    }
    private static void sigmaevent()
    {
        int temp = secsForAllEvents;
        while (temp != 0)
        {
            if (new Random().Next(0, 100) > 50)
            {
                sim.Mouse.MoveMouseBy(2000, 0);
            }
            Thread.Sleep(1000);
            temp--;
        }
    }

    [DllImport("user32.dll")]
    private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
}
using System;

class Button
{
    public string Color { get; set; }
    public int Size { get; set; }
    public string Text { get; set; }
    private EventHandler clickHandlers;

    public event EventHandler Click
    {
        add
        {
            if (clickHandlers != null)
            {
                foreach (Delegate d in clickHandlers.GetInvocationList())
                {
                    if (d == value)
                    {
                        Console.WriteLine("Этот подписчик уже есть");
                        return;
                    }
                }
            }
            int count = clickHandlers?.GetInvocationList().Length ?? 0;
            if (count >= 3)
            {
                Console.WriteLine("Максимальное количество подписчиков");
                return;
            }
            clickHandlers += value;
        }
        remove
        {
            clickHandlers -= value;
        }
    }

    public void Press()
    {
        clickHandlers?.Invoke(this, EventArgs.Empty);
    }
}

class Program
{
    static void NewText(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Text = "кнопк";
        Console.WriteLine("Новый текст - " + btn.Text);
    }

    static void NewColor(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Color = "красная";
        Console.WriteLine("Новый цвет - " + btn.Color);
    }

    static void NewSize(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Size = 44;
        Console.WriteLine("Новый размер - " + btn.Size);
    }

    static void Main()
    {
        Button myButton = new Button();
        myButton.Text = "бябябя";
        myButton.Color = "синяя";


        myButton.Size = 33;


        myButton.Click += NewText;
        myButton.Click += NewColor;
        myButton.Click += NewSize;


        myButton.Press();


        myButton.Click += NewText;
        Console.ReadLine();
    }
}

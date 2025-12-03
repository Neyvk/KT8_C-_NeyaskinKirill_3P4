using System;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        Timer timer;
        Clock clock;
        Counter counter;

        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            clock = new Clock(timer, clockLabel);
            counter = new Counter(timer, timerLabel);
            timer.Start();
        }
    }

    public class Timer
    {
        private System.Windows.Forms.Timer timer;
        public event EventHandler Tick;

        public Timer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object s, EventArgs e)
        {
            Tick(this, EventArgs.Empty);
        }

        public void Start() => timer.Start();
        public void Stop() => timer.Stop();
    }

    public class Clock
    {
        Label label;

        public Clock(Timer timer, Label l)
        {
            label = l;
            timer.Tick += Timer_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            label.Text = DateTime.Now.ToLongTimeString();
        }
    }

    public class Counter
    {
        int count = 0;
        Label label;

        public Counter(Timer timer, Label l)
        {
            label = l;
            timer.Tick += Timer_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            count++;
            label.Text = count.ToString();
        }
    }
}

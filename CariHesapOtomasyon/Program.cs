using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariHesapOtomasyon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Kullanicilar kullanici = new Kullanicilar();
            //kullanici.kullaniciID = 1;
            //Application.Run(new Form2(kullanici));
            Application.Run(new Form1());
        }
    }
}

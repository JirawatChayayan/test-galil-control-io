using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test_galil
{
    internal class Program
    {
        static gclib gclib = new gclib();
        static void Main(string[] args)
        {
            OpenGalil("192.168.2.110");

            while (true)
            {
                try
                {
                    bool sensor_in2 = (int)(double.Parse(SendMessage("@IN[02]"))) == 1;

                    if(sensor_in2)
                    {
                        SendCommand("SB1");
                    }
                    else
                    {
                        SendCommand("CB1");
                    }



                    Console.WriteLine();
                    Thread.Sleep(1000);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }
            Console.ReadLine();
            CloseGalil();
        }

        private static string SendMessage(string cmd)
        {
            string cmd_upper = cmd.Trim().ToUpper();
            return gclib.GCommand($"MG {cmd_upper}");
        }

        private static string SendCommand(string cmd)
        {
            string cmd_upper = cmd.Trim().ToUpper();
            return gclib.GCommand($"{cmd_upper}");
        }

        private static void OpenGalil(string ip)
        {
            gclib.GOpen($"{ip} --direct --subscribe ALL");
        }

        private static void CloseGalil()
        {
            gclib.GClose();
        }
    }
}

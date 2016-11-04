using System;

namespace QvEventLogConnectorSimple
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length >= 2)
            {
                new QvLibraServer().Run(args[0], args[1]);
            }       
        }
    }
}

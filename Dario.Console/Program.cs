﻿using Microsoft.Owin.Hosting;
using System.Threading;
using Owin;

namespace Dario.Console
{
    public class Program
    {
        private static readonly ManualResetEvent QuitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            var server = "http://*:{0}";
            var port = 5001;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out port);
            }
            
            #if DEBUG
                server = "http://localhost:{0}";
            #endif

            System.Console.CancelKeyPress += (sender, eArgs) =>
            {
                QuitEvent.Set();
                eArgs.Cancel = true;
            };
            //using (WebApp.Start<Startup1>(string.Format(server, port)))
            using (WebApp.Start<Startup1>(string.Format("http://*:{0}", port)))
            {
                System.Console.WriteLine("Started, running on port: {0}",port);
                QuitEvent.WaitOne();
            }

        }
    }

    public class Startup1
    {

        public void Configuration(IAppBuilder app)
        {
            /**app.UseHandlerAsync((req, res) =>
            {
                res.ContentType = "text/plain";
                return res.WriteAsync("Hallo hallo");
            });*/
            app.UseWelcomePage();
        }


    }

}

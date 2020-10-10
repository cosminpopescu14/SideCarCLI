﻿using McMaster.Extensions.CommandLineUtils;
using System;

namespace SideCarCLI
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication()
            {
                MakeSuggestionsInErrorMessage = true,
                FullName = "SideCar for any other application",
                ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated,
                ExtendedHelpText= @"
                The most simplest form it is: 
                    SideCarCLI startApp --name YourApp
                "
            };


            app.HelpOption("-h|--help", inherited: true);
            app.Option("-max|--maxSeconds", "max seconds for the StartApp to run", CommandOptionType.SingleOrNoValue); ;
            app.Command("_about", cmd =>
            {
                cmd.OnExecute(() =>
                {
                    app.ShowVersion();
                    Console.WriteLine("made by Andrei Ignat, http://msprogrammer.serviciipeweb.ro/category/sidecar/");
                });
            });
            
            app.Command("startApp", cmd =>
            {
                cmd.FullName = "start the CLI application that you need to intercept";
                cmd.ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated;
                cmd.Option("-n|--name <fullPathToApplication>", "Path to the StartApp", CommandOptionType.SingleValue);
                cmd.Option("-a|--arguments <arguments_to_the_app>", "StartApp arguments", CommandOptionType.MultipleValue);
                cmd.Option("-f|--folder <folder_where_execute_the_app>", "folder where to execute the StartApp - default folder of the StartApp ", CommandOptionType.SingleOrNoValue);
            });

            app.Command("lineInterceptors", cmd =>
            {
                cmd.FullName = "Specify application for start when StartApp has a new line output";
                cmd.ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated;
                cmd.Option("-l|--list", "List interceptors for lines", CommandOptionType.NoValue);                                
                cmd.Option("-a|--add", "Add interceptor to execute", CommandOptionType.MultipleValue);
                cmd.Option("-f|--folder", "folder where to start the interceptor" ,CommandOptionType.SingleOrNoValue);
            });

            app.Command("timer", cmd =>
            {
                cmd.FullName = "Specify timer to start an application at repeating interval ";
                cmd.ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated;
                cmd.Option("-i|--intervalRepeatSeconds", "Repeat interval in seconds", CommandOptionType.SingleValue);
                cmd.Option("-l|--list", "List interceptors to execute periodically", CommandOptionType.NoValue);
                cmd.Option("-a|--add", "Add interceptor to execute", CommandOptionType.MultipleValue);
                cmd.Option("-f|--folder", "folder where to start the interceptor" ,CommandOptionType.SingleOrNoValue);
            
            });
            app.Command("finishInterceptors", cmd =>
            {
                cmd.FullName = "Specify interceptors for start when finish the app";
                cmd.ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated;
                cmd.Option("-l|--list", "List interceptors for finish application", CommandOptionType.NoValue);
                cmd.Option("-a|--add", "Add interceptor to execute", CommandOptionType.MultipleValue);
                cmd.Option("-f|--folder", "folder where to start the interceptor", CommandOptionType.SingleOrNoValue);

            });
            app.Command("plugins", cmd =>
            {
                cmd.ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated;

                cmd.FullName = " Load dynamically plugins ";
                cmd.ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated;
                cmd.Option("-f|--folder", "folder with plugins", CommandOptionType.SingleValue);
                cmd.Option("-l|--list", "List plugins", CommandOptionType.NoValue);
                cmd.Option("-a|--add", "Add interceptor to execute", CommandOptionType.MultipleValue);

            });

            app.Command("_listAllCommands", cmd =>
            {

                cmd.FullName = " List all commands for the app";

                cmd.OnExecute(() =>
                {
                    Console.WriteLine($"--------------");
                    app.ShowHelp();
                    Console.WriteLine($"--------------");
                    foreach (var item in app.Commands)
                    {
                        Console.WriteLine($"Command : {item.Name} ");
                        Console.WriteLine(item.FullName);
                        item.ShowHelp();
                        Console.WriteLine($"--------------");
                    }
                });
            });
            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a command");
                app.ShowHelp();
                return 1;
            });
            return app.Execute(args);

        }
    }
}

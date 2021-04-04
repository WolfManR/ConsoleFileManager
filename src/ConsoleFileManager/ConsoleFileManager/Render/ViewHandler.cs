﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConsoleFileManager.Render.Controls;

namespace ConsoleFileManager.Render
{
    public static class ViewHandler
    {
        private static Configuration config;
        public static ListView View;
        static ViewHandler()
        {
            LoadConfiguration();
            UpdateView();
            InitializeViews();
        }
        
        public static void InitializeViews()
        {
            FilesManager.ChangeDirectory(config.OpenedPath);
        }

        public static void LoadConfiguration()
        {
            config = new Configuration();
        }

        public static void UpdateView()
        {
            Console.BufferHeight = Console.WindowHeight = config.WindowHeight;
            Console.BufferWidth = Console.WindowWidth = config.WindowWidth;
        }

        

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void ContinuousPrint(IEnumerable<string> lines)
        {
            Console.WriteLine();
            foreach (var line in lines)
                Console.WriteLine(line);
            Console.WriteLine();
        }

        public static void PrintException(Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine(exception.Message);
            Console.WriteLine();
        }

        public static string Ask(string question, bool inline)
        {
            if(inline)
                Console.Write(question);
            else
                Console.WriteLine(question);

            return Console.ReadLine();
        }

        public static void Prev()
        {
            View.Prev();
        }

        public static void Next()
        {
            View.Next();
        }
    }
}
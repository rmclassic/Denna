﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace NotifierTask
{
    class Localdb
    {
        //for database creation
        public static void CreateDatabase()
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<todo>();

            }


        }
        
        //save user name
        public static void Iuser(string name)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = name;



        }
        //logout proceed
        public static void Logout(string name)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = null;
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.DropTable<todo>();
                ApplicationData.Current.LocalSettings.Values["Username"] = null;
            }



        }
        // add a todo list
        public static async Task Addtodo(string titl, string describe, DateTime date)
        {


            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Insert(new todo()
                {
                    title = titl,
                    detail = describe,
                    time = date,
                    isdone = 0

                });

            }
        }
        
        // get all list
        public static ObservableCollection<todo> getlist()
        {
            ObservableCollection<todo> todos = new ObservableCollection<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title, Id = message.Id });
                }

            }
            return todos;


        }
        //get stuff for today
        public static ObservableCollection<todo> Getfordoday(DateTime now)
        {
            ObservableCollection < todo > todos = getlist();


            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            ObservableCollection<todo> todoss = new ObservableCollection<todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            return todoss;



        }
        //get all todo list with order
        public static ObservableCollection<todo> getall(DateTime now)
        {
            ObservableCollection<todo> todos = getlist();
            

            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            ObservableCollection<todo> todoss = new ObservableCollection<todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            foreach (var item in todos)
            {
                if (item.time >= endtoday)
                {
                    todoss.Add(item);
                }
            }


            return todoss;


        }
    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LanguageScgool.Model;

namespace LanguageScgool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SchoolDBOEntities db = new SchoolDBOEntities();
        public static bool IsAutorizate;
        public static User AutorizateUser;
        public static bool Admin = false;
    }
}

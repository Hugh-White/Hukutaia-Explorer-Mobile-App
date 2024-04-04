﻿using HukutaiaExplorer.MVVM.Views.StaticPages;

namespace HukutaiaExplorer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Setting the MainPage of the app to an instance of a NavPage, which is an instance of LandingPage in Static Pages Folder
            MainPage = new NavigationPage(new LandingPage());
        }
    }
}

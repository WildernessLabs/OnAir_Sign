﻿using MobileMaple.ViewModel;

namespace MobileMaple.View
{
    public partial class MaplePage : ContentPage
    {
        public MaplePage()
        {
            InitializeComponent();
            BindingContext = new MapleViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as MapleViewModel).SearchServersCommand.Execute(null);
        }
    }
}
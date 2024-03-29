﻿using System.ComponentModel;
using chatApp.Services;
using chatApp.ViewModel;

namespace chatApp;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel viewModel, MessageService service)
    {
        BindingContext = viewModel;
        InitializeComponent();

        MessageTemplateSelector.Service = service;
    }
    private void Entry_OnCompleted(object sender, EventArgs e)
    {
        MainViewModel viewModel = BindingContext as MainViewModel;
        viewModel.SendMessage();
    }

    private void ChatView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        MainViewModel viewModel = BindingContext as MainViewModel;
        if (viewModel.ChatHistory.Count > 0)
        {
            ChatView.ScrollTo(viewModel.ChatHistory.Count - 1);
        }
    }
}
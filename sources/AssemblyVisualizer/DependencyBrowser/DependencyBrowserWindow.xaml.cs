﻿// Copyright 2011 Denis Markelov
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AssemblyVisualizer.Model;
using System.Windows.Media.Animation;
using AssemblyVisualizer.Controls.ZoomControl;

namespace AssemblyVisualizer.DependencyBrowser
{
    /// <summary>
    /// Interaction logic for DependencyBrowserWindow.xaml
    /// </summary>
    partial class DependencyBrowserWindow : Window
    {
        public DependencyBrowserWindow(IEnumerable<AssemblyInfo> assemblies)
        {
            InitializeComponent();

            ViewModel = new DependencyBrowserWindowViewModel(assemblies);

            ViewModel.FillGraphRequest += FillGraphRequestHandler;
            ViewModel.OriginalSizeRequest += OriginalSizeRequestHandler;
            ViewModel.ShowInnerSearchRequest += ShowInnerSearchRequestHandler;
            ViewModel.HideInnerSearchRequest += HideInnerSearchRequestHandler;
            ViewModel.FocusSearchRequest += FocusSearchRequestHandler;
        }

        public DependencyBrowserWindowViewModel ViewModel
        {
            get
            {
                return DataContext as DependencyBrowserWindowViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void FillGraphRequestHandler()
        {
            zoomControl.ZoomToFill();
        }

        private void OriginalSizeRequestHandler()
        {
            var animation = new DoubleAnimation(1, TimeSpan.FromSeconds(1));
            zoomControl.BeginAnimation(ZoomControl.ZoomProperty, animation);
        }

        private void ShowInnerSearchRequestHandler()
        {
            var animation = new DoubleAnimation(1, TimeSpan.FromMilliseconds(200));
            brdSearch.Visibility = Visibility.Visible;
            brdSearch.BeginAnimation(OpacityProperty, animation);
        }

        private void HideInnerSearchRequestHandler()
        {
            var animation = new DoubleAnimation(0, TimeSpan.FromMilliseconds(200));
            animation.Completed += HideSearchAnimationCompletedHandler;
            brdSearch.BeginAnimation(OpacityProperty, animation);
        }

        private void HideSearchAnimationCompletedHandler(object sender, EventArgs e)
        {
            brdSearch.Visibility = Visibility.Collapsed;
        }

        private void FocusSearchRequestHandler()
        {
            txtSearch.Focus();
        }

        private void SearchPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                ViewModel.HideSearchCommand.Execute(null);
            }
            if (e.Key == Key.Enter)
            {
                e.Handled = true;                
                ViewModel.BrowseFoundAssemblies();
                ViewModel.HideSearchCommand.Execute(null);
            }
        }
    }
}
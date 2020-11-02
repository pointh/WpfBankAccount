﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SavingsAccount sa;
        public MainWindow()
        {
            InitializeComponent();
            sa = new SavingsAccount(0.0m);
            sa.Subscribe(UpdateBalanceLabel);
            sa.Subscribe(UpdateDebugInfo);
        }

        private void UpdateBalanceLabel()
        {
            NewBalance.Content = sa.balance.ToString();
        }

        private void UpdateDebugInfo()
        {
            Debug.WriteLine($"New balance: {sa.balance}");
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            sa.Deposit(int.Parse(Amount.Text));
        }
    }
}

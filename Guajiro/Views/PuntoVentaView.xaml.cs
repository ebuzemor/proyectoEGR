﻿using System;
using System.Collections.Generic;
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

namespace Guajiro.Views
{
    /// <summary>
    /// Lógica de interacción para PuntoVentaView.xaml
    /// </summary>
    public partial class PuntoVentaView : UserControl
    {
        public PuntoVentaView()
        {
            InitializeComponent();
            FechaPago.BlackoutDates.AddDatesInPast();
        }
    }
}

﻿// <copyright file="PluginManagement.xaml.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
// Licensed under the MIT license.
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace TunnelRelay
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using System.Windows.Shapes;
    using TunnelRelay.Core;

    /// <summary>
    /// Interaction logic for PluginManagement.xaml
    /// </summary>
    public partial class PluginManagement : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManagement"/> class.
        /// </summary>
        public PluginManagement()
        {
            this.InitializeComponent();
            this.lstPluginList.ItemsSource = TunnelRelayEngine.Plugins;
            this.lstPluginList.SelectedIndex = 0;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                Parallel.ForEach(TunnelRelayEngine.Plugins.Where(plugin => plugin.IsEnabled), (plugin) => plugin.InitializePlugin());
            }
            catch (Exception ex)
            {
                Logger.LogError(CallInfo.Site(), ex);
                MessageBox.Show("Plugin initialization failed!!", "Plugin management", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }

            base.OnClosed(e);
        }
    }
}

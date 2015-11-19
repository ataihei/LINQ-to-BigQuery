using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using LINQPad.Extensibility.DataContext;
using Microsoft.Win32;

namespace BigQuery.Linq
{
    public partial class ConnectionDialog : Window
    {
        readonly IConnectionInfo connectionInfo;
        readonly DriverProperty property;

        public ConnectionDialog(IConnectionInfo connectionInfo)
        {
            InitializeComponent();
            this.connectionInfo = connectionInfo;
            this.property = new DriverProperty(connectionInfo);

            JsonTextBox.Text = property.ContextJsonAuthenticationKey;
            P12PasswordTextBox.Text = property.ContextP12Password;
            UserTextBox.Text = property.ContextUser;
            ProjectIdBox.Text = property.ContextProjectId;
            UseDataSetCheckBox.IsChecked = property.ContextIsOnlyDataSet;
            DataSetBox.Text = property.ContextDataSet;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (AuthTypeP12RadioButton.IsChecked ?? false)
            {
                property.ContextP12Password = P12PasswordTextBox.Text;
                property.AuthenticationType = GoogleApiAuthenticationType.P12;
                if (!string.IsNullOrEmpty(P12FilePathTextBox.Text))
                {
                    using (var stream = File.Open(P12FilePathTextBox.Text, FileMode.Open))
                    {
                        var bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, (int)stream.Length);
                        property.ContextP12BinaryData = bytes;
                    }
                }
            }
            if (AuthTypeJsonRadioButton.IsChecked ?? false)
            {
                property.AuthenticationType = GoogleApiAuthenticationType.Json;
                property.ContextJsonAuthenticationKey = JsonTextBox.Text;
            }
            property.ContextUser = UserTextBox.Text;
            property.ContextProjectId = ProjectIdBox.Text;
            property.ContextIsOnlyDataSet = UseDataSetCheckBox.IsChecked ?? false;
            property.ContextDataSet = DataSetBox.Text;

            property.DisplayName = property.ContextProjectId + (property.ContextIsOnlyDataSet ? $"({property.ContextDataSet})" : "");

            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void P12FileSelectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "P12 File(.p12)|*.p12";
            if (dialog.ShowDialog() ?? false)
                P12FilePathTextBox.Text = dialog.FileName;
        }
    }
}
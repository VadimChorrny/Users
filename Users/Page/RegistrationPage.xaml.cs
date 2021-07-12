using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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

namespace Users.Page
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage
    {

        // provider factory
        private DbProviderFactory fact = null;
        // connection class to database
        private DbConnection conn = null;
        // data adapter for disconnected mode
        private DbDataAdapter da = null;
        // DataSet
        private DataSet set = null;

        private bool isAdmin;
        public RegistrationPage()
        {
            InitializeComponent();
            string cs = ConfigurationManager.ConnectionStrings["Users"].ConnectionString;
            // create factory from provider name
            fact = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["Users"].ProviderName);
            // create sql connection class
            conn = fact.CreateConnection();
            conn.ConnectionString = cs;
        }

        private void Registration(string login,string pass,bool isAdmin)
        {
            conn.Open();
            string sql = "insert into Users (Login,Password,isAdmin) values (@login,@password,@admin)";
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            DbParameter param = fact.CreateParameter();
            param.ParameterName = "@login";
            param.DbType = DbType.String;
            param.Value = login;
            cmd.Parameters.Add(param);
            
            DbParameter param1 = fact.CreateParameter();
            param1.ParameterName = "@password";
            param1.DbType = DbType.String;
            param1.Value = pass;
            cmd.Parameters.Add(param1);
            
            DbParameter param2 = fact.CreateParameter();
            param2.ParameterName = "@admin";
            param2.DbType = DbType.Boolean;
            param2.Value = isAdmin;
            cmd.Parameters.Add(param2);

            cmd.ExecuteScalar();
            conn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkboxIsAdmin.IsChecked)
            {
                isAdmin = true;
            }
            else
            {
                isAdmin = false;
            }
            Registration(txtLogin.Text, txtPassword.Text,isAdmin);
        }
    }
}

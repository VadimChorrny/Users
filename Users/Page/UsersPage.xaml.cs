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
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage 
    {
        // provider factory
        private DbProviderFactory fact = null;
        // connection class to database
        private DbConnection conn = null;
        // data adapter for disconnected mode
        private DbDataAdapter da = null;
        // DataSet
        private DataSet set = null;

        public UsersPage()
        {
            InitializeComponent();
            // read connection string from config file
            string cs = ConfigurationManager.ConnectionStrings["Users"].ConnectionString;
            // create factory from provider name
            fact = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["Users"].ProviderName);

            // create sql connection class
            conn = fact.CreateConnection();
            conn.ConnectionString = cs;
        }

        private void LoadAllUsers()
        {
            try
            {
                // query for select data
                string sql = "SELECT * FROM Users;";
                // create data adapter
                da = fact.CreateDataAdapter();
                da.SelectCommand = conn.CreateCommand();
                da.SelectCommand.CommandText = sql;
                // create command builder for auto generate insert, update and delete queries
                var builder = fact.CreateCommandBuilder();
                builder.DataAdapter = da;

                // create empty DataSet
                set = new DataSet();
                // execute select query on server and put data to DataSet
                da.Fill(set);

                // bind table to DataGrid
                dataGrid.ItemsSource = set.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

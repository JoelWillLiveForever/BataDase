using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using BataDase.MVVM.ViewModels;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace BataDase.MVVM.Views.ManageV
{
    public partial class AccountV : UserControl
    {
        public BindingList<UsersM> users { get; set; }
        int userID;
        private AppDBContext dbContext;
        public DialogV dialogV;
        public TextBlock Money;
        public TextBox money;

        public AccountV()
        {
            InitializeComponent();
            userID = Properties.Settings.Default.CurrentUserId;
            dbContext = AppDBContext.GetInstance();
            dbContext.UsersMs.Load();

            users = dbContext.UsersMs.Local.ToBindingList();

            for (int i = 0; i < users.Count; i++) 
            {
                // Ищем из списка юзеров, того кто на данный момент пользуется программой
                if (users[i]._user_id == userID)
                {
                    loginShow.Text = users[i]._login;
                    surnameShow.Text = users[i]._surname;
                    nameShow.Text = users[i]._name;
                    lastnameShow.Text = users[i]._lastname;
                    billShow.Text = users[i]._bill.ToString() + " " + App.Current.Resources["Text_Rubles"];
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CurrentUserId = -1;
            Properties.Settings.Default.IsAdmin = true;
            Properties.Settings.Default.Visibility = "Visible";

            MainVM.LoginVM_RelayCommand.Execute(null);
        }

		private void AddMoney_Click(object sender, RoutedEventArgs e)
		{
            dialogV = new DialogV();
            Grid dialogGrid = dialogV.Dialog_Grid;

            Thickness temp = new Thickness(5);

            Money = new TextBlock();
            Money.Text = App.Current.Resources["Text_Summ"] + ":";
            Grid.SetRow(Money, 0);
            Grid.SetColumn(Money, 0);

            money = new TextBox();
            money.Margin = temp;
            Grid.SetRow(money, 0);
            Grid.SetColumn(money, 1);

            Button button = dialogV.Button_Execute;
            button.Content = App.Current.Resources["Text_AddMoney"];

            button.Click += new RoutedEventHandler(ExecuteAddMoney);

            // Вешаем элементы в Grid
            dialogGrid.Children.Add(Money);

            dialogGrid.Children.Add(money);

            // Заполняем нижний Button текстом и вешаем локальный обработчик события нажатия

            dialogV.ShowDialog();

            // Очищаем Grid
            dialogGrid.Children.Remove(Money);

            dialogGrid.Children.Remove(money);

            money.Text = null;
        }
        public void ExecuteAddMoney(object sender, RoutedEventArgs e)
        {
            float result;
            // Проверки TextBox на null и пустую строку
            if (money.Text == null || money.Text == "")
            {
                MessageBox.Show("Укажите сумму!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!float.TryParse(money.Text, out result))
            {
                MessageBox.Show("Некорректная сумма!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            UsersM temp = new UsersM();
            temp._bill = float.Parse(money.Text);

			for (int i = 0; i < users.Count; i++)
			{
				// Ищем из списка юзеров, того кто на данный момент пользуется программой
				if (users[i]._user_id == userID)
				{
                    users[i]._bill += temp._bill;
                    MessageBox.Show(App.Current.Resources["Text_AddMoneyMessage"].ToString() + " " +
                                users[i]._bill.ToString() + " " +
                                App.Current.Resources["Text_Rubles"].ToString(), "Info",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                }
                dbContext.Entry(users[i]).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
            users = dbContext.UsersMs.Local.ToBindingList();

            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;
using Week10Assignment10._3.Data;

namespace Week10Assignment10._3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Records.context = new CarContext();
            Records.context.Database.EnsureCreated();
        }

    }

}

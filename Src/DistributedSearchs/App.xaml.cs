// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using DistributedSearchs.Model;

namespace DistributedSearchs
{
    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [System.ComponentModel.Composition.Import] private MainWindow _mainWindow;

        public App()
        {
            CreateCompositionCatalog();

            InitActiveRecord();
        }

        private void InitActiveRecord()
        {
            var source = ActiveRecordSectionHandler.Instance;
            ActiveRecordStarter.Initialize(source, typeof(FileRecord),typeof(FileReference));
            new SessionScope(FlushAction.Never);
            ActiveRecordStarter.CreateSchema();
        }

        private void CreateCompositionCatalog()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof (App).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            var container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                MessageBox.Show(compositionException.ToString());
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            _mainWindow.Show();
        }
    }
}
namespace TemplateMethodExample
{
    public abstract class AbstractReportGenerator
    {
        public void GenerateReport()
        {
            BeforeGeneration();
            OpenSource();
            LoadData();
            AnalyzeData();
            FormatReport();
            SaveReport();
            AfterGeneration();

            Console.WriteLine("Report generation completed.");
            Console.WriteLine();
        }

        protected virtual void BeforeGeneration()
        {
        }

        protected abstract void OpenSource();

        protected abstract void LoadData();

        protected abstract void AnalyzeData();

        protected virtual void FormatReport()
        {
            Console.WriteLine("Standard report formatting applied.");
        }

        protected abstract void SaveReport();

        protected virtual void AfterGeneration()
        {
        }
    }

    public class SalesReportGenerator : AbstractReportGenerator
    {
        protected override void OpenSource()
        {
            Console.WriteLine("Opened sales data source.");
        }

        protected override void LoadData()
        {
            Console.WriteLine("Loaded sales data.");
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("Analyzed sales performance indicators.");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Sales report saved to SalesReport.pdf.");
        }

        protected override void AfterGeneration()
        {
            Console.WriteLine("Notification sent: sales report is ready.");
        }
    }

    public class InventoryReportGenerator : AbstractReportGenerator
    {
        protected override void BeforeGeneration()
        {
            Console.WriteLine("Checked inventory data relevance.");
        }

        protected override void OpenSource()
        {
            Console.WriteLine("Opened inventory data source.");
        }

        protected override void LoadData()
        {
            Console.WriteLine("Loaded inventory data.");
        }

        protected override void AnalyzeData()
        {
            Console.WriteLine("Analyzed inventory stock levels.");
        }

        protected override void FormatReport()
        {
            Console.WriteLine("Advanced report formatting with tables applied.");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Inventory report saved to InventoryReport.pdf.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AbstractReportGenerator salesReport = new SalesReportGenerator();
            salesReport.GenerateReport();

            AbstractReportGenerator inventoryReport = new InventoryReportGenerator();
            inventoryReport.GenerateReport();
        }
    }
}
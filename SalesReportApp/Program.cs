using System;
using System.IO;
using System.Text;
using System.Globalization;

public class SalesReportGenerator 
{
    public static void GenerateSalesReport(string salesDirectory, string reportFilePath)
    {
        var reportBuilder = new StringBuilder();
        decimal totalSales = 0;
        
        reportBuilder.AppendLine("Sales Summary");
        reportBuilder.AppendLine("----------------------------");
        
        // Get all sales files in the directory
        string[] salesFiles = Directory.GetFiles(salesDirectory, "*.txt");
        
        // Dictionary to store filename and sales amount
        var salesDetails = new Dictionary<string, decimal>();
        
        // Process each sales file
        foreach (string filePath in salesFiles)
        {
            string fileName = Path.GetFileName(filePath);
            decimal fileSales = 0;
            
            try
            {
                // Read the file and parse sales data
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    // Assuming each line contains a sales amount
                    // You may need to adjust this based on your file format
                    if (decimal.TryParse(line.Trim(), out decimal saleAmount))
                    {
                        fileSales += saleAmount;
                    }
                }
                
                salesDetails[fileName] = fileSales;
                totalSales += fileSales;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {fileName}: {ex.Message}");
            }
        }
        
        // Add total sales to report
        reportBuilder.AppendLine($" Total Sales: {totalSales:C}");
        reportBuilder.AppendLine();
        reportBuilder.AppendLine(" Details:");
        
        // Add individual file details
        foreach (var kvp in salesDetails)
        {
            reportBuilder.AppendLine($"  {kvp.Key}: {kvp.Value:C}");
        }
        
        // Write report to file
        try
        {
            File.WriteAllText(reportFilePath, reportBuilder.ToString());
            Console.WriteLine($"Sales report generated successfully: {reportFilePath}");
            Console.WriteLine("\nReport Content:");
            Console.WriteLine(reportBuilder.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing report file: {ex.Message}");
        }
    }
    
    // Example usage method
    public static void Main(string[] args)
    {
        // Create sample sales directory and files for testing
        string salesDir = Path.Combine(Directory.GetCurrentDirectory(), "SalesData");
        Directory.CreateDirectory(salesDir);
        
        // Create sample sales files
        CreateSampleSalesFiles(salesDir);
        
        // Generate the sales report
        string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "SalesReport.txt");
        GenerateSalesReport(salesDir, reportPath);
    }
    
    private static void CreateSampleSalesFiles(string directory)
    {
        // Create sample sales files for demonstration
        File.WriteAllText(Path.Combine(directory, "january_sales.txt"), "1500.50\n2300.75\n1200.00");
        File.WriteAllText(Path.Combine(directory, "february_sales.txt"), "1800.25\n2100.00\n1650.75");
        File.WriteAllText(Path.Combine(directory, "march_sales.txt"), "2200.00\n1950.50\n2400.25");
    }
}
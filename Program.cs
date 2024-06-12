using System;
using System.IO;
using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace HtmlToPdfExample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await getPDFfromInFileHTML();
            Console.WriteLine("PDF created successfully!");
        }
        public static async Task getPDFfromInFileHTML()
        {
            
            string htmlFilePath = Path.GetFullPath("index.html");
            string outputPdfPath = "output.pdf";

            // Ensure the HTML file exists
            if (!File.Exists(htmlFilePath))
            {
                Console.WriteLine($"The file {htmlFilePath} does not exist.");
                return;
            }

            // Read the HTML content from the file
            string htmlContent = await File.ReadAllTextAsync(htmlFilePath);

            // Download the Chromium revision if it does not already exist
            await new BrowserFetcher().DownloadAsync();

            // Launch the browser
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            // Open a new page
            var page = await browser.NewPageAsync();

            // Set the HTML content
            await page.SetContentAsync(htmlContent);

            // Define the PDF options
            var pdfOptions = new PdfOptions
            {
                Format = PaperFormat.A4
            };

            // Save the PDF to a file
            await page.PdfAsync(outputPdfPath, pdfOptions);

            // Close the browser
            await browser.CloseAsync();

            Console.WriteLine($"PDF created successfully at: {outputPdfPath}");
        }
        async void getPDFfromInLineHTML()
        {
            // Download the Chromium revision if it does not already exist
            await new BrowserFetcher().DownloadAsync();

            // Launch the browser
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            // Open a new page
            var page = await browser.NewPageAsync();

            string htmlcontent = @"<tr>
                                <td>1</td>
                                <td>John Doe</td>
                                <td>A software engineer</td>
                            </tr>";

            string all = "";

            for (int i = 0; i < 200; i++)
            {
                all = all + htmlcontent;
            }


            // Define the HTML content
            string htmlContent = $@"
                <html>
                <head>
                    <title>Sample PDF</title>
                </head>
                <body>
                    <h1>Sample PDF Document</h1>
                    <table border='1'>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            {all}
                        </tbody>
                    </table>
                </body>
                </html>";

            // Set the HTML content
            await page.SetContentAsync(htmlContent);

            // Define the PDF options
            var pdfOptions = new PdfOptions
            {
                Format = PaperFormat.A4
            };

            // Save the PDF to a file
            await page.PdfAsync("HtmlToPdfExample.pdf", pdfOptions);

            // Close the browser
            await browser.CloseAsync();




        }
    }
}

using PDFGenerator.Models;
using System.Text;

namespace PDF_Generator.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(PDFData data)
        {
            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <h1>PDF</h1>
                                <div>
                                    <p><span>Email Address:</span> {data.FirstName} </p>
                                    <p><span>Doc Ref ID:</span> {data.Surname}</p>
                                    <p><span>Body Text:</span> {data.Body}</p>
                                </div>
                            </body>
                         </html>");
            return sb.ToString();
        }
    }
}
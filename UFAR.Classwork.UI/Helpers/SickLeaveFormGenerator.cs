using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using System;
using UFAR.Classwork.Data.Entities;


namespace UFAR.Classwork.UI.Helpers
{
    public class SickLeaveFormGenerator
    {
        public void GenerateSickLeavePdf(PatientAccount patient)
        {
            // Define file path for the PDF
            string filePath = $"wwwroot/Sick_Leave_Request_{patient.ID}.pdf";

            // Create a new PDF document
            using (PdfWriter writer = new PdfWriter(filePath))
            using (PdfDocument pdf = new PdfDocument(writer))
            using (Document document = new Document(pdf))
            {
                // Title and headers
                document.Add(new Paragraph("Republic of Armenia, Ministry of Health")
                    .SetBold().SetTextAlignment(TextAlignment.CENTER));
                document.Add(new Paragraph("Sick Leave Request Form")
                    .SetBold().SetTextAlignment(TextAlignment.CENTER));

                // Employee Information
                document.Add(new Paragraph("\nEmployee Information:"));
                document.Add(new Paragraph($"Full Name: {patient.Name} {patient.Surname}"));
                document.Add(new Paragraph($"Employee ID: {patient.ID}"));
                document.Add(new Paragraph($"Department: {patient.Departement}"));
                document.Add(new Paragraph($"Position: {patient.Position}"));

                // Sick Leave Details
                document.Add(new Paragraph("\nSick Leave Details:"));
                document.Add(new Paragraph($"Reason for Leave (Brief description): {patient.IllnessDescription}"));
                document.Add(new Paragraph($"Start Date of Sick Leave: {DateTime.Now:dd/MM/yyyy}"));
                document.Add(new Paragraph($"Expected Return Date: {DateTime.Now.AddDays(patient.DaysTime):dd/MM/yyyy}"));

              

             
            }
        }
    }
}

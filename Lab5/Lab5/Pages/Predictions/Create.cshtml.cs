using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab5.Data;
using Lab5.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Hosting;

namespace Lab5.Pages.Predictions
{
    public class CreateModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly Lab5.Data.PredictionDataContext _context;

        public CreateModel(Lab5.Data.PredictionDataContext context, BlobServiceClient blobServiceClient, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Prediction Prediction { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            Prediction.FileName = imageFile?.FileName;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Prediction.FileName != null && Prediction.FileName.Length > 0)
            {
                // Get the container name based on the selected question (Earth or Computer)
                var containerName = Prediction.Question == Question.Earth ? earthContainerName : computerContainerName;

                // Create the Blob container if it doesn't exist
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Prediction.FileName)}";

                //Save the file to Azure Blob Storage
                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                               using (var stream = imageFile.OpenReadStream())
                                {
                                    await blobClient.UploadAsync(stream);
                                }
                // Set the URL property to the Blob Storage URL for display in the Index page
                Prediction.Url = blobClient.Uri.ToString();
            }

            _context.Predictions.Add(Prediction);
            await _context.SaveChangesAsync();

            // Redirect to the Index page with the image filename and question as query parameters
            return RedirectToPage("./Index");
        }
    }
}

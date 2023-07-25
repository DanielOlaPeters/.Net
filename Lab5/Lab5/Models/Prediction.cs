using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Models
{
    public enum Question
    {
        Earth,
        Computer
    }
    public class Prediction
    {
        public int PredictionId { get; set; }

        [StringLength(50)]
        public string FileName { get; set; }

        [Url]
        public string Url { get; set; }

        [Required]
        public Question Question { get; set; }

    }
}

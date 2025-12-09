// David Kezi Setondo 15634
using System.ComponentModel.DataAnnotations;

namespace Zestaw1.Models
{
    public enum UnitType
    {
        cm,
        mm,
        m
    }

    public enum WoodColor
    {
        [Display(Name = "Dąb")] Dab = 5,
        [Display(Name = "Orzech")] Orzech = 4,
        [Display(Name = "Topola")] Topola = 3,
        [Display(Name = "Buk")] Buk = 2,
        [Display(Name = "Sosna")] Sosna = 1
    }

    public class Panel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Długość jest wymagana")]
        [Display(Name = "Długość")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Długość musi być większa od 0")]
        public double Length { get; set; }

        [Required(ErrorMessage = "Szerokość jest wymagana")]
        [Display(Name = "Szerokość")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Szerokość musi być większa od 0")]
        public double Width { get; set; }

        [Required(ErrorMessage = "Jednostka jest wymagana")]
        [Display(Name = "Jednostka długości")]
        public UnitType LengthUnit { get; set; }

        [Required(ErrorMessage = "Jednostka jest wymagana")]
        [Display(Name = "Jednostka szerokości")]
        public UnitType WidthUnit { get; set; }

        [Required(ErrorMessage = "Kolor jest wymagany")]
        [Display(Name = "Kolor drewna")]
        public WoodColor Color { get; set; }

        [Display(Name = "Czy brzegować?")]
        public bool HasBorder { get; set; }

        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public void CalculatePrice()
        {
            double lengthInMeters = ConvertToMeters(Length, LengthUnit);
            double widthInMeters = ConvertToMeters(Width, WidthUnit);

            if (lengthInMeters > 1.2 || widthInMeters > 1.2)
            {
                throw new InvalidOperationException("Żaden wymiar nie może przekraczać 1200 mm");
            }

            double area = lengthInMeters * widthInMeters;
            decimal colorPrice = (int)Color;
            decimal basePrice = (decimal)area * colorPrice;
            
            if (HasBorder)
            {
                double perimeter = 2 * (lengthInMeters + widthInMeters);
                decimal borderPrice = (decimal)perimeter * 0.2m;
                basePrice += borderPrice;
            }

            Price = Math.Round(basePrice, 2);
        }

        private double ConvertToMeters(double value, UnitType unit)
        {
            return unit switch
            {
                UnitType.mm => value / 1000,
                UnitType.cm => value / 100,
                UnitType.m => value,
                _ => value
            };
        }

        public string GetDimensions()
        {
            return $"{Length} {LengthUnit} × {Width} {WidthUnit}";
        }
    }
}
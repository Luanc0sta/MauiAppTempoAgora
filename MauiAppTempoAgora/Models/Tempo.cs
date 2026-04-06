namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        public double? lon { get; set; } // Longitude
        public double? lat { get; set; } // Latitude
        public double? temp_min { get; set; } // Temperatura mínima
        public double? temp_max { get; set; } // Temperatura máxima
        public int? visibility { get; set; } // Visibilidade
        public double? speed { get; set; } // Velocidade do vento
        public string? main { get; set; } // Tipo principal
        public string? description { get; set; } // Descrição
        public string? sunrise { get; set; } // Nascer do sol 
        public string? sunset { get; set; } // Pôr do sol 
    }
}
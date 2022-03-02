namespace ApiCore
{
    public class Recibos
    {
        public int? Id { get; set; }
        public string? Proveedor { get; set; }
        public string? Moneda { get; set; }

        public decimal? Monto { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Comentario { get; set; }

        public int UserId { get; set; }
          
    }
}

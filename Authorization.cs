using System.ComponentModel.DataAnnotations;

namespace Read_sheet_data_api
{
    public class Authorization
    {
        [Key]
        public string UserId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecrert { get; set; }
        public string[] Scopes { get; set; }
    }
}

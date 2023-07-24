using System.ComponentModel.DataAnnotations.Schema;

namespace DataProtection.Web.Models
{
    public partial class Product
    {
        /// <summary>
        /// Id şifrelenmiş halini tutmalıyız
        /// </summary>
        [NotMapped]
        public string EncryptedId { get; set; }
    }
}

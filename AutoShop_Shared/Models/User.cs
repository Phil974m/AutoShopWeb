using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AutoShop_Shared.Models
{
    public class User
    {
        
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("partitionkey")]
        public string PartitionKey { get; set; }


        [JsonProperty("email")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="entrer une adresse mail valide")]
        [Display(Name ="Courriel*", Prompt = "Veuillez saisir votre adresse mail")]
        [EmailAddress(ErrorMessage =" Ce n'est pas une adresse mail valable")]

        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Nom")]
       // [DisplayFormat(DataFormatString =)]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
        
        public string Photo { get; set; }

        [Display(Name = "Niveau")]
        [Range(1,100, ErrorMessage = "le niveau doit être compris entre 1 et 100")]
        public byte Level { get; set; }
        public uint Experience { get; set; }

        //pour qu'il prend le meme nom comme json
        [JsonProperty("badge")]
        public Badge ActiveBadge{ get; set; }
    }
}

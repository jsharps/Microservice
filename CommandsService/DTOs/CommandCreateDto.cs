using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public class CommandCreateDto
    {
         [Required]
        public int HowTo { get; set; }

        [Required]
        public int CommandLine { get; set; }
    }
}